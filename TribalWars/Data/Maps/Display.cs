#region Using
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Drawing;
using TribalWars.Data.Tribes;
using TribalWars.Data.Players;
using TribalWars.Data.Villages;
using System.Diagnostics;
using TribalWars.Data.Maps.Displays;
#endregion

namespace TribalWars.Data.Maps
{
    // cachen van topmost, bottommost villages
    // als alle dorpen zichtbaar zijn kan alles gecached worden
    //private Tuple<Rectangle, Bitmap>[]
    // -> cache entire world for each zoom it's possible for

    /// <summary>
    /// Manages the painting of a TW map
    /// </summary>
    public sealed class Display
    {
        #region Fields
        private readonly Map _map;

        private readonly Pen _continentPen;
        private readonly Pen _provincePen;
        private Rectangle? _visibleRectangle;

        private Brush _backgroundBrush;
        private Color _backgroundColor;

        private Bitmap _background;
        private Painter _painter;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the manager that holds
        /// Shape &amp; Icon displays
        /// </summary>
        public DisplayManager DisplayManager { get; private set; }

        /// <summary>
        /// Gets or sets the canvas background color
        /// </summary>
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                _backgroundBrush = new SolidBrush(value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether continent lines should be drawn
        /// </summary>
        public bool ContinentLines { get; set; }

        /// <summary>
        /// Gets a value indicating whether province lines should be drawn
        /// </summary>
        public bool ProvinceLines { get; set; }

        /// <summary>
        /// Gets or sets a value indicating wether abandoned 
        /// villages should be shown on the map
        /// </summary>
        public bool HideAbandoned { get; set; }

        /// <summary>
        /// Gets or sets a value indicating wether unmarked
        /// villages should be shown on the map
        /// </summary>
        public bool MarkedOnly { get; set; }
        #endregion

        #region Constructors
        public Display(Map map)
            : this(map, DisplayTypes.None)
        {

        }

        public Display(Map map, DisplayTypes displayType)
        {
            _map = map;

            _backgroundBrush = new SolidBrush(Color.Green);

            DisplayManager = new DisplayManager(map, displayType);

            _continentPen = new Pen(Color.Black, 1);
            _continentPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            _provincePen = new Pen(Color.FromArgb(42, 94, 31), 1f);
            _provincePen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

            _map.EventPublisher.LocationChanged += EventPublisher_LocationChanged;
        }
        #endregion

        #region Reset Cache
        private void EventPublisher_LocationChanged(object sender, Events.MapLocationEventArgs e)
        {
            // TODO: also need to call this on resize probably?

            // TODO: if there is some overlap between e.NewLocation and e.OldLocation
            // redraw only the new part of the background and move the rest
            // keep a _drawnRectangle variable that represents the drawn part of the background
            if (e.OldLocation != null)
            {
                //Debug.Assert(e.OldLocation != e.NewLocation);
                if (e.OldLocation.Zoom != e.NewLocation.Zoom)
                {
                    _background = null;
                }
                else
                {
                    _background = null;
                }
            }

            // TODO: this is not ideal. VisibleRectangle is not updated for all LocationChanged handlers that have already executed
            _visibleRectangle = GetGameRectangle();
        }

        /// <summary>
        /// Resets the views of the Map Display
        /// </summary>
        public void Reset(DisplayTypes type)
        {
            DisplayManager.Reset(type);
            _background = null;
        }

        /// <summary>
        /// Deletes the entire cached image
        /// </summary>
        public void ResetCache()
        {
            // TODO: all calls to this one need to be evaluated
            //       for some we can probably do a partial redraw
            _background = null;
            _map.Control.Invalidate();
        }
        #endregion

        #region Painting
        /// <summary>
        /// Paints the canvas
        /// </summary>
        public void Paint(Graphics g2, Rectangle fullMap)
        {
            if (_background != null)
            {
                //Debug.WriteLine("passed for cache " + fullMap.ToString());
                g2.DrawImageUnscaled(_background, 0, 0);
            }
            else
            {
                //Debug.WriteLine("passed for Paint " + fullMap.ToString());

                var timing = Stopwatch.StartNew();

                //Debug.Assert(rec == fullMap); // is not true on resizing
                Debug.Assert(fullMap == new Rectangle(new Point(0, 0), _map.Control.Size));

                #region Some attempt at concurrency
                //var mapParts = new Rectangle[2];
                //mapParts[0] = new Rectangle(fullMap.X, fullMap.Y, fullMap.Width / 2, fullMap.Height);
                //mapParts[1] = new Rectangle(mapParts[0].Right + 1, mapParts[0].Bottom + 1, fullMap.Width - mapParts[0].Width, fullMap.Height);

                ////mapParts[0] = new Rectangle(fullMap.X, fullMap.Y, fullMap.Width / 2, fullMap.Height);
                ////mapParts[1] = new Rectangle(mapParts[0].Right + 1, mapParts[0].Bottom + 1, fullMap.Width - mapParts[0].Width, fullMap.Height);

                //var painter1 = new Painter(this, mapParts[0], _map.Location.Zoom);
                //var painter2 = new Painter(this, mapParts[1], _map.Location.Zoom);

                //var canvas = new Bitmap(fullMap.Width, fullMap.Height);
                //Graphics graphics = Graphics.FromImage(canvas);
                //graphics.DrawImageUnscaled(painter1.GetBitmap(), mapParts[0]);
                //graphics.DrawImageUnscaled(painter2.GetBitmap(), mapParts[1]);

                //_background = canvas;
                //_visibleRectangle = Rectangle.Union(painter1.GetVisibleGameRectangle(), painter2.GetVisibleGameRectangle());
                ////_visibleRectangle = painter2.GetVisibleGameRectangle();
                #endregion

                // Normal way: 1sec on zoom 1
                if (_painter == null || true)
                {
                    // TODO: we zaten hier:
                    // also create new painter when the _map.Location.Zoom changes. 
                    // also when we change DisplayType
                    // factory method?
                    _painter = new Painter(this, fullMap, _map.Location.Zoom);
                }
                _background = _painter.GetBitmap();
                _visibleRectangle = _painter.GetVisibleGameRectangle();

                timing.Stop();
                //Debug.WriteLine("Painting NEW:{0} in {1}", _map.Location, timing.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
                
                #region THA OLD CODE
                //timing.Restart();
                // x1: 0.6 secs

                // THIS IS THE OLD CODE:
                // Use it to see if we've slowed things down or not...

                //_background = new Bitmap(_map.Control.PictureWidth, _map.Control.PictureHeight);
                //Graphics g = Graphics.FromImage(_background);

                //Point gameTopLeft = GetGameLocation(fullMap.Left, fullMap.Top);
                //Point gameBottomRight = GetGameLocation(fullMap.Right, fullMap.Bottom);
                //_visibleRectangle = new Rectangle(gameTopLeft.X, gameTopLeft.Y, gameBottomRight.X - gameTopLeft.X, gameBottomRight.Y - gameTopLeft.Y);

                //int xOffset = GetMapLocation(gameTopLeft).X;
                //int yOffset = GetMapLocation(gameTopLeft).Y;

                //DisplayBase displayType = DisplayManager.CurrentDisplay;
                //int zoom = _map.Location.Zoom;
                //int width = displayType.GetVillageWidthSpacing(zoom);
                //int height = displayType.GetVillageHeightSpacing(zoom);
                //int mapY = 0;

                //int villageWidth = displayType.GetVillageWidth(zoom);
                //int villageHeight = displayType.GetVillageHeight(zoom);

                //// Draw villages
                //int mapX = fullMap.Left + xOffset;
                //mapY = fullMap.Top + yOffset;
                //if (false)
                //{
                //    // Different way to loop over the villages
                //    // timing is pretty much the same...
                //    /*for (int gameY = gameTopLeft.Y; gameY <= gameBottomRight.Y; gameY++)
                //    {
                //        mapX = fullMap.Left + xOffset;
                //        for (int gameX = gameTopLeft.X; gameX <= gameBottomRight.X; gameX++)
                //        {
                //            _viewManager.Paint(g, new Point(gameX, gameY), mapX, mapY, villageWidth, villageHeight);
                //            mapX += width;
                //            villagesDrawed++;
                //        }
                //        mapY += height;
                //    }*/
                //}
                //else
                //{
                //    int gameX = 0;
                //    int gameY = gameTopLeft.Y;
                //    g.FillRectangle(_backgroundBrush, fullMap);
                //    for (int yMap = mapY; yMap <= fullMap.Bottom; yMap += height)
                //    {
                //        gameX = gameTopLeft.X;
                //        for (int xMap = mapX; xMap <= fullMap.Right; xMap += width)
                //        {
                //            DisplayManager.Paint(g, new Point(gameX, gameY), xMap, yMap, villageWidth, villageHeight);
                //            gameX += 1;
                //        }
                //        gameY += 1;
                //    }
                //}

                ////// Horizontal continent lines
                //int gridOffset = 5;
                //mapY = fullMap.Top + yOffset - (gameTopLeft.Y % gridOffset) * height;
                ////for (int gameY = _visibleRectangle.Y - (_visibleRectangle.Y % gridOffset); gameY <= _visibleRectangle.Height; gameY += gridOffset)
                //for (int gameY = gameTopLeft.Y - (gameTopLeft.Y % gridOffset); gameY <= gameBottomRight.Y; gameY += gridOffset)
                //{
                //    if (gameY > 0 && gameY < 1000)
                //    {
                //        if (gameY % 100 == 0)
                //            g.DrawLine(_continentPen, 0, mapY, fullMap.Width, mapY);
                //        else if (width > 4)
                //            g.DrawLine(_provincePen, 0, mapY, fullMap.Width, mapY);
                //    }
                //    mapY += height * gridOffset;
                //}

                //// Vertical continent lines
                //mapX = fullMap.Left + xOffset - (gameTopLeft.X % gridOffset) * width;
                //for (int gameX = gameTopLeft.X - (gameTopLeft.X % gridOffset); gameX <= gameBottomRight.X; gameX += gridOffset)
                //{
                //    if (gameX > 0 && gameX < 1000)
                //    {
                //        if (gameX % 100 == 0)
                //            g.DrawLine(_continentPen, mapX, 0, mapX, fullMap.Height);
                //        else if (width > 4)
                //            g.DrawLine(_provincePen, mapX, 0, mapX, fullMap.Height);
                //    }
                //    mapX += width * gridOffset;
                //}
                //timing.Stop();
                //Debug.WriteLine("Painting OLD:{0} in {1}", _map.Location, timing.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
                #endregion

                g2.DrawImageUnscaled(_background, 0, 0);
            }
        }

        /// <summary>
        /// Paints the villages and continent/province lines on a canvas
        /// </summary>
        private class Painter
        {
            #region Fields
            private readonly Bitmap _canvas;
            private readonly Graphics _g;
            private readonly Rectangle _toPaint;
            private readonly Rectangle _visibleGameRectangle;

            private readonly int _villageWidth;
            private readonly int _villageHeight;
            private readonly int _villageWidthSpacing;
            private readonly int _villageHeightSpacing;

            private readonly Display _display;
            #endregion

            #region Constructor
            public Painter(Display display, Rectangle mapSize, int zoom)
            {
                _display = display;
                _canvas = new Bitmap(mapSize.Width, mapSize.Height);
                _g = Graphics.FromImage(_canvas);
                _g.FillRectangle(display._backgroundBrush, _toPaint);

                _visibleGameRectangle = display.GetGameRectangle();
                // TODO: calculate the best min/map coords here.
                // _visibleGameRectangle is sometimes negative!!

                // Also draw villages that are only partially visible at left/top
                Point mapOffset = _display.GetMapLocation(_visibleGameRectangle.Location);
                mapSize.Offset(mapOffset);
                _toPaint = mapSize;

                DisplayBase displayType = display.DisplayManager.CurrentDisplay;
                _villageWidthSpacing = displayType.GetVillageWidthSpacing(zoom);
                _villageHeightSpacing = displayType.GetVillageHeightSpacing(zoom);

                _villageWidth = displayType.GetVillageWidth(zoom);
                _villageHeight = displayType.GetVillageHeight(zoom);

                DrawVillages();
                DrawContinentLines();
            }
            #endregion

            #region Villages
            private void DrawVillages()
            {
                int mapX = _toPaint.Left;
                int mapY = _toPaint.Top;

                int gameY = _visibleGameRectangle.Y;

                for (int yMap = mapY; yMap <= _toPaint.Height; yMap += _villageHeightSpacing)
                {
                    int gameX = _visibleGameRectangle.X;
                    for (int xMap = mapX; xMap <= _toPaint.Width; xMap += _villageWidthSpacing)
                    {
                        _display.DisplayManager.Paint(_g, new Point(gameX, gameY), new Rectangle(xMap, yMap, _villageWidth, _villageHeight));
                        gameX += 1;
                    }
                    gameY += 1;
                }
            }
            #endregion

            #region Continent Lines
            private void DrawContinentLines()
            {
                //Horizontal
                DrawContinentLines(_toPaint.Top, _toPaint.Bottom, _visibleGameRectangle.Y, _visibleGameRectangle.Bottom, _villageHeightSpacing, true, _toPaint.Left, _toPaint.Right, _visibleGameRectangle.X, _visibleGameRectangle.Right, _villageWidthSpacing);

                // Vertical
                DrawContinentLines(_toPaint.Left, _toPaint.Right, _visibleGameRectangle.X, _visibleGameRectangle.Right, _villageWidthSpacing, false, _toPaint.Top, _toPaint.Bottom, _visibleGameRectangle.Y, _visibleGameRectangle.Bottom, _villageHeightSpacing);
            }

            /// <summary>
            /// Draws all horizontal or vertical continent lines (and province lines if required).
            /// </summary>
            /// <remarks>
            /// For horizontal lines: map/gameXXX params are the VERTICAL coordinates. otherMap/gameXXX are then the horizontals.
            /// For vertical lines: map/gameXXX params are the HORIZONTAL coordinates.
            /// </remarks>
            private void DrawContinentLines(int mapMin, int mapMax, int gameMin, int gameMax, int villageSize, bool isHorizontal, int otherMapMin, int otherMapMax, int otherGameMin, int otherGameMax, int otherVillageSize)
            {
                const int provinceWidth = 5;
                const int continentWidth = 100;

                if (_villageWidthSpacing > 4)
                {
                    // These are the province lines:
                    DrawContinentLines(_display._provincePen, mapMin, mapMax, gameMin, gameMax, villageSize, isHorizontal, provinceWidth, otherMapMin, otherMapMax, otherGameMin, otherGameMax, otherVillageSize);
                }

                DrawContinentLines(_display._continentPen, mapMin, mapMax, gameMin, gameMax, villageSize, isHorizontal, continentWidth, otherMapMin, otherMapMax, otherGameMin, otherGameMax, otherVillageSize);
            }

            private void DrawContinentLines(Pen pen, int mapMin, int mapMax, int gameMin, int gameMax, int villageSize, bool isHorizontal, int sizeBetweenLines, int otherMapMin, int otherMapMax, int otherGameMin, int otherGameMax, int otherVillageSize)
            {
                // Don't start the loop before the start of the world
                int mapStart = mapMin - (gameMin % sizeBetweenLines) * villageSize;
                int gameStart = gameMin - (gameMin % sizeBetweenLines);
                if (gameStart < 0)
                {
                    mapStart += villageSize * gameStart * -1;
                    gameStart = 0;
                }

                // Loop only until end of the world
                if (gameMax > 1000)
                {
                    gameMax = 1000;
                }

                // Don't draw before the start of the world
                int otherMapStart = otherMapMin - (otherGameMin % sizeBetweenLines) * villageSize;
                int otherGameStart = otherGameMin - (otherGameMin % sizeBetweenLines);
                if (otherGameStart < 0)
                {
                    otherMapStart += villageSize * otherGameStart * -1;
                }

                // Don't draw after the end of the world
                int otherMapEnd = otherMapMax;
                if (otherGameMax > 1000)
                {
                    otherMapEnd += otherVillageSize * (otherGameMax - 1000) * -1;
                }

                int map = mapStart;
                for (int game = gameStart; game <= gameMax; game += sizeBetweenLines)
                {
                    Debug.Assert(game >= 0);
                    Debug.Assert(game <= 1000);

                    if (isHorizontal)
                    {
                        _g.DrawLine(pen, otherMapStart, map, otherMapEnd, map);
                    }
                    else
                    {
                        _g.DrawLine(pen, map, otherMapStart, map, otherMapEnd);
                    }

                    map += villageSize * sizeBetweenLines;
                }
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// Gets the game rectangle representing what is drawn on the map
            /// </summary>
            public Rectangle GetVisibleGameRectangle()
            {
                return _visibleGameRectangle;
            }

            /// <summary>
            /// Gets the canvas with all villages drawn to it
            /// </summary>
            public Bitmap GetBitmap()
            {
                return _canvas;
            }
            #endregion
        }
        #endregion

        #region IsVisible
        /// <summary>
        /// Gets a value indicating whether a tribe is currently visible
        /// </summary>
        public bool IsVisible(Tribe tribe)
        {
            if (!_visibleRectangle.HasValue) return false;
            return tribe.Any(village => _visibleRectangle.Value.Contains(village.Location));
        }

        /// <summary>
        /// Gets a value indicating whether a player is currently visible
        /// </summary>
        public bool IsVisible(Player player)
        {
            if (!_visibleRectangle.HasValue) return false;
            return player.Any(village => _visibleRectangle.Value.Contains(village.Location));
        }

        /// <summary>
        /// Gets a value indicating whether a village is currently visible
        /// </summary>
        public bool IsVisible(Village village)
        {
            if (!_visibleRectangle.HasValue) return false;
            return _visibleRectangle.Value.Contains(village.Location);
        }
        #endregion

        #region Game from/to Map Converters
        /// <summary>
        /// Converts a game location to the map location
        /// </summary>
        /// <remarks>Assumes the location needs to be converted for the main map</remarks>
        public Point GetMapLocation(Point loc)
        {
            // Get location from game and convert it to location on the map
            int height = DisplayManager.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);
            int width = DisplayManager.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);

            int off = (loc.X - _map.Location.X) * width;
            loc.X = off + _map.CanvasSize.Width / 2;
            off = (loc.Y - _map.Location.Y) * height;
            loc.Y = off + (_map.CanvasSize.Height / 2);
            return loc;
        }

