#region Using
using System.Windows.Forms;

#endregion

namespace TribalWars.Maps.Manipulators.EventArg
{
    public class MapKeyEventArgs : System.EventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the key event arguments
        /// </summary>
        public KeyEventArgs KeyEventArgs { get; private set; }
        #endregion

        #region Constructors
        public MapKeyEventArgs(KeyEventArgs e)
        {
            KeyEventArgs = e;
        }
        #endregion
    }
}