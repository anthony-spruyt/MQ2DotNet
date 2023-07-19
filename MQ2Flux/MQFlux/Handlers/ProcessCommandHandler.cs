using MediatR;
using MQFlux.Commands;
using MQFlux.Commands.CombatCommands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
{
    public class ProcessCommandHandler : IRequestHandler<ProcessCommand, bool>
    {
        private readonly IMediator mediator;

        public ProcessCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<bool> Handle(ProcessCommand request, CancellationToken cancellationToken)
        {
            await mediator.Send(new DismissAlertWindowCommand(), cancellationToken);
            await mediator.Send(new LearnALanguageCommand(), cancellationToken);

            return 
                //await mediator.Send(new AutoAttackCommand(), cancellationToken) ||
                await mediator.Send(new ForageCommand(), cancellationToken) ||
                await mediator.Send(new DispenseCommand(), cancellationToken) ||
                await mediator.Send(new SummonFoodAndDrinkCommand(), cancellationToken) ||
                await mediator.Send(new EatAndDrinkCommand(), cancellationToken) ||
                await mediator.Send(new PutStatFoodInTopSlotsCommand(), cancellationToken) ||
                await mediator.Send(new MeditateCommand(), cancellationToken) ||
                await mediator.Send(new DoneMeditatingCommand(), cancellationToken);
        }
    }
}
