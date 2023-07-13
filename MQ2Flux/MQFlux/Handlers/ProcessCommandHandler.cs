using MediatR;
using MQFlux.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers
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
            await mediator.Send(new LearnALanguageCommand(), cancellationToken);
            if (await mediator.Send(new ForageCommand(), cancellationToken)) return;
            if (await mediator.Send(new DispenseCommand(), cancellationToken)) return;
            if (await mediator.Send(new SummonFoodAndDrinkCommand(), cancellationToken)) return;
            if (await mediator.Send(new EatAndDrinkCommand(), cancellationToken)) return;
            if (await mediator.Send(new SortInventoryCommand(), cancellationToken)) return;
        }
    }
}
