#region Using
using System.Drawing;
using System.Drawing.Drawing2D;
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
            g.DrawImage(Bitmap, new Rectangle(x, y, width, height));
            
            if (width < 31)
            {
                g.FillEllipse(ExtraColorBrush ?? ColorBrush, x + 0.5f, y + 0.5f, 4, 4);
            }
            else
            {
                int size = width > 35 ? 7 : 5;
                g.FillEllipse(ColorBrush, x + 0.5f, y + 0.5f, size, size);
                if (ExtraColorBrush != null && width > 20)
                {
                    // cirkel naast eerste cirkel
                    float xOffset = width > 35 ? 10f : 6f;
                    g.FillEllipse(ExtraColorBrush, x + xOffset, y + 0.5f, size, size);
                }
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