using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Core
{
    public abstract class CommandHandler<TRequest, TResult> : IRequestHandler<TRequest, CommandResponse<TResult>>
        where TRequest : Command<TResult>
    {
        public abstract Task<CommandResponse<TResult>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
