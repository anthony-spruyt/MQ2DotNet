using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class PulseCommandHandler : IRequestHandler<PulseCommand, bool>
    {
        private readonly IMediator mediator;

        public PulseCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<bool> Handle(PulseCommand request, CancellationToken cancellationToken)
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
