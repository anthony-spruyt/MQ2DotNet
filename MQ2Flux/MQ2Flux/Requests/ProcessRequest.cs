using MediatR;
using MQ2Flux.Models;

namespace MQ2Flux
{
    public class ProcessRequest : ICharacterConfigRequest, IRequest
    {
        public string[] Args { get; set; }
        public IMq2Context Context { get; set; }
        public FluxConfig Config { get; set; }
        public CharacterConfig Character { get; set; }
    }
}
