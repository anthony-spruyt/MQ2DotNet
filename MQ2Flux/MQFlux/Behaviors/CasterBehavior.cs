using MediatR;
using MQ2DotNet.EQ;
using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface ICasterRequest
    {

    }

    public class CasterBehavior<TRequest, TResponse> : PCCommandBehavior<TRequest> where TRequest : PCCommand
    {
        private readonly IContext context;

        public CasterBehavior(IContext context)
        {
            this.context = context;
        }

        public override Task<CommandResponse<bool>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<bool>> next, CancellationToken cancellationToken)
        {
            if
            (
                request is ICasterRequest &&
                (
                    !context.TLO.Me.Class.CanCast ||
                    context.TLO.Me.Class == Class.Bard
                )
            )
            {
                return ShortCircuitResultTask();
            }

            return next();
        }
    }
}
