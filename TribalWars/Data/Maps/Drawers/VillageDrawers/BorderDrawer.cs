#region Using

using System.Drawing;
using TribalWars.Data.Maps.Displays;

#endregion

namespace TribalWars.Data.Maps.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws a border around a rectangle or
    /// ellipse drawer
    /// </summary>
    public class BorderDrawer : DrawerBase
    {
        #region Fields

        private readonly ShapeDisplay.Shapes _shape;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the pen the border gets drawn with
        /// </summary>
        public Pen Pen { get; private set; }

        #endregion

        #region Constructors
        public BorderDrawer(Color color, ShapeDisplay.Shapes shape)
        {
            // TODO: borderdrawer doesn't really work does it?
            // A border gets drawn but not as it should be
            Pen = new Pen(color, 3);
            Pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            _shape = shape;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints one villageborder to the map
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            if (_shape == ShapeDisplay.Shapes.EllipseDrawer)
                g.DrawEllipse(Pen, x, y, width, height);
            else
                g.DrawRectangle(Pen, x, y, width, height);
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public override void Dispose(bool disposing)
        {
            Pen.Dispose();
        }
        #endregion
    }
}
