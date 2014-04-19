#region Using
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators.Managers;
using TribalWars.Data.Villages;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Helpers.EventArgs
{
    public class MapMouseMoveEventArgs : MapMouseEventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the map location
        /// </summary>
        public Point Location { get; private set; }
        #endregion

        #region Constructors
        public MapMouseMoveEventArgs(ManipulatorManagerBase p, Graphics g, MouseEventArgs e, Point loc, Village vil,
                                     Rectangle rec)
            : base(p, g, e, vil, rec)
        {
            Location = loc;
        }
        #endregion
    }
}