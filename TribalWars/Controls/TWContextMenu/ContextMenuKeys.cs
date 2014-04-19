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
            public const string Visit = "VISIT";
            public const string Player = "PLAYER";
            public const string Tribe = "TRIBE";
            public const string Mark = "MARK";
            public const string ClipboardText = "CLIPBOARDTEXT";
            public const string Pinpoint = "PINPOINT";
            public const string Playerseperator = "PLAYERSEP";
        }

        public class Player
        {
            public const string Bbcode = "PLAYERBBCODE";
        }

        public class Polygon
        {
            public const string Generate = "GENERATE";
            public const string Delete = "DELETE";
            public const string Edit = "EDIT";
            public const string Hide = "HIDE";
            public const string Showall = "SHOWALL";
            public const string Show = "SHOW";
            public const string Clearall = "CLEARALL";
            public const string Hideall = "HIDEALL";
            public const string Groups = "GROUPS";
        }
        #endregion

        #region Constructors
        private ContextMenuKeys()
        {

        }
        #endregion
    }
}
