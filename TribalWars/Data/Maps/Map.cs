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
    public class Map
    {
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
        public Display Display { get; private set; }

        /// <summary>
        /// Gets or sets the Map location &amp; zoom level
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// Gets the home position of the map
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
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new MiniMap
        /// </summary>
        public Map(Map mainMap)
        {
            EventPublisher = new Publisher(this);
            Display = new Display(this, DisplayTypes.MiniMap);
            MarkerManager = mainMap.MarkerManager;
            Manipulators = new ManipulatorManagerController(this, mainMap);
        }

        /// <summary>
        /// Creates a new Map
        /// </summary>
        public Map()
        {
            EventPublisher = new Publisher(this);
            Display = new Display(this);
            MarkerManager = new MarkerManager(this);
            Manipulators = new ManipulatorManagerController(this);
        }

        /// <summary>
        /// Sets the UserControl for the Map Class
        /// </summary>
        public void InitializeMap(ScrollableMapControl map)
        {
            Control = map;
            Control.SetMap(this);
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
        public void SetCenter(int x, int y)
        {
            SetCenter(this, new Location(x, y, Location.Zoom), false);
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
            Location location = TribalWars.Data.Maps.Display.GetSpan(villages, false);
            SetCenter(location);
        }

        /// <summary>
        /// Changes the x and y coordinates and the zoom level
        /// </summary>
        public void SetCenter(int x, int y, int zoom)
        {
            SetCenter(this, new Location(x, y, zoom), false);
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
                    HomeLocation = new Location(value);

                DisplayBase.ZoomInfo info = Display.DisplayManager.CurrentDisplay.Zoom;
                if (value.Zoom < info.Minimum)
                    value = new Location(value, info.Minimum);
                else if (value.Zoom > info.Maximum)
                    value = new Location(value, info.Maximum);

                if (!value.Equals(Location) || forceRaiseEvent)
                {
                    Location oldLocation = Location;
                    Location = value;
                    EventPublisher.SetMapCenter(sender, new MapLocationEventArgs(value, oldLocation, info));
                }
            }
            else
                Location = null;
        }

        /// <summary>
        /// Changes the current view on the map
        /// </summary>
        public void ChangeDisplay()
        {
            ChangeDisplay(Display.DisplayManager.CurrentDisplayType);
        }

        /// <summary>
        /// Resets the map to allow loading of new settings
        /// </summary>
        /// <remarks>Resets the minipulators and display</remarks>
        public void ChangeDisplay(DisplayTypes display)
        {
            ChangeDisplay(display, Location);
        }

        /// <summary>
        /// Resets the map to allow loading of new settings
        /// </summary>
        /// <remarks>Resets the minipulators and display</remarks>
        public void ChangeDisplay(DisplayTypes display, Location location)
        {
            if (Display.DisplayManager.CurrentDisplayType != display)
            {
                if (Display.DisplayManager.CurrentDisplay != null)
                    Display.DisplayManager.CurrentDisplay.Zoom.Current = location.Zoom;
                Display.Reset(display);
                SetCenter(this, new Location(location, Display.DisplayManager.CurrentDisplay.Zoom.Current), true);

                EventPublisher.SetDisplayType(this, new MapDisplayTypeEventArgs(display, Location));
            }
        }

        /// <summary>
        /// Center on home location
        /// </summary>
        public void GoHome()
        {
            if (HomeDisplay != Display.DisplayManager.CurrentDisplayType)
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
    }
}
