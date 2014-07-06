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
        protected override void PaintVillageCore(Graphics g, Rectangle village)
        {
            if (village.Width > 5)
            {
                int off = village.Width - (village.Width / 2 - 1);
                int w = village.Width - off;

                g.FillRectangle(
                    _brush,
                    village.X + off/2,
                    village.Y + off/2,
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