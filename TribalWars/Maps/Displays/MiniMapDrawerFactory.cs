#region Using
using System.Drawing;
using TribalWars.Maps.Drawers;
using TribalWars.Maps.Drawers.VillageDrawers;
using TribalWars.Maps.Markers;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Displays
{
    public sealed class MiniMapDrawerFactory : DrawerFactoryBase
    {
        #region Fields
        public const int MaxZoomLevel = 3;
        #endregion

        #region Properties
        /// <summary>
        /// Returns a value indicating whether the display supports decorating villages
        /// </summary>
        public override bool SupportDecorators
        { 
            get { return false; }
        }

        public override bool AllowText
        {
            get { return false; }
        }

        public override DisplayTypes Type
        {
            get { return DisplayTypes.Shape; }
        }
        #endregion

        #region Constructors
        public MiniMapDrawerFactory(int currentZoom)
            : base(CreateZoom(currentZoom))
        {
            
        }

        public static ZoomInfo CreateZoom(int zoom)
        {
            return new ZoomInfo(1, MaxZoomLevel, zoom);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the size of a village
        /// </summary>
        protected override VillageDimensions CalculateVillageDimensions()
        {
            return new VillageDimensions(Zoom.Current);
        }

        protected override DrawerBase CreateVillageDrawerCore(Village.BonusType villageBonus, BackgroundDrawerData data, Marker marker)
        {
            if (marker.Settings.ExtraColor != Color.Transparent) return new MiniMapDrawer(marker.Settings.ExtraColor);
            return new MiniMapDrawer(marker.Settings.Color);
        }

        protected override DrawerBase CreateVillageDecoratorDrawerCore(DecoratorDrawerData data, BackgroundDrawerData mainData)
        {
            return null;
        }

        public override string ToString()
        {
            return string.Format("MiniMapDisplay (z{0})", World.Default.Map.Location.Zoom);
        }
        #endregion
    }
}
