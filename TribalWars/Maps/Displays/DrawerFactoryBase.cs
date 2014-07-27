#region Using
using System;
using System.Diagnostics;
using System.Drawing;
using TribalWars.Maps.Drawers;
using TribalWars.Maps.Markers;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Displays
{
    /// <summary>
    /// Base class for the Shape and IconDisplay factories
    /// </summary>
    public abstract class DrawerFactoryBase
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
            get { return true; }
        }

        public abstract bool AllowText { get; }

        public abstract DisplayTypes Type { get; }

        /// <summary>
        /// Gets the size of a village
        /// </summary>
        public VillageDimensions Dimensions { get; private set; }
        #endregion

        #region Constructors
        public static ZoomInfo CreateZoom(DisplayTypes displayType, int zoomLevel)
        {
            ZoomInfo zoom;
            switch (displayType)
            {
                case DisplayTypes.Icon:
                    zoom = IconDrawerFactory.CreateZoom(zoomLevel);
                    break;

                case DisplayTypes.Shape:
                    zoom = ShapeDrawerFactory.CreateZoom(zoomLevel);
                    break;

                default:
                    throw new Exception("oeps");
            }

            return zoom;
        }

        public static ZoomInfo CreateMiniMapZoom(int zoom)
        {
            return MiniMapDrawerFactory.CreateZoom(zoom);
        }

        public static MiniMapDrawerFactory CreateMiniMap(int zoom)
        {
            var factory = new MiniMapDrawerFactory(zoom);
            factory.SetVillageDimensions();
            return factory;
        }

        public static DrawerFactoryBase Create(DisplayTypes displayType, int zoomLevel, IconDrawerFactory.Scenery scenery)
        {
            DrawerFactoryBase drawerFactory;
            switch (displayType)
            {
                case DisplayTypes.Icon:
                    drawerFactory = new IconDrawerFactory(zoomLevel, scenery);
                    break;

                case DisplayTypes.Shape:
                    drawerFactory = new ShapeDrawerFactory(zoomLevel);
                    break;

                default:
                    throw new Exception("oeps");
            }

            drawerFactory.SetVillageDimensions();
            return drawerFactory;
        }

        protected DrawerFactoryBase(ZoomInfo zoom)
        {
            _zoom = zoom;
        }
        #endregion

        #region Public Methods
        public virtual int GetMinimumZoomLevel(Size maxVillageSize, bool tryStayInCurrentZoom, out bool couldSatisfyVillageSize)
        {
            couldSatisfyVillageSize = true;
            var newZoom = Math.Min(maxVillageSize.Width, maxVillageSize.Height);

            // Try to stay in current zoom level
            if (tryStayInCurrentZoom)
            {
                newZoom = Math.Min(newZoom, Zoom.Current);
            }

            // Always return valid zoom
            if (newZoom < Zoom.Minimum)
            {
                return Zoom.Current;
            }
            Debug.Assert(newZoom <= Zoom.Maximum);
            return newZoom;
        }

        /// <summary>
        /// Create drawer for village background (Shape/Icon)
        /// </summary>
        /// <param name="bonusType">Bonus villages have a different icon</param>
        /// <param name="data">The shape of the drawer</param>
        /// <param name="colors">The colors for the drawer</param>
        public DrawerBase CreateVillageDrawer(Village.BonusType bonusType, DrawerData data, Marker colors)
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
        public DrawerBase CreateVillageDecoratorDrawer(DrawerData data, Marker colors, DrawerData mainData)
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

        private void SetVillageDimensions()
        {
            Dimensions = CalculateVillageDimensions();
        }

        /// <summary>
        /// Calculates the size of a village
        /// </summary>
        protected abstract VillageDimensions CalculateVillageDimensions();

        protected abstract DrawerBase CreateVillageDrawerCore(Village.BonusType bonusType, DrawerData data, Marker marker);

        protected abstract DrawerBase CreateVillageDecoratorDrawerCore(DrawerData data, Marker colors, DrawerData mainData);
        #endregion
    }
}
