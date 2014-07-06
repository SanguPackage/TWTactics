#region Using
using System.Windows.Forms;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Data.Maps.Manipulators.Helpers.EventArgs
{
    public class MapMouseEventArgs : System.EventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the mouse event arguments
        /// </summary>
        public MouseEventArgs MouseEventArgs { get; private set; }

        /// <summary>
        /// Gets the village
        /// </summary>
        public Village Village { get; private set; }
        #endregion

        #region Constructors
        public MapMouseEventArgs(MouseEventArgs e, Village vil)
        {
            MouseEventArgs = e;
            Village = vil;
        }
        #endregion
    }
}