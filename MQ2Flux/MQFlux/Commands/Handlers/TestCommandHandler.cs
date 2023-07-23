using MediatR;
using Microsoft.Extensions.Logging;
using MQFlux.Extensions;
using MQFlux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class TestCommandHandler : IRequestHandler<TestCommand, Unit>
    {
        private readonly ILogger<TestCommandHandler> logger;
        private readonly IMQLogger mqLogger;

        public TestCommandHandler(ILogger<TestCommandHandler> logger, IMQLogger mqLogger)
        {
            this.logger = logger;
            this.mqLogger = mqLogger;
        }

        public Task<Unit> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var target = request.Context.TLO.Target;

                if (target == null)
                {
                    request.Context.TLO.Me.DoTarget();
                }
                
                var meBuffs = request.Context.TLO.Me.Buffs;
                var meBuffDurations = meBuffs.Select(i => i.Duration.GetValueOrDefault(TimeSpan.Zero));
                var spawnBuffs = request.Context.TLO.Me.Spawn.Buffs;
                var spawnBuffDurations = spawnBuffs.Select(i => i.Duration.GetValueOrDefault(TimeSpan.Zero));

                foreach ( var buff in meBuffs)
                {
                    mqLogger.Log($"{buff.Name} {buff.Duration}");
                }

                foreach (var buff in spawnBuffs)
                {
                    mqLogger.Log($"{buff.Name} {buff.Duration}");
                }
            }
            catch (Exception ex)
            {
                mqLogger.Log(ex.ToString());
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
