#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Villages;

#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// EventArgs for a tribe
    /// </summary>
    public class TribeEventArgs : VillagesEventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the tribe
        /// </summary>
        public Tribe SelectedTribe { get; private set; }
        #endregion

        #region Constructors
        public TribeEventArgs(Tribe tribe, VillageTools tool)
            : base(tribe, tool)
        {
            SelectedTribe = tribe;
        }
        #endregion
    }
}
