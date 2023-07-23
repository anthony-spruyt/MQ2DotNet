namespace MQFlux.Core
{
    public abstract class PCCommandHandler<TRequest> : CommandHandler<TRequest, bool> where TRequest : PCCommand
    {

    }
}
