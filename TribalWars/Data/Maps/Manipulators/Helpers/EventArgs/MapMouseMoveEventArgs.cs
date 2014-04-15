#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Data.Maps.Manipulators.Helpers
{
    public class MapMouseMoveEventArgs : MapMouseEventArgs
    {
        #region Fields
        private Point _mapLocation;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the map location
        /// </summary>
        public Point Location
        {
            get { return _mapLocation; }
        }
        #endregion

        #region Constructors
        public MapMouseMoveEventArgs(ManipulatorManagerBase p, Graphics g, MouseEventArgs e, Point loc, Village vil, Rectangle rec)
            : base(p, g, e, vil, rec)
        {
            _mapLocation = loc;
        }
        #endregion
    }
}
