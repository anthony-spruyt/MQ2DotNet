using System;

namespace MQ2Flux.Services
{
    public class MQ2Logger : IMQ2Logger
    {
        private const int MAX_TEXT_LENGTH = 1000;

        private readonly IMQ2Context context;

        public MQ2Logger(IMQ2Context context)
        {
            this.context = context;

        }

        public void Log(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Log($"{nameof(text)} is null, empty or whitespace");

                return;
            }

            context.MQ2?.WriteChatSafe($"\am[{nameof(MQ2Flux)}] \aw{text}");
        }

        public void LogError(Exception exception, string message = null)
        {
            string errorMessage = string.IsNullOrWhiteSpace(message) ? "A MQ2Flux error has occured" : message;

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
