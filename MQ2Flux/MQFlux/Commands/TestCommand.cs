using Microsoft.Extensions.Logging;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class TestCommand :
        PCCommand,
        ICharacterConfigRequest
    {
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IContext Context { get; set; }
    }

    public class TestCommandHandler : PCCommandHandler<TestCommand>
    {
        private readonly ILogger<TestCommandHandler> logger;
        private readonly IMQLogger mqLogger;

        public TestCommandHandler(ILogger<TestCommandHandler> logger, IMQLogger mqLogger)
        {
            this.logger = logger;
            this.mqLogger = mqLogger;
        }

        public override Task<CommandResponse<bool>> Handle(TestCommand request, CancellationToken cancellationToken)
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

                foreach (var buff in meBuffs)
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

            return CommandResponse.FromResultTask(true);
        }
    }
}
