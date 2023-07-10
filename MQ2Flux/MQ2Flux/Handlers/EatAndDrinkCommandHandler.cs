using MediatR;
using MQ2Flux.Commands;
using MQ2Flux.Extensions;
using MQ2Flux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    internal class EatAndDrinkCommandHandler : IRequestHandler<EatAndDrinkCommand>
    {
        private readonly IMQ2Logger mq2Logger;
        private readonly IMQ2ChatHistory chatHistory;

        public EatAndDrinkCommandHandler(IMQ2Logger mq2Logger, IMQ2ChatHistory chatHistory)
        {
            this.mq2Logger = mq2Logger;
            this.chatHistory = chatHistory;
        }

        public Task Handle(EatAndDrinkCommand request, CancellationToken cancellationToken)
        {
            var mq2 = request.Context.MQ2;
            var me = request.Context.TLO.Me;
            var amIHungry = me.AmIHungry();
            var amIThirsty = me.AmIThirsty();

            if (!amIHungry && !amIThirsty)
            {
                return Task.CompletedTask;
            }

            var allMyInv = me.Inventory.Flatten();

            if (me.AmIHungry())
            {
                var food = allMyInv
                    .Where(i => i.IsEdible() && !(request.Character.DontConsume.Contains(i.Name)))
                    .OrderBy(i => i.GetNutrientScore())
                    .FirstOrDefault();

                if (food != null)
                {
                    mq2Logger.Log($"Eating {food.Name}");
                    mq2.DoCommand($"/useitem {food.Name}");
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

            if (me.AmIThirsty())
            {
                var drink = allMyInv
                    .Where(i => i.IsDrinkable() && !(request.Character.DontConsume.Contains(i.Name)))
                    .OrderBy(i => i.GetNutrientScore())
                    .FirstOrDefault();

                if (drink != null)
                {
                    mq2Logger.Log($"Drinking {drink.Name}");
                    mq2.DoCommand($"/useitem {drink.Name}");
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

            return Task.CompletedTask;
        }
    }
}
