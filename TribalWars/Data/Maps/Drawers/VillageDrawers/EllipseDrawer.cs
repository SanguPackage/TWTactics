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
    /// Draws village ellipses
    /// </summary>
    public class EllipseDrawer : DrawerBase
    {
        #region Fields
        private Brush _brush;
        private Brush _extraColorBrush;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the brush the ellipse gets drawn with
        /// </summary>
        public Brush Brush
        {
            get { return _brush; }
        }

        /// <summary>
        /// Gets the brush a smaller circle inside
        /// the main circle gets drawn with
        /// </summary>
        public Brush ExtraColorBrush
        {
            get { return _extraColorBrush; }
        }
        #endregion

        #region Constructors
        public EllipseDrawer(MarkerGroup colors)
        {
            _brush = new SolidBrush(colors.Color);
            if (colors.ExtraColor != Color.Transparent)
            {
                _extraColorBrush = new SolidBrush(colors.ExtraColor);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Draws one village to the map
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            if (width > 5)
            {
                g.FillEllipse(_brush, x, y, width, height);
                if (_extraColorBrush != null)
                {
                    float rec = width - 6;
                    float spacing = 3; // width / 4f;
                    if (rec < 3)
                    {
                        rec = 3;
                        spacing = 1;
                    }
                    g.FillEllipse(_extraColorBrush, x + spacing, y + spacing, rec, rec);
                }
            }
            else
            {
                if (_extraColorBrush != null && width > 3) g.FillRectangle(_extraColorBrush, x, y, width, height);
                else g.FillRectangle(_brush, x, y, width, height);
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public override void Dispose(bool disposing)
        {
            _brush.Dispose();
            if (_extraColorBrush != null)
                _extraColorBrush.Dispose();
        }
        #endregion
    }
}
