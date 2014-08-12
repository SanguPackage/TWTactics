#region Using
using System.Drawing;

#endregion

namespace TribalWars.Maps.Manipulators.EventArg
{
    public class MapPaintEventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the graphics object
        /// </summary>
        public Graphics Graphics { get; private set; }

        /// <summary>
        /// Gets the full map rectangle
        /// </summary>
        public Rectangle FullMapRectangle { get; private set; }
        #endregion

        #region Constructors
        public MapPaintEventArgs(Graphics g, Rectangle fullRec)
        {
            Graphics = g;
            FullMapRectangle = fullRec;
        }
        #endregion
    }
}