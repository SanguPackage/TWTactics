#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Tools;
using TribalWars.Data.Reporting;
using TribalWars.Data.Maps;
using TribalWars.Data.Events;
using TribalWars.Data.Maps.Manipulators;
using TribalWars.Data.Maps.Drawers;
#endregion

namespace TribalWars.Controls.Maps
{
    /// <summary>
    /// The actual visual map UserControl
    /// </summary>
    public partial class MapControl : UserControl
    {
        #region Fields
        private Map _map;
        private readonly Ruler _ruler;
        private Point _point00 = new Point(0, 0);
        #endregion

        #region Properties
        public ScrollableMapControl ScrollableMap
        {
            get { return MapPicture; }
        }
        #endregion

        #region Constructors
        public MapControl()
        {
            InitializeComponent();
            _ruler = new Ruler();
        }
        #endregion

        #region Map Events
        private void EventPublisher_LocationChanged(object sender, MapLocationEventArgs e)
        {
            _ruler.ClearCache();
            XRuler.Invalidate();
            YRuler.Invalidate();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the map parent
        /// </summary>
        public void SetMap(Map map)
        {
            _map = map;
            _map.EventPublisher.LocationChanged += EventPublisher_LocationChanged;
        }
        #endregion

        #region Rulers
        /// <summary>
        /// Draws the horizontal ruler
        /// </summary>
        private void XRuler_Paint(object sender, PaintEventArgs e)
        {
            if (!DesignMode && _ruler != null && World.Default.HasLoaded)
            {
                Location loc = _map.Location;
                Point gameLocation = _map.Display.GetGameLocation(_point00);
                Point mapLocation = _map.Display.GetMapLocation(gameLocation);
                int width = _map.Display.Dimensions.SizeWithSpacing.Width;
                _ruler.DrawHorizontalRuler(e.Graphics, XRuler, gameLocation, mapLocation, width);
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// Draws the vertical ruler
        /// </summary>
        private void YRuler_Paint(object sender, PaintEventArgs e)
        {
            if (!DesignMode && _ruler != null && World.Default.HasLoaded)
            {
                Location loc = _map.Location;
                Point gameLocation = _map.Display.GetGameLocation(_point00);
                Point mapLocation = _map.Display.GetMapLocation(gameLocation);
                int width = _map.Display.Dimensions.SizeWithSpacing.Width;
                _ruler.DrawVerticalRuler(e.Graphics, YRuler, gameLocation, mapLocation, width);
            }
            base.OnPaint(e);
        }

        #region Ruler Class
        /// <summary>
        /// Draws the rulers
        /// </summary>
        private class Ruler
        {
            #region Fields
            private readonly Font _rulerFont;
            private readonly StringFormat _rulerStringFormat;

            private Bitmap _cacheX;
            private Bitmap _cacheY;
            #endregion

            #region Constructors
            public Ruler()
            {
                _rulerFont = new Font("Verdana", 10);
                _rulerStringFormat = new StringFormat(StringFormatFlags.DirectionVertical);
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// Draws the horizontal ruler
            /// </summary>
            public void DrawHorizontalRuler(Graphics g, Panel panel, Point gameLeftTop, Point mapLeftTop, int villageWidth)
            {
                if (_cacheX != null)
                {
                    g.DrawImageUnscaled(_cacheX, 0, 0);
                }
                else
                {
                    _cacheX = new Bitmap(panel.ClientRectangle.Width, panel.ClientRectangle.Height);
                    Graphics g2 = Graphics.FromImage(_cacheX);

                    int mapOffset = villageWidth;
                    int gameOffset = 1;
                    if (mapOffset < 53)
                    {
                        int temp = 53 / villageWidth;
                        if (temp < 5) temp = 5;
                        else temp += temp % 5;

                        gameOffset = temp;
                        mapOffset = villageWidth * temp;
                    }

                    for (int i = mapLeftTop.X; i <= panel.Width; i += mapOffset)
                    {
                        if (gameLeftTop.X > 0 && gameLeftTop.X < 1000 && i > 0)
                        {
                            g2.DrawString(gameLeftTop.X.ToString(), _rulerFont, Brushes.Black, i - 10 + villageWidth / 2, 3);
                            g2.DrawLine(Pens.Black, i + villageWidth / 2, panel.Height, i + villageWidth / 2, panel.Height - 4);

                            g.DrawString(gameLeftTop.X.ToString(), _rulerFont, Brushes.Black, i - 10 + villageWidth / 2, 3);
                            g.DrawLine(Pens.Black, i + villageWidth / 2, panel.Height, i + villageWidth / 2, panel.Height - 4);
                        }

                        gameLeftTop.X += gameOffset;
                    }
                }
            }

            /// <summary>
            /// Draws the vertical ruler
            /// </summary>
            public void DrawVerticalRuler(Graphics g, Panel panel, Point gameLeftTop, Point mapLeftTop, int villageHeight)
            {
                if (_cacheY != null)
                {
                    g.DrawImageUnscaled(_cacheY, 0, 0);
                }
                else
                {
                    _cacheY = new Bitmap(panel.ClientRectangle.Width, panel.ClientRectangle.Height);
                    Graphics g2 = Graphics.FromImage(_cacheY);

                    int beginOffset = gameLeftTop.Y % 5;
                    int mapOffset = villageHeight;
                    int gameOffset = 1;
                    if (mapOffset < 38)
                    {
                        int temp = 38 / villageHeight;
                        if (temp < 5) temp = 5;
                        else temp += temp % 5;

                        gameOffset = temp;
                        mapOffset = villageHeight * temp;
                    }

                    for (int i = mapLeftTop.Y; i <= panel.Height; i += mapOffset)
                    {
                        if (gameLeftTop.Y > 0 && gameLeftTop.Y < 1000 && i > 0)
                        {
                            g2.DrawString(gameLeftTop.Y.ToString(), _rulerFont, Brushes.Black, 0, i - 10 + villageHeight / 2, _rulerStringFormat);
                            g2.DrawLine(Pens.Black, panel.Width, i + villageHeight / 2, panel.Width - 4, i + villageHeight / 2);

                            g.DrawString(gameLeftTop.Y.ToString(), _rulerFont, Brushes.Black, 0, i - 10 + villageHeight / 2, _rulerStringFormat);
                            g.DrawLine(Pens.Black, panel.Width, i + villageHeight / 2, panel.Width - 4, i + villageHeight / 2);
                        }

                        gameLeftTop.Y += gameOffset;
                    }
                }
            }

            public void ClearCache()
            {
                _cacheX = null;
                _cacheY = null;
            }
            #endregion
        }
        #endregion
        #endregion
    }
}