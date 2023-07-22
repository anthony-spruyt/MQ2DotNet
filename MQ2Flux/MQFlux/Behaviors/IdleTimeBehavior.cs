using MediatR;
using MQFlux.Commands;
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

    public class IdleTimeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : PCCommand<TResponse>
    {
        private readonly IMediator mediator;

        public IdleTimeBehavior(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IIdleTimeRequest idleTimeRequest)
            {
                var dateTime = await mediator.Send(new IdleSinceQuery(), cancellationToken);

                if (DateTime.UtcNow - dateTime < idleTimeRequest.IdleTime)
                {
                    return default;
                }
            }

            return await next();
        }
    }
}
