using MediatR;
using MQFlux.Core;
using MQFlux.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IIdleTimeRequest
    {
        /// <summary>
        /// The minimum required idle time to have passed for this behavior.
        /// </summary>
        TimeSpan IdleTime { get; }
    }

    public class IdleTimeBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IMediator mediator;

        public IdleTimeBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is IIdleTimeRequest idleTimeRequest)
            {
                var response = await mediator.Send(new IdleSinceQuery(), cancellationToken);
                var lastIdle = response.Result;

                if (DateTime.UtcNow - lastIdle < idleTimeRequest.IdleTime)
                {
                    return ShortCircuitResult();
                }
            }

            return await next();
        }
    }
}
