#region Using
using System;
using System.Drawing;
using TribalWars.Maps.Drawers;
using TribalWars.Maps.Drawers.VillageDrawers;
using TribalWars.Maps.Markers;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Displays
{
    /// <summary>
    /// Create and cache ShapeDrawers
    /// </summary>
    public sealed class ShapeDrawerFactory : DrawerFactoryBase
    {
        #region Properties
        public override bool AllowText
        {
            get { return Zoom.Current >= 5; }
        }

        public override DisplayTypes Type
        {
            get { return DisplayTypes.Shape; }
        }
        #endregion

        #region Constructors
        public ShapeDrawerFactory(int zoomLevel)
            : base(CreateZoom(zoomLevel))
        {

        }

        public static ZoomInfo CreateZoom(int zoomLevel)
        {
            return new ZoomInfo(1, 25, zoomLevel);
        }
        #endregion

        #region Public Methods
        protected override DrawerBase CreateVillageDrawerCore(Village.BonusType villageBonus, DrawerData data, Marker marker)
        {
            switch (data.ShapeDrawer)
            {
                case "RectangleDrawer":
                    return new ShapeDrawer(false, marker);

                case "EllipseDrawer":
                    return new ShapeDrawer(true, marker);

                default:
                    return null;
            }
        }

        protected override DrawerBase CreateVillageDecoratorDrawerCore(DrawerData data, Marker colors, DrawerData mainData)
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

        /// <summary>
        /// Gets the size of a village
        /// </summary>
        protected override VillageDimensions CalculateVillageDimensions()
        {
            var villageSize = new Size(Zoom.Current, Zoom.Current);

            if (Zoom.Current < 5) return new VillageDimensions(villageSize, villageSize);
            return new VillageDimensions(villageSize, new Size(Zoom.Current + 1, Zoom.Current + 1));
        }

        public override string ToString()
        {
            return String.Format("ShapeDisplay (z{0})", Zoom.Current);
        }
        #endregion
    }
}
