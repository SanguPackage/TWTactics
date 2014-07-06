#region Using
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;

using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Maps.Drawers.VillageDrawers;
using TribalWars.Data.Maps.Markers;
using TribalWars.Tools;
#endregion

namespace TribalWars.Data.Maps.Displays
{
    /// <summary>
    /// Create and cache ShapeDrawers
    /// </summary>
    public sealed class ShapeDisplay : DisplayBase
    {
        #region Enums
        /// <summary>
        /// The different drawers for ShapeDisplay
        /// </summary>
        public enum Shapes
        {
            DummyDrawer,
            RectangleDrawer,
            EllipseDrawer,
            XDrawer,
            BorderDrawer
        }
        #endregion

        #region Constructors
        public ShapeDisplay()
            : base(new ZoomInfo(1, 25, 10))
        {

        }
        #endregion

        #region Public Methods
        protected override DrawerBase CreateDrawerCore(DrawerData data, MarkerGroup colors, DrawerData mainData)
        {
            var shape = GetShape(data.ShapeDrawer);
            switch (shape)
            {
                case Shapes.RectangleDrawer:
                    return new ShapeDrawer(false, colors);

                case Shapes.EllipseDrawer:
                    return new ShapeDrawer(true, colors);

                case Shapes.XDrawer:
                    return new XDrawer(colors.Color);

                case Shapes.BorderDrawer:
                    var color = (Color)data.ExtraDrawerInfo;
                    return new BorderDrawer(color, GetShape(mainData.ShapeDrawer));

                case Shapes.DummyDrawer:
                    return DrawerBase.CreateEmptyDrawer();
            }
            
            throw new InvalidEnumArgumentException();
        }

        protected override Data CreateData(DrawerData data, MarkerGroup colors, DrawerData mainData)
        {
            // BorderDrawer here created with the additional DrawerData parameter
            // so that its Data is cached only once
            if (GetShape(data.ShapeDrawer) == Shapes.BorderDrawer)
            {
                return new Data(mainData.ShapeDrawer, XmlHelper.GetColor(data.ExtraDrawerInfo.ToString()), Color.Transparent);
            }
            return new Data(data.ShapeDrawer, colors.Color, colors.ExtraColor);
        }

        public static Shapes GetShape(string shape)
        {
            switch (shape)
            {
                case "RectangleDrawer":
                    return Shapes.RectangleDrawer;
                case "EllipseDrawer":
                    return Shapes.EllipseDrawer;
                case "XDrawer":
                    return Shapes.XDrawer;
                case "BorderDrawer":
                    return Shapes.BorderDrawer;
            }
            return Shapes.DummyDrawer;
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
