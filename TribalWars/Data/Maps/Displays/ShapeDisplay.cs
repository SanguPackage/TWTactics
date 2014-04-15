#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Markers;
using TribalWars.Tools;
#endregion

namespace TribalWars.Data.Maps.Displays
{
    /// <summary>
    /// Create and cache ShapeDrawers
    /// </summary>
    public class ShapeDisplay : DisplayBase
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

        #region Fields

        #endregion

        #region Properties

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
            switch (GetShape(data.ShapeDrawer))
            {
                case Shapes.RectangleDrawer:
                    return new RectangleDrawer(colors);
                case Shapes.EllipseDrawer:
                    return new EllipseDrawer(colors);
                case Shapes.XDrawer:
                    return new XDrawer(colors.Color);
                case Shapes.BorderDrawer:
                    return new BorderDrawer(XmlHelper.GetColor(data.ExtraDrawerInfo.ToString()), GetShape(mainData.ShapeDrawer));
            }
            return new RectangleDrawer();
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

        public override int GetVillageHeightSpacing(int zoom)
        {
            if (zoom < 5) return zoom;
            return zoom + 1;
        }

        public override int GetVillageWidthSpacing(int zoom)
        {
            if (zoom < 5) return zoom;
            return zoom + 1;
        }

        public override string ToString()
        {
            return string.Format("ShapeDisplay (z{0})", World.Default.Map.Location.Zoom);
        }
        #endregion

        public override int GetVillageWidth(int zoom)
        {
            return zoom;
        }

        public override int GetVillageHeight(int zoom)
        {
            return zoom;
        }
    }
}
