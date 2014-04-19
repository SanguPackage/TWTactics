#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Villages;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// EventArgs for one village
    /// </summary>
    public class VillageEventArgs : VillagesEventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the village
        /// </summary>
        public Village SelectedVillage { get; private set; }

        /// <summary>
        /// Gets the village
        /// </summary>
        public override Village FirstVillage
        {
            get
            {
                return SelectedVillage;
            }
        }
        #endregion

        #region Constructors
        public VillageEventArgs(Village vil, VillageTools tool)
            : base(vil, tool)
        {
            SelectedVillage = vil;
        }
        #endregion
    }
}
