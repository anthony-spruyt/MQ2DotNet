using MQFlux.Behaviors;
using MQFlux.Services;
using System;

namespace MQFlux.Commands
{
    public class MeditateCommand :
        PCCommand<bool>,
        IStandingStillRequest,
        INotCastingRequest,
        ICasterRequest,
        IConsciousRequest,
        INotFeignedDeathRequest,
        IBankWindowNotOpenRequest,
        IIdleTimeRequest
    {
        public bool AllowBard => false;
        public IContext Context { get; set; }
        public TimeSpan IdleTime => TimeSpan.FromSeconds(5);
    }
}
