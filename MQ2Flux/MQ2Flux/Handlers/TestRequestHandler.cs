using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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
                logger.LogDebugJson(request.Context.TLO.Cursor);
                //logger.LogDebugJson(request.Context.TLO.Alias["/chaseon"]); // need test data, that didnt get anything, guess no alias
                //logger.LogDebugJson(request.Context.TLO.AlertByNumber); // need test data
                //logger.LogDebugJson(request.Context.TLO.Alerts); // need test data
                //logger.LogDebugJson(request.Context.TLO.AdvLoot); // need to test while there is actually something to loot
                //logger.LogDebugJson(request.Context.Spawns.All); // bad mapping
                //logger.LogDebugJson(request.Context.Spawns.AllGround); // crashes

            }
            catch (Exception ex)
            {
                mQ2logger.Log(ex.ToString());
            }

            await Task.CompletedTask;
        }
    }
}
