#region Using
using System.Collections.Generic;
using TribalWars.Maps.Drawers;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Views
{
    /// <summary>
    /// Display villages based on the amount of points
    /// </summary>
    public sealed class PointsView : ViewBase
    {
        #region Constructors
        public PointsView(string name)
            : base(name, false)
        {
            
        }
        #endregion

        #region Public Methods
        public override DrawerData GetDrawerData(Village village)
        {
            foreach (KeyValuePair<ViewData, DrawerData> pair in _drawers)
            {
                if (village.Points < pair.Key.Value) return pair.Value;
            }
            return null;
        }
        #endregion
    }
}
