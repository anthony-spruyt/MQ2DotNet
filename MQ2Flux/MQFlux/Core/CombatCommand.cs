using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Core
{
    public abstract class CombatCommand : PCCommand, IContextRequest
    {
        public abstract IContext Context { get; set; }
    }
}