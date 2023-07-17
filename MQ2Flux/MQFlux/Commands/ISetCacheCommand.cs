using MediatR;

public interface ISetCacheCommand<TValue> : IRequest
{
    TValue Value { get; set; }
}