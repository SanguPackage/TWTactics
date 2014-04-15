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
        #region Fields
        private bool _added;
        private MarkerBase _marker;
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the marker
        /// has been added or removed
        /// </summary>
        public bool IsAdded
        {
            get { return _added; }
        }

        /// <summary>
        /// Gets the marker that has changed
        /// </summary>
        public MarkerBase Marker
        {
            get { return _marker; }
        }
        #endregion

        #region Constructors
        public MapMarkerEventArgs(MarkerBase marker, bool isAdded)
        {
            _added = isAdded;
            _marker = marker;
        }
        #endregion

    }
}
