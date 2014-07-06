#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TribalWars.Data.Maps.Manipulators.Managers;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Drawers;
using System.Drawing;
using TribalWars.Data.Events;
using TribalWars.Data.Maps.Manipulators;
using TribalWars.Controls.Maps;
using TribalWars.Data.Maps.Views;
using TribalWars.Data.Maps.Markers;
using TribalWars.Data.Maps.Displays;
#endregion

namespace TribalWars.Data.Maps
{
    /// <summary>
    /// Representation of a TW map
    /// </summary>
    public sealed class Map
    {
        #region Fields
        private Display _display;
        #endregion

        #region Properties
        /// <summary>
        /// Gets all villages to mark
        /// </summary>
        public MarkerManager MarkerManager { get; private set; }

        /// <summary>
        /// Gets the object that interacts with the user
        /// </summary>
        public ManipulatorManagerController Manipulators { get; private set; }

        /// <summary>
        /// Gets the object that encapsulates event raising
        /// </summary>
        public Publisher EventPublisher { get; private set; }

        /// <summary>
        /// Gets the map visual settings
        /// </summary>
        public Display Display
        {
            get { return _display; }
            private set
            {
                if (_display != null)
                {
                    _display.Dispose();
                }
                _display = value;
            }
        }

        /// <summary>
        /// Size of the map canvas
        /// </summary>
        public Size CanvasSize
        {
            get { return Control.ClientRectangle.Size; }
        }

        /// <summary>
        /// Gets or sets the Map location &amp; zoom level
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// Gets or sets the home position of the map
        /// </summary>
        public Location HomeLocation { get; set; }

        /// <summary>
        /// Gets the home icon/shape display on the map
        /// </summary>
        public DisplayTypes HomeDisplay { get; set; }

        /// <summary>
        /// Gets the map UserControl
        /// </summary>
        public ScrollableMapControl Control { get; private set; }

