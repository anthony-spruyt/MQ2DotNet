using MediatR;
using MQFlux.Commands.CombatCommands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers.CombatCommandHandlers
{
    public class MeleeAutoAttackCommandHandler : IRequestHandler<MeleeAutoAttackCommand, bool>
    {
        public Task<bool> Handle(MeleeAutoAttackCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }
    }
}
