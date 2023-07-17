namespace MQFlux.Commands
{
    public class SetZoningCommand : ISetCacheCommand<bool>
    {
        public bool Value { get; set; }

        public SetZoningCommand(bool value)
        {
            Value = value;
        }
    }
}
