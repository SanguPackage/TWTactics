#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using TribalWars.Data.Maps.Views;
#endregion

namespace TribalWars.Data.Maps.Drawers
{
    /// <summary>
    /// Base class for map drawers
    /// </summary>
    /// <remarks>
    /// We cannot make this class a decorator because
    /// it is a flyweight
    /// </remarks>
    public class DrawerBase : IDrawer
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public DrawerBase()
        {
                       
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints non village related stuff
        /// </summary>
        public void Paint(Graphics g, Rectangle rec, Rectangle fullMap)
        {
            PaintCore(g);
        }

        /// <summary>
        /// Paints one village on the map
        /// </summary>
        public void PaintVillage(Graphics g, int x, int y, int width, int height)
        {
            PaintVillageCore(g, x, y, width, height);
        }

        /// <summary>
        /// Paints non village related stuff
        /// </summary>
        protected virtual void PaintCore(Graphics g)
        {
            // TODO: is this even used anymore?
        }

        /// <summary>
        /// Paints one village on the map
        /// </summary>
        protected virtual void PaintVillageCore(Graphics g, int x, int y, int width, int height)
        {

        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose of unmanaged resources
        /// </summary>
        public virtual void Dispose(bool disposing)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        }
        #endregion
    }
}
