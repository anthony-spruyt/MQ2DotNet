using Microsoft.Extensions.DependencyInjection;
using System;

namespace MQ2Flux.Services
{
    public interface IMQLogger
    {
        void Log(string text, TimeSpan? noSpam = null);
        void LogError(Exception exception, string message = null);
    }

    public static class MQLoggerExtensions
    {
        public static IServiceCollection AddMQLogging(this IServiceCollection services)
        {
            return services.AddSingleton<IMQLogger, MQLogger>();
        }
    }

    public class MQLogger : IMQLogger
    {
        private const int MAX_TEXT_LENGTH = 1000;

        private readonly IMQContext context;
        private readonly IChatHistory chatHistory;

        public MQLogger(IMQContext context, IChatHistory chatHistory)
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

            context.MQ?.WriteChatSafe($"\am[{nameof(MQFlux)}] \aw{text}");
        }

        public void LogError(Exception exception, string message = null)
        {
            string errorMessage = string.IsNullOrWhiteSpace(message) ? "A MQFlux error has occured" : message;

            if (!chatHistory.NoSpam(TimeSpan.FromSeconds(5), errorMessage))
            {
                return;
            }

            context.MQ?.WriteChatSafe($"\ar[{nameof(MQFlux)}] \aw{errorMessage}");

            if (exception == null)
            {
                return;
            }

            context.MQ?.WriteChatSafe($"\ar[{nameof(MQFlux)}] \awException message: {exception.Message}");
            context.MQ?.WriteChatSafe($"\ar[{nameof(MQFlux)}] \awStack Tace: {exception.StackTrace}");
        }
    }
}
