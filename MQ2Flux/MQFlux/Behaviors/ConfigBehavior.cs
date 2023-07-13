using MediatR;
using MQFlux.Models;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IConfigRequest
    {
        /// <summary>
        /// The configuration that is set by the middleware. Do not set this when creating a new request.
        /// </summary>
        FluxConfig Config { get; set; }
    }

    public class ConfigBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMQFluxConfig config;

        public ConfigBehavior(IMQFluxConfig config)
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