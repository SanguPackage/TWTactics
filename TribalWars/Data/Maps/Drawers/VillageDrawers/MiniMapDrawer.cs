#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TribalWars.Data.Maps.Markers;
#endregion

namespace TribalWars.Data.Maps.Drawers
{
    /// <summary>
    /// Draws village rectangles
    /// </summary>
    public class MiniMapDrawer : DrawerBase
    {
        #region Fields
        private Brush _colorBrush;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the brush the rectangle is drawn with
        /// </summary>
        public Brush Brush
        {
            get { return _colorBrush; }
        }
        #endregion

        #region Constructors
        public MiniMapDrawer(Color color)
        {
            _colorBrush = new SolidBrush(color);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints one village to the map
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            g.FillRectangle(_colorBrush, x, y, width, height);
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public override void Dispose(bool disposing)
        {
            _colorBrush.Dispose();
        }
        #endregion
    }
}
