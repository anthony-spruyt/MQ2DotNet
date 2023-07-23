using MQFlux.Core;
using System;

namespace MQFlux.Commands
{
    public class IdleSinceCommand : ISetCacheCommand<DateTime>
    {
        public DateTime Value { get; set; }

        public IdleSinceCommand()
        {
            Value = DateTime.UtcNow;
        }
    }
}