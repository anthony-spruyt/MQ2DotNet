using MediatR;
using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands.CombatCommands
{
    public abstract class CombatCommand<TResponse> : IContextRequest, IRequest<TResponse>
    {
        public abstract IContext Context { get; set; }
    }
}
