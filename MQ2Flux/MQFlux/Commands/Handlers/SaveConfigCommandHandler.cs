using MediatR;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Commands.Handlers.Handlers
{
    public class SaveConfigCommandHandler : IRequestHandler<SaveConfigCommand, Unit>
    {
        private readonly IConfig config;

        public SaveConfigCommandHandler(IConfig config)
        {
            this.config = config;
        }

        public async Task<Unit> Handle(SaveConfigCommand request, CancellationToken cancellationToken)
        {
            await config.SaveAsync(request.Notify);

            return Unit.Value;
        }
    }
}
