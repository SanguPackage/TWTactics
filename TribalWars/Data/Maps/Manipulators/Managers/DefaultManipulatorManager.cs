#region Using
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Data.Maps.Manipulators.Implementations;
using TribalWars.Data.Villages;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Managers
{
    /// <summary>
    /// The default manipulatormanager for a map
    /// </summary>
    public class DefaultManipulatorManager : ManipulatorManagerBase
    {
        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// Moves the map with the mouse
        /// </summary>
        internal MapDraggerManipulator MapDragger { get; private set; }

        /// <summary>
        /// Marks the active village
        /// </summary>
        internal ActiveVillageManipulator ActiveVillageManipulator { get; private set; }

        /// <summary>
        /// Moves the map with the keyboard
        /// </summary>
        internal MapMoverManipulator MapMover { get; private set; }

        /// <summary>
        /// Marks players and tribes
        /// </summary>
        internal PinPointManipulator MapPinPointer { get; private set; }
        #endregion

        #region Constructors
        public DefaultManipulatorManager(Map map)
            : base(map)
        {
            // Active manipulators
            ActiveVillageManipulator = new ActiveVillageManipulator(map, SystemColors.HighlightText, Color.LimeGreen, Color.Red, SystemColors.HighlightText);
            MapMover = new MapMoverManipulator(map);
            MapDragger = new MapDraggerManipulator(map, this, 1);
            MapPinPointer = new PinPointManipulator(map);

            _manipulators.Add(ActiveVillageManipulator);
            _manipulators.Add(MapMover);
            _manipulators.Add(MapDragger);
            _manipulators.Add(MapPinPointer);
        }
        #endregion

        #region Public Methods
        public virtual void ShowVillageContext(Point location, Village village)
        {
            if (World.Default.VillageContextMenu != null && village != null)
                World.Default.VillageContextMenu.Show(_map.Control, location, village);
        }
        #endregion

        #region Event Handlers
        #endregion

        #region IMapManipulator Members
        protected internal override bool MouseUpCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Right)
            {
                ShowVillageContext(e.MouseEventArgs.Location, e.Village);
                return false;
            }
            if (_fullControllManipulator != null)
            {
                return _fullControllManipulator.MouseUpCore(e);
            }

            return base.MouseUpCore(e);
        }
        #endregion

        #region IMapDrawer Members
        public override void Dispose()
        {
        }
        #endregion
    }
}