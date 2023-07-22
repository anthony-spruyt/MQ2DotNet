﻿using MediatR;
using MQFlux.Behaviors;
using MQFlux.Models;
using MQFlux.Services;

namespace MQFlux.Commands
{
    public class InitializeCommand : PCCommand<Unit>, ICharacterConfigRequest
    {
        public IContext Context { get; set; }
        public FluxConfig Config { get; set; }
        public CharacterConfig Character { get; set; }
    }
}
