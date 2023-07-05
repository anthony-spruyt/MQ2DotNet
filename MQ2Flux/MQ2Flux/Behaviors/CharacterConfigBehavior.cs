using MediatR;
using MQ2Flux.Models;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Behaviors
{
    public interface ICharacterConfigRequest : IConfigRequest, IMQ2ContextRequest
    {
        CharacterConfig Character { get; set; }
    }

    public class CharacterConfigBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
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
                string name = charConfigRequest.Context.TLO.Me.Name;
                string server = charConfigRequest.Context.TLO.EverQuest.Server;

                characterConfig = charConfigRequest?.Config?.Characters?.FirstOrDefault
                (
                    c =>
                        string.Compare(c.Name, name, true) == 0 &&
                        string.Compare(c.Server, server, true) == 0
                );

                if (characterConfig == null)
                {
                    characterConfig = new CharacterConfig()
                    {
                        Name = name,
                        Server = server
                    };


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