using MediatR;
using MQFlux.Models;
using MQFlux.Services;
using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface ICharacterConfigRequest : IFluxConfigRequest
    {
        /// <summary>
        /// The effective character configuration that is set by the middleware. Do not set this when creating a new request.
        /// </summary>
        CharacterConfig Character { get; set; }
    }

    public class CharacterConfigBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IConfig config;

        public CharacterConfigBehavior(IConfig config)
        {
            this.config = config;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICharacterConfigRequest charConfigRequest)
            {
                charConfigRequest.Character = await GetCharacterConfig(charConfigRequest);
            }

            return await next();
        }

        private async Task<CharacterConfig> GetCharacterConfig(ICharacterConfigRequest charConfigRequest)
        {
            try
            {
                return await config.GetCharacterConfig();
            }
            catch (Exception ex)
            {
                throw new CharacterConfigException("Failed to get character configuration.", ex);
            }
        }
    }

    public class CharacterConfigException : ApplicationException
    {
        public CharacterConfigException()
        {
        }

        public CharacterConfigException(string message) : base(message)
        {
        }

        public CharacterConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CharacterConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}