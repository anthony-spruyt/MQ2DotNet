using MediatR;
using Microsoft.Extensions.Logging;
using MQ2Flux.Commands;
using MQ2Flux.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class TestRequestHandler : IRequestHandler<TestCommand>
    {
        private readonly ILogger<TestRequestHandler> logger;
        private readonly IMQ2Logger mQ2logger;

        public TestRequestHandler(ILogger<TestRequestHandler> logger, IMQ2Logger mQ2logger)
        {
            this.logger = logger;
            this.mQ2logger = mQ2logger;
        }

        public async Task Handle(TestCommand request, CancellationToken cancellationToken)
        {
            mQ2logger.Log($"{nameof(TestRequestHandler)}");

            try
            {
                

            }
            catch (Exception ex)
            {
                mQ2logger.Log(ex.ToString());
            }

            await Task.CompletedTask;
        }
    }
}
