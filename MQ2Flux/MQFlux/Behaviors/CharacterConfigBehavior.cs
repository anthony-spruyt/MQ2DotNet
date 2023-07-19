﻿using MediatR;
using MQFlux.Handlers;
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
        CharacterConfig Character { get; set; }
    }

    public class CharacterConfigBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICache cache;

        public CharacterConfigBehavior(ICache cache)
        {
            this.cache = cache;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ICharacterConfigRequest charConfigRequest)
            {
                charConfigRequest.Character = GetCharacterConfig(charConfigRequest);
            }

            return next();
        }

        private CharacterConfig GetCharacterConfig(ICharacterConfigRequest charConfigRequest)
        {
            CharacterConfig characterConfig;

            try
            {
                var name = charConfigRequest.Context.TLO.Me.Name;
                var server = charConfigRequest.Context.TLO.EverQuest.Server;

                if (cache.TryGetValue(CacheKeys.CharacterConfig, out CharacterConfig value))
                {
                    characterConfig = value;
                }
                else
                {
                    characterConfig = charConfigRequest?.Config?.Characters?.FirstOrDefault
                    (
                        c =>
                            string.Compare(c.Name, name, true) == 0 &&
                            string.Compare(c.Server, server, true) == 0
                    );

                    characterConfig = characterConfig.Effective(charConfigRequest.Config?.Defaults);

                    cache.TryAdd(CacheKeys.CharacterConfig, characterConfig);
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