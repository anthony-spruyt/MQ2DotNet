namespace MQ2Flux.Models
{
    public class FluxConfig
    {
        public string Version { get; set; }

        public DefaultConfig Defaults { get; set; }

        public CharacterConfig[] Characters { get; set; }

        public FluxConfig()
        {
            Version = "1.0.0";
        }
    }

    public class DefaultConfig
    {
    }

    public class CharacterConfig
    {
        public string Name { get; set; }
        public string Server { get; set; }
    }
}
