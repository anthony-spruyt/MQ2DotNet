﻿using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class AutoAttackCommandHandler : CombatCommandHandler<AutoAttackCommand>
    {
        public override Task<CommandResponse<bool>> Handle(AutoAttackCommand request, CancellationToken cancellationToken)
        {
            //TODO
            //if ((request.Context.TLO.Target?.Aggressive).GetValueOrDefault(false) &&
            //    !request.Context.TLO.Me.AutoMeleeAttack)
            //{
            //    request.Context.MQ.DoCommand("/attack on");
            //}

            return CommandResponse.FromResultTask(false);
        }
    }
}
