using JetBrains.Annotations;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace MQ2DotNet.MQ2API.DataTypes
{
    /// <summary>
    /// This contains data related to the specified in-game window.
    /// Windows come in many forms, but all are represented with the generic window type.
    /// In some of the descriptions, a bold window type may be specified, which defines the behavior for that type of window.
    /// This type is used for both windows and controls on the window.
    /// Some members are only applicable to controls e.g. Checked.
    /// Last Verified: 2023-07-03
    /// https://docs.macroquest.org/reference/data-types/datatype-window/
    /// </summary>
    [PublicAPI]
    [MQ2Type("window")]
    public class WindowType : MQ2DataType
    {
        public const int MAX_CHILDREN = 100;

        internal WindowType(MQ2TypeFactory mq2TypeFactory, MQ2TypeVar typeVar) : base(mq2TypeFactory, typeVar)
        {
            List = new ListMember(this);
            _tab = new IndexedMember<WindowType, string, WindowType, int>(this, "Tab");
        }
        
        /// <summary>
        /// Sends a left mouse button down notification to the window/control
        /// </summary>
        public void LeftMouseDown() => GetMember<MQ2DataType>("LeftMouseDown");

        /// <summary>
        /// Sends a left mouse button up notification to the window/control
        /// </summary>
        public void LeftMouseUp() => GetMember<MQ2DataType>("LeftMouseUp");

        /// <summary>
        /// Sends a left mouse button held notification to the window/control
        /// </summary>
        public void LeftMouseHeld() => GetMember<MQ2DataType>("LeftMouseHeld");

        /// <summary>
        /// Sends a left mouse button held up notification to the window/control
        /// </summary>
        public void LeftMouseHeldUp() => GetMember<MQ2DataType>("LeftMouseHeldUp");

        /// <summary>
        /// Sends a right mouse button down notification to the window/control
        /// </summary>
        public void RightMouseDown() => GetMember<MQ2DataType>("RightMouseDown");

        /// <summary>
        /// Sends a right mouse button up notification to the window/control
        /// </summary>
        public void RightMouseUp() => GetMember<MQ2DataType>("RightMouseUp");

        /// <summary>
        /// Sends a right mouse button held notification to the window/control
        /// </summary>
        public void RightMouseHeld() => GetMember<MQ2DataType>("RightMouseHeld");

        /// <summary>
        /// Sends a right mouse held up notification to the window/control
        /// </summary>
        public void RightMouseHeldUp() => GetMember<MQ2DataType>("RightMouseHeldUp");

        /// <summary>
        /// Does the action of opening a window
        /// </summary>
        public void DoOpen() => GetMember<MQ2DataType>("DoOpen");

        /// <summary>
        /// Does the action of closing a window
        /// </summary>
        public void DoClose() => GetMember<MQ2DataType>("DoClose");

        /// <summary>
        /// Select an item in a listbox or combobox
        /// </summary>
        /// <param name="index">1 based index of the item to select</param>
        public void Select(int index) => GetMember<MQ2DataType>("Select", index.ToString());

        /// <summary>
        /// TODO: new method
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Move(int left, int top, int width, int height) => GetMember<MQ2DataType>("Move", $"{left},{top},{width},{height}");

        /// <summary>
        /// TODO: new method
        /// </summary>
        /// <param name="color"></param>
        public void SetBGColor(Color color) => GetMember<MQ2DataType>("SetBGColor", color.ToArgb().ToString());

        /// <summary>
        /// TODO: new method
        /// </summary>
        /// <param name="rgb"></param>
        public void SetBGColor(int rgb) => GetMember<MQ2DataType>("SetBGColor", rgb.ToString());

        /// <summary>
        /// TODO: new method
        /// </summary>
        /// <param name="alpha"></param>
        public void SetAlpha(int alpha) => GetMember<MQ2DataType>("SetAlpha", alpha.ToString());

        /// <summary>
        /// TODO: new method
        /// </summary>
        /// <param name="alpha"></param>
        public void SetFadeAlpha(int alpha) => GetMember<MQ2DataType>("SetFadeAlpha", alpha.ToString());

        /// <summary>
        /// If the window is a TabBox, set the current tab by index (base 1).
        /// </summary>
        /// <param name="index">The base 1 index.</param>
        public void SetCurrentTab(int index) => GetMember<MQ2DataType>("SetCurrentTab", index.ToString());

        /// <summary>
        /// If the window is a TabBox, set the current tab by name.
        /// </summary>
        /// <param name="tabText"></param>
        public void SetCurrentTab(string tabText) => GetMember<MQ2DataType>("SetCurrentTab", tabText);

        /// <summary>
        /// Returns TRUE if the window is open
        /// </summary>
        public bool Open => GetMember<BoolType>("Open");
        
        /// <summary>
        /// Get a child window by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public WindowType GetChild(string name) => Children ? GetMember<WindowType>("Child", name) : null;

        /// <summary>
        /// Parent window
        /// </summary>
        [JsonIgnore]
        public WindowType Parent => GetMember<WindowType>("Parent");
        
        /// <summary>
        /// First child window/control
        /// </summary>
        [JsonIgnore]
        public WindowType FirstChild => Children ? GetMember<WindowType>("FirstChild") : null;
        
        /// <summary>
        /// Next sibling window
        /// </summary>
        [JsonIgnore]
        public WindowType Next => GetMember<WindowType>("Next");

        /// <summary>
        /// Get all child windows.
        /// </summary>
        public IEnumerable<WindowType> GetChildren()
        {
            List<WindowType> items = new List<WindowType>();

            if (!Children)
            {
                return items;
            }

            var current = FirstChild;

            while (current.Siblings && items.Count <= MAX_CHILDREN)
            {
                items.Add(current);

                current = current.Next;
            }

            return items;
        }
        
        /// <summary>
        /// Vertical scrollbar range
        /// </summary>
        public uint? VScrollMax => GetMember<IntType>("VScrollMax");
        
        /// <summary>
        /// Vertical scrollbar position
        /// </summary>
        public uint? VScrollPos => GetMember<IntType>("VScrollPos");
        
        /// <summary>
        /// Vertical scrollbar position in % to range from 0 to 100
        /// </summary>
        public int? VScrollPct => GetMember<IntType>("VScrollPct");
        
        /// <summary>
        /// Horizontal scrollbar range
        /// </summary>
        public uint? HScrollMax => GetMember<IntType>("HScrollMax");
        
        /// <summary>
        /// Horizontal scrollbar position
        /// </summary>
        public uint? HScrollPos => GetMember<IntType>("HScrollPos");
        
        /// <summary>
        /// Horizontal scrollbar position in % to range from 0 to 100
        /// </summary>
        public uint? HScrollPct => GetMember<IntType>("HScrollPct");
        
        /// <summary>
        /// Returns TRUE if the window has children
        /// </summary>
        public bool Children => GetMember<BoolType>("Children");
        
        /// <summary>
        /// Returns TRUE if the window has siblings
        /// </summary>
        public bool Siblings => GetMember<BoolType>("Siblings");
        
        /// <summary>
        /// Returns TRUE if the window is minimized
        /// </summary>
        public bool Minimized => GetMember<BoolType>("Minimized");
        
        /// <summary>
        /// Returns TRUE if the mouse is currently over the window
        /// </summary>
        public bool MouseOver => GetMember<BoolType>("MouseOver");
        
        /// <summary>
        /// Screen X position
        /// </summary>
        public uint? X => GetMember<IntType>("X");
        
        /// <summary>
        /// Screen Y position
        /// </summary>
        public uint? Y => GetMember<IntType>("Y");

        /// <summary>
        /// TODO: new member.
        /// </summary>
        public string Size => GetMember<StringType>("Size");

        /// <summary>
        /// Width in pixels
        /// </summary>
        public uint? Width => GetMember<IntType>("Width");
        
        /// <summary>
        /// Height in pixels
        /// </summary>
        public uint? Height => GetMember<IntType>("Height");
        
        /// <summary>
        /// Background color
        /// </summary>
        public Color? BGColor => GetMember<ArgbType>("BGColor");
        
        /// <summary>
        /// Window's text.
        /// STMLBox: returns the contents of the STML.
        /// Page: returns the name of the page's Tab.
        /// </summary>
        public string Text => GetMember<StringType>("Text");
        
        /// <summary>
        /// TooltipReference text
        /// </summary>
        public string Tooltip => GetMember<StringType>("Tooltip");
        
        /// <summary>
        /// Returns TRUE if the button has been checked
        /// </summary>
        public bool Checked => GetMember<BoolType>("Checked");
        
        /// <summary>
        /// Returns TRUE if the window is highlighted
        /// </summary>
        public bool Highlighted => GetMember<BoolType>("Highlighted");
        
        /// <summary>
        /// Returns TRUE if the window is enabled
        /// </summary>
        public bool Enabled => GetMember<BoolType>("Enabled");
        
        /// <summary>
        /// Window style code
        /// </summary>
        public uint? Style => GetMember<IntType>("Style");

        /// <summary>
        /// Access to list box items
        /// </summary>
        public ListMember List { get; }

        /// <summary>
        /// Name of window piece, e.g. "ChatWindow" for top level windows, or the piece name for child windows. Note: this is Custom UI dependent
        /// </summary>
        public string Name => GetMember<StringType>("Name");
        
        /// <summary>
        /// ScreenID of window piece. Note: This is not Custom UI dependent, it must be the same on all UIs
        /// </summary>
        public string ScreenID => GetMember<StringType>("ScreenID");
        
        /// <summary>
        /// Type of window piece (Screen for top level windows, or Listbox, Button, Gauge, Label, Editbox, Slider, etc)
        /// </summary>
        public string Type => GetMember<StringType>("Type");
        
        /// <summary>
        /// Number of items in a Listbox or Combobox
        /// </summary>
        public uint? Items => GetMember<IntType>("Items");
        
        /// <summary>
        /// Has the other person clicked the Trade button?
        /// </summary>
        public bool HisTradeReady => GetMember<BoolType>("HisTradeReady");
        
        /// <summary>
        /// Have I clicked the Trade button?
        /// </summary>
        public bool MyTradeReady => GetMember<BoolType>("MyTradeReady");

        /// <summary>
        /// Index (base 1) of the currently selected/highlighted item in a list or treeview
        /// </summary>
        public int? GetCurSel => GetMember<IntType>("GetCurSel");

        /// <summary>
        /// TODO: new member
        /// </summary>
        public float? Value => GetMember<FloatType>("Value");

        /// <summary>
        /// TabBox: The number of tabs present in the TabBox.
        /// </summary>
        public uint? TabCount => GetMember<IntType>("TabCount");

        /// <summary>
        /// TabBox: Looks up the Page window that matches the provided index (base 1) or tab text.
        /// Tab [#/Name]
        /// </summary>
        private IndexedMember<WindowType, string, WindowType, int> _tab;

        /// <summary>
        /// TabBox: Looks up the Page window that matches the provided tab text.
        /// Tab [Name]
        /// </summary>
        /// <param name="tabText"></param>
        /// <returns></returns>
        public WindowType GetTab(string tabText) => _tab[tabText];

        /// <summary>
        /// TabBox: Looks up the Page window that matches the provided index (base 1).
        /// Tab [#]
        /// </summary>
        /// <param name="index">The 1 based index.</param>
        /// <returns></returns>
        public WindowType GetTab(int index) => _tab[index];

        /// <summary>
        /// TabBox: All tabs.
        /// </summary>
        public IEnumerable<WindowType> Tabs
        {
            get
            {
                var index = 1;
                var count = TabCount;

                while (count.HasValue && index <= count)
                {
                    var item = GetTab(index);

                    if (item != null)
                    {
                        index++;

                        yield return item;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// TabBox: Returns the Page window associated with the currently selected tab.
        /// </summary>
        public WindowType CurrentTab => GetMember<WindowType>("CurrentTab");

        /// <summary>
        /// TabBox: Returns the index of the currently selected tab.
        /// </summary>
        public int? CurrentTabIndex => GetMember<IntType>("CurrentTabIndex");

        /// <summary>
        /// TRUE if window exists, FALSE if not
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Provides custom index access for list box items
        /// </summary>
        public class ListMember
        {
            private readonly WindowType _owner;

            internal ListMember(WindowType owner)
            {
                _owner = owner;
            }

            /// <summary>
            /// Get the text for the Nth item in a list box. Only works on list boxes. Use of y is optional and allows selection of the column of the window to get text from.
            /// Text of an item at a given location in the list, row and column are 1 based indexes
            /// </summary>
            /// <param name="row"></param>
            /// <param name="column"></param>
            /// <returns></returns>
            public string this[int row, int column = 0] => _owner.GetMember<StringType>("List", $"{row},{column}");

            /// <summary>
            /// Find an item in a list box by partial match (use window.List[=text] for exact). Only works on list boxes. Use of y is optional and allows selection of the column of the window to search in.
            /// Returns the 1 based index of an item in the list with a specified text
            /// </summary>
            /// <param name="text"></param>
            /// <param name="column">The column (1 based) in which to search</param>
            /// <param name="exactMatch">If true, match exact text only, otherwise partial</param>
            /// <returns></returns>
            public int? this[string text, int column = 0, bool exactMatch = false] => _owner.GetMember<IntType>("List", $"{(exactMatch ? "=" : "")}{text},{column}");
        }
    }
}