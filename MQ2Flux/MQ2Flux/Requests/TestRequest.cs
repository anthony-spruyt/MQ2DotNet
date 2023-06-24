using MediatR;
using MQ2Flux.Models;

namespace MQ2Flux
{
    public class TestRequest : ICharacterConfigRequest, IRequest
    {
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IMq2Context Context { get; set; }
    }
}
