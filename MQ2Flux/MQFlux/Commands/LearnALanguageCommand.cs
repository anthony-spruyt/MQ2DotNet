using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class LearnALanguageCommand :
        PCCommand,
        ICharacterConfigRequest,
        IGroupedRequest,
        IConsciousRequest
    {
        public IContext Context { get; set; }
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
    }
}
