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
    public class DefenseView : ViewBase
    {
        #region Fields
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public DefenseView(string name)
            : base(name, Types.Defense, Categories.Decorator)
        {
            
        }
        #endregion

        #region Public Methods
        public override DrawerData GetDrawer(Village village)
        {
            
            return null;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
