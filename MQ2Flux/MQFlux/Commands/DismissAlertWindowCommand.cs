using MediatR;
using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class DismissAlertWindowCommand : PCCommand<Unit>, IContextRequest
    {
        public IContext Context { get; set; }
    }
}
