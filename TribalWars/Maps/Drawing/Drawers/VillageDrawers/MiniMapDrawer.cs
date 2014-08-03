#region Using
using System.Drawing;

#endregion

namespace TribalWars.Maps.Drawing.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws village rectangles
    /// </summary>
    public class MiniMapDrawer : DrawerBase
    {
        #region Fields
        private readonly Brush _colorBrush;
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
        protected override void PaintVillageCore(Graphics g, Rectangle village)
        {
            g.FillRectangle(_colorBrush, village);
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