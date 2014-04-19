#region Using
using System.Drawing;
using TribalWars.Data.Maps.Markers;

#endregion

namespace TribalWars.Data.Maps.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws village rectangles
    /// </summary>
    public class RectangleDrawer : DrawerBase
    {
        #region Fields
        private readonly Brush _colorBrush;
        private readonly Brush _extraColorBrush;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the brush the rectangle is drawn with
        /// </summary>
        public Brush Brush
        {
            get { return _colorBrush; }
        }

        /// <summary>
        /// Gets the brush an additional rectangle is drawn
        /// inside the first rectangle
        /// </summary>
        public Brush ExtraColorBrush
        {
            get { return _extraColorBrush; }
        }
        #endregion

        #region Constructors
        public RectangleDrawer()
        {
            _colorBrush = new SolidBrush(Color.Transparent);
        }

        public RectangleDrawer(Color color)
        {
            _colorBrush = new SolidBrush(color);
        }

        public RectangleDrawer(MarkerGroup colors)
        {
            _colorBrush = new SolidBrush(colors.Color);
            if (colors.ExtraColor != Color.Transparent)
            {
                _extraColorBrush = new SolidBrush(colors.ExtraColor);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints one village to the map
        /// </summary>
        protected override void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {
            // TODO : crap :)
            // alles moet gecached zijn, alle offsets...
            // de drawers worden geupdate bij een 'ChangeDisplay'
            // in feite gewoon de DisplayManager een Update() laten doen op alle drawers
            // en daar de offsets etc berekenen
            if (_extraColorBrush != null && width > 5)
            {
                g.FillRectangle(_colorBrush, x, y, width, height);
                float rec = width - 6;
                float spacing = 3; // width / 4f;
                if (rec < 3)
                {
                    rec = 3;
                    spacing = 1;
                }
                g.FillRectangle(_extraColorBrush, x + spacing, y + spacing, rec, rec);
            }
            else
            {
                if (_extraColorBrush != null && width > 3) g.FillRectangle(_extraColorBrush, x, y, width, height);
                else g.FillRectangle(_colorBrush, x, y, width, height);
            }
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public override void Dispose(bool disposing)
        {
            _colorBrush.Dispose();
            if (_extraColorBrush != null)
                _extraColorBrush.Dispose();
        }
        #endregion
    }
}