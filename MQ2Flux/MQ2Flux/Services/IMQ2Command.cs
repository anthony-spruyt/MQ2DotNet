using System;
using System.Threading;

namespace MQ2Flux.Services
{
    public interface IMQ2Command : IDisposable
    {
        string Command { get; }
        CancellationToken CancellationToken { get; }

        void Handle(string[] args);
    }
}
