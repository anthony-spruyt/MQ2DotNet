using MediatR;
using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class DismissAlertWindowCommand : IContextRequest, IRequest
    {
        public IContext Context { get ; set; }
    }
}
