#region Using
using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;

using TribalWars.Data.Villages;
using TribalWars.Data.Maps;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
#endregion

namespace TribalWars.Data.Maps.Markers
{
    /// <summary>
    /// The base class for map markers
    /// </summary>
    public abstract class MarkerBase : IEnumerable<Village>
    {
        #region Fields
        protected MarkerGroup _parent;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the MarkerGroup the Marker belongs to
        /// </summary>
        public MarkerGroup Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
        #endregion

        #region Constructors
        public MarkerBase()
        {
            
        }
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
