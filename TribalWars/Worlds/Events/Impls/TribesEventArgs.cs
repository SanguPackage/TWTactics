#region Using
using System;
using System.Collections.Generic;
using TribalWars.Villages;

#endregion

namespace TribalWars.Worlds.Events.Impls
{
    /// <summary>
    /// EventArgs wrapper for multiple tribes
    /// </summary>
    public class TribesEventArgs : EventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the tribes
        /// </summary>
        public IEnumerable<Tribe> Tribes { get; private set; }

        /// <summary>
        /// Gets the tool requesting the event
        /// </summary>
        public VillageTools Tool { get; private set; }
        #endregion

        #region Constructors
        public TribesEventArgs(IEnumerable<Tribe> tribe, VillageTools tool)
        {
            Tribes = tribe;
            Tool = tool;
        }
        #endregion
    }
}
