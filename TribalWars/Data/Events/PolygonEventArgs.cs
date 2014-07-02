#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Controls;
using TribalWars.Data.Maps.Manipulators.Helpers;

#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// Provides village data for the polygon event
    /// </summary>
    public class PolygonEventArgs : EventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the polygons
        /// </summary>
        public IEnumerable<Polygon> Polygons { get; private set; }
        #endregion

        #region Constructors
        public PolygonEventArgs(IEnumerable<Polygon> polys)
        {
            Polygons = polys;
        }
        #endregion
    }
}
