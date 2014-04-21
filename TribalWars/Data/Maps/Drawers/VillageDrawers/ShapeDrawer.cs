#region Using
using System;
using System.Diagnostics;
using System.Drawing;
using TribalWars.Data.Maps.Displays;
using TribalWars.Data.Maps.Markers;
#endregion

namespace TribalWars.Data.Maps.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws village ellipses with optional extra marking
    /// </summary>
    public class ShapeDrawer : DrawerBase
    {
        #region Fields
        /// <summary>
        /// Once the width of a village is above this number, no longer display inner shapes nor borders
        /// </summary>
        public const int InnerShapeTreshold = 5;

        /// <summary>
        /// Below this village width, always display a rectangle
        /// (ellipses start to look funny with 3px width)
        /// </summary>
        private const int ForceRectangleTreshold = 4;

        private readonly Brush _colorBrush;
        private readonly Brush _extraColorBrush;
        private readonly Action<Graphics, Brush, float, float, float, float> _filler;

        private static readonly Action<Graphics, Brush, float, float, float, float> EllipseFiller = (g, brush, x, y, width, height) => g.FillEllipse(brush, x, y, width, height);
        private static readonly Action<Graphics, Brush, float, float, float, float> RectangleFiller = (g, brush, x, y, width, height) => g.FillRectangle(brush, x, y, width, height);
        #endregion

        #region Constructors
        public ShapeDrawer(bool isEllipse, MarkerGroup colors)
        {
            _filler = isEllipse ? EllipseFiller : RectangleFiller;

            _colorBrush = new SolidBrush(colors.Color);
            if (colors.ExtraColor != Color.Transparent)
            {
                _extraColorBrush = new SolidBrush(colors.ExtraColor);
            }
        }
        #endregion

        #region InnerShape
        /// <summary>
        /// Dimensions of an inner shape (rectangle or ellipse)
        /// </summary>
        private class InnerShape
        {
            /// <summary>
            /// Offset on all sides from the village shape
            /// </summary>
            public int Padding { get; private set; }

            /// <summary>
            /// The width/height of the shape
            /// </summary>
            public int Width { get; private set; }

            public InnerShape(int padding, int width)
            {
                Padding = padding;
                Width = width;
            }
        }

        /// <summary>
        /// Gets the ShapeDrawers special marker boundry
        /// </summary>
        private static InnerShape GetInnerShape(int width)
        {
            Debug.Assert(width > InnerShapeTreshold);
            int innerWidth = width - 6;
            return innerWidth < 3 ? new InnerShape(1, 3) : new InnerShape(3, innerWidth);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draws one village to the map
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            if (width > InnerShapeTreshold && _extraColorBrush != null)
            {
                _filler(g, _colorBrush, x, y, width, height);

                InnerShape innerShape = GetInnerShape(width);
                _filler(g, _extraColorBrush, x + innerShape.Padding, y + innerShape.Padding, innerShape.Width, innerShape.Width);
            }
            else
            {
                if (width < ForceRectangleTreshold) g.FillRectangle(_extraColorBrush ?? _colorBrush, x, y, width, height);
                _filler(g, _extraColorBrush ?? _colorBrush, x, y, width, height);
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
        }
        #endregion
    }
}
