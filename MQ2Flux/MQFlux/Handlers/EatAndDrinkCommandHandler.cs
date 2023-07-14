using MediatR;
using MQ2DotNet.MQ2API;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Commands;
using MQFlux.Extensions;
using MQFlux.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class EatAndDrinkCommandHandler : IRequestHandler<EatAndDrinkCommand, bool>
    {
        private readonly IChatHistory chatHistory;
        private readonly IItemService itemService;

        public EatAndDrinkCommandHandler(IChatHistory chatHistory, IItemService itemService)
        {
            this.chatHistory = chatHistory;
            this.itemService = itemService;
        }

        public async Task<bool> Handle(EatAndDrinkCommand request, CancellationToken cancellationToken)
        {
            if (!request.Character.AutoEatAndDrink.GetValueOrDefault(false))
            {
                return false;
            }

            var me = request.Context.TLO.Me;
            var amIHungry = me.AmIHungry();
            var amIThirsty = me.AmIThirsty();

            if (!amIHungry && !amIThirsty)
            {
                return false;
            }

            var dontConsume = request.Character.DontConsume;
            var mq = request.Context.MQ;
            var allMyInv = me.Bags.Flatten();

            if (await HandleHungerAsync(dontConsume, mq, me, allMyInv, cancellationToken))
            {
                return true;
            }
            return await HandleThirstAsync(dontConsume, mq, me, allMyInv, cancellationToken);
        }

        private async Task<bool> HandleThirstAsync(IEnumerable<string> dontConsume, MQ2 mq, CharacterType me, IEnumerable<ItemType> allMyInv, CancellationToken cancellationToken)
        {
            if (me.AmIThirsty())
            {
                // First get summoned ordered from least to most nutritious.
                var drink = allMyInv
                    .Where(i => i.NoRent && i.IsDrinkable() && !(dontConsume.Contains(i.Name)))
                    .OrderBy(i => i.GetNutrientScore(me))
                    .ThenBy(i => i.Stack)
                    .FirstOrDefault();

                // If no summoned get non summoned ordered from least to most nutritious.
                if (drink == null)
                {
                    drink = allMyInv
                        .Where(i => i.IsDrinkable() && !(dontConsume.Contains(i.Name)))
                        .OrderBy(i => i.GetNutrientScore(me))
                        .ThenBy(i => i.Stack)
                        .FirstOrDefault();
                }

                if (drink != null)
                {
                    return await itemService.UseItemAsync(drink, "Drinking", cancellationToken);
                }
                else if (me.Grouped)
                {
                    var message = "I am thirsty and out of drink. Can anyone please help me out?";

                    if (chatHistory.NoSpam(TimeSpan.FromSeconds(60), message))
                    {
                        mq.DoCommand($"/g {message}");
                    }
                }
            }

            return false;
        }

        private async Task<bool> HandleHungerAsync(IEnumerable<string> dontConsume, MQ2 mq, CharacterType me, IEnumerable<ItemType> allMyInv, CancellationToken cancellationToken)
        {
            if (me.AmIHungry())
            {
                // First get summoned ordered from least to most nutritious.
                var food = allMyInv
                    .Where(i => i.NoRent && i.IsEdible() && !(dontConsume.Contains(i.Name)))
                    .OrderBy(i => i.GetNutrientScore(me))
                    .ThenBy(i => i.Stack)
                    .FirstOrDefault();

                // If no summoned get non summoned ordered from least to most nutritious.
                if (food == null)
                {
                    food = allMyInv
                        .Where(i => i.IsEdible() && !(dontConsume.Contains(i.Name)))
                        .OrderBy(i => i.GetNutrientScore(me))
                        .ThenBy(i => i.Stack)
                        .FirstOrDefault();
                }

                if (food != null)
                {
                    return await itemService.UseItemAsync(food, "Eating", cancellationToken);
                }
                else if (me.Grouped)
                {
                    var message = "I am hungry and out of food. Can anyone please help me out?";

                    if (chatHistory.NoSpam(TimeSpan.FromSeconds(60), message))
                    {
                        mq.DoCommand($"/g {message}");
                    }
                }
            }

            return false;
        }
    }
}
