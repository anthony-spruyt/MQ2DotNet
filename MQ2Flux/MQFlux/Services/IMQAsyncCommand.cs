using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQFlux.Services
{
    public interface IMQAsyncCommand : IDisposable
    {
        string Command { get; }
        CancellationToken CancellationToken { get; }

        Task HandleAsync(string[] args);
    }
}
