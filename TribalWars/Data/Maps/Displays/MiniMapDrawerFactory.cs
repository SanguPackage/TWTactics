#region Using
using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Maps.Drawers.VillageDrawers;
using TribalWars.Data.Maps.Markers;
using System.Drawing;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Data.Maps.Displays
{
    public sealed class MiniMapDrawerFactory : DrawerFactoryBase
    {
        #region Fields
        private const int FixedZoomLevel = 3;
        private static readonly VillageDimensions Dimensions = new VillageDimensions(FixedZoomLevel);
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
            get { return DisplayTypes.MiniMap; }
        }
        #endregion

        #region Constructors
        public MiniMapDrawerFactory()
            : base(new ZoomInfo(1, 1, 1))
        {

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the size of a village
        /// </summary>
        protected override VillageDimensions CalculateVillageDimensions()
        {
            return Dimensions;
        }

        protected override DrawerBase CreateVillageDrawerCore(Village.BonusType villageBonus, DrawerData data, MarkerGroup colors)
        {
            if (colors.ExtraColor != Color.Transparent) return new MiniMapDrawer(colors.ExtraColor);
            return new MiniMapDrawer(colors.Color);
        }

        protected override DrawerBase CreateVillageDecoratorDrawerCore(DrawerData data, MarkerGroup colors, DrawerData mainData)
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
