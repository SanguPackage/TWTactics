#region Using
using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Maps.Drawers.VillageDrawers;
using TribalWars.Data.Maps.Markers;
using System.Drawing;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Data.Maps.Displays
{
    public sealed class MiniMapDisplay : DisplayBase
    {
        #region Fields
        private const int FixedZoomLevel = 3;
        #endregion

        #region Properties
        /// <summary>
        /// Returns a value indicating whether the display supports decorating villages
        /// </summary>
        public override bool SupportDecorators
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region Constructors
        public MiniMapDisplay()
            : base(new ZoomInfo(1, 1, 1))
        {

        }
        #endregion

        #region Public Methods
        protected override DrawerBase CreateVillageDrawerCore(Village.BonusType villageBonus, DrawerData data, MarkerGroup colors)
        {
            if (colors.ExtraColor != Color.Transparent) return new MiniMapDrawer(colors.ExtraColor);
            return new MiniMapDrawer(colors.Color);
        }

        protected override DrawerBase CreateVillageDecoratorDrawerCore(DrawerData data, MarkerGroup colors, DrawerData mainData)
        {
            return null;
        }

        public override int GetVillageHeightSpacing(int zoom)
        {
            return FixedZoomLevel;
        }

        public override int GetVillageWidthSpacing(int zoom)
        {
            return FixedZoomLevel;
        }

        public override int GetVillageWidth(int zoom)
        {
            return FixedZoomLevel;
        }

        public override int GetVillageHeight(int zoom)
        {
            return FixedZoomLevel;
        }

        public override string ToString()
        {
            return string.Format("MiniMapDisplay (z{0})", World.Default.Map.Location.Zoom);
        }
        #endregion
    }
}
