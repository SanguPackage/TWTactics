#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using TribalWars.Maps.Displays;
using TribalWars.Maps.Drawers;
using TribalWars.Maps.Markers;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps
{
    /// <summary>
    /// Manages the painting of a TW map
    /// </summary>
    public sealed class Display : IDisposable
    {
        #region Fields
        private readonly Map _map;
        private readonly MarkerManager _markers;

        private Rectangle _visibleRectangle;

        private Bitmap _background; // TODO: private PainterCache _cache?
        private Painter _painter;

        private readonly DrawerFactoryBase _drawerFactoryStrategy;
        private readonly DisplaySettings _settings;
        #endregion

        #region Properties
        public DisplaySettings Settings
        {
            get { return _settings; }
        }

        public DisplayTypes Type
        {
            get { return _drawerFactoryStrategy.Type; }
        }

        public VillageDimensions Dimensions
        {
            get { return _drawerFactoryStrategy.Dimensions; }
        }

        public bool AllowText
        {
            get { return _drawerFactoryStrategy.AllowText; }
        }

        public ZoomInfo Zoom
        {
            get { return _drawerFactoryStrategy.Zoom; }
        }
        #endregion

        #region Constructors
        public Display(DisplaySettings settings, bool isMiniMap, Map map, ref Location location)
            : this(settings, map)
        {
            // Validate zoom or we have a potential divide by zero etc
            if (isMiniMap)
            {
                ZoomInfo zoom = DrawerFactoryBase.CreateMiniMapZoom(location.Zoom);
                location = zoom.Validate(location);

                _drawerFactoryStrategy = DrawerFactoryBase.CreateMiniMap(location.Zoom);
            }
            else
            {
                ZoomInfo zoom = DrawerFactoryBase.CreateZoom(location.Display, location.Zoom);
                location = ValidateZoom(zoom, location);

                _drawerFactoryStrategy = DrawerFactoryBase.Create(location.Display, location.Zoom, settings.Scenery);
            }

            // TODO: make this lazy. Setting here = crash
            // Is fixed by calling UpdateLocation after Map.Location is set
            //_visibleRectangle = GetGameRectangle();
        }

        /// <summary>
        /// Returns the <see cref="Location"/> parameter or an updated
        /// one if the zoom level is invalid for the <see cref="Display"/>
        /// </summary>
        private Location ValidateZoom(ZoomInfo zoom, Location location)
        {
            if (location.Zoom < zoom.Minimum)
            {
                return new Location(location.Display, location.Point, zoom.Minimum);
            }
            if (location.Zoom > zoom.Maximum)
            {
                // Swap to Shape/Icon
                // If this changes also check the hack in Map.GetSpan
                if (location.Display == DisplayTypes.Icon)
                {
                    return new Location(DisplayTypes.Shape, location.Point, IconDrawerFactory.MinVillageSide);
                }
                if (location.Display == DisplayTypes.Shape)
                {
                    return new Location(DisplayTypes.Icon, location.Point, IconDrawerFactory.AutoSwitchFromShapeIndex);
                }

                return new Location(location.Display, location.Point, zoom.Maximum);
            }
            return location;
        }

        private Display(DisplaySettings settings, Map map)
        {
            _settings = settings;
            _map = map;
            _markers = map.MarkerManager;
        }
        #endregion

        #region Reset Cache
        public void UpdateLocation()
        {
            ResetCache();
            _visibleRectangle = GetGameRectangle();
        }

        /// <summary>
        /// Deletes the entire cached image
        /// </summary>
        public void ResetCache()
        {
            _background = null;
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

                // Normal way: 1sec on zoom 1
                if (_painter == null || true)
                {
                    // we zaten hier:
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


        // TODO: move all painting stuff to a different class PAINTER

        /// <summary>
        /// Draws a village on the map
        /// </summary>
        /// <param name="g">The graphics object</param>
        /// <param name="game">The game location of the village</param>
        /// <param name="mapVillage">Where and how big to draw the village</param>
        private void Paint(Graphics g, Point game, Rectangle mapVillage)
        {
            if (!(game.X >= 0 && game.X < 1000 && game.Y >= 0 && game.Y < 1000))
                return;

            Village village;
            DrawerBase finalCache = null;
            if (World.Default.Villages.TryGetValue(game, out village))
            {
                Marker marker = _markers.GetMarker(Settings, village);
                if (marker != null)
                {
                    // Paint village icon/shape
                    DrawerData mainData = World.Default.GetDrawerData(village, marker.Settings.View);
                    finalCache = _drawerFactoryStrategy.CreateVillageDrawer(village.Bonus, mainData, marker);
                    if (finalCache != null)
                    {
                        finalCache.PaintVillage(g, mapVillage);

                        if (_drawerFactoryStrategy.SupportDecorators && village.Type != VillageType.None)
                        {
                            // Paint extra village decorators
                            DrawerData data = World.Default.GetDrawerData(village, "VillageType");
                            if (data != null)
                            {
                                DrawerBase decoratorVillageType = _drawerFactoryStrategy.CreateVillageDecoratorDrawer(data, marker, mainData);
                                if (decoratorVillageType != null)
                                {
                                    decoratorVillageType.PaintVillage(g, mapVillage);
                                }
                            }
                        }
                    }
                }
            }

            if (finalCache == null)
            {
                PaintNonVillage(g, game, mapVillage);
            }
        }

        /// <summary>
        /// Paint grass, mountains, ...
        /// </summary>
        private void PaintNonVillage(Graphics g, Point game, Rectangle mapVillage)
        {
            DrawerBase finalCache = _drawerFactoryStrategy.CreateNonVillageDrawer(game, mapVillage);
            if (finalCache != null)
            {
                finalCache.PaintVillage(g, mapVillage);
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
               
                _visibleGameRectangle = display.GetGameRectangle();
                // _visibleGameRectangle is sometimes negative!!

                // Also draw villages that are only partially visible at left/top
                Point mapOffset = _display.GetMapLocation(_visibleGameRectangle.Location);
                mapSize.Offset(mapOffset);
                _toPaint = mapSize;

                using (var backgroundBrush = new SolidBrush(_display.Settings.BackgroundColor))
                {
                    _g.FillRectangle(backgroundBrush, _toPaint);
                }

                var dimensions = display.Dimensions;
                _villageWidthSpacing = dimensions.SizeWithSpacing.Width;
                _villageHeightSpacing = dimensions.SizeWithSpacing.Height;

                _villageWidth = dimensions.Size.Width;
                _villageHeight = dimensions.Size.Height;

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
                        _display.Paint(_g, new Point(gameX, gameY), new Rectangle(xMap, yMap, _villageWidth, _villageHeight));
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

                if (_villageWidthSpacing > 4 && _display.Settings.ProvinceLines)
                {
                    using (var provincePen = _display.Settings.CreateProvincePen())
                    {
                        provincePen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                        DrawContinentLines(provincePen, mapMin, mapMax, gameMin, gameMax, villageSize, isHorizontal, provinceWidth, otherMapMin, otherMapMax, otherGameMin, otherGameMax, otherVillageSize);
                    }
                }

                if (_display.Settings.ContinentLines)
                {
                    using (var continentPen = _display.Settings.CreateContinentPen())
                    {
                        continentPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                        DrawContinentLines(continentPen, mapMin, mapMax, gameMin, gameMax, villageSize, isHorizontal, continentWidth, otherMapMin, otherMapMax, otherGameMin, otherGameMax, otherVillageSize);
                    }
                }
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
        /// Gets a value indicating whether at least one village is currently visible
        /// </summary>
        public bool IsVisible(IEnumerable<Village> villages)
        {
            return villages.Any(village => _visibleRectangle.Contains(village.Location));
        }
        #endregion

        #region Game from/to Map Converters
        /// <summary>
        /// Gets the minimum zoom level that can display villages as big as the parameter.
        /// But only change zoom when villages don't fit with current zoom.
        /// </summary>
        /// <param name="maxVillageSize">Get the zoom for villages this big</param>
        /// <param name="couldSatisfy">HACK to get an IconDisplay to switch to ShapeDisplay with Map.GetSpan</param>
        public int GetMinimumZoomLevel(Size maxVillageSize, out bool couldSatisfy)
        {
            return _drawerFactoryStrategy.GetMinimumZoomLevel(maxVillageSize, out couldSatisfy);
        }

        /// <summary>
        /// Converts a game location to the map location
        /// </summary>
        /// <remarks>Assumes the location needs to be converted for the main map</remarks>
        public Point GetMapLocation(Point loc)
        {
            // Get location from game and convert it to location on the map
            var villageSize = _drawerFactoryStrategy.Dimensions.SizeWithSpacing;

            int off = (loc.X - _map.Location.X) * villageSize.Width;
            loc.X = off + _map.CanvasSize.Width / 2;
            off = (loc.Y - _map.Location.Y) * villageSize.Height;
            loc.Y = off + (_map.CanvasSize.Height / 2);
            return loc;
        }

        /// <summary>
        /// 
        /// </summary>
        public Rectangle GetMapRectangle(Rectangle gameRectangle)
        {
            Point leftTop = GetMapLocation(gameRectangle.Location);
            Point rightBottom = GetMapLocation(new Point(gameRectangle.Right, gameRectangle.Bottom));
            return new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);
        }

        /// <summary>
        /// Converts a map location to the game location
        /// </summary>
        /// <remarks>Assumes the location needs to be converted for the main map</remarks>
        public Point GetGameLocation(Point loc)
        {
            // Get location from map and convert it to game location
            var villageSize = _drawerFactoryStrategy.Dimensions.SizeWithSpacing;

            int newx = (loc.X + (_map.Location.X * villageSize.Width) - (_map.CanvasSize.Width / 2)) / villageSize.Width;
            int newy = (loc.Y + (_map.Location.Y * villageSize.Height) - (_map.CanvasSize.Height / 2)) / villageSize.Height;
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

        #region IDisposable
        public void Dispose()
        {
        }
        #endregion
    }
}
