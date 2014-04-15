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
    public class MapVillageEventArgs : MapEventArgs
    {
        #region Fields
        private MouseEventArgs _mouse;
        private Village _village;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the mouse event arguments
        /// </summary>
        public MouseEventArgs MouseEventArgs
        {
            get { return _mouse; }
        }

        /// <summary>
        /// Gets the village
        /// </summary>
        public Village Village
        {
            get { return _village; }
        }
        #endregion

        #region Constructors
        public MapVillageEventArgs(ManipulatorManagerBase p, Graphics g, MouseEventArgs e, Village vil, Rectangle rec)
            : base(p, g, rec)
        {
            _mouse = e;
            _village = vil;
        }
        #endregion
    }
}
