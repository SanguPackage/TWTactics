#region Using
using System.Windows.Forms;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Manipulators.EventArg
{
    public class MapVillageEventArgs : System.EventArgs
    {
        #region Fields
        #endregion

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
        public MapVillageEventArgs(MouseEventArgs e, Village vil)
        {
            MouseEventArgs = e;
            Village = vil;
        }
        #endregion
    }
}