using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MQFlux
{
    public static class UtilityExtensions
    {
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            MaxDepth = 64,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        public static string Serialize(this object @this)
        {
            return JsonSerializer.Serialize(@this, options: jsonOptions);
        }

        public static void LogDebugJson(this ILogger logger, object @object)
        {
            _ = Task.Run
            (
                () =>
                {
                    try
                    {
                        var json = @object.Serialize();

                        logger.LogDebug(json);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Failed to serialize and log the requested object.");
                    }
                }
            ).ConfigureAwait(false);
        }
    }
}
