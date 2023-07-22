using MediatR;

namespace MQFlux.Commands
{
    public class Command<TResponse> : IRequest<TResponse>
    {
    }
}
