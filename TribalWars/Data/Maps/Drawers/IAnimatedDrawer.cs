#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TribalWars.Data.Maps.Drawers
{
    /// <summary>
    /// Interface for animated drawing on the map
    /// </summary>
    public interface IAnimatedDrawer : IDrawer
    {
        /// <summary>
        /// Draw animated stuff
        /// </summary>
        void TimerPaint(System.Drawing.Graphics g);
    }
}
