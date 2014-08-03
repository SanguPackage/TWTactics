#region Using
using System;
using System.Drawing;

#endregion

namespace TribalWars.Maps.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws a border around a ShapeDrawer
    /// </summary>
    public sealed class BorderDrawer : DrawerBase
    {
        #region Fields
        public static readonly Action<Graphics, Pen, Rectangle> EllipseDrawer = (g, pen, village) => g.DrawEllipse(pen, village);
        public static readonly Action<Graphics, Pen, Rectangle> RectangleDrawer = (g, pen, village) => g.DrawRectangle(pen, village);

        private readonly Action<Graphics, Pen, Rectangle> _drawShape;
        private readonly Pen _pen1;
        private readonly Pen _pen3;
        #endregion

        #region Constructors
        public BorderDrawer(Color color, Action<Graphics, Pen, Rectangle> drawShape)
        {
            _pen3 = new Pen(color, 3);
            _pen1 = new Pen(color, 1);
            _pen3.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            _pen1.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            _drawShape = drawShape;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints one villageborder to the map
        /// </summary>
        protected override void PaintVillageCore(Graphics g, Rectangle village)
        {
            if (village.Width > ShapeDrawer.InnerShapeTreshold)
            {
                Pen pen;
                if (village.Width < 15) pen = _pen1;
                else pen = _pen3;

                _drawShape(g, pen, village);
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
