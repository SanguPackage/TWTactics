#region Using
using System.Drawing;

#endregion

namespace TribalWars.Data.Maps.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws smaller rectangles inside a village
    /// </summary>
    public class XDrawer : DrawerBase
    {
        #region Properties
        private readonly Brush _brush;
        #endregion

        #region Constructors
        public XDrawer(Color color)
        {
            _brush = new SolidBrush(color);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Draws one X marker to a village location
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            if (width > 5)
            {
                int off = width - (width/2 - 1);
                int w = width - off;

                g.FillRectangle(
                    _brush,
                    x + off/2,
                    y + off/2,
                    w,
                    w);
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public override void Dispose(bool disposing)
        {
            _brush.Dispose();
        }
        #endregion
    }
}