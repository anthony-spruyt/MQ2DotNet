using MediatR;

namespace MQFlux.Core
{
    public abstract class Query<TResult> : IRequest<QueryResponse<TResult>>
    {
    }
}
