#region Using
using System;
using System.Diagnostics;
using System.Drawing;
using TribalWars.Maps.Markers;

#endregion

namespace TribalWars.Maps.Drawing.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws village ellipses with optional extra marking
    /// </summary>
    public sealed class ShapeDrawer : DrawerBase
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
        private readonly Action<Graphics, Brush, Rectangle> _filler;

        private static readonly Action<Graphics, Brush, Rectangle> EllipseFiller = (g, brush, village) => g.FillEllipse(brush, village);
        private static readonly Action<Graphics, Brush, Rectangle> RectangleFiller = (g, brush, village) => g.FillRectangle(brush, village);
        #endregion

        #region Constructors
        public ShapeDrawer(bool isEllipse, Marker colors)
        {
            _filler = isEllipse ? EllipseFiller : RectangleFiller;

            _colorBrush = new SolidBrush(colors.Settings.Color);
            if (colors.Settings.ExtraColor != Color.Transparent)
            {
                _extraColorBrush = new SolidBrush(colors.Settings.ExtraColor);
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
        protected override void PaintVillageCore(Graphics g, Rectangle village)
        {
            if (village.Width > InnerShapeTreshold && _extraColorBrush != null)
            {
                _filler(g, _colorBrush, village);

                InnerShape innerShape = GetInnerShape(village.Width);
                var innerRectangle = new Rectangle(village.X + innerShape.Padding, village.Y + innerShape.Padding, innerShape.Width, innerShape.Width);
                _filler(g, _extraColorBrush, innerRectangle);
            }
            else
            {
                if (village.Width < ForceRectangleTreshold) g.FillRectangle(_extraColorBrush ?? _colorBrush, village);
                _filler(g, _extraColorBrush ?? _colorBrush, village);
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
