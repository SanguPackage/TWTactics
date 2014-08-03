#region Using

#endregion

using System.Drawing;
using TribalWars.Maps.Drawers.VillageDrawers;
using TribalWars.Villages;

namespace TribalWars.Maps.Drawers
{
    /// <summary>
    /// Holds the data for creating a DrawerBase
    /// </summary>
    public sealed class BackgroundDrawerData
    {
        #region Properties
        /// <summary>
        /// Gets or sets which shapedrawer to use
        /// </summary>
        public string ShapeDrawer { get; private set; }

        /// <summary>
        /// Gets or sets which icondrawer to use
        /// </summary>
        public string IconDrawer { get; private set; }

        /// <summary>
        /// Gets or sets which icondrawer to use when it is a bonus village
        /// </summary>
        public string BonusIconDrawer { get; private set; }
        #endregion

        #region Constructors
        public BackgroundDrawerData(string shape, string icon, string bonusIcon)
        {
            ShapeDrawer = shape;
            IconDrawer = icon;
            BonusIconDrawer = bonusIcon;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return string.Format("Shape:{0}, Icon:{1}", ShapeDrawer, IconDrawer);
        }
        #endregion
    }

    /// <summary>
    /// Holds the data for creating a DrawerBase
    /// </summary>
    public sealed class DecoratorDrawerData
    {
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

        public class IconData
        {
            public Image Icon { get; set; }

            public IconOrientation Orientation { get; set; }

            public Color? Background { get; set; }

            public IconData(string icon, IconOrientation orientation, Color? background)
            {
                if (!string.IsNullOrEmpty(icon))
                {
                    Icon = (Image)Icons.Other.ResourceManager.GetObject(icon);
                }

                Orientation = orientation;
                Background = background;
            }

            public override string ToString()
            {
                return string.Format("{0}, Orientation={1}, Background={2}", Icon, Orientation, Background);
            }
        }

        #region Properties
        public ShapeData Shape { get; set; }

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
    }
}