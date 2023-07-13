using MediatR;
using MQFlux.Commands;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Handlers.Handlers
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
