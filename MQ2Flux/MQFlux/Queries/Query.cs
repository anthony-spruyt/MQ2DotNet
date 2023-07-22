using MediatR;

namespace MQFlux.Queries
{
    public abstract class Query<TResponse> : IRequest<TResponse>
    {
    }
}
