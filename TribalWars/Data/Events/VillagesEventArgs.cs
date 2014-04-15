#region Using
using System;
using System.Collections.Generic;
using System.Text;

using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// EventArgs wrapper for multiple villages
    /// </summary>
    public class VillagesEventArgs : EventArgs
    {
        // TODO: add a bool _draw to determinate if MapPicture.Invalidate is necessary

        #region Fields
        private IEnumerable<Village> _villages;
        private VillageCommand _action;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the villages
        /// </summary>
        public IEnumerable<Village> Villages
        {
            get { return _villages; }
        }

        /// <summary>
        /// Gets the action requesting the event
        /// </summary>
        public VillageCommand Action
        {
            get { return _action; }
        }

        /// <summary>
        /// Gets the tool requesting the event
        /// </summary>
        public VillageTools Tool
        {
            get { return _action.Tool; }
        }

        /// <summary>
        /// Gets the first village in the list
        /// </summary>
        public virtual Village FirstVillage
        {
            get
            {
                if (_villages != null)
                    foreach (Village vil in _villages)
                        return vil;

                return null;
            }
        }
        #endregion

        #region Constructors
        public VillagesEventArgs(IEnumerable<Village> vil, VillageTools tool)
        {
            _villages = vil;
            _action = new VillageCommand(tool);
        }

        public VillagesEventArgs(IEnumerable<Village> vil, VillageCommand action)
        {
            _villages = vil;
            _action = action;
        }
        #endregion
    }
}
