using MediatR;

namespace MQFlux.Core
{
    public class Command<TResult> : IRequest<CommandResponse<TResult>>
    {
    }
}
