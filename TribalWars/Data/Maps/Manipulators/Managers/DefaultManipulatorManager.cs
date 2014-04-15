#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Data.Villages;
using TribalWars.Controls.Maps;
using System.Xml;
using TribalWars.Data.Maps.Manipulators.Helpers;
#endregion

namespace TribalWars.Data.Maps.Manipulators
{
    /// <summary>
    /// The default manipulatormanager for a map
    /// </summary>
    public class DefaultManipulatorManager : ManipulatorManagerBase
    {
        #region Fields
        private ActiveVillageManipulator _activeVillageManipulator;
        private MapMoverManipulator _mapMover;
        private MapDraggerManipulator _mapDragger;
        private PinPointManipulator _mapPinPoint; // TODO: dit is nog een lege doos!
        #endregion

        #region Properties
        /// <summary>
        /// Moves the map with the mouse
        /// </summary>
        internal MapDraggerManipulator MapDragger
        {
            get { return _mapDragger; }
        }

        /// <summary>
        /// Marks the active village
        /// </summary>
        internal ActiveVillageManipulator ActiveVillageManipulator
        {
            get { return _activeVillageManipulator; }
        }

        /// <summary>
        /// Moves the map with the keyboard
        /// </summary>
        internal MapMoverManipulator MapMover
        {
            get { return _mapMover; }
        }

        /// <summary>
        /// Marks players and tribes
        /// </summary>
        internal PinPointManipulator MapPinPointer
        {
            get { return _mapPinPoint; }
        }
        #endregion

        #region Constructors
        public DefaultManipulatorManager(Map map)
            : base(map)
        {
            // Active manipulators
            _activeVillageManipulator = new ActiveVillageManipulator(map, SystemColors.HighlightText, Color.LimeGreen, Color.Red, SystemColors.HighlightText);
            _mapMover = new MapMoverManipulator(map);
            _mapDragger = new MapDraggerManipulator(map, this, 1);
            _mapPinPoint = new PinPointManipulator(map);

            _manipulators.Add(_activeVillageManipulator);
            _manipulators.Add(_mapMover);
            _manipulators.Add(_mapDragger);
            _manipulators.Add(_mapPinPoint);
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
