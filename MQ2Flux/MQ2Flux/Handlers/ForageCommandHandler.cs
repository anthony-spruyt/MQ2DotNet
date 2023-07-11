using MediatR;
using MQ2Flux.Commands;
using MQ2Flux.Extensions;
using MQ2Flux.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class ForageCommandHandler : IRequestHandler<ForageCommand, bool>
    {
        private readonly IItemService itemService;

        public ForageCommandHandler(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public async Task<bool> Handle(ForageCommand request, CancellationToken cancellationToken)
        {
            return false;

            var me = request.Context.TLO.Me;

            if (me.Combat || me.AmICasting() || !me.Abilities.Any(i => string.Compare(i.Skill.Name, "Forage", true) == 0))
            {
                return false;
            }

            request.Context.MQ2.DoCommand("");

            return true;
        }
    }
}
