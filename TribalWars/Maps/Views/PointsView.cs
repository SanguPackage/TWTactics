#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        #region Fields
        private readonly Dictionary<int, DrawerData> _drawers;
        #endregion

        #region Constructors
        public PointsView(string name)
            : base(name, false)
        {
            _drawers = new Dictionary<int, DrawerData>();
        }
        #endregion

        #region Public Methods
        public override DrawerData GetDrawerData(Village village)
        {
            foreach (KeyValuePair<int, DrawerData> pair in _drawers)
            {
                if (village.Points < pair.Key) return pair.Value;
            }
            return null;
        }

        public override void AddDrawer(WorldTemplate.WorldConfigurationViewsViewDrawersDrawer drawer)
        {
            Debug.Assert(drawer.ExtraValue == null, "Should always be null for PointsViews");
            _drawers.Add(Convert.ToInt32(drawer.Value), new DrawerData(drawer.Type, drawer.Icon, drawer.BonusIcon, drawer.ExtraValue));
        }
        #endregion
    }
}
