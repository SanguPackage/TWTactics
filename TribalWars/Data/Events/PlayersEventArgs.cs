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
    public sealed class PlayersEventArgs : EventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the players
        /// </summary>
        public IEnumerable<Player> Players { get; private set; }

        /// <summary>
        /// Gets the tool requesting the event
        /// </summary>
        public VillageTools Tool { get; private set; }

        /// <summary>
        /// Gets the first player in the list
        /// </summary>
        public Player FirstPlayer
        {
            get
            {
                if (Players != null)
                    foreach (Player ply in Players)
                        return ply;

                return null;
            }
        }
        #endregion

        #region Constructors
        public PlayersEventArgs(IEnumerable<Player> ply, VillageTools tool)
        {
            Players = ply;
            Tool = tool;
        }
        #endregion
    }
}
