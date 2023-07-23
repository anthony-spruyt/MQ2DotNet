namespace MQFlux.Core
{
    public abstract class PCCommandBehavior<TRequest> : CommandBehavior<TRequest, bool>
        where TRequest : PCCommand
    {

    }
}
