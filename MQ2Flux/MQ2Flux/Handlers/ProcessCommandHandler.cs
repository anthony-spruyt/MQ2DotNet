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
            await mediator.Send(new DismissAlertWindowCommand(), cancellationToken);
            await mediator.Send(new LearnALanguageCommand(), cancellationToken);

            if (await mediator.Send(new DispenseCommand(), cancellationToken))
            {
                return;
            }

            if (await mediator.Send(new SummonFoodAndDrinkCommand(), cancellationToken))
            {
                return;
            }

            if (await mediator.Send(new EatAndDrinkCommand(), cancellationToken))
            {
                return;
            }

            if (await mediator.Send(new ForageCommand(), cancellationToken))
            {
                return;
            }
        }
    }
}
