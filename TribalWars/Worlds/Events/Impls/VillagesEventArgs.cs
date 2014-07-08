#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using TribalWars.Villages;

#endregion

namespace TribalWars.Worlds.Events.Impls
{
    /// <summary>
    /// EventArgs wrapper for multiple villages
    /// </summary>
    public class VillagesEventArgs : EventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the villages
        /// </summary>
        public IEnumerable<Village> Villages { get; private set; }

        /// <summary>
        /// Gets the action requesting the event
        /// </summary>
        public VillageCommand Action { get; private set; }

        /// <summary>
        /// Gets the tool requesting the event
        /// </summary>
        public VillageTools Tool
        {
            get { return Action.Tool; }
        }

        /// <summary>
        /// Gets the first village in the list
        /// </summary>
        public virtual Village FirstVillage
        {
            get
            {
                if (Villages != null)
                {
                    return Villages.FirstOrDefault();
                }
                return null;
            }
        }
        #endregion

        #region Constructors
        public VillagesEventArgs(IEnumerable<Village> vil, VillageTools tool)
        {
            Villages = vil;
            Action = new VillageCommand(tool);
        }

        public VillagesEventArgs(IEnumerable<Village> vil, VillageCommand action)
        {
            Villages = vil;
            Action = action;
        }
        #endregion
    }
}
