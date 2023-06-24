using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux
{
    public class TestRequestHandler : IRequestHandler<TestRequest>
    {
        private readonly ILogger<TestRequestHandler> logger;
        private readonly IMq2Logger mQ2logger;

        public TestRequestHandler(ILogger<TestRequestHandler> logger, IMq2Logger mQ2logger)
        {
            this.logger = logger;
            this.mQ2logger = mQ2logger;
        }

        public async Task Handle(TestRequest request, CancellationToken cancellationToken)
        {
            mQ2logger.Log($"{nameof(TestRequestHandler)}");

            try
            {
                //request.Context.MQ2.DoCommand("/useitem \"Fresh Cookie Dispenser\"");
                //request.Context.MQ2.DoCommand("/autoinv");
                logger.LogDebugJson(request.Context.MQ2);
                logger.LogDebugJson(request.Context.Events);
                logger.LogDebugJson(request.Context.Commands);
                logger.LogDebugJson(request.Context.Chat);
                logger.LogDebugJson(request.Context.Spawns);
                logger.LogDebugJson(request.Context.TLO);
                logger.LogDebugJson(request.Context.TLO.Me);

            }
            catch (Exception ex)
            {
                mQ2logger.Log(ex.ToString());
            }

            await Task.CompletedTask;
        }
    }
}
