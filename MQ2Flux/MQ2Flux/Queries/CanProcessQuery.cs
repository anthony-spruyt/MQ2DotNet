using MediatR;
using MQ2Flux.Behaviors;
using MQ2Flux.Services;

namespace MQ2Flux.Queries
{
    public class CanProcessQuery : IMQ2ContextRequest, IRequest<bool>
    {
        public IMQ2Context Context { get; set; }
    }
}
