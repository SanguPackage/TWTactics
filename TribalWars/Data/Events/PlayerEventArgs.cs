using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Players;

namespace TribalWars.Data.Events
{
    /// <summary>
    /// EventArgs for a player
    /// </summary>
    public class PlayerEventArgs : VillagesEventArgs
    {
        #region Fields
        private Player _selectedPlayer;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the player
        /// </summary>
        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
        }
        #endregion

        #region Constructors
        public PlayerEventArgs(Player ply, VillageTools tool)
            : base(ply, tool)
        {
            _selectedPlayer = ply;
        }
        #endregion
    }
}
