using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public abstract class CombatCommand<TResponse> : PCCommand<TResponse>, IContextRequest
    {
        public abstract IContext Context { get; set; }
    }
}
