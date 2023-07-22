using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers
{
    public class PulseCommandHandler : IRequestHandler<PulseCommand, Unit>
    {
        private readonly IMediator mediator;

        public PulseCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(PulseCommand request, CancellationToken cancellationToken)
        {
            // Do anything that does not count as being busy/active, can be done in one frame and in parallel.
            // Order is not important.
            await Task.WhenAll
            (
                mediator.Send(new DismissAlertWindowCommand(), cancellationToken),
                mediator.Send(new LearnALanguageCommand(), cancellationToken)
            );

            // Do actions that count as being active and that can take more than one frame.
            // If an action returns true then the following actions will be short circuited.
            // Order is important.
            var didSomething = //await mediator.Send(new AutoAttackCommand(), cancellationToken) ||
                await mediator.Send(new ForageCommand(), cancellationToken) ||
                await mediator.Send(new DispenseCommand(), cancellationToken) ||
                await mediator.Send(new SummonFoodAndDrinkCommand(), cancellationToken) ||
                await mediator.Send(new EatAndDrinkCommand(), cancellationToken) ||
                await mediator.Send(new PutStatFoodInTopSlotsCommand(), cancellationToken) ||
                await mediator.Send(new MeditateCommand(), cancellationToken) ||
                await mediator.Send(new DoneMeditatingCommand(), cancellationToken);

            if (didSomething)
            {
                await mediator.Send(new IdleSinceCommand(), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
