using MQFlux.Behaviors;
using MQFlux.Core;
using MQFlux.Services;
using System;

namespace MQFlux.Commands
{
    public class MeditateCommand :
        PCCommand,
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
        public TimeSpan IdleTime => TimeSpan.FromSeconds(1);
    }
}
