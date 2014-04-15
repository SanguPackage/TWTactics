#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Maps.Displays;
using TribalWars.Data.Maps;
#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// Provides data for a change of DisplayType
    /// </summary>
    public class MapDisplayTypeEventArgs : EventArgs
    {
        #region Fields
        private DisplayTypes _display;
        private Location _loc;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the new map display type
        /// </summary>
        public DisplayTypes DisplayType
        {
            get { return _display; }
        }

        /// <summary>
        /// Gets the location on the displaytype
        /// </summary>
        public Location Location
        {
            get { return _loc; }
        }
        #endregion

        #region Constructors
        public MapDisplayTypeEventArgs(DisplayTypes display, Location loc)
        {
            _display = display;
            _loc = loc;
        }
        #endregion
    }
}
