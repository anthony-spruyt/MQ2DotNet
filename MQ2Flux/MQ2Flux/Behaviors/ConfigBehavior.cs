using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux
{
    public class ConfigBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMQ2Config config;

        public ConfigBehavior(IMQ2Config config)
        {
            this.config = config;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IConfigRequest configRequest)
            {
                configRequest.Config = config.FluxConfig;
            }

            return next();
        }
    }
}