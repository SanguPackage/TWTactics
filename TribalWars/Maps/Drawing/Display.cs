#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using TribalWars.Maps.Drawing.Displays;
using TribalWars.Maps.Drawing.Drawers;
using TribalWars.Maps.Drawing.Helpers;
using TribalWars.Maps.Manipulators;
using TribalWars.Maps.Markers;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Drawing
{
    /// <summary>
    /// Manages the painting of a TW map
    /// </summary>
    public sealed class Display : IDisposable
    {
        #region Fields
        private readonly Map _map;
        private readonly MarkerManager _markers;

        private Rectangle _visibleGameRectangle;
        private Rectangle _canvasRectangle;

        private Bitmap _background;

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
        public void UpdateLocation(Size canvasSize, Location oldLocation, Location newLocation)
        {
            //if (oldLocation == null || oldLocation.Display != newLocation.Display || oldLocation.Zoom != newLocation.Zoom
            //    || _background == null || _visibleGameRectangle.IsEmpty || canvasSize != _canvasRectangle.Size)
            //{
                ResetCache();
                _visibleGameRectangle = GetGameRectangle();
                //if (_background != null && canvasSize != _background.Size)
                //{
                //    // need fix for when resizing?
                //}
            //}
            //else
            //{
            //    var newRec = GetGameRectangle();

            //    var calcer = new RegionsToDrawCalculator(_visibleGameRectangle, newRec);
            //    if (!calcer.HasIntersection)
            //    {
            //        ResetCache();
            //    }
            //    else
            //    {
            //        // we zaten hier:
            //        // new approach -> create a screenshot of what it should be and of what it is....
            //        // will be easier to see what exactly is going on with this shit :)

            //        Rectangle backgroundMove;
            //        var gameRecsToDraw = calcer.GetNonOverlappingGameRectangles(out backgroundMove);

            //        backgroundMove.X *= Dimensions.SizeWithSpacing.Width * -1;


            //        using (var g = Graphics.FromImage(_background))
            //        {
            //            // B UG: this scrolls 2x too fast or something?
            //            g.DrawImageUnscaled(_background, backgroundMove);
            //            //using (var backgroundBrush = new SolidBrush(Settings.BackgroundColor))
            //            //{
            //            //    g.FillRectangle(backgroundBrush, _canvasRectangle);
            //            //}

            //            foreach (Rectangle gameRecToDraw in gameRecsToDraw)
            //            {
            //                //var mapRecToDraw = GetMapRectangle(gameRecToDraw);
            //                //mapRecToDraw.X = 0;
            //                //mapRecToDraw.Y = 0;
            //                //Bitmap drawed = PaintBackground(mapRecToDraw, gameRecToDraw);

            //                var partialCanvas = new Rectangle(0, 0, gameRecToDraw.Width * Dimensions.SizeWithSpacing.Width, gameRecToDraw.Height * Dimensions.SizeWithSpacing.Height);
            //                var partialGameRect = gameRecToDraw;
            //                Offset(ref partialCanvas, ref partialGameRect);

                            
            //                Bitmap drawed = PaintBackground(partialCanvas, partialGameRect);

            //                // B UG: this draws the initial stuff, not the new
            //                //g.DrawImageUnscaled(drawed, new Point(0, 0));
            //                g.DrawImageUnscaled(drawed, new Point(canvasSize.Width - drawed.Width, 0));
            //                using (var p = new Pen(Color.Black))
            //                    g.DrawRectangle(p, canvasSize.Width - drawed.Width, 0, canvasSize.Width, canvasSize.Height);
            //            }

                        
                        
            //        }
            //    }
                
            //    // _background = newBackground;
            //    _visibleGameRectangle = GetGameRectangle();
            //}
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
        private void PaintCachedBackground(Graphics g)
        {
            // Also draw villages that are only partially visible at left/top
            Point mapOffset = GetMapLocation(_visibleGameRectangle.Location);
            g.DrawImageUnscaled(_background, mapOffset.X, mapOffset.Y);
            //g.DrawImageUnscaled(_background, 0, 0);
        }

        private Bitmap PaintBackground(Rectangle canvasRectangle, Rectangle gameRectangle)
        {
            var canvas = new Bitmap(canvasRectangle.Width, canvasRectangle.Height);
            using (var g = Graphics.FromImage(canvas))
            {
                using (var backgroundBrush = new SolidBrush(Settings.BackgroundColor))
                {
                    g.FillRectangle(backgroundBrush, canvasRectangle);
                }

                var calcVillagesToDisplay = new DisplayVillageCalculator(Dimensions, gameRectangle, canvasRectangle);
                foreach (var village in calcVillagesToDisplay.GetVillages())
                {
                    Paint(g, village.GameLocation, new Rectangle(village.MapLocation, Dimensions.Size));
                }

                var continentDrawer = new ContinentLinesPainter(g, Settings, Dimensions, gameRectangle, canvasRectangle);
                continentDrawer.DrawContinentLines();
            }
            return canvas;
        }

        /// <summary>
        /// To take the _mapOffset into account
        /// </summary>
        private void Offset(ref Rectangle canvasRectangle, ref Rectangle gameRectangle)
        {
            canvasRectangle.Width += Dimensions.SizeWithSpacing.Width;
            canvasRectangle.Height += Dimensions.SizeWithSpacing.Height;

            gameRectangle.Width += 1;
            gameRectangle.Height += 1;
        }

        /// <summary>
        /// Paints the canvas
        /// </summary>
        public void Paint(Graphics g2, Rectangle canvasRectangle)
        {
            if (_background == null)
            {
                //Debug.WriteLine("passed for Paint " + fullMap.ToString());
                //var timing = Stopwatch.StartNew();

                var largerThanCanvas = canvasRectangle;
                var largerGameRectangle = GetGameRectangle();

                Offset(ref largerThanCanvas, ref largerGameRectangle);
                _background = PaintBackground(largerThanCanvas, largerGameRectangle);
                _visibleGameRectangle = GetGameRectangle();
                _canvasRectangle = canvasRectangle;

                //timing.Stop();
                //Debug.WriteLine("Painting NEW:{0} in {1}", _map.Location, timing.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
            }

            PaintCachedBackground(g2);
        }

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
                    BackgroundDrawerData mainData = World.Default.Views.GetBackgroundDrawerData(village, marker);
                    if (mainData != null)
                    {
                        finalCache = _drawerFactoryStrategy.CreateVillageDrawer(village.Bonus, mainData, marker);
                        if (finalCache != null)
                        {
                            finalCache.PaintVillage(g, mapVillage);

                            if (_drawerFactoryStrategy.SupportDecorators && village.Type != VillageType.None)
                            {
                                // Paint extra village decorators
                                foreach (DrawerBase decorator in World.Default.Views.GetDecoratorDrawers(_drawerFactoryStrategy, village, mainData))
                                {
                                    decorator.PaintVillage(g, mapVillage);
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
        #endregion

        #region IsVisible
        /// <summary>
        /// Gets a value indicating whether at least one village is currently visible
        /// </summary>
        public bool IsVisible(IEnumerable<Village> villages)
        {
            return villages.Any(village => _visibleGameRectangle.Contains(village.Location));
        }
        #endregion

        #region Game from/to Map Converters
        /// <summary>
        /// Gets the minimum zoom level that can display villages as big as the parameter.
        /// But only change zoom when villages don't fit with current zoom.
        /// </summary>
        /// <param name="maxVillageSize">Get the zoom for villages this big</param>
        /// <param name="tryStayInCurrentZoom">True: Try to respect the current zoom level and only change zoom level when really required</param>
        /// <param name="couldSatisfy">HACK to get an IconDisplay to switch to ShapeDisplay with Map.GetSpan</param>
        public int GetMinimumZoomLevel(Size maxVillageSize, bool tryStayInCurrentZoom, out bool couldSatisfy)
        {
            return _drawerFactoryStrategy.GetMinimumZoomLevel(maxVillageSize, tryStayInCurrentZoom, out couldSatisfy);
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

        #region Other
        public void Dispose()
        {
        }

        public override string ToString()
        {
            return string.Format("Map={0}, Visible={1}, VillageSize={2}", _map, _visibleGameRectangle, _drawerFactoryStrategy != null ? Dimensions.Size.ToString() : "NotInited");
        }
        #endregion
    }
}
