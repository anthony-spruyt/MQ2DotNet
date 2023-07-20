using Microsoft.Extensions.DependencyInjection;
using MQ2DotNet.MQ2API.DataTypes;
using System;

namespace MQFlux.Services
{
    public interface IMacroService
    {
        MacroType GetMacro();
        bool IsRunning();
        bool IsPaused();
        void Pause();
        void Resume();
    }

    public static class MacroServiceExtensions
    {
        public static IServiceCollection AddMacroService(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMacroService, MacroService>();
        }
    }

    public class MacroService : IMacroService
    {
        private readonly IContext context;
        private readonly IMQLogger mqLogger;

        public MacroService(IContext context, IMQLogger mqLogger)
        {
            this.context = context;
            this.mqLogger = mqLogger;
        }

        public MacroType GetMacro()
        {
            throw new NotImplementedException();
        }

        public bool IsPaused()
        {
            var macro = context.TLO.Macro;

            return (macro?.Paused).GetValueOrDefault(false);
        }

        public bool IsRunning()
        {
            var macro = context.TLO.Macro;

            return macro != null && macro.Paused.HasValue;
        }

        public void Pause()
        {
            if (!IsRunning() ||
                IsPaused())
            {
                return;
            }

            mqLogger.Log("Pausing macro...", TimeSpan.Zero);
            context.MQ.DoCommand("/mqp");
        }

        public void Resume()
        {
            if (!IsRunning() ||
                !IsPaused())
            {
                return;
            }

            mqLogger.Log("Resuming macro...", TimeSpan.Zero);
            context.MQ.DoCommand("/mqp");
        }
    }
}
