using System.Collections.Generic;

namespace MQ2Flux
{
    public interface IMq2CommandProvider
    {
        IEnumerable<IMq2AsyncCommand> AsyncCommands { get; }
        IEnumerable<IMq2Command> Commands { get; }
        void Load();
        void Unload();
    }
}
