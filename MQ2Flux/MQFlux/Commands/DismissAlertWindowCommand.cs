using MediatR;
using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class DismissAlertWindowCommand : Command<Unit>, IContextRequest
    {
        public IContext Context { get; set; }
    }
}
