﻿using MediatR;
using MQFlux.Commands;
using MQFlux.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
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
            if (DateTime.UtcNow.Second % 2 != 0)
            {
                return Task.FromResult(false);
            }

            var me = context.TLO.Me;

            if (!me.Spawn.Standing && me.PctMana == 100 && me.PctHPs == 100)
            {
                me.Stand();

                mqLogger.Log("Standing up because I am finished meditating", TimeSpan.Zero);

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
