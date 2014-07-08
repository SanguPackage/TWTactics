#region Using
using System;
using System.Collections.Generic;
using TribalWars.Villages;

#endregion

namespace TribalWars.Worlds.Events.Impls
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
