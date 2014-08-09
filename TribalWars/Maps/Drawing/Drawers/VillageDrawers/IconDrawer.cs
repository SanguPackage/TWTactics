#region Using
using System.Drawing;
using TribalWars.Maps.Markers;

#endregion

namespace TribalWars.Maps.Drawing.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws a village bitmap with
    /// </summary>
    public sealed class IconDrawer : DrawerBase
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
        public IconDrawer(Bitmap icon, MarkerSettings colors)
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
        protected override void PaintVillageCore(Graphics g, Rectangle village)
        {
            g.DrawImage(Bitmap, village);

            int size;
            float xOffset;
            if (village.Width < 31)
            {
                size = 4;
                xOffset = 5f;
            }
            else if (village.Width > 35)
            {
                size = 7;
                xOffset = 10f;
            }
            else
            {
                size = 5;
                xOffset = 6f;
            }

            if (village.Width > 30)
            {
                g.FillEllipse(ColorBrush, village.X + 0.5f, village.Y + 0.5f, size, size);
                if (ExtraColorBrush != null)
                {
                    g.FillEllipse(ExtraColorBrush, village.X + xOffset, village.Y + 0.5f, size, size);
                }
            }
            else
            {
                g.FillRectangle(ColorBrush, village.X + 0.5f, village.Y + 0.5f, size, size);
                if (ExtraColorBrush != null)
                {
                    g.FillRectangle(ExtraColorBrush, village.X + xOffset, village.Y + 0.5f, size, size);
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