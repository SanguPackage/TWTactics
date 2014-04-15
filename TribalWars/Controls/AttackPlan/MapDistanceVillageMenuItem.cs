using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TribalWars.Controls
{
    public class MapDistanceVillageMenuItem
        : ToolStripMenuItem
    {
        #region Fields
        private MapDistanceControl _target;
        #endregion

        #region Properties
        public MapDistanceControl Target
        {
            get { return _target; }
            set { _target = value; }
        }
        #endregion

        #region Constructors
        public MapDistanceVillageMenuItem(string desc, MapDistanceControl targetControl)
            : base(desc)
        {
            _target = targetControl;
        }
        #endregion
    }
}
