using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class DismissAlertWindowCommand : PCCommand, IContextRequest
    {
        public IContext Context { get; set; }
    }
}
