using System;

namespace MQ2Flux.Services
{
    public interface IMQ2Logger
    {
        void Log(string text);
        void LogError(Exception exception, string message = null);
    }
}
