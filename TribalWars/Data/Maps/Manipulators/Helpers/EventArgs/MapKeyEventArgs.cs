#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Data.Maps.Manipulators.Helpers
{
    public class MapKeyEventArgs : MapEventArgs
    {
        #region Fields
        private KeyEventArgs _keys;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the key event arguments
        /// </summary>
        public KeyEventArgs KeyEventArgs
        {
            get { return _keys; }
        }
        #endregion

        #region Constructors
        public MapKeyEventArgs(ManipulatorManagerBase p, Graphics g, KeyEventArgs e, Rectangle rec)
            : base(p, g, rec)
        {
            _keys = e;
        }
        #endregion
    }
}
