using MediatR;
using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class MeditateCommand :
        IStandingStillRequest, 
        INotWhenCastingRequest, 
        ICasterRequest, 
        IConsciousRequest,
        IRequest<bool>
    {
        public IContext Context { get; set; }
        public bool AllowBard => false;
    }
}
