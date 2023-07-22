namespace MQFlux.Commands
{
    public class ZoningCommand : ISetCacheCommand<bool>
    {
        public bool Value { get; set; }

        public ZoningCommand(bool value)
        {
            Value = value;
        }
    }
}
