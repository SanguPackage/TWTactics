#region Imports
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using TribalWars.Controls.Maps;
using TribalWars.Controls.TWContextMenu;
using System.Windows.Forms;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Manipulators.Helpers;
#endregion

namespace TribalWars.Data.Maps.Manipulators
{
    /// <summary>
    /// Manages the user interaction with a map
    /// </summary>
    public class ManipulatorManagerController
    {
        #region Delegates
        public delegate void MouseMovedDelegate(MouseEventArgs e, Point mapLocation, Village village, Point activeLocation, Point activeVillage);
        #endregion

        #region Fields
        private Map _map;

        private ManipulatorManagerBase _currentManipulator;
        private Dictionary<ManipulatorManagerTypes, ManipulatorManagerBase> _manipulators;

        private DefaultManipulatorManager _defaultManipulator;
        private PolygonManipulatorManager _polygonManipulator;

        protected Point _activeLocation;
        protected Point _activeVillage;
        protected Point _lastActiveVillage;

        // MouseMove Delegate move to the controller
        // --> the controller makes sure only the necessary MouseMove, KeyDown etc methods are executed
        // --> ScrollableMapControl executes the methods of the Controller
        private MouseMovedDelegate _mouseMoved;




        // MapMoverManipulator: overrides SetFullControl for cursor
        // BBCodeManipulator: implements stuff for contextmenu
        #endregion

        #region Properties
        /// <summary>
        /// Gets the currently active manipulatormanager
        /// </summary>
        public ManipulatorManagerBase CurrentManipulator
        {
            get { return _currentManipulator; }
        }

        /// <summary>
        /// Gets all available manipulatormanagers
        /// </summary>
        public Dictionary<ManipulatorManagerTypes, ManipulatorManagerBase> Manipulators
        {
            get { return _manipulators; }
        }

        /// <summary>
        /// Gets the map the manipulators are active on
        /// </summary>
        public Map Map
        {
            get { return _map; }
        }

        /// <summary>
        /// Gets the default manipulator
        /// </summary>
        public DefaultManipulatorManager DefaultManipulator
        {
            get { return _defaultManipulator; }
        }

        /// <summary>
        /// Gets the polygon manipulator
        /// </summary>
        public PolygonManipulatorManager PolygonManipulator
        {
            get { return _polygonManipulator; }
        }

        /// <summary>
        /// The last village the cursor was on or is still on
        /// </summary>
        public Point ActiveVillage
        {
            get { return _activeVillage; }
        }

        /// <summary>
        /// The 2nd last village the cursors was on
        /// </summary>
        public Point LastActiveVillage
        {
            get { return _lastActiveVillage; }
        }

