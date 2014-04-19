#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// Provides data for the map painting event
    /// </summary>
    /// <remarks>TODO: Foreground/Background is not implemented</remarks>
    public class MapDrawingEventArgs : EventArgs
    {
        #region Fields
        private readonly bool _drawBackground = true;
        private readonly bool _drawForeground = true;

        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether the background needs
        /// to be redrawn
        /// </summary>
        public bool DrawBackground
        {
            get { return _drawBackground; }
        }

        /// <summary>
        /// Gets a value indicating whether the foreground needs
        /// to be redrawn
        /// </summary>
        public bool DrawForeground
        {
            get { return _drawForeground; }
        }
        #endregion

        #region Constructors
        public MapDrawingEventArgs()
        {

        }

        public MapDrawingEventArgs(bool drawBackground)
        {
            _drawBackground = drawBackground;
        }
        #endregion
    }
}
