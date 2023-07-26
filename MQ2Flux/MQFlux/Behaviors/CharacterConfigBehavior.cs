using MediatR;
using MQFlux.Models;
using MQFlux.Services;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Behaviors
{
    public interface ICharacterConfigRequest : IConfigRequest, IContextRequest
    {
        /// <summary>
        /// The effective character configuration that is set by the middleware. Do not set this when creating a new request.
        /// </summary>
        CharacterConfigSection Character { get; set; }
    }

    public class CharacterConfigBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICache cache;
        private readonly IConfig config;

        public CharacterConfigBehavior(ICache cache, IConfig config)
        {
            this.cache = cache;
            this.config = config;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICharacterConfigRequest charConfigRequest)
            {
                charConfigRequest.Character = GetCharacterConfig(charConfigRequest);
            }

            return next();
        }

        private CharacterConfigSection GetCharacterConfig(ICharacterConfigRequest charConfigRequest)
        {
            CharacterConfigSection characterConfig;

            try
            {
                var name = charConfigRequest.Context.TLO.Me.Name;
                var server = charConfigRequest.Context.TLO.EverQuest.Server;

                if (cache.TryGetValue(CacheKeys.CharacterConfig, out CharacterConfigSection value))
                {
                    characterConfig = value;
                }
                else
                {
                    characterConfig = charConfigRequest?.Config?.Characters?.FirstOrDefault
                    (
                        c =>
                            string.Compare(c.Name, name) == 0 &&
                            string.Compare(c.Server, server) == 0
                    );

                    var createNewCharacterConfigSection = characterConfig == null;

                    characterConfig = characterConfig.Effective(charConfigRequest.Config?.Defaults);

                    cache.TryAdd(CacheKeys.CharacterConfig, characterConfig);
                    
                    if (createNewCharacterConfigSection)
                    {
                        _ = Task.Run
                        (
                            () =>
                            {
                                config.Upsert(characterConfig);
                                config.Save(false);
                            }
                        ).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CharacterConfigException("Failed to get character configuration.", ex);
            }

            return characterConfig;
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