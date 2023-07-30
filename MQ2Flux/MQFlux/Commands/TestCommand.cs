using Microsoft.Extensions.Logging;
using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Extensions;
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
    }

    public class TestCommandHandler : PCCommandHandler<TestCommand>
    {
        private readonly IMQLogger mqLogger;
        private readonly IContext context;

        public TestCommandHandler(IMQLogger mqLogger, IContext context)
        {
            this.mqLogger = mqLogger;
            this.context = context;
        }

        public override async Task<CommandResponse<bool>> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var x = context;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                mqLogger.Log(ex.ToString());
            }

            return CommandResponse.FromResult(true);
        }
    }
}
