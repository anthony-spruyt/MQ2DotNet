using MediatR;

namespace MQFlux.Core
{
    public interface ISetCacheCommand<TValue> : IRequest
    {
        TValue Value { get; set; }
    }
}