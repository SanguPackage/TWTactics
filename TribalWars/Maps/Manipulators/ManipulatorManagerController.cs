#region Imports
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using TribalWars.Maps.AttackPlans;
using TribalWars.Maps.Controls;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Maps.Manipulators.Implementations.Church;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Maps.Polygons;
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
        private readonly List<ManipulatorBase> _roaming = new List<ManipulatorBase>();
        private ChurchManipulator _churchManipulator;

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
        public PolygonManipulatorManager PolygonManipulator { get; private set; }

        public AttackManipulatorManager AttackManipulator { get; private set; }

        public ChurchManipulator ChurchManipulator
        {
            get
            {
                if (_churchManipulator == null)
                {
                    _churchManipulator = new ChurchManipulator(Map);
                }
                return _churchManipulator;
            }
        }

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

        #region Manipulators
        /// <summary>
        /// Changes the active manipulatormanager
        /// </summary>
        public void SetManipulator(ManipulatorManagerTypes manipulator)
        {
            if (manipulator != ManipulatorManagerTypes.Default)
            {
                _previousType = manipulator;
            }

            Map.EventPublisher.Deselect(this);

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

        public void ToggleChurchManipulator()
        {
            ToggleRoamingManipulator(ChurchManipulator);
        }

        #region Roaming Manipulator
        /// <summary>
        /// A roaming manipulator is not part of a <see cref="ManipulatorManagerBase"/> but
        /// is added to whichever is active
        /// </summary>
        /// <remarks>
        /// ATTN: !!! Incomplete roaming manipulators implementation: !!!
        /// - Only those events used by <see cref="ChurchManipulator"/> are actually called
        /// - New roaming manipulators are not automatically persisted 
        /// - etc..
        /// </remarks>
        private void ToggleRoamingManipulator(ManipulatorBase manipulator)
        {
            Debug.Assert(manipulator is ChurchManipulator, "See remark above. RoamingManipulators only implemented for the Church.");
            if (_roaming.Contains(manipulator))
            {
                _roaming.Remove(manipulator);
            }
            else
            {
                _roaming.Add(manipulator);
            }
        }

        public string GetRoamingXml()
        {
            var str = new StringBuilder();
            str.Append(ChurchManipulator.WriteXml());
            return str.ToString();
        }

        public void ReadRoamingXml(XDocument newReader)
        {
            ChurchManipulator.ReadXml(newReader);
        }
        #endregion
        #endregion

        #region KeyEvents
        public bool KeyDown(KeyEventArgs e)
        {
            return CurrentManipulator.OnKeyDownCore(new MapKeyEventArgs(e));
        }

        public bool KeyUp(KeyEventArgs e)
        {
            return CurrentManipulator.OnKeyUpCore(new MapKeyEventArgs(e));
        }
        #endregion

        #region MouseEvents
        /// <summary>
        /// Add a method that will be triggered each time the mouse
        /// moves over the map
        /// </summary>
        public void AddMouseMoved(MouseMovedDelegate moved)
        {
            _mouseMoved += moved;
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
        #endregion

        #region Painting
        public void Paint(MapPaintEventArgs e)
        {
            foreach (ManipulatorBase roaming in _roaming)
            {
                roaming.Paint(e, false);
            }

            foreach (ManipulatorManagerBase manipulator in _manipulators.Values)
            {
                manipulator.Paint(e, manipulator == CurrentManipulator);
            }
        }

        public void TimerPaint(ScrollableMapControl mapPicture, Rectangle fullMap)
        {
            Graphics g = mapPicture.CreateGraphics();
            foreach (ManipulatorManagerBase manipulator in _manipulators.Values)
                manipulator.TimerPaint(new MapTimerPaintEventArgs(g, fullMap, manipulator == CurrentManipulator));
        }
        #endregion

        public void CleanUp()
        {
            foreach (var manipulator in _roaming)
            {
                manipulator.CleanUp();
            }

            _manipulators.ForEach(d => d.Value.CleanUp());
        }
    }
}