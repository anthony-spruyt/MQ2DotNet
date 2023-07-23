using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface INotFeignedDeathRequest : IContextRequest
    {

    }

    public class NotFeignedDeathBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if (request is INotFeignedDeathRequest notFeignedDeathRequest &&
                notFeignedDeathRequest.Context.TLO.Me.State == SpawnState.Feign)
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}

