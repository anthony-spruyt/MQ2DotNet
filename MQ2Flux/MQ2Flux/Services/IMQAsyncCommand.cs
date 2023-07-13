using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Services
{
    public interface IMQAsyncCommand : IDisposable
    {
        string Command { get; }
        CancellationToken CancellationToken { get; }

        Task HandleAsync(string[] args);
    }
}
