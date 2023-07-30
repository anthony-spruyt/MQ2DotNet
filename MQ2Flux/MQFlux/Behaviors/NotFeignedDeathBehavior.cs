using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotFeignedDeathRequest
    {

    }

    public class NotFeignedDeathBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public NotFeignedDeathBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is INotFeignedDeathRequest &&
                context.TLO.Me.State == SpawnState.Feign)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}

