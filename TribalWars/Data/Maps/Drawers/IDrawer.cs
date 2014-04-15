#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
#endregion

namespace TribalWars.Data.Maps.Drawers
{
    /// <summary>
    /// Draws something on the map
    /// </summary>
    public interface IDrawer : IDisposable
    {
        /// <summary>
        /// Draw stuff
        /// </summary>
        void Paint(Graphics graphics, Rectangle rec, Rectangle fullMap);

        /// <summary>
        /// Draw a village
        /// </summary>
        void PaintVillage(Graphics g, int x, int y, int width, int height);
    }
}