        /// <summary>
        /// 
        /// </summary>
        public Rectangle GetMapRectangle(Rectangle gameRectangle)
        {
            Point leftTop = _map.Display.GetMapLocation(gameRectangle.Location);
            Point rightBottom = _map.Display.GetMapLocation(new Point(gameRectangle.Right, gameRectangle.Bottom));
            return new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);
        }

        /// <summary>
        /// Converts a map location to the game location
        /// </summary>
        /// <remarks>Assumes the location needs to be converted for the main map</remarks>
        public Point GetGameLocation(Point loc)
        {
            // Get location from map and convert it to game location
            int height = DisplayManager.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);
            int width = DisplayManager.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);

            int newx = (loc.X + _map.Location.X * width - _map.CanvasSize.Width / 2) / width;
            int newy = (loc.Y + _map.Location.Y * height - _map.CanvasSize.Height / 2) / height;
            return new Point(newx, newy);
        }

        /// <summary>
        /// Converts map to game location and return the village
        /// </summary>
        public Village GetGameVillage(Point map)
        {
            Point game = GetGameLocation(map);
            if (World.Default.Villages.ContainsKey(game)) return World.Default.Villages[game];
            return null;
        }

        /// <summary>
        /// Converts the UserControl size to the game rectangle it represents
        /// </summary>
        public Rectangle GetGameRectangle()
        {
            Size fullMap = _map.CanvasSize;
            Point leftTop = GetGameLocation(new Point(0, 0));
            Point rightBottom = GetGameLocation(new Point(fullMap.Width, fullMap.Height));
            return new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);
        }

        public Rectangle GetGameRectangle(Rectangle mapRectangle)
        {
            Point gameLocation = GetGameLocation(mapRectangle.Location);
            Point gameSize = GetGameLocation(new Point(mapRectangle.Right, mapRectangle.Bottom));
            return new Rectangle(gameLocation, new Size(gameSize.X - gameLocation.X, gameSize.Y - gameLocation.Y));
        }
        #endregion
    }
}
