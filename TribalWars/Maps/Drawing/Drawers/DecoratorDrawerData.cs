using System.Drawing;
using TribalWars.Maps.Drawing.Drawers.VillageDrawers;

namespace TribalWars.Maps.Drawing.Drawers
{
    /// <summary>
    /// Holds the data for creating a DrawerBase
    /// </summary>
    public sealed class DecoratorDrawerData
    {
        #region Properties
        /// <summary>
        /// The data for shapes display (rectangles)
        /// </summary>
        public ShapeData Shape { get; set; }

        /// <summary>
        /// The data for tw icon display
        /// </summary>
        public IconData Icon { get; set; }
        #endregion

        #region Constructors
        public DecoratorDrawerData(ShapeData shape, IconData icon)
        {
            Shape = shape;
            Icon = icon;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return string.Format("Shape=({0}) | Icon=({1})", Shape, Icon);
        }
        #endregion

        /// <summary>
        /// The data when in rectangle display
        /// </summary>
        public class ShapeData
        {
            public string Drawer { get; set; }

            public Color Color { get; set; }

            public ShapeData(string drawer, Color color)
            {
                Drawer = drawer;
                Color = color;
            }

            public override string ToString()
            {
                return string.Format("{0}, Color={1}", Drawer, Color);
            }
        }

        /// <summary>
        /// The data when in TW images display
        /// </summary>
        public class IconData
        {
            public Image Icon { get; set; }

            public string IconName { get; set; }

            public IconOrientation Orientation { get; set; }

            public Color? Background { get; set; }

            public IconData(string icon, IconOrientation orientation, Color? background)
            {
                IconName = icon;
                Icon = (Image)Icons.Other.ResourceManager.GetObject(icon);

                Orientation = orientation;
                Background = background;
            }

            public override string ToString()
            {
                return string.Format("{0}, Orientation={1}, Background={2}", Icon, Orientation, Background);
            }
        }
    }
}