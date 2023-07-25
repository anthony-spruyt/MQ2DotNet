using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class ResumeCommand : ISetCacheCommand<bool>
    {
        public bool Value { get; set; }

        public ResumeCommand()
        {
            Value = false;
        }
    }

    public class ResumeCommandHandler : SetCacheCommandHandler<ResumeCommand, bool>
    {
        public ResumeCommandHandler(ICache cache) : base(cache)
        {
        }

        public override string Key => CacheKeys.IsPaused;
    }
}
