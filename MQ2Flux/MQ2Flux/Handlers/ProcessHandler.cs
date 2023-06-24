using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux
{
    public class ProcessHandler : IRequestHandler<ProcessRequest>
    {
        private readonly IMq2Logger logger;

        public ProcessHandler(IMq2Logger logger)
        {
            this.logger = logger;
        }

        public async Task Handle(ProcessRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
