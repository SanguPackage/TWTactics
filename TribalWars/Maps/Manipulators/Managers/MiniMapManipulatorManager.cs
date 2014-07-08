#region Using
using TribalWars.Controls;
using TribalWars.Maps.Manipulators.Implementations;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Manipulators.Managers
{
    /// <summary>
    /// Manages user interaction for the minimap
    /// </summary>
    public class MiniMapManipulatorManager : ManipulatorManagerBase
    {
        #region Fields
        private readonly MiniMapActiveVillageManipulator _activeVillage;
        #endregion

        #region Constructors
        public MiniMapManipulatorManager(Map map, Map mainMap)
            : base(map, false)
        {
            _activeVillage = new MiniMapActiveVillageManipulator(map, mainMap);
            _manipulators.Add(_activeVillage);
        }
        #endregion

        public override IContextMenu GetContextMenu(System.Drawing.Point location, Village village)
        {
            return null;
        }
    }
}
