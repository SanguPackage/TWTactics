#region Using
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Maps.Drawers.VillageDrawers;
using TribalWars.Data.Maps.Markers;
using TribalWars.Data.Villages;
using TribalWars.Tools;
#endregion

namespace TribalWars.Data.Maps.Displays
{
    /// <summary>
    /// Create and cache ShapeDrawers
    /// </summary>
    public sealed class ShapeDisplay : DisplayBase
    {
        #region Constructors
        public ShapeDisplay()
            : base(new ZoomInfo(1, 25, 10))
        {

        }
        #endregion

        #region Public Methods
        protected override DrawerBase CreateVillageDrawerCore(Village.BonusType villageBonus, DrawerData data, MarkerGroup colors)
        {
            switch (data.ShapeDrawer)
            {
                case "RectangleDrawer":
                    return new ShapeDrawer(false, colors);

                case "EllipseDrawer":
                    return new ShapeDrawer(true, colors);

                default:
                    return null;
            }
        }

        protected override DrawerBase CreateVillageDecoratorDrawerCore(DrawerData data, MarkerGroup colors, DrawerData mainData)
        {
            switch (data.ShapeDrawer)
            {
                case "BorderDrawer":
                    var color = (Color)data.ExtraDrawerInfo;
                    var drawer = mainData.ShapeDrawer == "EllipseDrawer" ? BorderDrawer.EllipseDrawer : BorderDrawer.RectangleDrawer;
                    return new BorderDrawer(color, drawer);

                default:
                    return null;
            }
        }

        private int GetVillageSize(int zoom)
        {
            return zoom;
        }

        public override int GetVillageWidth(int zoom)
        {
            return GetVillageSize(zoom);
        }

        public override int GetVillageHeight(int zoom)
        {
            return GetVillageSize(zoom);
        }

        private int GetVillageWithSpacingSize(int zoom)
        {
            if (zoom < 5) return zoom;
            return zoom + 1;
        }

        public override int GetVillageHeightSpacing(int zoom)
        {
            return GetVillageWithSpacingSize(zoom);
        }

        public override int GetVillageWidthSpacing(int zoom)
        {
            return GetVillageWithSpacingSize(zoom);
        }

        public override string ToString()
        {
            return String.Format("ShapeDisplay (z{0})", World.Default.Map.Location.Zoom);
        }
        #endregion
    }
}
