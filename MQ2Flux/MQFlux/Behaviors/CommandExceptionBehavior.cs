using MediatR;
using Microsoft.Extensions.Logging;
using MQFlux.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public class CommandExceptionBehavior<TRequest, TResult> : CommandBehavior<TRequest, TResult>
        where TRequest : Command<TResult>
    {
        private readonly ILogger<CommandExceptionBehavior<TRequest, TResult>> logger;

        public CommandExceptionBehavior(ILogger<CommandExceptionBehavior<TRequest, TResult>> logger)
        {
            this.logger = logger;
        }

        public override Task<CommandResponse<TResult>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<TResult>> next, CancellationToken cancellationToken)
        {
            try
            {
                return next();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Command error");

                return CommandResponse.FromResultTask<TResult>(ex);
            }
        }
    }
}
