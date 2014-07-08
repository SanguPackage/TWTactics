#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Maps;
using TribalWars.Data.Events;
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace TribalWars.Controls.Maps
{
    /// <summary>
    /// A mini map for a regular map
    /// </summary>
    public class MiniMapControl : ScrollableMapControl
    {
        #region Fields
        private Map _mainMap;
        #endregion

        #region Public Methods
        public void SetMap(Map map, Map mainMap)
        {
            Map = map;
            _mainMap = mainMap;
        }

        public override void GiveFocus()
        {
            _mainMap.GiveFocus();
        }
        #endregion
    }
}
