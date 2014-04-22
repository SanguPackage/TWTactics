#region Using
using System;
using System.Diagnostics;
using System.Drawing;
using TribalWars.Data.Maps.Displays;

#endregion

namespace TribalWars.Data.Maps.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws a border around a ShapeDrawer
    /// </summary>
    public sealed class BorderDrawer : DrawerBase
    {
        #region Fields
        private static readonly Action<Graphics, Pen, int, int, int, int> EllipseDrawer = (g, pen, x, y, width, height) => g.DrawEllipse(pen, x, y, width, height);
        private static readonly Action<Graphics, Pen, int, int, int, int> RectangleDrawer = (g, pen, x, y, width, height) => g.DrawRectangle(pen, x, y, width, height);

        private readonly ShapeDisplay.Shapes _shape;
        private readonly Pen _pen1;
        private readonly Pen _pen3;
        #endregion

        #region Constructors
        public BorderDrawer(Color color, ShapeDisplay.Shapes shape)
        {
            _pen3 = new Pen(color, 3);
            _pen1 = new Pen(color, 1);
            _pen3.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            _pen1.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            _shape = shape;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints one villageborder to the map
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            if (width > ShapeDrawer.InnerShapeTreshold)
            {
                Pen pen;
                if (width < 15) pen = _pen1;
                else pen = _pen3;

                var drawShape = _shape == ShapeDisplay.Shapes.EllipseDrawer ? EllipseDrawer : RectangleDrawer;
                drawShape(g, pen, x, y, width, height);
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public override void Dispose(bool disposing)
        {
            _pen1.Dispose();
            _pen3.Dispose();
        }
        #endregion
    }
}
