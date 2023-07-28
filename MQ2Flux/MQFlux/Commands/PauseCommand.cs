using MQFlux.Core;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

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

        public override async Task Handle(PauseCommand request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            // TODO wait for pulse loop to acknowledge.
        }
    }
}
