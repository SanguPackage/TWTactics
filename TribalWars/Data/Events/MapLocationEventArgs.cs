#region Using
using System;
using TribalWars.Data.Maps;
using TribalWars.Data.Maps.Displays;
#endregion

namespace TribalWars.Data.Events
{
    public class MapLocationEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the new map location
        /// </summary>
        public Location NewLocation { get; private set; }

        /// <summary>
        /// Gets the old map location
        /// </summary>
        public Location OldLocation { get; private set; }

        /// <summary>
        /// Gets data for the zoom level of the
        /// new location
        /// </summary>
        public DisplayBase.ZoomInfo ZoomInfo { get; private set; }

        #endregion

        #region Constructors
        public MapLocationEventArgs(Location newLocation, Location oldLocation, DisplayBase.ZoomInfo zoom)
        {
            NewLocation = newLocation;
            OldLocation = oldLocation;
            ZoomInfo = zoom;
        }
        #endregion
    }
}
