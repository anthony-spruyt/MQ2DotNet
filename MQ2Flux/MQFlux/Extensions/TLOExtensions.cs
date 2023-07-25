using MQ2DotNet.Services;

namespace MQFlux.Extensions
{
    public static class TLOExtensions
    {
        public static bool IsSpellBookOpen(this TLO @this)
        {
            return @this.IsWindowOpen("SpellBookWnd");
        }

        /// <summary>
        /// Closes the spellbook if it is iopen.
        /// </summary>
        /// <param name="this"></param>
        public static void CloseSpellBook(this TLO @this)
        {
            var spellBookWindow = @this.GetWindow("SpellBookWnd");

            if ((spellBookWindow?.Open).GetValueOrDefault(false))
            {
                spellBookWindow.DoClose();
            }
        }

        public static bool IsWindowOpen(this TLO @this, string windowName)
        {
            var window = @this.GetWindow(windowName);

            return (window?.Open).GetValueOrDefault(false);
        }
    }
}
