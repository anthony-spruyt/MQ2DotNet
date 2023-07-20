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
        INotFeignedDeathRequest,
        IRequest<bool>
    {
        public IContext Context { get; set; }
        public bool AllowBard => false;
    }
}
