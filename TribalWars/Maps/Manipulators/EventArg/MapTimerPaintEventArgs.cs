#region Using
using System.Drawing;

#endregion

namespace TribalWars.Maps.Manipulators.EventArg
{
    public class MapTimerPaintEventArgs
    {
        #region Fields
        #endregion

        #region Properties
        /// <summary>
        /// Gets the graphics object
        /// </summary>
        public Graphics Graphics { get; private set; }

        /// <summary>
        /// Gets the full map rectangle
        /// </summary>
        public Rectangle FullMapRectangle { get; private set; }

        /// <summary>
        /// Gets a value indicating whether it is the active
        /// manipulator
        /// </summary>
        public bool IsActiveManipulator { get; private set; }
        #endregion

        #region Constructors
        public MapTimerPaintEventArgs(Graphics g, Rectangle fullRec, bool isActive)
        {
            Graphics = g;
            FullMapRectangle = fullRec;
            IsActiveManipulator = isActive;
        }
        #endregion
    }
}