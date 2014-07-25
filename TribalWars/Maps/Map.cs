#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Janus.Windows.Common;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls;
using System.Drawing;
using TribalWars.Maps.Controls;
using TribalWars.Maps.Displays;
using TribalWars.Maps.Icons;
using TribalWars.Maps.Manipulators;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Maps.Markers;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Villages;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Maps
{
    /// <summary>
    /// Representation of a TW map
    /// </summary>
    public sealed class Map
    {
        #region Fields
        private Display _display;
        private ScrollableMapControl _control;
        private Location _location;
        private readonly JanusSuperTip _toolTip;
        private IContextMenu _activeContextMenu;
        private DisplaySettings _displaysettings;
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
            get { return _control.ClientRectangle.Size; }
        }

        /// <summary>
        /// Gets or sets the Map location &amp; zoom level
        /// </summary>
        public Location Location
        {
            get { return _location; }
            private set
            {
                if (_location != value)
                {
                    _location = value;
                    Display.UpdateLocation(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the home position of the map
        /// </summary>
        public Location HomeLocation { get; set; }

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

            _toolTip = JanusControls.CreateTooltip();
            SetTooltipProperties();
        }

        /// <summary>
        /// Creates a new Map
        /// </summary>
        public Map()
        {
            EventPublisher = new Publisher(this);
            MarkerManager = new MarkerManager();
            Manipulators = new ManipulatorManagerController(this);

            _toolTip = JanusControls.CreateTooltip();
            SetTooltipProperties();
        }

        private void SetTooltipProperties()
        {
            _toolTip.InitialDelay = 400;
            _toolTip.AutoPopDelay = 6000;
            //_toolTip.ToolTipPopUp += (sender, args) =>
            //    {
            //        Debug.WriteLine("------------->" + _control.Cursor + " setting to " + _cursor);
            //        _control.Cursor = _cursor;
            //    };
        }

        public void InitializeMiniMapDisplay(DisplaySettings settings)
        {
            _displaysettings = settings;
            Display = Display.CreateMiniMapDisplay(settings, this);
        }

        public void SetDisplaySettings(DisplaySettings settings)
        {
            _displaysettings = settings;
        }

        /// <summary>
        /// Sets the UserControl for the Map Class
        /// </summary>
        public void InitializeMap(MapControl map)
        {
            _control = map.ScrollableMap;
            _control.SetMap(this);
            map.SetMap(this);
        }

        /// <summary>
        /// Sets the UserControl for the Map Class and start it as a minimap
        /// </summary>
        public void InitializeMap(MiniMapControl miniMap, Map mainMap)
        {
            _control = miniMap;
            _control.SetMap(this);
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
                SetCenter(this, new Location(Location.Display, x, y, Location.Zoom));
            }
        }

        /// <summary>
        /// Changes the zoom level
        /// </summary>
        public void SetZoomLevel(int zoom)
        {
            SetCenter(this, new Location(Location.Display, Location.X, Location.Y, zoom));
        }

        /// <summary>
        /// Changes the zoom level
        /// </summary>
        public void IncreaseZoomLevel(int amount)
        {
            if (Display.Type == DisplayTypes.Icon) amount *= -1;
            SetCenter(this, new Location(Location.Display, Location.X, Location.Y, Location.Zoom + amount));
        }

        /// <summary>
        /// Changes the x and y coordinates
        /// </summary>
        public void SetCenter(Point point)
        {
            SetCenter(this, new Location(Location.Display, point.X, point.Y, Location.Zoom));
        }

        /// <summary>
        /// Changes center so that all villages are visible
        /// </summary>
        public void SetCenter(IEnumerable<Village> villages)
        {
            Debug.Assert(villages != null);
            Location location = GetSpan(villages);
            SetCenter(location);
        }

        /// <summary>
        /// Calculates the coordinates and zoom level so all villages are visible
        /// </summary>
        /// <param name="vils">Villages that have to be visible</param>
        private Location GetSpan(IEnumerable<Village> vils)
        {
            int leftX = 999, topY = 999, rightX = 0, bottomY = 0;
            foreach (Village vil in vils)
            {
                if (vil.X < leftX) leftX = vil.X;
                if (vil.X > rightX) rightX = vil.X;
                if (vil.Y < topY) topY = vil.Y;
                if (vil.Y > bottomY) bottomY = vil.Y;
            }

            return GetSpan(new Rectangle(leftX, topY, rightX - leftX, bottomY - topY));
        }

        /// <summary>
        /// Calculates the coordinates and zoom level so all villages are visible
        /// </summary>
        public Location GetSpan(Rectangle game, int villagesExtraVisible = 5)
        {
            var middle = new Point(
                (game.Left + game.Right) / 2, 
                (game.Top + game.Bottom) / 2);

            var maxVillageSize = new Size(
                CanvasSize.Width / (game.Width + villagesExtraVisible), 
                CanvasSize.Height / (game.Height + villagesExtraVisible));

            int newZoomLevel = Display.GetMinimumZoomLevel(maxVillageSize);
            return new Location(Location.Display, middle, newZoomLevel);
        }

        /// <summary>
        /// Changes the x and y coordinates and the zoom level
        /// </summary>
        public void SetCenter(Point loc, int zoom)
        {
            SetCenter(this, new Location(Location.Display, loc, zoom));
        }

        /// <summary>
        /// Changes the center of the map
        /// </summary>
        public void SetCenter(Location loc)
        {
            SetCenter(this, loc);
        }

        /// <summary>
        /// Changes the center of the map
        /// </summary>
        private void SetCenter(object sender, Location location)
        {
            if (location != null)
            {
                if (Display == null || Display.Type != location.Display || !Display.CanHandleZoom(location.Zoom))
                {


                    Display = new Display(_displaysettings, this, ref location);
                }

                if (!location.Equals(Location))
                {
                    Location oldLocation = Location;
                    Location = location;

                    EventPublisher.SetMapCenter(sender, new MapLocationEventArgs(location, oldLocation, Display.Zoom));
                }
            }
            else
            {
                Debug.Assert(false, "setting location to null... let's not go there");
                Location = null;
            }
        }

        /// <summary>
        /// Center on home location
        /// </summary>
        public void GoHome()
        {
            SetCenter(HomeLocation);
        }

        /// <summary>
        /// Save the current location as your home location
        /// </summary>
        public void SaveHome()
        {
            HomeLocation = Location;
        }
        #endregion

        #region ContextMenu
        public void ShowContextMenu(IContextMenu menu, Point mapLocation)
        {
            StopTooltip();

            _activeContextMenu = menu;
            menu.Show(_control, mapLocation);
        }
        #endregion

        #region Tooltip
        public void ShowTooltip(string title, string body)
        {
            if (!TooltipAllowed())
            {
                return;
            }

            var settings = new SuperTipSettings();
            settings.HeaderText = title;
            settings.Text = body;

            ShowTooltip(settings);
        }

        public void ShowTooltip(Village village)
        {
            if (!TooltipAllowed())
            {
                return;
            }

            var settings = new SuperTipSettings();
            settings.ToolTipStyle = ToolTipStyle.Standard;
            settings.HeaderText = village.Tooltip.Title;
            settings.Text = village.Tooltip.Text;
            settings.Image = village.Type.GetImage(false);

            if (!string.IsNullOrEmpty(village.Tooltip.Footer))
            {
                settings.FooterText = village.Tooltip.Footer;
                settings.FooterImage = Other.Note;
            }

            ShowTooltip(settings);
        }

        private void ShowTooltip(SuperTipSettings settings)
        {
            _toolTip.Show(settings, _control);
        }

        private bool TooltipAllowed()
        {
            if (_activeContextMenu != null && _activeContextMenu.IsVisible())
            {
                // No tooltips when there is a contextmenu active
                return false;
            }

            return true;
        }

        public void StopTooltip()
        {
            _toolTip.HideActiveToolTip();
        }
        #endregion

        #region Map Cursor
        /// <summary>
        /// Resets the cursor of the map
        /// </summary>
        public void SetCursor()
        {
            _control.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Changes the cursor of the map
        /// </summary>
        public void SetCursor(Cursor cursor)
        {
            if (cursor == Cursors.Default)
            {
                SetCursor();
            }
            else
            {
                _control.Cursor = cursor;
            }
        }
        #endregion

        #region Other
        public void Invalidate(bool resetBackgroundCache = true)
        {
            if (resetBackgroundCache && Display != null)
            {
                Display.ResetCache();
            }
            _control.Invalidate();
        }

        public void GiveFocus()
        {
            _control.GiveFocus();
        }

        /// <summary>
        /// Creates a screenshot of the map
        /// </summary>
        public void Screenshot(string fileName)
        {
            using (var shot = new Bitmap(CanvasSize.Width, CanvasSize.Height))
            {
                _control.DrawToBitmap(shot, new Rectangle(new Point(0, 0), CanvasSize));
                shot.Save(fileName);
            }
        }

        public override string ToString()
        {
            return string.Format("ControlName={0}, Loc={1}", _control.Name, Location);
        }
        #endregion
    }
}
