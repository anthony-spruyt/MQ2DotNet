using MediatR;
using Microsoft.Extensions.Logging;
using MQ2Flux.Commands;
using MQ2Flux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Handlers
{
    public class ProcessHandler : IRequestHandler<ProcessCommand>
    {
        private readonly ILogger<ProcessHandler> logger;
        private readonly IMQ2Logger mq2Logger;

        public ProcessHandler(ILogger<ProcessHandler> logger, IMQ2Logger mq2Logger)
        {
            this.logger = logger;
            this.mq2Logger = mq2Logger;
        }

        public async Task Handle(ProcessCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
