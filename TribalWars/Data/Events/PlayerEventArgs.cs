using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Villages;

namespace TribalWars.Data.Events
{
    /// <summary>
    /// EventArgs for a player
    /// </summary>
    public class PlayerEventArgs : VillagesEventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the player
        /// </summary>
        public Player SelectedPlayer { get; private set; }
        #endregion

        #region Constructors
        public PlayerEventArgs(Player ply, VillageTools tool)
            : base(ply, tool)
        {
            SelectedPlayer = ply;
        }
        #endregion
    }
}
