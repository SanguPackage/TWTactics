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
        #region Fields
        private Tribe _tribe;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the tribe to mark
        /// </summary>
        public Tribe Tribe
        {
            get { return _tribe; }
            set
            {
                _tribe = value;
            }
        }
        #endregion

        #region Constructors
        public TribeMarker()
        {

        }

        public TribeMarker(Tribe tribe)
        {
            Tribe = tribe;
        }
        #endregion

        #region BBCode
        public override string ToString()
        {
            if (_tribe == null) return base.ToString();
            return _tribe.ToString();
        }
        #endregion

        #region IEnumerable<Village> Members
        public override IEnumerator<Village> GetEnumerator()
        {
            return _tribe.GetEnumerator();
        }
        #endregion
    }
}
