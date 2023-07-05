using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux.Services
{
    public interface IMQ2AsyncCommand : IDisposable
    {
        string Command { get; }
        CancellationToken CancellationToken { get; }

        Task HandleAsync(string[] args);
    }
}
