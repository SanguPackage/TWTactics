#region Using
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Manipulators.EventArg
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
        public MapMouseMoveEventArgs(MouseEventArgs e, Point loc, Village vil)
            : base(e, vil)
        {
            Location = loc;
        }
        #endregion
    }
}