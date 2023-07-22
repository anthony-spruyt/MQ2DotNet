using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class DoneMeditatingCommand :
        PCCommand<bool>,
        IConsciousRequest,
        IStandingStillRequest,
        INotCastingRequest,
        ICasterRequest,
        ISpellbookNotOpenRequest,
        IBankWindowNotOpenRequest,
        INotFeignedDeathRequest
    {
        public IContext Context { get; set; }

        public bool AllowBard => false;
    }
}
