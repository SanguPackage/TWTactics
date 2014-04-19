#region Using
using System.Collections.Generic;
using TribalWars.Data.Villages;

#endregion

namespace TribalWars.Data.Maps.Markers
{
    /// <summary>
    /// The base class for map markers
    /// </summary>
    public abstract class MarkerBase : IEnumerable<Village>
    {
        #region Properties
        /// <summary>
        /// Gets or sets the MarkerGroup the Marker belongs to
        /// </summary>
        public MarkerGroup Parent { get; set; }
        #endregion

        #region IEnumerable<Village> Members
        public abstract IEnumerator<Village> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}