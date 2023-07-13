using MediatR;
using MQ2Flux.Commands;
using MQ2Flux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers.Handlers
{
    public class SaveConfigCommandHandler : IRequestHandler<SaveConfigCommand>
    {
        private readonly IMQFluxConfig config;

        public SaveConfigCommandHandler(IMQFluxConfig config)
        {
            this.config = config;
        }

        public async Task Handle(SaveConfigCommand request, CancellationToken cancellationToken)
        {
            await config.SaveAsync(request.Notify);
        }
    }
}
