﻿using MediatR;
using MQ2Flux.Behaviors;
using MQ2Flux.Models;
using MQ2Flux.Services;

namespace MQ2Flux.Commands
{
    public class DispenseCommand : ICharacterConfigRequest, INotInCombatRequest, IStandingStillRequest, INotWhenCastingRequest, IRequest<bool>
    {
        public CharacterConfig Character { get; set; }
        public FluxConfig Config { get; set; }
        public IMQ2Context Context { get; set; }
    }
}
