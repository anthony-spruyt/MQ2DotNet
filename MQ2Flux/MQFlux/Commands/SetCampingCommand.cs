namespace MQFlux.Commands
{
    public class SetCampingCommand : ISetCacheCommand<bool>
    {
        public bool Value { get; set; }

        public SetCampingCommand(bool value)
        {
            Value = value;
        }
    }
}
