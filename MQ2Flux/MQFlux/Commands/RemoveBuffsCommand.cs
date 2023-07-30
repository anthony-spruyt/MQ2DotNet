using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class RemoveBuffsCommand :
        PCCommand,
        IConsciousRequest,
        INotCastingRequest
    {
        public bool AllowBard => true;
    }

    public class RemoveBuffsCommandHandler : PCCommandHandler<RemoveBuffsCommand>
    {
        private readonly IContext context;

        public RemoveBuffsCommandHandler(IContext context)
        {
            this.context = context;
        }

        public override async Task<CommandResponse<bool>> Handle(RemoveBuffsCommand request, CancellationToken cancellationToken)
        {
            var buffs = context.TLO.Me.Buffs;

            foreach (var buff in buffs)
            {
                buff.Remove();

                await Task.Yield();
            }

            return CommandResponse.FromResult(buffs.Count() > 0);
        }
    }
}
