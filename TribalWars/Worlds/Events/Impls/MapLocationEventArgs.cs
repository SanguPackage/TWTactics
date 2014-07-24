#region Using
using System;
using TribalWars.Maps;
using TribalWars.Maps.Displays;

#endregion

namespace TribalWars.Worlds.Events.Impls
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
        public DrawerFactoryBase.ZoomInfo ZoomInfo { get; private set; }

        /// <summary>
        /// Returns true if we switched Icon/Shape Display
        /// </summary>
        public bool IsDisplayChange
        {
            get
            {
                if (OldLocation == null) return true;
                return NewLocation.Display != OldLocation.Display;
            }
        }
        #endregion

        #region Constructors
        public MapLocationEventArgs(Location newLocation, Location oldLocation, DrawerFactoryBase.ZoomInfo zoom)
        {
            NewLocation = newLocation;
            OldLocation = oldLocation;
            ZoomInfo = zoom;
        }
        #endregion
    }
}
