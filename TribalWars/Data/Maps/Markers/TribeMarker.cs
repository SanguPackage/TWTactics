#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Villages;
using System.Xml;
using TribalWars.Data.Tribes;
#endregion

namespace TribalWars.Data.Maps.Markers
{
    /// <summary>
    /// Defines a TW Tribe to be marked on a map
    /// </summary>
    public sealed class TribeMarker : MarkerBase
    {
        #region Properties
        /// <summary>
        /// Gets the tribe to mark
        /// </summary>
        public Tribe Tribe { get; private set ; }
        #endregion

        #region Constructors
        public TribeMarker(Tribe tribe)
        {
            Tribe = tribe;
        }
        #endregion

        #region BBCode
        public override string ToString()
        {
            if (Tribe == null) return base.ToString();
            return Tribe.ToString();
        }
        #endregion

        #region IEnumerable<Village> Members
        public override IEnumerator<Village> GetEnumerator()
        {
            return Tribe.GetEnumerator();
        }
        #endregion
    }
}
