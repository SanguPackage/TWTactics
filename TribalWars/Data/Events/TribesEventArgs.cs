#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Tribes;
#endregion

namespace TribalWars.Data.Events
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

        /// <summary>
        /// Gets the first player in the list
        /// </summary>
        public virtual Tribe FirstTribe
        {
            get
            {
                if (Tribes != null)
                    foreach (Tribe tribe in Tribes)
                        return tribe;

                return null;
            }
        }
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
