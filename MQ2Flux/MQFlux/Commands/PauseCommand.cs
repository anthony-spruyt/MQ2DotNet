﻿using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class PauseCommand : ISetCacheCommand<bool>
    {
        public bool Value { get; set; }

        public PauseCommand()
        {
            Value = true;
        }
    }

    public class PauseCommandHandler : SetCacheCommandHandler<PauseCommand, bool>
    {
        public PauseCommandHandler(ICache cache) : base(cache)
        {
        }

        public override string Key => CacheKeys.IsPaused;
    }
}
