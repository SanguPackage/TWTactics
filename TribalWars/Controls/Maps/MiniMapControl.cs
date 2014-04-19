#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Maps;
using TribalWars.Data.Events;
using System.Drawing;
using TribalWars.Data.Villages;
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

        #region Event Handlers
        private void MainMap_SizeChanged(object sender, EventArgs e)
        {
            Map.Display.ResetCache();
            Map.Control.Invalidate();
        }
        #endregion

        #region Public Methods
        public void SetMap(Map map, Map mainMap)
        {
            Map = map;
            _mainMap = mainMap;

            mainMap.Control.SizeChanged += MainMap_SizeChanged;
        }

        public override void GiveFocus()
        {
            _mainMap.Control.GiveFocus();
        }
        #endregion
    }
}
