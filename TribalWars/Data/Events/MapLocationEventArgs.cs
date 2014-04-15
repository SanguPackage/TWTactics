#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Maps;
using TribalWars.Data.Maps.Displays;
#endregion

namespace TribalWars.Data.Events
{
    public class MapLocationEventArgs : EventArgs
    {
        #region Fields
        private Location _NewLocation;
        private DisplayBase.ZoomInfo _zoom;
        private Location _oldLocation;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the new map location
        /// </summary>
        public Location NewLocation
        {
            get { return _NewLocation; }
        }

        /// <summary>
        /// Gets the old map location
        /// </summary>
        public Location OldLocation
        {
            get { return _oldLocation; }
        }

        /// <summary>
        /// Gets data for the zoom level of the
        /// new location
        /// </summary>
        public DisplayBase.ZoomInfo ZoomInfo
        {
            get { return _zoom; }
        }
        #endregion

        #region Constructors
        public MapLocationEventArgs(Location newLocation, Location oldLocation, DisplayBase.ZoomInfo zoom)
        {
            _NewLocation = newLocation;
            _oldLocation = oldLocation;
            _zoom = zoom;
        }
        #endregion
    }
}
