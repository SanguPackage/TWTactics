#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using TribalWars.Data.Maps.Markers;
using TribalWars.Data.Tribes;
using TribalWars.Data.Players;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Maps.Views;
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
        private Map _map;

        private bool _continentLines;
        private bool _provinceLines;
        private bool _hideAbandoned;
        private bool _markedOnly;

        private Pen _continentPen;
        private Pen _provincePen;
        private Rectangle? _visibleRectangle;

        private Brush _backgroundBrush;
        private Color _backgroundColor;

        private DisplayManager _displayManager;

        private Bitmap _background;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the manager that holds
        /// Shape &amp; Icon displays
        /// </summary>
        public DisplayManager DisplayManager
        {
            get { return _displayManager; }
        }

        /// <summary>
        /// Gets the brush used to paint the entire map canvas
        /// </summary>
        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set
            {
                if (_backgroundBrush != null)
                    _backgroundBrush.Dispose();
                _backgroundBrush = value;
            }
        }

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
        public bool ContinentLines
        {
            get { return _continentLines; }
            set { _continentLines = value; }
        }

        /// <summary>
        /// Gets a value indicating whether province lines should be drawn
        /// </summary>
        public bool ProvinceLines
        {
            get { return _provinceLines; }
            set { _provinceLines = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating wether abandoned 
        /// villages should be shown on the map
        /// </summary>
        public bool HideAbandoned
        {
            get { return _hideAbandoned; }
            set { _hideAbandoned = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating wether unmarked
        /// villages should be shown on the map
        /// </summary>
        public bool MarkedOnly
        {
            get { return _markedOnly; }
            set { _markedOnly = value; }
        }
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

            _displayManager = new DisplayManager(map, displayType);

            _continentPen = new Pen(Color.Black, 1);
            _continentPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            _provincePen = new Pen(Color.FromArgb(42, 94, 31), 1f);
            _provincePen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

            _map.EventPublisher.LocationChanged += new EventHandler<TribalWars.Data.Events.MapLocationEventArgs>(EventPublisher_LocationChanged);
        }

        private void EventPublisher_LocationChanged(object sender, TribalWars.Data.Events.MapLocationEventArgs e)
        {
            // TODO: if there is some overlap between e.NewLocation and e.OldLocation
            // redraw only the new part of the background and move the rest
            // keep a _drawnRectangle variable that represents the drawn part of the background
            _background = null;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Resets the views of the Map Display
        /// </summary>
        public void Reset(DisplayTypes type)
        {
            _displayManager.Reset(type);
            _background = null;
        }

        /// <summary>
        /// Deletes one village from the cached image
        /// </summary>
        public void ResetCache(Village village)
        {
            // TODO: redraw village only on the background!!!
            _background = null;
        }

        /// <summary>
        /// Deletes the entire cached image
        /// </summary>
        public void ResetCache()
        {
            _background = null;
        }

        /// <summary>
        /// Paints the canvas
        /// </summary>
        public void Paint(Graphics g2, Rectangle rec, Rectangle fullMap)
        {
            /*if (rec != fullMap)
            {
                int x = 0;
            }*/

            
            if (_background != null)
            {
                //g2.DrawImage(_background, rec.X, rec.Y, rec, GraphicsUnit.Pixel);
                g2.DrawImageUnscaled(_background, 0, 0);
            }
            else
            {
                System.Diagnostics.Stopwatch timing = System.Diagnostics.Stopwatch.StartNew();

                _background = new Bitmap(_map.Control.PictureWidth, _map.Control.PictureHeight);
                Graphics g = Graphics.FromImage(_background);

                Point gameTopLeft = GetGameLocation(fullMap.Left, fullMap.Top);
                Point gameBottomRight = GetGameLocation(fullMap.Right, fullMap.Bottom);
                _visibleRectangle = new Rectangle(gameTopLeft.X, gameTopLeft.Y, gameBottomRight.X - gameTopLeft.X, gameBottomRight.Y - gameTopLeft.Y);

                int xOffset = GetMapLocation(gameTopLeft).X;
                int yOffset = GetMapLocation(gameTopLeft).Y;

                DisplayBase displayType = _displayManager.CurrentDisplay;
                int zoom = _map.Location.Zoom;
                int width = displayType.GetVillageWidthSpacing(zoom);
                int height = displayType.GetVillageHeightSpacing(zoom);
                int mapX = 0;
                int mapY = 0;

                int villageWidth = displayType.GetVillageWidth(zoom);
                int villageHeight = displayType.GetVillageHeight(zoom);

                int villagesDrawed = 0;
                int looped = 0;

                // Draw villages
                mapX = fullMap.Left + xOffset;
                mapY = fullMap.Top + yOffset;
                if (false)
                {
                    // Different way to loop over the villages
                    // timing is pretty much the same...
                    /*for (int gameY = gameTopLeft.Y; gameY <= gameBottomRight.Y; gameY++)
                    {
                        mapX = fullMap.Left + xOffset;
                        for (int gameX = gameTopLeft.X; gameX <= gameBottomRight.X; gameX++)
                        {
                            _viewManager.Paint(g, new Point(gameX, gameY), mapX, mapY, villageWidth, villageHeight);
                            mapX += width;
                            villagesDrawed++;
                        }
                        mapY += height;
                    }*/
                }
                else
                {
                    int gameX = 0; 
                    int gameY = gameTopLeft.Y;
                    g.FillRectangle(_backgroundBrush, fullMap);
                    for (int yMap = mapY; yMap <= fullMap.Bottom; yMap += height)
                    {
                        gameX = gameTopLeft.X;
                        for (int xMap = mapX; xMap <= fullMap.Right; xMap += width)
                        {
                            _displayManager.Paint(g, new Point(gameX, gameY), xMap, yMap, villageWidth, villageHeight);
                            gameX += 1;
                            villagesDrawed++;
                        }
                        gameY += 1;
                    }
                }



                //_background = new Bitmap(_map.Control.PictureWidth, _map.Control.PictureHeight);
                //Graphics g = Graphics.FromImage(_background);

                //Point gameTopLeft = GetGameLocation(fullMap.Left, fullMap.Top);
                //Point gameBottomRight = GetGameLocation(fullMap.Right, fullMap.Bottom);

                //int xOffset = GetMapLocation(gameTopLeft).X;
                //int yOffset = GetMapLocation(gameTopLeft).Y;

                //DisplayBase displayType = _viewManager.CurrentDisplay;
                //int zoom = _map.Location.Zoom;
                //int width = displayType.GetVillageWidthSpacing(zoom);
                //int height = displayType.GetVillageHeightSpacing(zoom);
                //int mapX = 0;
                //int mapY = 0;

                //int villageWidth = displayType.GetVillageWidth(zoom);
                //int villageHeight = displayType.GetVillageHeight(zoom);

                //// Draw villages
                //mapX = fullMap.Left + xOffset;
                //mapY = fullMap.Top + yOffset;
                //for (int gameY = gameTopLeft.Y; gameY <= gameBottomRight.Y; gameY++)
                //{
                //    mapX = fullMap.Left + xOffset;
                //    for (int gameX = gameTopLeft.X; gameX <= gameBottomRight.X; gameX++)
                //    {
                //        _viewManager.Paint(g, new Point(gameX, gameY), mapX, mapY, villageWidth, villageHeight);
                //        mapX += width;
                //    }
                //    mapY += height;
                //}












                // Horizontal continent lines
                int gridOffset = 5;
                mapY = fullMap.Top + yOffset - (gameTopLeft.Y % gridOffset) * height;
                for (int gameY = gameTopLeft.Y - (gameTopLeft.Y % gridOffset); gameY <= gameBottomRight.Y; gameY += gridOffset)
                {
                    if (gameY > 0 && gameY < 1000)
                    {
                        if (gameY % 100 == 0)
                            g.DrawLine(_continentPen, 0, mapY, fullMap.Width, mapY);
                        else //if (gameY % 10 == 0)
                            g.DrawLine(_provincePen, 0, mapY, fullMap.Width, mapY);
                        //else
                        //    g.DrawLine(pen, 0, mapY, fullMap.Width, mapY);
                    }
                    mapY += height * gridOffset;
                }

                // Vertical continent lines
                mapX = fullMap.Left + xOffset - (gameTopLeft.X % gridOffset) * width;
                for (int gameX = gameTopLeft.X - (gameTopLeft.X % gridOffset); gameX <= gameBottomRight.X; gameX += gridOffset)
                {
                    if (gameX > 0 && gameX < 1000)
                    {
                        if (gameX % 100 == 0)
                            g.DrawLine(_continentPen, mapX, 0, mapX, fullMap.Height);
                        else //if (gameX % 10 == 0)
                            g.DrawLine(_provincePen, mapX, 0, mapX, fullMap.Height);
                        //else
                        //    g.DrawLine(pen, mapX, 0, mapX, fullMap.Height);
                    }
                    mapX += width * gridOffset;
                }

                timing.Stop();
                Debug.WriteLine(string.Format("Painting:{0} in {1}", _map.Location.ToString(), timing.Elapsed.TotalSeconds.ToString()));
                Debug.WriteLine("Drawed : " + villagesDrawed.ToString() + " for " + looped.ToString());

                g2.DrawImageUnscaled(_background, 0, 0);
            }
        }

        /// <summary>
        /// Gets a value indicating whether a tribe is currently visible
        /// </summary>
        public bool IsVisible(Tribe tribe)
        {
            if (!_visibleRectangle.HasValue) return false;
            foreach (Village village in tribe)
                if (_visibleRectangle.Value.Contains(village.Location))
                    return true;
            return false;
        }

        /// <summary>
        /// Gets a value indicating whether a player is currently visible
        /// </summary>
        public bool IsVisible(Player player)
        {
            if (!_visibleRectangle.HasValue) return false;
            foreach (Village village in player)
                if (_visibleRectangle.Value.Contains(village.Location)) return true;
            return false;
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
            int height = _displayManager.CurrentDisplay.GetVillageHeightSpacing(sets.Zoom);
            int width = _displayManager.CurrentDisplay.GetVillageWidthSpacing(sets.Zoom);

            int off = (int)((x - sets.X) * width);
            x = off + mapWidth / 2; // +(2 * x - 1);
            off = (int)((y - sets.Y) * height);
            y = off + (mapHeight / 2); // +(2 * y - 1);
            return new Point(x, y);
        }


        /// <summary>
        /// Calculates the coordinates and zoom level so all villages are visible
        /// </summary>
        /// <param name="vils">Villages that have to be visible</param>
        public static Location GetSpan(IEnumerable<Village> vils)
        {
            return GetSpan(vils, new Size(World.Default.Map.Control.PictureWidth, World.Default.Map.Control.PictureHeight));
        }

        /// <summary>
        /// Calculates the coordinates and zoom level so all villages are visible
        /// </summary>
        /// <param name="vils">Villages that have to be visible</param>
        public static Location GetSpan(IEnumerable<Village> vils, Size world)
        {
            int leftX = 999, topY = 999, rightX = 0, bottomY = 0;
            foreach (Village vil in vils)
            {
                if (vil.X < leftX) leftX = vil.X;
                if (vil.X > rightX) rightX = vil.X;
                if (vil.Y < topY) topY = vil.Y;
                if (vil.Y > bottomY) bottomY = vil.Y;
            }

            return GetSpan(world, leftX, topY, rightX, bottomY);
        }

        /// <summary>
        /// Calculates the coordinates and zoom level so all villages are visible
        /// </summary>
        public static Location GetSpan(Size world, int leftX, int topY, int rightX, int bottomY)
        {
            int x = (leftX + rightX) / 2;
            int y = (topY + bottomY) / 2;

            int requiredWidth = world.Width / (rightX - leftX + 5);
            int requiredHeight = world.Height / (bottomY - topY + 5);

            int zoom = Math.Max(World.Default.Map.Location.Zoom, Math.Min(requiredWidth, requiredHeight));

            return new Location(x, y, zoom);
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

            int height = _displayManager.CurrentDisplay.GetVillageHeightSpacing(sets.Zoom);
            int width = _displayManager.CurrentDisplay.GetVillageWidthSpacing(sets.Zoom);

            int newx = (int)((x + sets.X * width - mapWidth / 2) / width);
            int newy = (int)((y + sets.Y * height - mapHeight / 2) / height);
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
