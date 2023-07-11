using MediatR;
using MQ2Flux.Behaviors;
using MQ2Flux.Services;

namespace MQ2Flux.Commands
{
    public class LearnALanguageCommand : IMQ2ContextRequest, IRequest
    {
        public IMQ2Context Context { get; set; }
    }
}
