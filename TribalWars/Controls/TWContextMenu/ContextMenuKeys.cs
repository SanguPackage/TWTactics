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
    public static class ContextMenuKeys
    {
        #region Constants
        public static class VillageKeys
        {
            public const string Visit = "VISIT";
            public const string Player = "PLAYER";
            public const string Tribe = "TRIBE";
            public const string Mark = "MARK";
            public const string ClipboardText = "CLIPBOARDTEXT";
            public const string Pinpoint = "PINPOINT";
            public const string Playerseperator = "PLAYERSEP";
        }

        public static class Polygon
        {
            public const string Generate = "GENERATE";
            public const string Delete = "DELETE";
            public const string Edit = "EDIT";
            public const string Hide = "HIDE";
            public const string Showall = "SHOWALL";
            public const string Show = "SHOW";
            public const string Clearall = "CLEARALL";
            public const string Hideall = "HIDEALL";
        }
        #endregion
    }
}
