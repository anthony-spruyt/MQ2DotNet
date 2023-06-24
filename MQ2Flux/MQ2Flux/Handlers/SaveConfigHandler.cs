using MediatR;
using MQ2Flux.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class SaveConfigHandler : IRequestHandler<SaveConfigRequest>
    {
        private readonly IMQ2Config config;

        public SaveConfigHandler(IMQ2Config config)
        {
            this.config = config;
        }

        public async Task Handle(SaveConfigRequest request, CancellationToken cancellationToken)
        {
            await config.SaveAsync(request.Notify);
        }
    }
}
