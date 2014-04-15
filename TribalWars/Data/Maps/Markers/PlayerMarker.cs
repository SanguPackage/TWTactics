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
    public class PlayerMarker : MarkerBase
    {
        #region Fields
        private Player _player;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the player to mark
        /// </summary>
        public Player Player
        {
            get { return _player; }
            set
            {
                _player = value;
            }
        }
        #endregion

        #region Constructors
        public PlayerMarker()
        {

        }

        public PlayerMarker(Player ply)
        {
            Player = ply;
        }
        #endregion

        #region BBCode
        public override string ToString()
        {
            if (_player == null) return base.ToString();
            return _player.ToString();
        }
        #endregion

        #region IEnumerable<Village> Members
        public override IEnumerator<Village> GetEnumerator()
        {
            return _player.GetEnumerator();
        }
        #endregion
    }
}
