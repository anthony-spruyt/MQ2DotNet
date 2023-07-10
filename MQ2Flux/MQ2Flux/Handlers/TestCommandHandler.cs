using MediatR;
using Microsoft.Extensions.Logging;
using MQ2Flux.Commands;
using MQ2Flux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class TestCommandHandler : IRequestHandler<TestCommand>
    {
        private readonly ILogger<TestCommandHandler> logger;
        private readonly IMQ2Logger mQ2logger;

        public TestCommandHandler(ILogger<TestCommandHandler> logger, IMQ2Logger mQ2logger)
        {
            this.logger = logger;
            this.mQ2logger = mQ2logger;
        }

        public async Task Handle(TestCommand request, CancellationToken cancellationToken)
        {
            mQ2logger.Log($"{nameof(TestCommandHandler)}");

            try
            {
                var x = request.Context.TLO.Me.Inventory.ToArray();
                var y = x.FirstOrDefault(i => i.Name?.Contains("Tea") ?? false);
                var z = y.Type;
            }
            catch (Exception ex)
            {
                mQ2logger.Log(ex.ToString());
            }

            await Task.CompletedTask;
        }
    }
}