        /// <summary>
        /// The location the cursors is on
        /// </summary>
        public Point ActiveLocation
        {
            get { return _activeLocation; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new ManipulatorManager for a minimap
        /// </summary>
        public ManipulatorManagerController(Map miniMap, Map mainMap)
        {
            _map = miniMap;
            _manipulators = new Dictionary<ManipulatorManagerTypes, ManipulatorManagerBase>();
            _currentManipulator = new MiniMapManipulatorManager(miniMap, mainMap);
            _manipulators.Add(ManipulatorManagerTypes.Default, _currentManipulator);
        }

        /// <summary>
        /// Initializes a new ManipulatorManager
        /// </summary>
        public ManipulatorManagerController(Map map)
        {
            _map = map;
            _manipulators = new Dictionary<ManipulatorManagerTypes, ManipulatorManagerBase>();
            _defaultManipulator = new DefaultManipulatorManager(map);
            _currentManipulator = _defaultManipulator;
            _manipulators.Add(ManipulatorManagerTypes.Default, _currentManipulator);
            _polygonManipulator = new PolygonManipulatorManager(map);
            _manipulators.Add(ManipulatorManagerTypes.Polygon, _polygonManipulator);
        }
        #endregion

        #region Public Methods
        /*/// <summary>
        /// Adds the delegate that will be executed when moving the mouse over the
        /// map to all manipulatormanagers
        /// </summary>
        public void AddMouseMoved(DefaultManipulatorManager.MouseMovedDelegate mouseMoved)
        {
            foreach (ManipulatorManagerBase manipulator in _manipulators.Values)
            {
                manipulator.AddMouseMoved(mouseMoved);
            }
        }*/

        /// <summary>
        /// Add a method that will be triggered each time the mouse
        /// moves over the map
        /// </summary>
        public void AddMouseMoved(MouseMovedDelegate moved)
        {
            _mouseMoved += moved;
        }

        /// <summary>
        /// Changes the active manipulatormanager
        /// </summary>
        public void SetManipulator(ManipulatorManagerTypes manipulator)
        {
            _currentManipulator = _manipulators[manipulator];
            _currentManipulator.Initialize();
            _map.EventPublisher.ChangeManipulator(this, new TribalWars.Data.Events.ManipulatorEventArgs(_currentManipulator, manipulator));
        }

        public bool KeyDown(KeyEventArgs e, ScrollableMapControl mapPicture)
        {
            Graphics g = null;
            return _currentManipulator.OnKeyDownCore(new MapKeyEventArgs(_currentManipulator, g, e, mapPicture.ClientRectangle));
        }

        public bool KeyUp(KeyEventArgs e, ScrollableMapControl mapPicture)
        {
            Graphics g = null;
            return _currentManipulator.OnKeyUpCore(new MapKeyEventArgs(_currentManipulator, g, e, mapPicture.ClientRectangle));
        }

        public bool OnVillageDoubleClick(MouseEventArgs e, Village village, ScrollableMapControl mapPicture)
        {
            Graphics g = null;
            return _currentManipulator.OnVillageDoubleClickCore(new MapVillageEventArgs(_currentManipulator, g, e, village, mapPicture.ClientRectangle));
        }

        public bool MouseDown(MouseEventArgs e, Village village, ScrollableMapControl mapPicture)
        {
            Graphics g = null;
            bool down = false;
            if (village != null && e.Button == MouseButtons.Left) down = down || _currentManipulator.OnVillageClickCore(new MapVillageEventArgs(_currentManipulator, g, e, village, mapPicture.ClientRectangle));
            return _currentManipulator.MouseDownCore(new MapMouseEventArgs(_currentManipulator, g, e, village, mapPicture.ClientRectangle)) || down;
        }

        public bool MouseUp(MouseEventArgs e, Village village, ScrollableMapControl mapPicture)
        {
            Graphics g = null;
            bool up = _currentManipulator.MouseUpCore(new MapMouseEventArgs(_currentManipulator, g, e, village, mapPicture.ClientRectangle));
            return up;
        }

        public bool MouseMove(MouseEventArgs e, ScrollableMapControl mapPicture, ToolTip villageTooltip)
        {
            // TODO: Creating a graphics object here *every* time might not be a good idea :)
            Point game = _map.Display.GetGameLocation(e.X, e.Y);
            //System.Diagnostics.Debug.Print(game.ToString());
            Village village = World.Default.GetVillage(game);
            Point map = _map.Display.GetMapLocation(game);
            Graphics g = mapPicture.CreateGraphics();

            // Display village tooltip
            if (village != null)
            {
                if (_activeVillage != village.Location || !villageTooltip.Active)
                {
                    _lastActiveVillage = ActiveVillage;
                    _activeVillage = village.Location;

                    if (_currentManipulator.ShowTooltip)
                    {
                        villageTooltip.Active = true;
                        villageTooltip.ToolTipTitle = village.ToString();
                        villageTooltip.SetToolTip(mapPicture, _map.Manipulators.CurrentManipulator.VillageTooltip(village));
                    }
                }
            }
            else
            {
                if (_currentManipulator.ShowTooltip)
                    villageTooltip.Active = false;
            }

            // Invoke the MouseMoved delegate each time the current mouse location is different from the last location
            if (_mouseMoved != null && _activeLocation != game)
            {
                _activeLocation = game;
                _mouseMoved(e, map, village, _activeLocation, _activeVillage);
            }

            // TODO: also only call this one if _activeLocation != game?
            return _currentManipulator.MouseMoveCore(new MapMouseMoveEventArgs(_currentManipulator, g, e, map, village, mapPicture.ClientRectangle));
        }

        public void Paint(Graphics graphics, Rectangle rec, Rectangle fullMap)
        {
            foreach (ManipulatorManagerBase manipulator in _manipulators.Values)
                manipulator.Paint(new MapPaintEventArgs(graphics, rec, fullMap, manipulator == _currentManipulator));
        }

        public void TimerPaint(ScrollableMapControl mapPicture, Rectangle fullMap)
        {
            Graphics g = mapPicture.CreateGraphics();
            foreach (ManipulatorManagerBase manipulator in _manipulators.Values)
                manipulator.TimerPaint(new MapTimerPaintEventArgs(g, fullMap, manipulator == _currentManipulator));
        }
        #endregion
    }
}
