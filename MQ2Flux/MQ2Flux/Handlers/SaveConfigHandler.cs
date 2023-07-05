using MediatR;
using MQ2Flux.Commands;
using MQ2Flux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers.Handlers
{
    public class SaveConfigHandler : IRequestHandler<SaveConfigCommand>
    {
        private readonly IMQ2Config config;

        public SaveConfigHandler(IMQ2Config config)
        {
            this.config = config;
        }

        public async Task Handle(SaveConfigCommand request, CancellationToken cancellationToken)
        {
            await config.SaveAsync(request.Notify);
        }
    }
}
