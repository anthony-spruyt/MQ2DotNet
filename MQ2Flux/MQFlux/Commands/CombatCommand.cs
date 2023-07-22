using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public abstract class CombatCommand<TResponse> : Command<TResponse>, IContextRequest
    {
        public abstract IContext Context { get; set; }
    }
}
