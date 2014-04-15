#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Players;
#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// EventArgs wrapper for multiple players
    /// </summary>
    public class PlayersEventArgs : EventArgs
    {
        #region Fields
        private IEnumerable<Player> _players;
        private VillageTools _tool;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the players
        /// </summary>
        public IEnumerable<Player> Players
        {
            get { return _players; }
        }

        /// <summary>
        /// Gets the tool requesting the event
        /// </summary>
        public VillageTools Tool
        {
            get { return _tool; }
        }

        /// <summary>
        /// Gets the first player in the list
        /// </summary>
        public virtual Player FirstPlayer
        {
            get
            {
                if (_players != null)
                    foreach (Player ply in _players)
                        return ply;

                return null;
            }
        }
        #endregion

        #region Constructors
        public PlayersEventArgs(IEnumerable<Player> ply, VillageTools tool)
        {
            _players = ply;
            _tool = tool;
        }
        #endregion
    }
}
