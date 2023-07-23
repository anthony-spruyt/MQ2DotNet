using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Core
{
    public abstract class QueryBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, QueryResponse<TResult>>
        where TRequest : Query<TResult>
    {
        public abstract Task<QueryResponse<TResult>> Handle(TRequest request, RequestHandlerDelegate<QueryResponse<TResult>> next, CancellationToken cancellationToken);
    }
}
