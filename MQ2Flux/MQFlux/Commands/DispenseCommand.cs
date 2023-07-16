﻿using MediatR;
using MQFlux.Behaviors;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class DispenseCommand :
        ICharacterConfigRequest,
        IConsciousRequest,
        INotInCombatRequest, 
        IStandingStillRequest, 
        INotWhenCastingRequest, 
        INoItemOnCursorRequest, 
        IRequest<bool>
    {
        public bool AllowBard => false;
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IMQContext Context { get; set; }
    }
}
