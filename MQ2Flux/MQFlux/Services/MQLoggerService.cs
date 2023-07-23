using Microsoft.Extensions.DependencyInjection;
using System;

namespace MQFlux.Services
{
    public interface IMQLogger
    {
        void Log(string text, TimeSpan? noSpam = null);
        void LogError(Exception exception, string message = null);
    }

    public static class MQLoggerServiceExtensions
    {
        public static IServiceCollection AddMQLogger(this IServiceCollection services)
        {
            return services.AddSingleton<IMQLogger, MQLoggerService>();
        }
    }

    public class MQLoggerService : IMQLogger
    {
        private const int MAX_TEXT_LENGTH = 1000;

        private readonly IContext context;
        private readonly IChatHistory chatHistory;

        public MQLoggerService(IContext context, IChatHistory chatHistory)
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

            context.MQ?.WriteChatSafe($"{Now()}\am[{nameof(MQFlux)}] \aw{text}");
        }

        public void LogError(Exception exception, string message = null)
        {
            string errorMessage = string.IsNullOrWhiteSpace(message) ? "A MQFlux error has occured" : message;

            if (!chatHistory.NoSpam(TimeSpan.FromSeconds(5), errorMessage))
            {
                return;
            }

            context.MQ?.WriteChatSafe($"{Now()}\ar[{nameof(MQFlux)}] \aw{errorMessage}");

            if (exception == null)
            {
                return;
            }

            context.MQ?.WriteChatSafe($"\ar[{nameof(MQFlux)}] \awException message: {exception.Message}");
            context.MQ?.WriteChatSafe($"\ar[{nameof(MQFlux)}] \awStack Tace: {exception.StackTrace}");
        }

        private static string Now()
        {
            return $"\a-y[{DateTime.Now:HH:mm:ss}]";
        }
    }
}
