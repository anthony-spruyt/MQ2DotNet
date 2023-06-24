using System;
using System.Threading;
using System.Threading.Tasks;

namespace MQ2Flux
{
    public interface IMq2AsyncCommand : IDisposable
    {
        string Command { get; }
        CancellationToken CancellationToken { get; }

        Task HandleAsync(string[] args);
    }
}
