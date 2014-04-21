#region Using
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Manages the painting of a TW map
    /// </summary>
    public class Display
    {
        #region Fields
        private readonly Map _map;

        private readonly Pen _continentPen;
        private readonly Pen _provincePen;
        private Rectangle? _visibleRectangle;

        private Brush _backgroundBrush;
        private Color _backgroundColor;

        private Bitmap _background;
        private Location _location;
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

        #region Public Methods
        private void EventPublisher_LocationChanged(object sender, Events.MapLocationEventArgs e)
        {
            // TODO: also need to call this on resize probably?

            // TODO: if there is some overlap between e.NewLocation and e.OldLocation
            // redraw only the new part of the background and move the rest
            // keep a _drawnRectangle variable that represents the drawn part of the background



            _background = null;
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
            _background = null;
        }

        private class Painter
        {
            private readonly Bitmap _canvas;
            private readonly Graphics _g;
            private readonly Location _currentLocation;
            private readonly Location _prevLocation;
            private readonly Rectangle _toPaint;
            private readonly Rectangle _visibleGameRectangle;

            private readonly int _villageWidth;
            private readonly int _villageHeight;
            private readonly int _villageWidthSpacing;
            private readonly int _villageHeightSpacing;

            private readonly Display _display;

            public Painter(Display display, Rectangle mapSize, Location loc)
            {
                _display = display;
                _canvas = new Bitmap(mapSize.Width, mapSize.Height);
                _g = Graphics.FromImage(_canvas);
                _g.FillRectangle(display._backgroundBrush, _toPaint);
                _currentLocation = loc;

                Point gameTopLeft = display.GetGameLocation(mapSize.Location);
                Point gameBottomRight = display.GetGameLocation(mapSize.Width, mapSize.Height);
                _visibleGameRectangle = new Rectangle(gameTopLeft.X, gameTopLeft.Y, gameBottomRight.X - gameTopLeft.X, gameBottomRight.Y - gameTopLeft.Y);

                // Also draw villages that are only partially visible at left/top
                Point mapOffset = _display.GetMapLocation(_visibleGameRectangle.Location);
                mapSize.Offset(mapOffset);
                _toPaint = mapSize;

                DisplayBase displayType = display.DisplayManager.CurrentDisplay;
                int zoom = loc.Zoom;
                _villageWidthSpacing = displayType.GetVillageWidthSpacing(zoom);
                _villageHeightSpacing = displayType.GetVillageHeightSpacing(zoom);

                _villageWidth = displayType.GetVillageWidth(zoom);
                _villageHeight = displayType.GetVillageHeight(zoom);

                DrawVillages();
                DrawContinentLines();
            }

            private void DrawContinentLines()
            {
                //Horizontal
                DrawContinentLines(_toPaint.Top, _toPaint.Width, _visibleGameRectangle.Y, _visibleGameRectangle.Bottom, _villageHeightSpacing, true, _toPaint.Left, _toPaint.Height, _visibleGameRectangle.X, _visibleGameRectangle.Right);

                // Vertical
                DrawContinentLines(_toPaint.Left, _toPaint.Height, _visibleGameRectangle.X, _visibleGameRectangle.Right, _villageWidthSpacing, false, _toPaint.Top, _toPaint.Width, _visibleGameRectangle.Y, _visibleGameRectangle.Bottom);
            }

            private void DrawContinentLines(int mapMin, int mapMax, int gameMin, int gameMax, int villageSize, bool isHorizontal, int otherMapMin, int otherMapMax, int otherGameMin, int otherGameMax)
            {
                const int provinceWidth = 5;
                const int continentWidth = 100;

                if (_villageWidthSpacing > 4)
                {
                    DrawContinentLine(_display._provincePen, mapMin, mapMax, gameMin, gameMax, villageSize, isHorizontal, provinceWidth, otherMapMin, otherMapMax, otherGameMin, otherGameMax);
                }

                DrawContinentLine(_display._continentPen, mapMin, mapMax, gameMin, gameMax, villageSize, isHorizontal, continentWidth, otherMapMin, otherMapMax, otherGameMin, otherGameMax);

                //int mapStart = mapMin - (gameMin % provinceWidth) * villageSize;
                //int gameStart = gameMin - (gameMin % provinceWidth);
                //for (int gameY = gameStart; gameY <= gameMax; gameY += 100)
                //{
                //    Debug.Assert(gameY >= 0);
                //    Debug.Assert(gameY <= 1000);

                //    if (isHorizontal) _g.DrawLine(_display._continentPen, 0, mapStart, mapMax, mapStart);
                //    else _g.DrawLine(_display._continentPen, mapStart, 0, mapStart, mapMax);

                //    mapStart += villageSize * 100;
                //}

                //int map = mapMin - (gameMin % gridOffset) * villageSize;
                //for (int gameY = gameMin - (gameMin % gridOffset); gameY <= gameMax; gameY += gridOffset)
                //{
                //    Debug.Assert(gameY >= 0);
                //    Debug.Assert(gameY <= 1000);

                //    Pen pen = null;
                //    if (gameY % 100 == 0)
                //    {
                //        pen = _display._continentPen;
                //    }
                //    else if (_villageWidthSpacing > 4)
                //    {
                //        pen = _display._provincePen;
                //    }
                //    if (pen != null)
                //    {
                //        if (isHorizontal) _g.DrawLine(pen, 0, map, mapMax, map);
                //        else _g.DrawLine(pen, map, 0, map, mapMax);
                //    }
                //    map += villageSize * gridOffset;
                //}

                //    if (gameY >= 0 && gameY <= 1000)
            }

            private void DrawContinentLine(Pen pen, int mapMin, int mapMax, int gameMin, int gameMax, int villageSize, bool isHorizontal, int sizeBetweeLines, int otherMapMin, int otherMapMax, int otherGameMin, int otherGameMax)
            {
                int mapStart = mapMin - (gameMin % sizeBetweeLines) * villageSize;
                int gameStart = gameMin - (gameMin % sizeBetweeLines);
                if (gameStart < 0)
                {
                    mapStart += villageSize * gameStart * -1;
                    gameStart = 0;
                }
                if (gameMax > 1000)
                {
                    //mapMax -= villageSize * (gameMax - 1000);
                    gameMax = 1000;
                }

                //mapMax = mapStart + 1000 * villageSize;

                //int otherMapStart = otherMapMin - (otherGameMin % sizeBetweeLines) * villageSize;
                //int otherGameStart = otherGameMin - (otherGameMin % sizeBetweeLines);
                //if (otherGameStart < 0)
                //{
                //    otherMapStart += villageSize * otherGameStart * -1;
                //    otherGameStart = 0;
                //}
                //if (otherGameMax > 1000)
                //{
                //    otherMapMax += villageSize * (otherGameMax - 1000);
                //    otherGameMax = 1000;
                //}


                int map = mapStart;
                for (int game = gameStart; game <= gameMax; game += sizeBetweeLines)
                {
                    Debug.Assert(game >= 0);
                    Debug.Assert(game <= 1000);

                    if (isHorizontal)
                    {
                        //_g.DrawLine(pen, otherMapStart, map, mapMax, map);
                        _g.DrawLine(pen, 0, map, mapMax, map);
                    }
                    else
                    {
                        //_g.DrawLine(pen, map, otherMapStart, map, mapMax);
                        _g.DrawLine(pen, map, 0, map, mapMax);
                    }

                    map += villageSize * sizeBetweeLines;
                }
            }

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
                        _display.DisplayManager.Paint(_g, new Point(gameX, gameY), xMap, yMap, _villageWidth, _villageHeight);
                        gameX += 1;
                    }
                    gameY += 1;
                }
            }

            public Rectangle GetVisibleGameRectangle()
            {
                return _visibleGameRectangle;
            }

            public Bitmap GetBitmap()
            {
                return _canvas;
            }
        }

        /// <summary>
        /// Paints the canvas
        /// </summary>
        public void Paint(Graphics g2, Rectangle rec, Rectangle fullMap)
        {
            if (_background != null)
            {
                g2.DrawImageUnscaled(_background, 0, 0);
            }
            else
            {
                var timing = Stopwatch.StartNew();

                Debug.Assert(rec == fullMap);
                Debug.Assert(fullMap == new Rectangle(new Point(0, 0), _map.Control.Size));

                var painter = new Painter(this, fullMap, _map.Location);
                _background = painter.GetBitmap();
                _visibleRectangle = painter.GetVisibleGameRectangle();

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

                //Graphics g = painter._g;

                // TODO: we zaten hier:
                // de rec param is hier mss al wat we zoeken?

                // roep functie DrawContinentLines 2x op
                // enkel de correcte lijnen tonen

                // cachen van topmost, bottommost villages
                // als alle dorpen zichtbaar zijn kan alles gecached worden

                // de beforeLocation vergelijken met de huidige
                // bij overlap daarvan starten

                //private Tuple<Rectangle, Bitmap>[]
                // -> cache entire world for each zoom it's possible for

                // Horizontal continent lines
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
                //int mapX = fullMap.Left + xOffset - (gameTopLeft.X % gridOffset) * width;
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

                timing.Stop();
                Debug.WriteLine("Painting:{0} in {1}", _map.Location, timing.Elapsed.TotalSeconds);

                g2.DrawImageUnscaled(_background, 0, 0);
            }
        }

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

        #region Point Converters
        /// <summary>
        /// Converts a game location to the map location
        /// </summary>
        /// <remarks>Assumes the location needs to be converted for the main map</remarks>
        public Point GetMapLocation(Point loc)
        {
            return GetMapLocation(_map.Location, _map.Control.PictureWidth, _map.Control.PictureHeight, loc.X, loc.Y);
        }

        /// <summary>
        /// Converts a game location to the map location
        /// </summary>
        public Point GetMapLocation(int x, int y)
        {
            return GetMapLocation(_map.Location, _map.Control.PictureWidth, _map.Control.PictureHeight, x, y);
        }

        /// <summary>
        /// Converts a game location to the map location
        /// </summary>
        public Point GetMapLocation(Location sets, int mapWidth, int mapHeight, int x, int y)
        {
            // Get location from game and convert it to location on the map
            int height = DisplayManager.CurrentDisplay.GetVillageHeightSpacing(sets.Zoom);
            int width = DisplayManager.CurrentDisplay.GetVillageWidthSpacing(sets.Zoom);

            int off = (x - sets.X) * width;
            x = off + mapWidth / 2; // +(2 * x - 1);
            off = (y - sets.Y) * height;
            y = off + (mapHeight / 2); // +(2 * y - 1);
            return new Point(x, y);
        }


        /// <summary>
        /// Calculates the coordinates and zoom level so all villages are visible
        /// </summary>
        /// <param name="vils">Villages that have to be visible</param>
        /// <param name="tryRespectCurrentZoom">False: use optimal zoom level, True: try to keep current zoom level</param>
        public static Location GetSpan(IEnumerable<Village> vils, bool tryRespectCurrentZoom = true)
        {
            return GetSpan(vils, new Size(World.Default.Map.Control.PictureWidth, World.Default.Map.Control.PictureHeight), tryRespectCurrentZoom);
        }

        /// <summary>
        /// Calculates the coordinates and zoom level so all villages are visible
        /// </summary>
        /// <param name="vils">Villages that have to be visible</param>
        /// <param name="world"></param>
        /// <param name="tryRespectCurrentZoom">False: use optimal zoom level, True: try to keep current zoom level</param>
        public static Location GetSpan(IEnumerable<Village> vils, Size world, bool tryRespectCurrentZoom = true)
        {
            int leftX = 999, topY = 999, rightX = 0, bottomY = 0;
            foreach (Village vil in vils)
            {
                if (vil.X < leftX) leftX = vil.X;
                if (vil.X > rightX) rightX = vil.X;
                if (vil.Y < topY) topY = vil.Y;
                if (vil.Y > bottomY) bottomY = vil.Y;
            }

            return GetSpan(world, leftX, topY, rightX, bottomY, tryRespectCurrentZoom);
        }

        /// <summary>
        /// Calculates the coordinates and zoom level so all villages are visible
        /// </summary>
        public static Location GetSpan(Size world, int leftX, int topY, int rightX, int bottomY, bool tryRespectCurrentZoom = true)
        {
            int x = (leftX + rightX) / 2;
            int y = (topY + bottomY) / 2;

            int requiredWidth = world.Width / (rightX - leftX + 5);
            int requiredHeight = world.Height / (bottomY - topY + 5);
            int requiredMin = Math.Min(requiredWidth, requiredHeight);

            if (!tryRespectCurrentZoom)
            {
                // use optimal zoom level for given parameters
                return new Location(x, y, requiredMin);
            }

            // Only change zoom when villages don't fit with current zoom
            return new Location(x, y, Math.Min(requiredMin, World.Default.Map.Location.Zoom));
        }

        /// <summary>
        /// Converts a map location to the game location
        /// </summary>
        /// <remarks>Assumes the location needs to be converted for the main map</remarks>
        public Point GetGameLocation(Point loc)
        {
            return GetGameLocation(_map.Location, _map.Control.PictureWidth, _map.Control.PictureHeight, loc.X, loc.Y);
        }

        /// <summary>
        /// Converts a map location to the game location
        /// </summary>
        /// <remarks>Assumes the location needs to be converted for the main map</remarks>
        public Point GetGameLocation(int x, int y)
        {
            return GetGameLocation(_map.Location, _map.Control.PictureWidth, _map.Control.PictureHeight, x, y);
        }

        /// <summary>
        /// Converts a game location to the map location
        /// </summary>
        public Point GetGameLocation(Location sets, int mapWidth, int mapHeight, int x, int y)
        {
            // Get location from map and convert it to game location
            //int off = (int)((x - sets.X));

            int height = DisplayManager.CurrentDisplay.GetVillageHeightSpacing(sets.Zoom);
            int width = DisplayManager.CurrentDisplay.GetVillageWidthSpacing(sets.Zoom);

            int newx = (x + sets.X * width - mapWidth / 2) / width;
            int newy = (y + sets.Y * height - mapHeight / 2) / height;
            return new Point(newx, newy);
        }

        /// <summary>
        /// Converts map to game location and return the village
        /// </summary>
        public Village GetGameVillage(int x, int y)
        {
            Point game = GetGameLocation(x, y);
            if (World.Default.Villages.ContainsKey(game)) return World.Default.Villages[game];
            return null;
        }

        /// <summary>
        /// Converts the UserControle size to the game rectangle it represents
        /// </summary>
        public Rectangle GetGameRectangle(Rectangle fullMap)
        {
            Point leftTop = GetGameLocation(fullMap.Left, fullMap.Top);
            Point rightBottom = GetGameLocation(fullMap.Right, fullMap.Bottom);
            return new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);
        }
        #endregion
    }
}
