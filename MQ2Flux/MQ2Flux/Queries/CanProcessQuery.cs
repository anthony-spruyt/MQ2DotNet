using MediatR;
using MQ2Flux.Behaviors;
using MQ2Flux.Services;

namespace MQ2Flux.Queries
{
    public class CanProcessQuery : IMQContextRequest, IRequest<bool>
    {
        public IMQContext Context { get; set; }
    }
}
