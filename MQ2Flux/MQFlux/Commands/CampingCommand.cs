using MQFlux.Core;

namespace MQFlux.Commands
{
    public class CampingCommand : ISetCacheCommand<bool>
    {
        public bool Value { get; set; }

        public CampingCommand(bool value)
        {
            Value = value;
        }
    }
}
