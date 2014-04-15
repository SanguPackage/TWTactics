#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TribalWars.Controls.TWContextMenu
{
    /// <summary>
    /// Provides the keys of the Commands
    /// </summary>
    public class ContextMenuKeys
    {
        #region Constants
        public class Village
        {
            public const string VISIT = "VISIT";
            public const string PLAYER = "PLAYER";
            public const string TRIBE = "TRIBE";
            public const string MARK = "MARK";
            public const string CLIPBOARD_TEXT = "CLIPBOARDTEXT";
            public const string PINPOINT = "PINPOINT";
            public const string PLAYERSEPERATOR = "PLAYERSEP";
            //public const string  = "";
            //public const string  = "";
            //public const string  = "";
        }

        public class Player
        {
            public const string BBCODE = "PLAYERBBCODE";
            //public const string  = "";
            //public const string  = "";
            //public const string  = "";
        }

        public class Polygon
        {
            public const string GENERATE = "GENERATE";
            public const string DELETE = "DELETE";
            public const string EDIT = "EDIT";
            public const string HIDE = "HIDE";
            public const string SHOWALL = "SHOWALL";
            public const string SHOW = "SHOW";
            public const string CLEARALL = "CLEARALL";
            public const string HIDEALL = "HIDEALL";
            public const string GROUPS = "GROUPS"; // --> Add a submenu item for each polygon group to toggle visibility
        }
        #endregion

        #region Constructors
        private ContextMenuKeys()
        {

        }
        #endregion
    }
}
