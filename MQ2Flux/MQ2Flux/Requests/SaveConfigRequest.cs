using MediatR;
using MQ2Flux.Models;

namespace MQ2Flux.Requests
{
    public class SaveConfigRequest : IRequest, IConfigRequest
    {
        public FluxConfig Config { get; set; }
        public bool Notify { get; set; } = false;
    }
}
