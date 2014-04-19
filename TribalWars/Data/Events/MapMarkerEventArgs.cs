#region Using
using System;
using System.Collections.Generic;
using System.Text;

using TribalWars.Data.Maps;
using TribalWars.Data.Maps.Markers;
#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// Encapsulates a change in the markers for a map
    /// </summary>
    public class MapMarkerEventArgs : EventArgs
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the marker
        /// has been added or removed
        /// </summary>
        public bool IsAdded { get; private set; }

        /// <summary>
        /// Gets the marker that has changed
        /// </summary>
        public MarkerBase Marker { get; private set; }
        #endregion

        #region Constructors
        public MapMarkerEventArgs(MarkerBase marker, bool isAdded)
        {
            IsAdded = isAdded;
            Marker = marker;
        }
        #endregion

    }
}
