﻿using MQ2DotNet.MQ2API;
using MQ2DotNet.Services;

namespace MQ2Flux
{
    public class Mq2Context : IMq2Context
    {
        public MQ2 MQ2 { get; private set; }

        public Chat Chat { get; private set; }

        public Commands Commands { get; private set; }

        public Events Events { get; private set; }

        public Spawns Spawns { get; private set; }

        public TLO TLO { get; private set; }

        public Mq2Context(MQ2 mq2, Chat chat, Commands commands, Events events, Spawns spawns, TLO tlo)
        {
            MQ2 = mq2;
            Chat = chat;
            Commands = commands;
            Events = events;
            Spawns = spawns;
            TLO = tlo;
        }
    }
}
