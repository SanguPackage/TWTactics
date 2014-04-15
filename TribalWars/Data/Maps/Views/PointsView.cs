#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Data.Maps.Views
{
    public class PointsView : ViewBase
    {
        #region Fields
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public PointsView(string name)
            : base(name, Types.Points, Categories.Background)
        {
            
        }
        #endregion

        #region Public Methods
        public override DrawerData GetDrawer(Village village)
        {
            foreach (KeyValuePair<ViewData, DrawerData> pair in _drawers)
            {
                if (village.Points < pair.Key.Value) return pair.Value;
            }
            return null;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
