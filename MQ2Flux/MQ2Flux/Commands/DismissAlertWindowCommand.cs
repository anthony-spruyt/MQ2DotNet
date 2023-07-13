using MediatR;
using MQ2Flux.Behaviors;
using MQ2Flux.Services;

namespace MQ2Flux.Commands
{
    public class DismissAlertWindowCommand : IMQContextRequest, IRequest
    {
        public IMQContext Context { get ; set; }
    }
}
