#region Using
using System;
using System.Collections.Generic;
using TribalWars.Maps.Manipulators.Polygons;

#endregion

namespace TribalWars.Worlds.Events.Impls
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
