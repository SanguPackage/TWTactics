#region Using
using System;
using System.Drawing;

#endregion

namespace TribalWars.Maps.Drawing.Drawers
{
    /// <summary>
    /// Base class for map drawers
    /// </summary>
    public class DrawerBase
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
        /// Paints one village on the map
        /// </summary>
        public void PaintVillage(Graphics g, Rectangle village)
        {
            PaintVillageCore(g, village);
        }

        /// <summary>
        /// Paints one village on the map
        /// </summary>
        protected virtual void PaintVillageCore(Graphics g, Rectangle village)
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