#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TribalWars.Data.Maps.Manipulators
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
