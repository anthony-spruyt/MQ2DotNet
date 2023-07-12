using MediatR;
using MQ2Flux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class ProcessCommandHandler : IRequestHandler<ProcessCommand>
    {
        private readonly IMediator mediator;

        public ProcessCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(ProcessCommand request, CancellationToken cancellationToken)
        {
            // TODO am I camping query and return if true.
            await mediator.Send(new DismissAlertWindowCommand(), cancellationToken);

            if (request.Character.AutoLearnLanguages.GetValueOrDefault(false))
            {
                await mediator.Send(new LearnALanguageCommand(), cancellationToken);
            }

            if
            (
                request.Character.AutoDispenseFoodAndDrink.GetValueOrDefault(false) &&
                await mediator.Send(new DispenseCommand(), cancellationToken)
            )
            {
                return;
            }

            if
            (
                request.Character.AutoSummonFoodAndDrink.GetValueOrDefault(false) &&
                await mediator.Send(new SummonFoodAndDrinkCommand(), cancellationToken)
            )
            {
                return;
            }

            if
            (
                request.Character.AutoForage.GetValueOrDefault(false) && 
                await mediator.Send(new ForageCommand(), cancellationToken)
            )
            {
                return;
            }

            if
            (
                request.Character.AutoEatAndDrink.GetValueOrDefault(false) && 
                await mediator.Send(new EatAndDrinkCommand(), cancellationToken)
            )
            {
                return;
            }

            if
            (
                request.Character.AutoSortInventory.GetValueOrDefault(false) &&
                await mediator.Send(new SortInventoryCommand(), cancellationToken)
            )
            {
                return;
            }
        }
    }
}
