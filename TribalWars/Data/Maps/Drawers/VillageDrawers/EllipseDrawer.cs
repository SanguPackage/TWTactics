#region Using

using System.Drawing;
using TribalWars.Data.Maps.Markers;

#endregion

namespace TribalWars.Data.Maps.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws village ellipses
    /// </summary>
    public class EllipseDrawer : DrawerBase
    {
        #region Properties
        /// <summary>
        /// Gets the brush the ellipse gets drawn with
        /// </summary>
        public Brush Brush { get; private set; }

        /// <summary>
        /// Gets the brush a smaller circle inside
        /// the main circle gets drawn with
        /// </summary>
        public Brush ExtraColorBrush { get; private set; }
        #endregion

        #region Constructors
        public EllipseDrawer(MarkerGroup colors)
        {
            Brush = new SolidBrush(colors.Color);
            if (colors.ExtraColor != Color.Transparent)
            {
                ExtraColorBrush = new SolidBrush(colors.ExtraColor);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Draws one village to the map
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            if (width > 5)
            {
                g.FillEllipse(Brush, x, y, width, height);
                if (ExtraColorBrush != null)
                {
                    float rec = width - 6;
                    float spacing = 3; // width / 4f;
                    if (rec < 3)
                    {
                        rec = 3;
                        spacing = 1;
                    }
                    g.FillEllipse(ExtraColorBrush, x + spacing, y + spacing, rec, rec);
                }
            }
            else
            {
                if (ExtraColorBrush != null && width > 3) g.FillRectangle(ExtraColorBrush, x, y, width, height);
                else g.FillRectangle(Brush, x, y, width, height);
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public override void Dispose(bool disposing)
        {
            Brush.Dispose();
            if (ExtraColorBrush != null)
                ExtraColorBrush.Dispose();
        }
        #endregion
    }
}
