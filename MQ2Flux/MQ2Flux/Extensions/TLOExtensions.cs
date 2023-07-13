using MQ2DotNet.Services;

namespace MQ2Flux.Extensions
{
    public static class TLOExtensions
    {
        public static bool IsSpellBookOpen(this TLO @this)
        {
            return @this.IsWindowOpen("SpellBookWnd");
        }

        public static void CloseSpellBook(this TLO @this)
        {
            var spellBookWindow = @this.GetWindow("SpellBookWnd");

            if (spellBookWindow?.Open ?? false)
            {
                spellBookWindow.DoClose();
            }
        }

        public static bool IsWindowOpen(this TLO @this, string windowName)
        {
            var window = @this.GetWindow(windowName);

            return window?.Open ?? false;
        }
    }
}
