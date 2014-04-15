#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Tribes;
#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// EventArgs for a tribe
    /// </summary>
    public class TribeEventArgs : VillagesEventArgs
    {
        #region Fields
        private Tribe _selectedTribe;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tribe
        /// </summary>
        public Tribe SelectedTribe
        {
            get { return _selectedTribe; }
        }
        #endregion

        #region Constructors
        public TribeEventArgs(Tribe tribe, VillageTools tool)
            : base(tribe, tool)
        {
            _selectedTribe = tribe;
        }
        #endregion
    }
}
