using System.Collections.Generic;

namespace MQ2Flux.Services
{
    public interface IMQ2CommandProvider
    {
        IEnumerable<IMQ2AsyncCommand> AsyncCommands { get; }
        IEnumerable<IMQ2Command> Commands { get; }
        void Load();
        void Unload();
    }
}
