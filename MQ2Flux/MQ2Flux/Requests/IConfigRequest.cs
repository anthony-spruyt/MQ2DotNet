using MediatR;
using MQ2Flux.Models;

namespace MQ2Flux
{
    public interface IConfigRequest
    {
        FluxConfig Config { get; set; }
    }
}