        /// <summary>
        /// Only after a map was painted, start reacting to events etc
        /// </summary>
        public bool HasPainted
        {
            get { return Location != null; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new MiniMap
        /// </summary>
        public Map(Map mainMap)
        {
            EventPublisher = new Publisher(this);
            MarkerManager = mainMap.MarkerManager;
            Manipulators = new ManipulatorManagerController(this, mainMap);
        }

        /// <summary>
        /// Creates a new Map
        /// </summary>
        public Map()
        {
            EventPublisher = new Publisher(this);
            MarkerManager = new MarkerManager();
            Manipulators = new ManipulatorManagerController(this);
        }

        public void InitializeDisplay(DisplaySettings settings, DisplayTypes type)
        {
            Display = new Display(settings, this, type);
        }

        /// <summary>
        /// Sets the UserControl for the Map Class
        /// </summary>
        public void InitializeMap(MapControl map)
        {
            Control = map.ScrollableMap;
            Control.SetMap(this);
            map.SetMap(this);
        }

        /// <summary>
        /// Sets the UserControl for the Map Class and start it as a minimap
        /// </summary>
        public void InitializeMap(MiniMapControl miniMap, Map mainMap)
        {
            Control = miniMap;
            Control.SetMap(this);
            miniMap.SetMap(this, mainMap);
        }
        #endregion

        #region Change Map Center
        /// <summary>
        /// Center on middle of a continent
        /// </summary>
        public void SetCenterContinent(int continent)
        {
            // TODO: Unused method. But functionality exists somewhere else?
            if (continent <= 99 && continent >= 0)
            {
                int x = continent % 10 * 100 + 50;
                int y = (continent - continent % 10) * 10 + 50;
                SetCenter(this, new Location(x, y, Location.Zoom), false);
            }
        }

        /// <summary>
        /// Changes the x and y coordinates
        /// </summary>
        public void SetCenter(Point point)
        {
            SetCenter(this, new Location(point.X, point.Y, Location.Zoom), false);
        }

        /// <summary>
        /// Changes the zoom level
        /// </summary>
        public void SetCenter(int zoom)
        {
            SetCenter(this, new Location(Location.X, Location.Y, zoom), false);
        }

        /// <summary>
        /// Changes the x and y coordinates
        /// </summary>
        public void SetCenter(Village village)
        {
            SetCenter(this, new Location(village.X, village.Y, Location.Zoom), false);
        }

        /// <summary>
        /// Changes center so that all villages are visible
        /// </summary>
        public void SetCenter(IEnumerable<Village> villages)
        {
            Debug.Assert(villages != null);
            Location location = GetSpan(villages, false);
            SetCenter(location);
        }

        /// <summary>
        /// Calculates the coordinates and zoom level so all villages are visible
        /// </summary>
        /// <param name="vils">Villages that have to be visible</param>
        /// <param name="tryRespectCurrentZoom">False: use optimal zoom level, True: try to keep current zoom level</param>
        private Location GetSpan(IEnumerable<Village> vils, bool tryRespectCurrentZoom = true)
        {
            int leftX = 999, topY = 999, rightX = 0, bottomY = 0;
            foreach (Village vil in vils)
            {
                if (vil.X < leftX) leftX = vil.X;
                if (vil.X > rightX) rightX = vil.X;
                if (vil.Y < topY) topY = vil.Y;
                if (vil.Y > bottomY) bottomY = vil.Y;
            }

            return GetSpan(leftX, topY, rightX, bottomY, tryRespectCurrentZoom);
        }

        /// <summary>
        /// Calculates the coordinates and zoom level so all villages are visible
        /// </summary>
        private Location GetSpan(int leftX, int topY, int rightX, int bottomY, bool tryRespectCurrentZoom = true)
        {
            // BUG: This only works for ShapeDisplay. IconDisplay zoom levels are disconnected from villageHeight/Width
            // requiredWidth/Height think that zoom level 5 equals a villageWidth/Height of 5

            int x = (leftX + rightX) / 2;
            int y = (topY + bottomY) / 2;

            int requiredWidth = CanvasSize.Width / (rightX - leftX + 5);
            int requiredHeight = CanvasSize.Height / (bottomY - topY + 5);

            // TODO: This is the calculation that we need to make a strategy for
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
        /// Changes the x and y coordinates and the zoom level
        /// </summary>
        public void SetCenter(Point loc, int zoom)
        {
            SetCenter(this, new Location(loc, zoom), false);
        }

        /// <summary>
        /// Changes the x and y coordinates and the zoom level
        /// </summary>
        public void SetCenter(Location loc, int zoom)
        {
            SetCenter(this, new Location(loc, zoom), false);
        }

        /// <summary>
        /// Changes the center of the map
        /// </summary>
        public void SetCenter(Location loc)
        {
            SetCenter(this, loc, false);
        }

        /// <summary>
        /// Changes the center of the map
        /// </summary>
        public void SetCenter(object sender, Location loc)
        {
            SetCenter(sender, loc, false);
        }

        /// <summary>
        /// Changes the center of the map
        /// </summary>
        private void SetCenter(object sender, Location value, bool forceRaiseEvent)
        {
            if (value != null)
            {
                if (Location == null)
                {
                    HomeLocation = new Location(value);
                }

                DisplayBase.ZoomInfo info = Display.CurrentDisplay.Zoom;
                if (value.Zoom < info.Minimum)
                {
                    value = new Location(value, info.Minimum);
                }
                else if (value.Zoom > info.Maximum)
                {
                    value = new Location(value, info.Maximum);
                }

                if (!value.Equals(Location) || forceRaiseEvent)
                {
                    Location oldLocation = Location;
                    Location = value;
                    EventPublisher.SetMapCenter(sender, new MapLocationEventArgs(value, oldLocation, info));
                }
            }
            else
            {
                Location = null;
            }
        }

        /// <summary>
        /// Resets the map to allow loading of new settings
        /// </summary>
        /// <remarks>Resets the minipulators and display</remarks>
        public void ChangeDisplay(DisplayTypes display, int zoom)
        {
            ChangeDisplay(display, new Location(Location, zoom));
        }

        /// <summary>
        /// Resets the map to allow loading of new settings
        /// </summary>
        /// <remarks>Resets the minipulators and display</remarks>
        public void ChangeDisplay(DisplayTypes display, Location location, bool forceDisplay = false)
        {
            if (forceDisplay || Display.CurrentDisplay.Type != display)
            {
                Display = new Display(Display.Settings, this, display);

                EventPublisher.SetDisplayType(this, new MapDisplayTypeEventArgs(display));

                SetCenter(this, new Location(location), true);
            }
        }

        /// <summary>
        /// Center on home location
        /// </summary>
        public void GoHome()
        {
            if (HomeDisplay != Display.CurrentDisplay.Type)
            {
                ChangeDisplay(HomeDisplay, HomeLocation);
            }
            else
            {
                SetCenter(HomeLocation);
            }
            
        }
        #endregion

        #region User Cursor
        /// <summary>
        /// Resets the cursor of the map
        /// </summary>
        public void SetCursor()
        {
            Control.Cursor = System.Windows.Forms.Cursors.Default;
        }

        /// <summary>
        /// Changes the cursor of the map
        /// </summary>
        public void SetCursor(System.Windows.Forms.Cursor cursor)
        {
            Control.Cursor = cursor;
        }
        #endregion

        public void Invalidate(bool resetBackgroundCache = true)
        {
            if (resetBackgroundCache)
            {
                Display.ResetCache();
            }
            Control.Invalidate();
        }
    }
}
