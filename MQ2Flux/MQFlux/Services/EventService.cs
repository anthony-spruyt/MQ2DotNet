using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.EQ;
using MQ2DotNet.MQ2API.DataTypes;
using MQFlux.Commands;
using System;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface IEventService
    {
        
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
        private readonly IMQContext context;
        private readonly IMediator mediator;
        private bool disposedValue;

        public EventService(IMQContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;

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
            _ = Task.Run(() => mediator.Send(new SetZoningCommand(true)));
        }

        private void Events_EndZone(object sender, EventArgs e)
        {
            _ = Task.Run(() => mediator.Send(new SetZoningCommand(false)));
        }

        private void Events_OnAddGroundItem(object sender, GroundType e)
        {
        }

        private void Events_OnAddSpawn(object sender, MQ2DotNet.MQ2API.DataTypes.SpawnType e)
        {
        }

        private void Events_OnChatEQ(object sender, string e)
        {
            if (e.EndsWith("seconds to prepare your camp."))
            {
                _ = Task.Run(() => mediator.Send(new SetCampingCommand(true)));
            }
            else if (e.StartsWith("You abandon your preparations to camp"))
            {
                _ = Task.Run(() => mediator.Send(new SetCampingCommand(false)));
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
            _ = Task.Run(() => mediator.Send(new SetGameStateCommand(e)));
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
