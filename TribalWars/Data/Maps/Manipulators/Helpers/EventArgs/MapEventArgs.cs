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
    public class MapEventArgs
    {
        #region Fields
        private ManipulatorManagerBase _parent;
        private Graphics _g;
        private Rectangle _rec;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the parent manipulator manager
        /// </summary>
        public ManipulatorManagerBase Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return _g; }
        }

        /// <summary>
        /// Gets the full map rectangle
        /// </summary>
        public Rectangle FullMapRectangle
        {
            get { return _rec; }
        }
        #endregion

        #region Constructors
        public MapEventArgs(ManipulatorManagerBase p, Graphics g, Rectangle rec)
        {
            _parent = p;
            _g = g;
            _rec = rec;
        }
        #endregion
    }
}
