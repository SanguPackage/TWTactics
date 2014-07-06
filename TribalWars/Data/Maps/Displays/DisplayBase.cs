#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;

using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Markers;
#endregion

namespace TribalWars.Data.Maps.Displays
{
    /// <summary>
    /// Base class for the Shape and IconDisplays
    /// </summary>
    public abstract class DisplayBase
    {
        #region Fields
        private readonly ZoomInfo _zoom;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the zoom possibilities of the display
        /// </summary>
        public ZoomInfo Zoom
        {
            get { return _zoom; }
        }

        /// <summary>
        /// Returns a value indicating whether the display supports decorating villages
        /// </summary>
        public virtual bool SupportDecorators
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Constructors
        protected DisplayBase(ZoomInfo zoom)
        {
            _zoom = zoom;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create drawer for village background (Shape/Icon)
        /// </summary>
        /// <param name="bonusType">Bonus villages have a different icon</param>
        /// <param name="data">The shape of the drawer</param>
        /// <param name="colors">The colors for the drawer</param>
        public DrawerBase CreateVillageDrawer(Village.BonusType bonusType, DrawerData data, MarkerGroup colors)
        {
            DrawerBase drawer = CreateVillageDrawerCore(bonusType, data, colors);
            return drawer;
        }

        /// <summary>
        /// Create drawer to further decorate a village drawn by <see cref="CreateVillageDrawer"/>
        /// </summary>
        /// <param name="data">The shape of the drawer</param>
        /// <param name="colors">The colors for the drawer</param>
        /// <param name="mainData">The data for the main drawer (used for BorderDrawer)</param>
        public DrawerBase CreateVillageDecoratorDrawer(DrawerData data, MarkerGroup colors, DrawerData mainData)
        {
            Debug.Assert(SupportDecorators);
            DrawerBase drawer = CreateVillageDecoratorDrawerCore(data, colors, mainData);
            return drawer;
        }

        /// <summary>
        /// Gets drawer for a location on the map where there is no village present
        /// </summary>
        public DrawerBase CreateNonVillageDrawer(Point game, Rectangle village)
        {
            return CreateNonVillageDrawerCore(game, village);
        }

        /// <summary>
        /// Gets drawer for a location on the map where there is no village present
        /// </summary>
        protected virtual DrawerBase CreateNonVillageDrawerCore(Point game, Rectangle village)
        {
            return null;
        }

        /// <summary>
        /// Gets the width of a village WITH the spacing to the next village
        /// </summary>
        public abstract int GetVillageWidthSpacing(int zoom);

        /// <summary>
        /// Gets the height of a village WITH the spacing to the next village
        /// </summary>
        public abstract int GetVillageHeightSpacing(int zoom);

        /// <summary>
        /// Gets the width of a village
        /// </summary>
        public abstract int GetVillageWidth(int zoom);

        /// <summary>
        /// Gets the height of a village
        /// </summary>
        public abstract int GetVillageHeight(int zoom);

        protected abstract DrawerBase CreateVillageDrawerCore(Village.BonusType bonusType, DrawerData data, MarkerGroup colors);

        protected abstract DrawerBase CreateVillageDecoratorDrawerCore(DrawerData data, MarkerGroup colors, DrawerData mainData);
        #endregion

        #region ZoomInfo
        /// <summary>
        /// Encapsulates the current zoom level
        /// and the zoom boundries
        /// </summary>
        public class ZoomInfo
        {
            #region Properties
            /// <summary>
            /// Gets the minimum zoom level
            /// </summary>
            public int Minimum { get; private set; }

            /// <summary>
            /// Gets the maximum zoom level
            /// </summary>
            public int Maximum { get; private set; }

            /// <summary>
            /// Gets or sets the zoom level that will be used
            /// when the user switches back to this displaytype
            /// </summary>
            public int Current { get; set; }
            #endregion

            #region Constructors
            public ZoomInfo(int min, int max, int current)
            {
                Minimum = min;
                Maximum = max;
                Current = current;
            }
            #endregion

            public override string ToString()
            {
                return string.Format("Min={0}, Max={1}, Current={2}", Minimum, Maximum, Current);
            }
        }
        #endregion
    }
}
