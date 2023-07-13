using System;
using System.Threading;

namespace MQFlux.Services
{
    public interface IMQCommand : IDisposable
    {
        string Command { get; }
        CancellationToken CancellationToken { get; }

        void Handle(string[] args);
    }
}
