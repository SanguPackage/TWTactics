#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
        #region Fields
        private Display _mapDisplay;
        private Location _mapLocation;
        private Location _mapStartLocation;
        private ScrollableMapControl _mapControl;
        private MarkerManager _markerManager;

        private Publisher _eventPublisher;

        private ManipulatorManagerController _manipulators;
        #endregion

        #region Properties
        /// <summary>
        /// Gets all villages to mark
        /// </summary>
        public MarkerManager MarkerManager
        {
            get { return _markerManager; }
        }

        /// <summary>
        /// Gets the object that interacts with the user
        /// </summary>
        public ManipulatorManagerController Manipulators
        {
            get { return _manipulators; }
        }

        /// <summary>
        /// Gets the object that encapsulates event raising
        /// </summary>
        public Publisher EventPublisher
        {
            get { return _eventPublisher; }
        }

        /// <summary>
        /// Gets the map visual settings
        /// </summary>
        public Display Display
        {
            get { return _mapDisplay; }
        }

        /// <summary>
        /// Gets or sets the Map location &amp; zoom level
        /// </summary>
        public Location Location
        {
            get { return _mapLocation; }
        }

        /// <summary>
        /// Gets the original position of the map
        /// </summary>
        public Location StartLocation
        {
            get { return _mapStartLocation; }
        }

        /// <summary>
        /// Gets the map UserControl
        /// </summary>
        public ScrollableMapControl Control
        {
            get { return _mapControl; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new MiniMap
        /// </summary>
        public Map(Map mainMap)
        {
            _eventPublisher = new Publisher(this);
            _mapDisplay = new Display(this, DisplayTypes.MiniMap);
            _markerManager = mainMap.MarkerManager;
            _manipulators = new ManipulatorManagerController(this, mainMap);
        }

        /// <summary>
        /// Creates a new Map
        /// </summary>
        public Map()
        {
            _eventPublisher = new Publisher(this);
            _mapDisplay = new Display(this);
            _markerManager = new MarkerManager(this);
            _manipulators = new ManipulatorManagerController(this);
        }

        /// <summary>
        /// Sets the UserControl for the Map Class
        /// </summary>
        public void InitializeMap(ScrollableMapControl map)
        {
            _mapControl = map;
            _mapControl.SetMap(this);
        }

        /// <summary>
        /// Sets the UserControl for the Map Class
        /// </summary>
        public void InitializeMap(MapControl map)
        {
            _mapControl = map.ScrollableMap;
            _mapControl.SetMap(this);
            map.SetMap(this);
        }

        /// <summary>
        /// Sets the UserControl for the Map Class and start it as a minimap
        /// </summary>
        public void InitializeMap(MiniMapControl miniMap, Map mainMap)
        {
            _mapControl = miniMap;
            _mapControl.SetMap(this);
            miniMap.SetMap(this, mainMap);
        }
        #endregion

        #region Change Map Center
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
                if (_mapLocation == null)
                    _mapStartLocation = new Location(value);

                DisplayBase.ZoomInfo info = _mapDisplay.DisplayManager.CurrentDisplay.Zoom;
                if (value.Zoom < info.Minimum)
                    value = new Location(value, info.Minimum);
                else if (value.Zoom > info.Maximum)
                    value = new Location(value, info.Maximum);

                if (!value.Equals(_mapLocation) || forceRaiseEvent)
                {
                    Location oldLocation = _mapLocation;
                    _mapLocation = value;
                    _eventPublisher.SetMapCenter(sender, new MapLocationEventArgs(value, oldLocation, info));
                }
            }
            else
                _mapLocation = null;
        }

        /// <summary>
        /// Changes the current view on the map
        /// </summary>
        public void ChangeDisplay()
        {
            ChangeDisplay(_mapDisplay.DisplayManager.CurrentDisplayType);
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
            if (_mapDisplay.DisplayManager.CurrentDisplayType != display)
            {
                if (_mapDisplay.DisplayManager.CurrentDisplay != null)
                    _mapDisplay.DisplayManager.CurrentDisplay.Zoom.Current = location.Zoom;
                _mapDisplay.Reset(display);
                SetCenter(this, new Location(location, _mapDisplay.DisplayManager.CurrentDisplay.Zoom.Current), true);

                _eventPublisher.SetDisplayType(this, new MapDisplayTypeEventArgs(display, Location));
            }
        }
        #endregion

        #region User Cursor
        /// <summary>
        /// Resets the cursor of the map
        /// </summary>
        public void SetCursor()
        {
            _mapControl.Cursor = System.Windows.Forms.Cursors.Default;
        }

        /// <summary>
        /// Changes the cursor of the map
        /// </summary>
        public void SetCursor(System.Windows.Forms.Cursor cursor)
        {
            _mapControl.Cursor = cursor;
        }
        #endregion
    }
}
