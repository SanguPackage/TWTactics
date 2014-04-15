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
        #region Fields
        private IEnumerable<Tribe> _tribes;
        private VillageTools _tool;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tribes
        /// </summary>
        public IEnumerable<Tribe> Tribes
        {
            get { return _tribes; }
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
        public virtual Tribe FirstTribe
        {
            get
            {
                if (_tribes != null)
                    foreach (Tribe tribe in _tribes)
                        return tribe;

                return null;
            }
        }
        #endregion

        #region Constructors
        public TribesEventArgs(IEnumerable<Tribe> tribe, VillageTools tool)
        {
            _tribes = tribe;
            _tool = tool;
        }
        #endregion
    }
}
