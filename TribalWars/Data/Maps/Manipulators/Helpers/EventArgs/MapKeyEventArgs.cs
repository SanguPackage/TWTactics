#region Using
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators.Managers;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Helpers.EventArgs
{
    public class MapKeyEventArgs : MapEventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the key event arguments
        /// </summary>
        public KeyEventArgs KeyEventArgs { get; private set; }
        #endregion

        #region Constructors
        public MapKeyEventArgs(ManipulatorManagerBase p, Graphics g, KeyEventArgs e, Rectangle rec)
            : base(p, g, rec)
        {
            KeyEventArgs = e;
        }
        #endregion
    }
}