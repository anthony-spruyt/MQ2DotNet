using MediatR;
using MQFlux.Behaviors;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class DoneMeditatingCommand :
        IConsciousRequest,
        IStandingStillRequest,
        INotWhenCastingRequest,
        ICasterRequest,
        INotWhenSpellbookIsOpenRequest,
        INotFeignedDeathRequest,
        IRequest<bool>
    {
        public IContext Context { get; set; }

        public bool AllowBard => false;
    }
}
