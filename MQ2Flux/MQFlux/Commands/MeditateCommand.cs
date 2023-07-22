using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class MeditateCommand :
        Command<bool>,
        IStandingStillRequest,
        INotCastingRequest,
        ICasterRequest,
        IConsciousRequest,
        INotFeignedDeathRequest,
        IBankWindowNotOpenRequest
    {
        public IContext Context { get; set; }
        public bool AllowBard => false;
    }
}
