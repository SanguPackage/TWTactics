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
    public class MapPaintEventArgs
    {
        #region Fields
        private Graphics _g;
        private Rectangle _fullRec;
        private Rectangle _rec;
        private bool _isActive;
        #endregion

        #region Properties
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
            get { return _fullRec; }
        }

        /// <summary>
        /// Gets the invalidated rectangle
        /// </summary>
        public Rectangle MapRectangle
        {
            get { return _rec; }
        }

        /// <summary>
        /// Gets a value indicating whether it is the active
        /// manipulator
        /// </summary>
        public bool IsActiveManipulator
        {
            get { return _isActive; }
        }
        #endregion

        #region Constructors
        public MapPaintEventArgs(Graphics g, Rectangle rec, Rectangle fullRec, bool isActive)
        {
            _g = g;
            _fullRec = fullRec;
            _rec = rec;
            _isActive = isActive;
        }
        #endregion
    }
}
