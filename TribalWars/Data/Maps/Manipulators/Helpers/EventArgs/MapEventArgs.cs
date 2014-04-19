#region Using
using System.Drawing;
using TribalWars.Data.Maps.Manipulators.Managers;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Helpers.EventArgs
{
    public class MapEventArgs
    {
        #region Fields
        private readonly ManipulatorManagerBase _parent;
        private readonly Graphics _g;
        private readonly Rectangle _rec;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the parent manipulator manager
        /// </summary>
        public ManipulatorManagerBase Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return _g; }
        }

        /// <summary>
        /// Gets the full map rectangle
        /// </summary>
        public Rectangle FullMapRectangle
        {
            get { return _rec; }
        }
        #endregion

        #region Constructors
        public MapEventArgs(ManipulatorManagerBase p, Graphics g, Rectangle rec)
        {
            _parent = p;
            _g = g;
            _rec = rec;
        }
        #endregion
    }
}