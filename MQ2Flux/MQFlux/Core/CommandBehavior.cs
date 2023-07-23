using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Core
{
    public abstract class CommandBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, CommandResponse<TResult>>
        where TRequest : Command<TResult>
    {
        public abstract Task<CommandResponse<TResult>> Handle(TRequest request, RequestHandlerDelegate<CommandResponse<TResult>> next, CancellationToken cancellationToken);

        protected CommandResponse<TResult> ShortCircuitResult(TResult result = default)
        {
            return CommandResponse.FromResult(this, result);
        }

        protected Task<CommandResponse<TResult>> ShortCircuitResultTask(TResult result = default)
        {
            return Task.FromResult(ShortCircuitResult(result));
        }
    }
}
