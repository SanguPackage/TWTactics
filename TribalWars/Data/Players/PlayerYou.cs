#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Globalization;
#endregion

namespace TribalWars.Data.Players
{
    /// <summary>
    /// Representation of your player in the world
    /// </summary>
    public class PlayerYou
    {
        #region Properties
        /// <summary>
        /// Gets or sets the active player
        /// </summary>
        public Player Player { get; set; }
        #endregion

        #region Constructors
        private static readonly PlayerYou _instance = new PlayerYou();

        private PlayerYou()
        {

        }

        /// <summary>
        /// Gets the currently active player
        /// </summary>
        public static PlayerYou Default
        {
            [DebuggerStepThrough]
            get { return _instance; }
        }
        #endregion
    }
}
