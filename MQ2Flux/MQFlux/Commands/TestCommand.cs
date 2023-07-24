using Microsoft.Extensions.Logging;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;
using System;
using System.Collections.Generic;
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
        private readonly ITargetService targetService;

        public TestCommandHandler(ILogger<TestCommandHandler> logger, IMQLogger mqLogger, ITargetService targetService)
        {
            this.logger = logger;
            this.mqLogger = mqLogger;
            this.targetService = targetService;
        }

        public override async Task<CommandResponse<bool>> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var spawns = request.Context.TLO.GroundSpawns;
                

                var count = spawns.Count();
            }
            catch (Exception ex)
            {
                mqLogger.Log(ex.ToString());
            }

            return CommandResponse.FromResult(true);
        }
    }
}
