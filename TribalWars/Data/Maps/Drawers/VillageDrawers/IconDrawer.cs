#region Using
using System.Drawing;
using TribalWars.Data.Maps.Markers;

#endregion

namespace TribalWars.Data.Maps.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws a village bitmap with
    /// </summary>
    public class IconDrawer : DrawerBase
    {
        #region Properties
        /// <summary>
        /// Gets the village bitmap
        /// </summary>
        public Bitmap Bitmap { get; private set; }

        /// <summary>
        /// Gets the brush the first circle is drawn with
        /// </summary>
        public Brush ColorBrush { get; private set; }

        /// <summary>
        /// Gets the brush the second circle is drawn with
        /// </summary>
        public Brush ExtraColorBrush { get; private set; }
        #endregion

        #region Constructors
        public IconDrawer(Bitmap icon, MarkerGroup colors)
        {
            ColorBrush = new SolidBrush(colors.Color);
            if (colors.ExtraColor != Color.Transparent)
            {
                ExtraColorBrush = new SolidBrush(colors.ExtraColor);
                //_extraPen = new Pen(colors.ExtraColor, 2);
            }

            Bitmap = icon;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Draws one village to the map
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            g.DrawImageUnscaledAndClipped(Bitmap, new Rectangle(x, y, width, height));
            g.FillEllipse(ColorBrush, x + 0.5f, y + 0.5f, 7, 7);

            if (ExtraColorBrush != null)
            {
                // cirkel rond de dorpen
                //g.DrawEllipse(_extraPen, x + 5, y + 5, 53 - 10, 38 - 10);

                // vierkantje in het midden
                //g.FillRectangle(_extraColorBrush, x + 20, y + 11, 12, 12);

                // cirkel naast eerste cirkel
                g.FillEllipse(ExtraColorBrush, x + 10f, y + 0.5f, 7, 7);
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public override void Dispose(bool disposing)
        {
            ColorBrush.Dispose();
            if (ExtraColorBrush != null)
                ExtraColorBrush.Dispose();
            Bitmap.Dispose();
        }
        #endregion
    }
}