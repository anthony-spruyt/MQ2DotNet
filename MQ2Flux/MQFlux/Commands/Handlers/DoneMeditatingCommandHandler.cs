using MediatR;
using MQFlux.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class DoneMeditatingCommandHandler : IRequestHandler<DoneMeditatingCommand, bool>
    {
        private readonly IContext context;
        private readonly IMQLogger mqLogger;

        public DoneMeditatingCommandHandler(IContext context, IMQLogger mqLogger)
        {
            this.context = context;
            this.mqLogger = mqLogger;
        }

        public Task<bool> Handle(DoneMeditatingCommand request, CancellationToken cancellationToken)
        {
            //if (DateTime.UtcNow.Second % 2 != 0)
            //{
            //    return Task.FromResult(false);
            //}

            var me = context.TLO.Me;

            if (!me.Standing && me.PctMana > 99 && me.PctHPs > 99)
            {
                me.Stand();

                mqLogger.Log("Standing up because I am finished meditating", TimeSpan.Zero);

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
