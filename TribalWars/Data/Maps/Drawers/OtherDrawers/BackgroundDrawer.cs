#region Using

using System.Drawing;

#endregion

namespace TribalWars.Data.Maps.Drawers.OtherDrawers
{
    /// <summary>
    /// Draws a bitmap to the map
    /// </summary>
    public class BackgroundDrawer : DrawerBase
    {
        #region Fields
        private readonly Bitmap _bitmap;
        #endregion

        #region Constructors
        public BackgroundDrawer(Bitmap icon)
        {
            _bitmap = icon;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints one non-village to the map (mountains, ...)
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            g.DrawImage(_bitmap, new Rectangle(x, y, width, height));
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public override void Dispose(bool disposing)
        {
            _bitmap.Dispose();
        }
        #endregion
    }
}
