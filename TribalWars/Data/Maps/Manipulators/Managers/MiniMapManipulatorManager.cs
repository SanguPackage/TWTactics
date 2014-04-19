#region Using
using TribalWars.Data.Maps.Manipulators.Implementations;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Managers
{
    /// <summary>
    /// Manages user interaction for the minimap
    /// </summary>
    public class MiniMapManipulatorManager : ManipulatorManagerBase
    {
        #region Fields
        private MiniMapActiveVillageManipulator _activeVillage;
        #endregion

        #region Constructors
        public MiniMapManipulatorManager(Map map, Map mainMap)
            : base(map)
        {
            // Active manipulators
            _activeVillage = new MiniMapActiveVillageManipulator(map, mainMap);
            _manipulators.Add(_activeVillage);

            ShowTooltip = false;
        }
        #endregion
    }
}
