using MQ2DotNet.MQ2API.DataTypes;

namespace MQFlux.Extensions
{
    public static class MacroTypeExtensions
    {
        public static bool IsRunning(this MacroType @this)
        {
            return @this != null && @this.Paused.HasValue;
        }

        public static bool IsPaused(this MacroType @this)
        {
            return (@this?.Paused).GetValueOrDefault(false);
        }

        public static void Pause(this MacroType @this)
        {
            if (!@this.IsRunning() ||
                @this.IsPaused())
            {
                return;
            }

            @this.Pause();
        }

        public static void Resume(this MacroType @this)
        {
            if (!@this.IsRunning() ||
                !@this.IsPaused())
            {
                return;
            }

            @this.Resume();
        }
    }
}
