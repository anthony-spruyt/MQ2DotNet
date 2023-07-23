using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Core
{
    public abstract class QueryHandler<TRequest, TResult> : IRequestHandler<TRequest, QueryResponse<TResult>>
        where TRequest : Query<TResult>
    {
        public abstract Task<QueryResponse<TResult>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
