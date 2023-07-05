using MQ2Flux.Models;
using System.Threading.Tasks;

namespace MQ2Flux.Services
{
    public interface IMQ2Config
    {
        FluxConfig FluxConfig { get; }

        Task SaveAsync(bool notify = false);
    }
}
