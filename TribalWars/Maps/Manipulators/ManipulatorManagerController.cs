#region Imports
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Maps.Controls;
using TribalWars.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Maps.Manipulators
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
        private ManipulatorManagerTypes? _previousType;

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

        public AttackManipulatorManager AttackManipulator { get; set; }

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
            AttackManipulator = new AttackManipulatorManager(map);
            _manipulators.Add(ManipulatorManagerTypes.Attack, AttackManipulator);
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
            if (manipulator != ManipulatorManagerTypes.Default)
            {
                _previousType = manipulator;
            }

            CurrentManipulator = _manipulators[manipulator];
            CurrentManipulator.Initialize();
            Map.EventPublisher.ChangeManipulator(this, new ManipulatorEventArgs(manipulator));
        }

        /// <summary>
        /// Keep track of last non-default manipulator
        /// and switch between default and last
        /// </summary>
        public void SwitchManipulator()
        {
            if (!_previousType.HasValue)
            {
                SetManipulator(ManipulatorManagerTypes.Attack);
            }
            else if (CurrentManipulator == _manipulators[ManipulatorManagerTypes.Default])
            {
                SetManipulator(_previousType.Value);
            }
            else
            {
                SetManipulator(ManipulatorManagerTypes.Default);
            }
        }

        public bool KeyDown(KeyEventArgs e)
        {
            return CurrentManipulator.OnKeyDownCore(new MapKeyEventArgs(e));
        }

        public bool KeyUp(KeyEventArgs e)
        {
            return CurrentManipulator.OnKeyUpCore(new MapKeyEventArgs(e));
        }

        public bool OnVillageDoubleClick(MouseEventArgs e, Village village)
        {
            return CurrentManipulator.OnVillageDoubleClickCore(new MapVillageEventArgs(e, village));
        }

        public bool MouseDown(MouseEventArgs e, Village village)
        {
            bool redraw = false;
            if (village != null && e.Button == MouseButtons.Left)
            {
                redraw = CurrentManipulator.OnVillageClickCore(new MapVillageEventArgs(e, village));
            }
            return CurrentManipulator.MouseDownCore(new MapMouseEventArgs(e, village)) 
                || redraw;
        }

        public bool MouseUp(MouseEventArgs e, Village village)
        {
            bool redraw = CurrentManipulator.MouseUpCore(new MapMouseEventArgs(e, village));
            return redraw;
        }

        public bool MouseLeave()
        {
            // Avoid showing a tooltip outside the MapControl
            Map.StopTooltip();

            return CurrentManipulator.MouseLeave();
        }

        public bool MouseMove(MouseEventArgs e)
        {
            Point game = Map.Display.GetGameLocation(e.Location);
            Village village = World.Default.GetVillage(game);
            Point map = Map.Display.GetMapLocation(game);

            // Display village tooltip
            if (village != null)
            {
                if (ActiveVillage != village.Location)
                {
                    LastActiveVillage = ActiveVillage;
                    ActiveVillage = village.Location;
                    CurrentManipulator.ShowTooltip(village);
                }
            }
            else
            {
                Map.StopTooltip();
            }

            // Invoke the MouseMoved delegate each time the current mouse location is different from the last location
            if (_mouseMoved != null && ActiveLocation != game)
            {
                ActiveLocation = game;
                _mouseMoved(e, map, village, ActiveLocation, ActiveVillage);
            }

            return CurrentManipulator.MouseMoveCore(new MapMouseMoveEventArgs(e, map, village));
        }

        public bool MouseWheel(MouseEventArgs e)
        {
            return CurrentManipulator.MouseWheel(e);
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