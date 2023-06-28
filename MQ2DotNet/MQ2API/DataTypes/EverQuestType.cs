using JetBrains.Annotations;
using MQ2DotNet.EQ;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// MQ2 type for general game information.
    /// Last Verified: 2023-06-25
    /// </summary>
    [PublicAPI]
    [MQ2Type("everquest")]
    public class EverQuestType : MQ2DataType
    {
        internal EverQuestType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            _chatChannel = new IndexedStringMember<int, BoolType, string>(this, "ChatChannel");
            _charSelectList = new IndexedMember<CharSelectListType, string, CharSelectListType, int>(this, "CharSelectList");
            _validLoc = new IndexedMember<BoolType, string>(this, "ValidLoc");
        }

        /// <summary>
        /// Handle to the window
        /// </summary>
        public long? HWND => GetMember<Int64Type>("HWND");

        /// <summary>
        /// Current game state.
        /// </summary>
        public GameState? GameState => GetMember<StringType>("GameState");

        /// <summary>
        /// Username of your account name
        /// </summary>
        public string LoginName => GetMember<StringType>("LoginName");

        /// <summary>
        /// Name of the server in short form e.g. firiona
        /// </summary>
        public string Server => GetMember<StringType>("Server");

        /// <summary>
        /// Last command executed
        /// </summary>
        public string LastCommand => GetMember<StringType>("LastCommand");

        /// <summary>
        /// Name of the person you last received a tell from
        /// </summary>
        public string LastTell => GetMember<StringType>("LastTell");

        /// <summary>
        /// Number of clock ticks this instance of eqgame.exe has been running for
        /// </summary>
        public uint? Running => GetMember<IntType>("Running");

        /// <summary>
        /// X (horizontal) coordinate of the mouse cursor in UI coordinate space, relative to the left edge of the game window
        /// </summary>
        public uint? MouseX => GetMember<IntType>("MouseX");

        /// <summary>
        /// Y (vertical) coordinate of the mouse cursor in UI coordinate space relative to the top edge of the game window
        /// </summary>
        public uint? MouseY => GetMember<IntType>("MouseY");

        /// <summary>
        /// Ping time to the EQ server in milliseconds
        /// </summary>
        public uint? Ping => GetMember<IntType>("Ping");

        /// <summary>
        /// Number of chat channels you are in
        /// </summary>
        public uint? ChatChannels => GetMember<IntType>("ChatChannels");

        /// <summary>
        /// Name of a chat channel by number, or true/false if you are in a chat channel by name
        /// </summary>
        private IndexedStringMember<int, BoolType, string> _chatChannel;

        /// <summary>
        /// X (horizontal) start of viewport, always 0?
        /// </summary>
        public uint? ViewportX => GetMember<IntType>("ViewportX");

        /// <summary>
        /// Y (vertical) start of viewport, always 0?
        /// </summary>
        public uint? ViewportY => GetMember<IntType>("ViewportY");

        /// <summary>
        /// X (horizontal) end of viewport
        /// </summary>
        public uint? ViewportXMax => GetMember<IntType>("ViewportXMax");

        /// <summary>
        /// Y (vertical) end of viewport
        /// </summary>
        public uint? ViewportYMax => GetMember<IntType>("ViewportYMax");

        /// <summary>
        /// X (horizontal) center of viewport
        /// </summary>
        public uint? ViewportXCenter => GetMember<IntType>("ViewportXCenter");

        /// <summary>
        /// Y (vertical) center of viewport
        /// </summary>
        public uint? ViewportYCenter => GetMember<IntType>("ViewportYCenter");

        /// <summary>
        /// TODO: Document EverQuestType.LClickedObject
        /// </summary>
        public bool LClickedObject => GetMember<BoolType>("LClickedObject");

        /// <summary>
        /// Current window title
        /// </summary>
        public string WinTitle => GetMember<StringType>("WinTitle");
        
        /// <summary>
        /// Process ID of this eqgame.exe
        /// </summary>
        public uint? PID => GetMember<IntType>("PID");

        /// <summary>
        /// TODO: new member + confirm as it doesnt follow the usual pattern and templates <seealso cref="ScreenMode"/>
        /// </summary>
        public uint? xScreenMode => GetMember<IntType>("xScreenMode");

        /// <summary>
        /// Screen mode, 2 = windowed ?
        /// TODO: confirm this one, it doesnt follow the usual pattern and templates <seealso cref="xScreenMode"/>
        /// </summary>
        public int? ScreenMode => GetMember<IntType>("ScreenMode");

        /// <summary>
        /// Max foreground FPS
        /// </summary>
        public uint? MaxFPS => GetMember<IntType>("MaxFPS");

        /// <summary>
        /// Max background FPS
        /// </summary>
        public uint? MaxBGFPS => GetMember<IntType>("MaxBGFPS");

        /// <summary>
        /// Process priority of this eqgame.exe, one of "LOW", "BELOW NORMAL", "NORMAL", "ABOVE NORMAL", "REALTIME"
        /// TODO: map to an enum
        /// </summary>
        public string PPriority => GetMember<StringType>("PPriority");

        /// <summary>
        /// Is a /copylayout currently in progress?
        /// TODO: does this need to be a nullable bool?
        /// </summary>
        public bool? LayoutCopyInProgress => GetMember<BoolType>("LayoutCopyInProgress");

        /// <summary>
        /// Window the mouse cursor was last over
        /// </summary>
        public WindowType LastMouseOver => GetMember<WindowType>("LastMouseOver");

        /// <summary>
        /// Character in the character select list by name or position (1 based)
        /// </summary>
        private IndexedMember<CharSelectListType, string, CharSelectListType, int> _charSelectList;

        /// <summary>
        /// Name of the current UI skin
        /// </summary>
        public string CurrentUI => GetMember<StringType>("CurrentUI");

        /// <summary>
        /// True if using default UI skin
        /// TODO: does this need to be a nullable bool?
        /// </summary>
        public bool? IsDefaultUILoaded => GetMember<BoolType>("IsDefaultUILoaded");

        /// <summary>
        /// Is the window in the foreground?
        /// TODO: does this need to be a nullable bool?
        /// </summary>
        public bool? Foreground => GetMember<BoolType>("Foreground");

        /// <summary>
        /// Is the given location, in yxz format separated by spaces, a valid location in the current zone?
        /// </summary>
        private IndexedMember<BoolType, string> _validLoc;

        /// <summary>
        /// Path to the Everquest folder
        /// </summary>
        public string Path => GetMember<StringType>("Path");

        /// <summary>
        /// UI scale factor
        /// </summary>
        public float? UiScale => GetMember<FloatType>("UiScale");

        public override string ToString()
        {
            return nameof(EverQuestType);
        }
    }
}