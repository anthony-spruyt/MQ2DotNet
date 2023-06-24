using System;

namespace MQ2Flux
{
    public interface IMq2Logger
    {
        void Log(string text);
        void LogError(Exception exception, string message = null);
    }
}
