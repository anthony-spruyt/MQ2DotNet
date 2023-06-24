using System;
using System.Threading;

namespace MQ2Flux
{
    public interface IMq2Command : IDisposable
    {
        string Command { get; }
        CancellationToken Token { get; }

        void Handle(string[] args);
    }
}
