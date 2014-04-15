#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Controls;
#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// Provides village data for the polygon event
    /// </summary>
    public class PolygonEventArgs : EventArgs
    {
        #region Fields
        private PolygonDataSet _ds;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the villages DataSet
        /// </summary>
        public PolygonDataSet Villages
        {
            get { return _ds; }
        }
        #endregion

        #region Constructors
        public PolygonEventArgs(PolygonDataSet ds)
        {
            _ds = ds;
        }
        #endregion
    }
}
