using MQ2Flux.Models;

namespace MQ2Flux
{
    public interface ICharacterConfigRequest : IConfigRequest, IMq2ContextRequest
    {
        CharacterConfig Character { get; set; }
    }
}
