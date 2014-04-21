#region Using
using System;
using System.Drawing;

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
        #region Construction
        private static readonly DrawerBase NullDrawer = new DrawerBase();

        /// <summary>
        /// Creates a NullObject drawer (ie doesn't do anything:)
        /// </summary>
        public static DrawerBase CreateEmptyDrawer()
        {
            return NullDrawer;
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