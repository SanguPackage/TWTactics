#region Using
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators.Managers;
using TribalWars.Data.Villages;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Helpers.EventArgs
{
    public class MapMouseEventArgs : MapEventArgs
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
        public MapMouseEventArgs(ManipulatorManagerBase p, Graphics g, MouseEventArgs e, Village vil, Rectangle rec)
            : base(p, g, rec)
        {
            MouseEventArgs = e;
            Village = vil;
        }
        #endregion
    }
}