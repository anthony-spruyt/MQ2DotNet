using MediatR;
using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class MeditateCommand :
        IStandingStillRequest, 
        INotCastingRequest, 
        ICasterRequest,
        IConsciousRequest,
        INotFeignedDeathRequest,
        IBankWindowNotOpenRequest,
        IRequest<bool>
    {
        public IContext Context { get; set; }
        public bool AllowBard => false;
    }
}
