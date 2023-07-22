﻿using MQFlux.Behaviors;
using MQFlux.Models;
using MQFlux.Services;
using System;

namespace MQFlux.Commands
{
    public class SummonFoodAndDrinkCommand :
        PCCommand<bool>,
        ICharacterConfigRequest,
        IConsciousRequest,
        INotInCombatRequest,
        IStandingStillRequest,
        ICasterRequest,
        INotCastingRequest,
        INoItemOnCursorRequest,
        INotFeignedDeathRequest,
        IIdleTimeRequest
    {
        public bool AllowBard => false;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IContext Context { get; set; }
        public TimeSpan IdleTime => TimeSpan.FromSeconds(1);
    }
}
