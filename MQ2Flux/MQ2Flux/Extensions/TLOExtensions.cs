namespace MQ2Flux.Extensions
{
    public static class TLOExtensions
    {
        public static bool IsSpellBookOpen(this MQ2DotNet.Services.TLO @this)
        {
            return @this.IsWindowOpen("SpellBookWnd");
        }

        public static bool IsWindowOpen(this MQ2DotNet.Services.TLO @this, string windowName)
        {
            var window = @this.GetWindow(windowName);

            return window?.Open ?? false;
        }
    }
}
