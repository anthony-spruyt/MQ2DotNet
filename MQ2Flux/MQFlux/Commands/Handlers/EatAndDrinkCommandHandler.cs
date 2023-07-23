using MQ2DotNet.MQ2API;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Core;
using MQFlux.Extensions;
using MQFlux.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class EatAndDrinkCommandHandler : PCCommandHandler<EatAndDrinkCommand>
    {
        private readonly IChatHistory chatHistory;
        private readonly IItemService itemService;

        public EatAndDrinkCommandHandler(IChatHistory chatHistory, IItemService itemService)
        {
            this.chatHistory = chatHistory;
            this.itemService = itemService;
        }

        public override async Task<CommandResponse<bool>> Handle(EatAndDrinkCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoEatAndDrink.GetValueOrDefault(false))
            {
                return CommandResponse.FromResult(false);
            }

            var me = request.Context.TLO.Me;
            var amIHungry = me.AmIHungry();
            var amIThirsty = me.AmIThirsty();

            if (!amIHungry && !amIThirsty)
            {
                return CommandResponse.FromResult(false);
            }

            var dontConsume = request.Character.DontConsume;
            var mq = request.Context.MQ;
            var allMyInv = me.Bags.Flatten();

            if (amIHungry && await HandleHungerAsync(dontConsume, mq, me, allMyInv, cancellationToken))
            {
                return CommandResponse.FromResult(true);
            }

            if (amIThirsty && await HandleThirstAsync(dontConsume, mq, me, allMyInv, cancellationToken))
            {
                return CommandResponse.FromResult(true);
            }

            return CommandResponse.FromResult(false);
        }

        private ItemType GetLeastNutritiousConsumable(IEnumerable<ItemType> items, CharacterType me)
        {
            return items.OrderBy(i => i.GetNutrientScore(me))
                .ThenBy(i => i.Stack)
                .FirstOrDefault();
        }

        private async Task<bool> HandleThirstAsync(IEnumerable<string> dontConsume, MQ2 mq, CharacterType me, IEnumerable<ItemType> allMyInv, CancellationToken cancellationToken)
        {
            ItemType drink = GetDrink(dontConsume, me, allMyInv);

            if (drink != null)
            {
                return await itemService.UseItem(drink, "Drinking", cancellationToken);
            }
            else if (me.Grouped)
            {
                NotifyGroup(mq, "thirsty", "drink");
            }

            return false;
        }

        private ItemType GetDrink(IEnumerable<string> dontConsume, CharacterType me, IEnumerable<ItemType> allMyInv)
        {
            // First get summoned least nutritious.
            var drink = GetLeastNutritiousConsumable(allMyInv.Where(i => i.NoRent && i.IsDrinkable() && !dontConsume.Contains(i.Name)), me) ??
                // If no summoned get non summoned least nutritious.
                GetLeastNutritiousConsumable(allMyInv.Where(i => i.IsDrinkable() && !dontConsume.Contains(i.Name)), me);
            return drink;
        }

        private async Task<bool> HandleHungerAsync(IEnumerable<string> dontConsume, MQ2 mq, CharacterType me, IEnumerable<ItemType> allMyInv, CancellationToken cancellationToken)
        {
            ItemType food = GetFood(dontConsume, me, allMyInv);

            if (food != null)
            {
                return await itemService.UseItem(food, "Eating", cancellationToken);
            }
            else if (me.Grouped)
            {
                NotifyGroup(mq, "hungry", "food");
            }

            return false;
        }

        private ItemType GetFood(IEnumerable<string> dontConsume, CharacterType me, IEnumerable<ItemType> allMyInv)
        {
            // First get summoned least nutritious.
            var food = GetLeastNutritiousConsumable(allMyInv.Where(i => i.NoRent && i.IsEdible() && !dontConsume.Contains(i.Name)), me) ??
                // If no summoned get non summoned least nutritious.
                GetLeastNutritiousConsumable(allMyInv.Where(i => i.IsEdible() && !dontConsume.Contains(i.Name)), me);
            return food;
        }

        private void NotifyGroup(MQ2 mq, string adjective, string noun)
        {
            var message = $"I am {adjective} and out of {noun}. Can anyone please help me out?";

            if (chatHistory.NoSpam(TimeSpan.FromSeconds(60), message))
            {
                mq.DoCommand($"/g {message}");
            }
        }
    }
}
