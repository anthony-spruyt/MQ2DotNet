using MediatR;
using Microsoft.Extensions.Logging;
using MQFlux.Commands;
using MQFlux.Extensions;
using MQFlux.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class TestCommandHandler : IRequestHandler<TestCommand>
    {
        private readonly ILogger<TestCommandHandler> logger;
        private readonly IMQLogger mqLogger;

        public TestCommandHandler(ILogger<TestCommandHandler> logger, IMQLogger mqLogger)
        {
            this.logger = logger;
            this.mqLogger = mqLogger;
        }

        public async Task Handle(TestCommand request, CancellationToken cancellationToken)
        {
            mqLogger.Log($"{nameof(TestCommandHandler)}");

            try
            {
                var spawnId = request.Context.TLO.Target.ID();

                mqLogger.Log("aa");
            }
            catch (Exception ex)
            {
                mqLogger.Log(ex.ToString());
            }

            await Task.CompletedTask;
        }
    }
}
