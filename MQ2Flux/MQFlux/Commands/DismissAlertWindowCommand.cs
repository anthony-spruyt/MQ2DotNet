using MediatR;
using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class DismissAlertWindowCommand : IMQContextRequest, IRequest
    {
        public IMQContext Context { get ; set; }
    }
}
