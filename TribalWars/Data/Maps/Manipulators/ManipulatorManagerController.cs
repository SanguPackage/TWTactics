#region Imports
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Controls.Maps;
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Data.Maps.Manipulators.Managers;
using TribalWars.Data.Villages;
using TribalWars.Tools;

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
        private readonly Dictionary<ManipulatorManagerTypes, ManipulatorManagerBase> _manipulators;

        private MouseMovedDelegate _mouseMoved;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the currently active manipulatormanager
        /// </summary>
        public ManipulatorManagerBase CurrentManipulator { get; private set; }

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
        private Map Map { get; set; }

        /// <summary>
        /// Gets the default manipulator
        /// </summary>
        private DefaultManipulatorManager DefaultManipulator { get; set; }

        /// <summary>
        /// Gets the polygon manipulator
        /// </summary>
        public PolygonManipulatorManager PolygonManipulator { get; set; }

        /// <summary>
        /// The last village the cursor was on or is still on
        /// </summary>
        private Point ActiveVillage { get; set; }

        /// <summary>
        /// The 2nd last village the cursors was on
        /// </summary>
        private Point LastActiveVillage { get; set; }

        /// <summary>
        /// The location the cursors is on
        /// </summary>
        private Point ActiveLocation { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new ManipulatorManager for a minimap
        /// </summary>
        public ManipulatorManagerController(Map miniMap, Map mainMap)
        {
            Map = miniMap;
            _manipulators = new Dictionary<ManipulatorManagerTypes, ManipulatorManagerBase>();
            CurrentManipulator = new MiniMapManipulatorManager(miniMap, mainMap);
            _manipulators.Add(ManipulatorManagerTypes.Default, CurrentManipulator);
        }

        /// <summary>
        /// Initializes a new ManipulatorManager
        /// </summary>
        public ManipulatorManagerController(Map map)
        {
            Map = map;
            _manipulators = new Dictionary<ManipulatorManagerTypes, ManipulatorManagerBase>();
            DefaultManipulator = new DefaultManipulatorManager(map);
            CurrentManipulator = DefaultManipulator;
            _manipulators.Add(ManipulatorManagerTypes.Default, CurrentManipulator);
            PolygonManipulator = new PolygonManipulatorManager(map);
            _manipulators.Add(ManipulatorManagerTypes.Polygon, PolygonManipulator);
        }
        #endregion

        #region Public Methods
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
            CurrentManipulator = _manipulators[manipulator];
            CurrentManipulator.Initialize();
            Map.EventPublisher.ChangeManipulator(this, new TribalWars.Data.Events.ManipulatorEventArgs(CurrentManipulator, manipulator));
        }

        public bool KeyDown(KeyEventArgs e, ScrollableMapControl mapPicture)
        {
            Graphics g = null;
            return CurrentManipulator.OnKeyDownCore(new MapKeyEventArgs(CurrentManipulator, g, e, mapPicture.ClientRectangle));
        }

        public bool KeyUp(KeyEventArgs e, ScrollableMapControl mapPicture)
        {
            Graphics g = null;
            return CurrentManipulator.OnKeyUpCore(new MapKeyEventArgs(CurrentManipulator, g, e, mapPicture.ClientRectangle));
        }

        public bool OnVillageDoubleClick(MouseEventArgs e, Village village, ScrollableMapControl mapPicture)
        {
            Graphics g = null;
            return CurrentManipulator.OnVillageDoubleClickCore(new MapVillageEventArgs(CurrentManipulator, g, e, village, mapPicture.ClientRectangle));
        }

        public bool MouseDown(MouseEventArgs e, Village village, ScrollableMapControl mapPicture)
        {
            Graphics g = null;
            bool redraw = false;
            if (village != null && e.Button == MouseButtons.Left)
            {
                redraw = CurrentManipulator.OnVillageClickCore(new MapVillageEventArgs(CurrentManipulator, g, e, village, mapPicture.ClientRectangle));
            }
            return CurrentManipulator.MouseDownCore(new MapMouseEventArgs(CurrentManipulator, g, e, village, mapPicture.ClientRectangle)) 
                || redraw;
        }

        public bool MouseUp(MouseEventArgs e, Village village, ScrollableMapControl mapPicture)
        {
            Graphics g = null;
            bool redraw = CurrentManipulator.MouseUpCore(new MapMouseEventArgs(CurrentManipulator, g, e, village, mapPicture.ClientRectangle));
            return redraw;
        }

        public bool MouseMove(MouseEventArgs e, ScrollableMapControl mapPicture, ToolTip villageTooltip)
        {
            Point game = Map.Display.GetGameLocation(e.Location);
            if (!game.IsValidGameCoordinate())
            {
                return false;
            }

            Village village = World.Default.GetVillage(game);
            Point map = Map.Display.GetMapLocation(game);
            Graphics g = mapPicture.CreateGraphics();

            // Display village tooltip
            if (village != null)
            {
                if (ActiveVillage != village.Location || !villageTooltip.Active)
                {
                    LastActiveVillage = ActiveVillage;
                    ActiveVillage = village.Location;

                    if (CurrentManipulator.ShowTooltip)
                    {
                        villageTooltip.Active = true;
                        villageTooltip.ToolTipTitle = village.Tooltip.Title;
                        villageTooltip.SetToolTip(mapPicture, Map.Manipulators.CurrentManipulator.VillageTooltip(village));
                    }
                }
            }
            else
            {
                if (CurrentManipulator.ShowTooltip)
                    villageTooltip.Active = false;
            }

            // Invoke the MouseMoved delegate each time the current mouse location is different from the last location
            if (_mouseMoved != null && ActiveLocation != game)
            {
                ActiveLocation = game;
                _mouseMoved(e, map, village, ActiveLocation, ActiveVillage);
            }

            // TODO: also only call this one if _activeLocation != game?
            return CurrentManipulator.MouseMoveCore(new MapMouseMoveEventArgs(CurrentManipulator, g, e, map, village, mapPicture.ClientRectangle));
        }

        public void Paint(Graphics graphics, Rectangle fullMap)
        {
            foreach (ManipulatorManagerBase manipulator in _manipulators.Values)
                manipulator.Paint(new MapPaintEventArgs(graphics, fullMap, manipulator == CurrentManipulator));
        }

        public void TimerPaint(ScrollableMapControl mapPicture, Rectangle fullMap)
        {
            Graphics g = mapPicture.CreateGraphics();
            foreach (ManipulatorManagerBase manipulator in _manipulators.Values)
                manipulator.TimerPaint(new MapTimerPaintEventArgs(g, fullMap, manipulator == CurrentManipulator));
        }
        #endregion
    }
}