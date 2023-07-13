using MediatR;
using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Queries
{
    public class CanProcessQuery : IMQContextRequest, IRequest<bool>
    {
        public IMQContext Context { get; set; }
    }
}
