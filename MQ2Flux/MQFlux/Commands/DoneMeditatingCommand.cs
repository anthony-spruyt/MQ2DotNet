using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class DoneMeditatingCommand :
        PCCommand,
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
