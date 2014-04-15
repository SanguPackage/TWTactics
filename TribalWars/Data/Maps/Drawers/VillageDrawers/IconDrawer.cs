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
    /// Draws a village bitmap with 
    /// </summary>
    public class IconDrawer : DrawerBase
    {
        #region Fields
        private Brush _colorBrush;
        private Bitmap _bitmap;
        private Brush _extraColorBrush;
        //private Pen _extraPen;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the village bitmap
        /// </summary>
        public Bitmap Bitmap
        {
            get { return _bitmap; }
        }

        /// <summary>
        /// Gets the brush the first circle is drawn with
        /// </summary>
        public Brush ColorBrush
        {
            get { return _colorBrush; }
        }

        /// <summary>
        /// Gets the brush the second circle is drawn with
        /// </summary>
        public Brush ExtraColorBrush
        {
            get { return _extraColorBrush; }
        }
        #endregion

        #region Constructors
        public IconDrawer(Bitmap icon, MarkerGroup colors)
        {
            _colorBrush = new SolidBrush(colors.Color);
            if (colors.ExtraColor != Color.Transparent)
            {
                _extraColorBrush = new SolidBrush(colors.ExtraColor);
                //_extraPen = new Pen(colors.ExtraColor, 2);
            }

            _bitmap = icon;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Draws one village to the map
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            g.DrawImageUnscaledAndClipped(_bitmap, new Rectangle(x, y, width, height));
            g.FillEllipse(_colorBrush, x + 0.5f, y + 0.5f, 7, 7);

            if (_extraColorBrush != null)
            {
                // cirkel rond de dorpen
                //g.DrawEllipse(_extraPen, x + 5, y + 5, 53 - 10, 38 - 10);

                // vierkantje in het midden
                //g.FillRectangle(_extraColorBrush, x + 20, y + 11, 12, 12);

                // cirkel naast eerste cirkel
                g.FillEllipse(_extraColorBrush, x + 10f, y + 0.5f, 7, 7);
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public override void Dispose(bool disposing)
        {
            _colorBrush.Dispose();
            if (_extraColorBrush != null)
                _extraColorBrush.Dispose();
            _bitmap.Dispose();
        }
        #endregion
    }
}
