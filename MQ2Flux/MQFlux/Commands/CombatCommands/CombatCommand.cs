using MediatR;
using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands.CombatCommands
{
    public abstract class CombatCommand<TResponse> : IMQContextRequest, IRequest<TResponse>
    {
        public abstract IMQContext Context { get; set; }
    }
}
