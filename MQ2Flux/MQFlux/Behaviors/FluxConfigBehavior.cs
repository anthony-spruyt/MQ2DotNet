using MediatR;
using MQFlux.Models;
using MQFlux.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface IFluxConfigRequest
    {
        /// <summary>
        /// The configuration that is set by the middleware. Do not set this when creating a new request.
        /// </summary>
        FluxConfig Config { get; set; }
    }

    public class FluxConfigBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IConfig config;

        public FluxConfigBehavior(IConfig config)
        {
            this.config = config;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IFluxConfigRequest configRequest)
            {
                configRequest.Config = await config.GetFluxConfig(cancellationToken);
            }

            return await next();
        }
    }
}