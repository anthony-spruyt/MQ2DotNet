using JetBrains.Annotations;
using MQ2DotNet.EQ;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// Data types related to the current EverQuest session.
    /// Last Verified: 2023-07-01
    /// https://docs.macroquest.org/reference/data-types/datatype-everquest/
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
            LoginName = GetMember<StringType>("LoginName");
            Server = GetMember<StringType>("Server");
        }

        /// <summary>
        /// Window handle.
        /// </summary>
        public long? HWND => GetMember<Int64Type>("HWND");

        /// <summary>
        /// Shows the current game state. Values: CHARSELECT, INGAME, PRECHARSELECT, UNKNOWN
        /// </summary>
        public GameState? GameState => GetMember<StringType>("GameState");

        /// <summaryMouseX
        /// Your station name
        /// </summary>
        public string LoginName { get; }

        /// <summary>
        /// Name of the server in short form e.g. firiona.
        /// doco says "Full name of your server" but this returns the short name.
        /// </summary>
        public string Server { get; }

        /// <summary>
        /// Last command entered
        /// </summary>
        public string LastCommand => GetMember<StringType>("LastCommand");

        /// <summary>
        /// Name of last person to send you a tell
        /// </summary>
        public string LastTell => GetMember<StringType>("LastTell");

        /// <summary>
        /// Running time of current MQ2 session, in milliseconds
        /// </summary>
        public TimeSpan? Running
        {
            get
            {
                var milliseconds = (uint?)GetMember<IntType>("Running");

                if (milliseconds.HasValue)
                {
                    return TimeSpan.FromMilliseconds(milliseconds.Value);
                }

                return null;
            }
        }

        /// <summary>
        /// Mouse's X location.
        /// X (horizontal) coordinate of the mouse cursor in UI coordinate space, relative to the left edge of the game window.
        /// </summary>
        public uint? MouseX => GetMember<IntType>("MouseX");

        /// <summary>
        /// Mouse's Y location.
        /// Y (vertical) coordinate of the mouse cursor in UI coordinate space relative to the top edge of the game window.
        /// </summary>
        public uint? MouseY => GetMember<IntType>("MouseY");

        /// <summary>
        /// Your current ping (milliseconds).
        /// </summary>
        public uint? Ping => GetMember<IntType>("Ping");

        /// <summary>
        /// Returns the number of channels currently joined
        /// </summary>
        public uint? ChatChannels => GetMember<IntType>("ChatChannels");

        /// <summary>
        /// Returns the name of chat channel #
        /// ChatChannel[#]
        /// Returns TRUE if channelname is joined
        /// ChatChannel[channelname]
        /// </summary>
        private readonly IndexedStringMember<int, BoolType, string> _chatChannel;

        /// <summary>
        /// Returns the name of chat channel #
        /// </summary>
        /// <param name="number">The base 1 chat channel number.</param>
        /// <returns></returns>
        public string GetChatChannel(int number) => _chatChannel[number];

        /// <summary>
        /// Returns TRUE if channelname is joined
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool InChannel(string name) => (bool)_chatChannel[name];

        /// <summary>
        /// The names of all the chat channels you are in.
        /// </summary>
        public IEnumerable<string> ChatChannelNames
        {
            get
            {
                var count = (int?)ChatChannels ?? 0;
                List<string> names = new List<string>(count);

                for (int i = 0; i < count; i++)
                {
                    names.Add(GetChatChannel(i + 1));
                }

                return names;
            }
        }

        /// <summary>
        /// EverQuest viewport upper left (X) position
        /// </summary>
        public uint? ViewportX => GetMember<IntType>("ViewportX");

        /// <summary>
        /// EverQuest viewport upper left (Y) position
        /// </summary>
        public uint? ViewportY => GetMember<IntType>("ViewportY");

        /// <summary>
        /// EverQuest viewport lower right (X) position
        /// </summary>
        public uint? ViewportXMax => GetMember<IntType>("ViewportXMax");

        /// <summary>
        /// EverQuest viewport lower right (Y) position
        /// </summary>
        public uint? ViewportYMax => GetMember<IntType>("ViewportYMax");

        /// <summary>
        /// EverQuest viewport center (X) position
        /// </summary>
        public uint? ViewportXCenter => GetMember<IntType>("ViewportXCenter");

        /// <summary>
        /// EverQuest viewport center (Y) position
        /// </summary>
        public uint? ViewportYCenter => GetMember<IntType>("ViewportYCenter");

        /// <summary>
        /// Returns TRUE if an object has been left clicked
        /// </summary>
        public bool LClickedObject => GetMember<BoolType>("LClickedObject");

        /// <summary>
        /// Titlebar text of the Everquest window.
        /// </summary>
        public string WinTitle => GetMember<StringType>("WinTitle");

        /// <summary>
        /// Process ID of this eqgame.exe
        /// </summary>
        public uint? PID => GetMember<IntType>("PID");

        /// <summary>
        /// TODO: new member + confirm as it doesnt follow the usual pattern and templates <seealso cref="ScreenMode"/>
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        public uint? xScreenMode => GetMember<IntType>("xScreenMode");
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Returns the screenmode as an integer, 2 is Normal and 3 is No Windows
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
        /// Returns the processor priority that Everquest is set to. Values: UNKNOWN, LOW, BELOW NORMAL, NORMAL, ABOVE NORMAL, HIGH, REALTIME
        /// TODO: map to an enum
        /// </summary>
        public string PPriority => GetMember<StringType>("PPriority");

        /// <summary>
        /// Returns TRUE if a layoutcopy is in progress and FALSE if not.
        /// </summary>
        public bool LayoutCopyInProgress => GetMember<BoolType>("LayoutCopyInProgress");

        /// <summary>
        /// Returns the last window you moused over
        /// </summary>
        public WindowType LastMouseOver => GetMember<WindowType>("LastMouseOver");

        /// <summary>
        /// Character in the character select list by name or position (1 based)
        /// </summary>
        private readonly IndexedMember<CharSelectListType, string, CharSelectListType, int> _charSelectList;

        /// <summary>
        /// Character in the character select list by position (1 based)
        /// </summary>
        /// <param name="nth"></param>
        /// <returns></returns>
        public CharSelectListType GetCharacter(int position) => _charSelectList[position];

        /// <summary>
        /// Character in the character select list by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CharSelectListType GetCharacter(string name) => _charSelectList[name];

        /// <summary>
        /// All characters in the character select list.
        /// </summary>
        public IEnumerable<CharSelectListType> Characters
        {
            get
            {
                List<CharSelectListType> items = new List<CharSelectListType>(8);

                var first = GetCharacter(1);

                if (first != null)
                {
                    items.Add(first);

                    for (int i = 1; i < first.Count; i++)
                    {
                        items.Add(GetCharacter(i));
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// return a string representing the currently loaded UI skin
        /// </summary>
        public string CurrentUI => GetMember<StringType>("CurrentUI");

        /// <summary>
        /// returns a bool true or false if the "Default" UI skin is the one loaded
        /// </summary>
        public bool IsDefaultUILoaded => GetMember<BoolType>("IsDefaultUILoaded");

        /// <summary>
        /// Returns TRUE if EverQuest is in Foreground
        /// </summary>
        public bool Foreground => GetMember<BoolType>("Foreground");

        /// <summary>
        /// Is the given location, in yxz format separated by spaces, a valid location in the current zone?
        /// </summary>
        private readonly IndexedMember<BoolType, string> _validLoc;

        /// <summary>
        /// Returns true if the given coordinates are valid.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public bool IsLocValid(int y, int x, int z) => (bool)_validLoc[$"{y} {x} {z}"];

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
            return OriginalToString();
        }
    }
}