#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Players;
using System.Xml;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Data.Maps.Markers
{
    /// <summary>
    /// Defines a TW Player to be marked on a map
    /// </summary>
    public sealed class PlayerMarker : MarkerBase
    {
        #region Properties
        /// <summary>
        /// Gets or sets the player to mark
        /// </summary>
        public Player Player { get; private set; }
        #endregion

        #region Constructors
        public PlayerMarker(Player ply)
        {
            Player = ply;
        }
        #endregion

        #region BBCode
        public override string ToString()
        {
            if (Player == null) return base.ToString();
            return Player.ToString();
        }
        #endregion

        #region IEnumerable<Village> Members
        public override IEnumerator<Village> GetEnumerator()
        {
            return Player.GetEnumerator();
        }
        #endregion
    }
}
