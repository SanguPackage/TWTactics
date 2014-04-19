#region Using
using System.Drawing;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Helpers.EventArgs
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

        /// <summary>
        /// Gets the invalidated rectangle
        /// </summary>
        public Rectangle MapRectangle { get; private set; }

        /// <summary>
        /// Gets a value indicating whether it is the active
        /// manipulator
        /// </summary>
        public bool IsActiveManipulator { get; private set; }
        #endregion

        #region Constructors
        public MapPaintEventArgs(Graphics g, Rectangle rec, Rectangle fullRec, bool isActive)
        {
            Graphics = g;
            FullMapRectangle = fullRec;
            MapRectangle = rec;
            IsActiveManipulator = isActive;
        }
        #endregion
    }
}