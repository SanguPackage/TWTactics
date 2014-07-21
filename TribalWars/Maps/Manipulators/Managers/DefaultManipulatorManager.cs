#region Using
using TribalWars.Maps.Manipulators.Implementations;
#endregion

namespace TribalWars.Maps.Manipulators.Managers
{
    /// <summary>
    /// The default manipulatormanager for a map
    /// </summary>
    public class DefaultManipulatorManager : ManipulatorManagerBase
    {
        #region Properties
        /// <summary>
        /// Moves the map with the mouse
        /// </summary>
        private MapDraggerManipulator MapDragger { get; set; }

        /// <summary>
        /// Marks the active village
        /// </summary>
        private ActiveVillageManipulator ActiveVillageManipulator { get; set; }

        /// <summary>
        /// Moves the map with the keyboard
        /// </summary>
        internal MapMoverManipulator MapMover { get; private set; }
        #endregion

        #region Constructors
        public DefaultManipulatorManager(Map map)
            : base(map, true)
        {
            // Active manipulators
            ActiveVillageManipulator = new ActiveVillageManipulator(map);
            MapMover = new MapMoverManipulator(map);
            MapDragger = new MapDraggerManipulator(map, this);

            _manipulators.Add(ActiveVillageManipulator);
            _manipulators.Add(MapMover);
            _manipulators.Add(MapDragger);
        }
        #endregion
    }
}