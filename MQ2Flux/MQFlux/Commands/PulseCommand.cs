using MediatR;
using MQFlux.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands
{
    public class PulseCommand : Command<Unit>
    {

    }

    public class PulseCommandHandler : CommandHandler<PulseCommand, Unit>
    {
        private readonly IMediator mediator;

        public PulseCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<CommandResponse<Unit>> Handle(PulseCommand request, CancellationToken cancellationToken)
        {
            var commands = new IRequest<CommandResponse<bool>>[]
            {
                new ForageCommand(),
                new BuffCommand(),
                new DispenseCommand(),
                new SummonFoodAndDrinkCommand(),
                new EatAndDrinkCommand(),
                new PutStatFoodInTopSlotsCommand(),
                new MeditateCommand(),
                new DoneMeditatingCommand(),
                new LearnALanguageCommand(),
            };

            CommandResponse<bool> response;

            for (int i = 0; i < commands.Length; i++)
            {
                response = await mediator.Send(commands[i], cancellationToken);

                if (response.Result)
                {
                    await mediator.Send(new IdleSinceCommand(), cancellationToken);

                    break;
                }
            }

            return CommandResponse.FromResult(Unit.Value);
        }
    }
}
