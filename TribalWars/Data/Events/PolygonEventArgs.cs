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
        #region Properties
        /// <summary>
        /// Gets the villages DataSet
        /// </summary>
        public PolygonDataSet Villages { get; private set; }
        #endregion

        #region Constructors
        public PolygonEventArgs(PolygonDataSet ds)
        {
            Villages = ds;
        }
        #endregion
    }
}
