using MediatR;
using MQFlux.Behaviors;
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
                new CleanBuffSlotsCommand(),
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
            IRequest<CommandResponse<bool>> command;

            for (int i = 0; i < commands.Length; i++)
            {
                command = commands[i];

                response = await mediator.Send(command, cancellationToken);

                if (response.Result)
                {
                    if (!(command is IIdleTimeRequest))
                    {
                        await mediator.Send(new IdleSinceCommand(), cancellationToken);
                    }

                    break;
                }
            }

            return CommandResponse.FromResult(Unit.Value);
        }
    }
}
