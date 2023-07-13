using MediatR;
using Microsoft.Extensions.Logging;
using MQFlux.Commands;
using MQFlux.Services;
using System;
using System.Linq;
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
                var x = request.Context.TLO.Me.Inventory.ToArray();
                var y = x.FirstOrDefault(i => i.Name?.Contains("Tea") ?? false);
                var z = y.Type;
            }
            catch (Exception ex)
            {
                mqLogger.Log(ex.ToString());
            }

            await Task.CompletedTask;
        }
    }
}
