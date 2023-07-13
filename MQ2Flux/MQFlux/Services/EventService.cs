using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.EQ;
using MQ2DotNet.MQ2API.DataTypes;
using System;

namespace MQFlux.Services
{
    public interface IEventService
    {
        bool Camping { get; }
        GameState GameState { get; }
        bool Zoning { get; }
    }

    public static class EventServiceExtensions
    {
        public static IServiceCollection AddEventService(this IServiceCollection services)
        {
            return services
                .AddSingleton<IEventService, EventService>();
        }
    }

    public class EventService : IEventService, IDisposable
    {
        public bool Camping { get; private set; }
        public GameState GameState { get; private set; }
        public bool Zoning { get; private set; }

        private readonly IMQContext context;

        private bool disposedValue;

        public EventService(IMQContext context)
        {
            this.context = context;

            Camping = false;
            GameState = context.TLO?.EverQuest?.GameState ?? GameState.Unknown;
            Zoning = false;

            var events = context.Events;

            events.BeginZone += Events_BeginZone;
            events.EndZone += Events_EndZone;
            events.OnAddGroundItem += Events_OnAddGroundItem;
            events.OnAddSpawn += Events_OnAddSpawn;
            events.OnChatEQ += Events_OnChatEQ;
            events.OnCleanUI += Events_OnCleanUI;
            events.OnDrawHUD += Events_OnDrawHUD;
            events.OnLoadPlugin += Events_OnLoadPlugin;
            events.OnMacroStart += Events_OnMacroStart;
            events.OnMacroStop += Events_OnMacroStop;
            events.OnReloadUI += Events_OnReloadUI;
            events.OnRemoveGroundItem += Events_OnRemoveGroundItem;
            events.OnRemoveSpawn += Events_OnRemoveSpawn;
            events.OnUnloadPlugin += Events_OnUnloadPlugin;
            events.OnZoned += Events_OnZoned;
            events.SetGameState += Events_SetGameState;
        }

        private void Events_BeginZone(object sender, EventArgs e)
        {
            Zoning = true;
        }

        private void Events_EndZone(object sender, EventArgs e)
        {
            Zoning = false;
        }

        private void Events_OnAddGroundItem(object sender, GroundType e)
        {
        }

        private void Events_OnAddSpawn(object sender, MQ2DotNet.MQ2API.DataTypes.SpawnType e)
        {
        }

        private void Events_OnChatEQ(object sender, string e)
        {
            if (!Camping && e.EndsWith("seconds to prepare your camp."))
            {
                Camping = true;
            }
            else if (Camping && e.StartsWith("You abandon your preparations to camp"))
            {
                Camping = false;
            }
        }

        private void Events_OnCleanUI(object sender, EventArgs e)
        {
        }

        private void Events_OnDrawHUD(object sender, EventArgs e)
        {
        }

        private void Events_OnLoadPlugin(object sender, string e)
        {
        }

        private void Events_OnMacroStart(object sender, string e)
        {
        }

        private void Events_OnMacroStop(object sender, string e)
        {
        }

        private void Events_OnReloadUI(object sender, EventArgs e)
        {
        }
        private void Events_OnRemoveGroundItem(object sender, GroundType e)
        {
        }

        private void Events_OnRemoveSpawn(object sender, MQ2DotNet.MQ2API.DataTypes.SpawnType e)
        {
        }

        private void Events_OnUnloadPlugin(object sender, string e)
        {
        }

        private void Events_OnZoned(object sender, EventArgs e)
        {
        }

        private void Events_SetGameState(object sender, GameState e)
        {
            GameState = e;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    var events = context.Events;

                    events.BeginZone -= Events_BeginZone;
                    events.EndZone -= Events_EndZone;
                    events.OnAddGroundItem -= Events_OnAddGroundItem;
                    events.OnAddSpawn -= Events_OnAddSpawn;
                    events.OnChatEQ -= Events_OnChatEQ;
                    events.OnCleanUI -= Events_OnCleanUI;
                    events.OnDrawHUD -= Events_OnDrawHUD;
                    events.OnLoadPlugin -= Events_OnLoadPlugin;
                    events.OnMacroStart -= Events_OnMacroStart;
                    events.OnMacroStop -= Events_OnMacroStop;
                    events.OnReloadUI -= Events_OnReloadUI;
                    events.OnRemoveGroundItem -= Events_OnRemoveGroundItem;
                    events.OnRemoveSpawn -= Events_OnRemoveSpawn;
                    events.OnUnloadPlugin -= Events_OnUnloadPlugin;
                    events.OnZoned -= Events_OnZoned;
                    events.SetGameState -= Events_SetGameState;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~EventService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
