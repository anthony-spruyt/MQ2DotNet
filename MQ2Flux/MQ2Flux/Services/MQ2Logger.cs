using Microsoft.Extensions.DependencyInjection;
using System;

namespace MQ2Flux.Services
{
    public interface IMQ2Logger
    {
        void Log(string text, TimeSpan? noSpam = null);
        void LogError(Exception exception, string message = null);
    }

    public static class MQ2LoggerExtensions
    {
        public static IServiceCollection AddMQ2Logging(this IServiceCollection services)
        {
            return services.AddSingleton<IMQ2Logger, MQ2Logger>();
        }
    }

    public class MQ2Logger : IMQ2Logger
    {
        private const int MAX_TEXT_LENGTH = 1000;

        private readonly IMQ2Context context;
        private readonly IMQ2ChatHistory chatHistory;

        public MQ2Logger(IMQ2Context context, IMQ2ChatHistory chatHistory)
        {
            this.context = context;
            this.chatHistory = chatHistory;
        }

        public void Log(string text, TimeSpan? noSpam = null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Log($"{nameof(text)} is null, empty or whitespace");

                return;
            }

            if (noSpam == null)
            {
                noSpam = TimeSpan.FromMilliseconds(1000);
            }

            if (noSpam != TimeSpan.Zero && !chatHistory.NoSpam(noSpam.Value, text))
            {
                return;
            }

            context.MQ2?.WriteChatSafe($"\am[{nameof(MQ2Flux)}] \aw{text}");
        }

        public void LogError(Exception exception, string message = null)
        {
            string errorMessage = string.IsNullOrWhiteSpace(message) ? "A MQ2Flux error has occured" : message;

            if (!chatHistory.NoSpam(TimeSpan.FromSeconds(5), errorMessage))
            {
                return;
            }

            context.MQ2?.WriteChatSafe($"\ar[{nameof(MQ2Flux)}] \aw{errorMessage}");

            if (exception == null)
            {
                return;
            }

            context.MQ2?.WriteChatSafe($"\ar[{nameof(MQ2Flux)}] \awException message: {exception.Message}");
            context.MQ2?.WriteChatSafe($"\ar[{nameof(MQ2Flux)}] \awStack Tace: {exception.StackTrace}");
        }
    }
}
