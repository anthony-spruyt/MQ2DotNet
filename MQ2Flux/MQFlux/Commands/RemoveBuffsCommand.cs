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
        public IContext Context { get; set; }
    }

    public class RemoveBuffsCommandHandler : PCCommandHandler<RemoveBuffsCommand>
    {
        public override async Task<CommandResponse<bool>> Handle(RemoveBuffsCommand request, CancellationToken cancellationToken)
        {
            var buffs = request.Context.TLO.Me.Buffs;

            foreach (var buff in buffs)
            {
                buff.Remove();

                await Task.Yield();
            }

            return CommandResponse.FromResult(buffs.Count() > 0);
        }
    }
}
