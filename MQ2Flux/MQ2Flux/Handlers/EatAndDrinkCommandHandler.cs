using MediatR;
using MQ2DotNet.MQ2API;
using MQ2DotNet.MQ2API.DataTypes;
using MQ2Flux.Commands;
using MQ2Flux.Extensions;
using MQ2Flux.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class EatAndDrinkCommandHandler : IRequestHandler<EatAndDrinkCommand, bool>
    {
        private readonly IMQ2ChatHistory chatHistory;
        private readonly IItemService itemService;

        public EatAndDrinkCommandHandler(IMQ2ChatHistory chatHistory, IItemService itemService)
        {
            this.chatHistory = chatHistory;
            this.itemService = itemService;
        }

        public async Task<bool> Handle(EatAndDrinkCommand request, CancellationToken cancellationToken)
        {
            var me = request.Context.TLO.Me;
            var amIHungry = me.AmIHungry();
            var amIThirsty = me.AmIThirsty();

            if (!amIHungry && !amIThirsty)
            {
                return false;
            }

            var dontConsume = request.Character.DontConsume;
            var mq2 = request.Context.MQ2;
            var allMyInv = me.Inventory.Flatten();

            if (await HandleHungerAsync(dontConsume, mq2, me, allMyInv, cancellationToken))
            {
                return true;
            }
            return await HandleThirstAsync(dontConsume, mq2, me, allMyInv, cancellationToken);
        }

        private async Task<bool> HandleThirstAsync(IEnumerable<string> dontConsume, MQ2 mq2, CharacterType me, IEnumerable<ItemType> allMyInv, CancellationToken cancellationToken)
        {
            if (me.AmIThirsty())
            {
                var drink = allMyInv
                    .Where(i => i.IsDrinkable() && !(dontConsume.Contains(i.Name)))
                    .OrderBy(i => i.GetNutrientScore(me))
                    .FirstOrDefault();

                if (drink != null)
                {
                    return await itemService.UseItemAsync(drink, "Drinking", cancellationToken);
                }
                else if (me.Grouped)
                {
                    var message = "I am thirsty and out of drink. Can anyone please help me out?";

                    if (chatHistory.NoSpam(TimeSpan.FromSeconds(60), message))
                    {
                        mq2.DoCommand($"/g {message}");
                    }
                }
            }

            return false;
        }

        private async Task<bool> HandleHungerAsync(IEnumerable<string> dontConsume, MQ2 mq2, CharacterType me, IEnumerable<ItemType> allMyInv, CancellationToken cancellationToken)
        {
            if (me.AmIHungry())
            {
                var food = allMyInv
                    .Where(i => i.IsEdible() && !(dontConsume.Contains(i.Name)))
                    .OrderBy(i => i.GetNutrientScore(me))
                    .FirstOrDefault();

                if (food != null)
                {
                    return await itemService.UseItemAsync(food, "Eating", cancellationToken);
                }
                else if (me.Grouped)
                {
                    var message = "I am hungry and out of food. Can anyone please help me out?";

                    if (chatHistory.NoSpam(TimeSpan.FromSeconds(60), message))
                    {
                        mq2.DoCommand($"/g {message}");
                    }
                }
            }

            return false;
        }
    }
}
