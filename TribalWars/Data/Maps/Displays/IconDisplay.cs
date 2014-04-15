#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Markers;
using System.IO;
using TribalWars.Data.Maps.Displays;
#endregion

namespace TribalWars.Data.Maps.Views
{
    /// <summary>
    /// Create and cache IconDrawers
    /// </summary>
    public class IconDisplay : DisplayBase
    {
        #region Fields
        private const int StandardIconWidth = 53;
        private const int StandardIconHeight = 38;

        private MemoryStream _background;
        private Dictionary<int, DrawerBase> _backgroundCache;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public IconDisplay()
            : base(new ZoomInfo(1, 1, 1))
        {
            _background = new MemoryStream(TribalWars.Data.Maps.Displays.WorldData.WorldBackgroundData);

            _backgroundCache = new Dictionary<int, DrawerBase>();
            _backgroundCache.Add(0, CreateArray(new BackgroundDrawer(Icons.Background.gras1)));
            _backgroundCache.Add(1, CreateArray(new BackgroundDrawer(Icons.Background.gras2)));
            _backgroundCache.Add(2, CreateArray(new BackgroundDrawer(Icons.Background.gras3)));
            _backgroundCache.Add(3, CreateArray(new BackgroundDrawer(Icons.Background.gras4)));

            _backgroundCache.Add(8, CreateArray(new BackgroundDrawer(Icons.Background.berg1)));
            _backgroundCache.Add(9, CreateArray(new BackgroundDrawer(Icons.Background.berg2)));
            _backgroundCache.Add(10, CreateArray(new BackgroundDrawer(Icons.Background.berg3)));
            _backgroundCache.Add(11, CreateArray(new BackgroundDrawer(Icons.Background.berg4)));

            _backgroundCache.Add(12, CreateArray(new BackgroundDrawer(Icons.Background.see)));

            _backgroundCache.Add(16, CreateArray(new BackgroundDrawer(Icons.Background.forest0000)));
            _backgroundCache.Add(17, CreateArray(new BackgroundDrawer(Icons.Background.forest0001)));
            _backgroundCache.Add(18, CreateArray(new BackgroundDrawer(Icons.Background.forest0010)));
            _backgroundCache.Add(19, CreateArray(new BackgroundDrawer(Icons.Background.forest0011)));
            _backgroundCache.Add(20, CreateArray(new BackgroundDrawer(Icons.Background.forest0100)));
            _backgroundCache.Add(21, CreateArray(new BackgroundDrawer(Icons.Background.forest0101)));
            _backgroundCache.Add(22, CreateArray(new BackgroundDrawer(Icons.Background.forest0110)));
            _backgroundCache.Add(23, CreateArray(new BackgroundDrawer(Icons.Background.forest0111)));
            _backgroundCache.Add(24, CreateArray(new BackgroundDrawer(Icons.Background.forest1000)));
            _backgroundCache.Add(25, CreateArray(new BackgroundDrawer(Icons.Background.forest1001)));
            _backgroundCache.Add(26, CreateArray(new BackgroundDrawer(Icons.Background.forest1010)));
            _backgroundCache.Add(27, CreateArray(new BackgroundDrawer(Icons.Background.forest1011)));
            _backgroundCache.Add(28, CreateArray(new BackgroundDrawer(Icons.Background.forest1100)));
            _backgroundCache.Add(29, CreateArray(new BackgroundDrawer(Icons.Background.forest1101)));
            _backgroundCache.Add(30, CreateArray(new BackgroundDrawer(Icons.Background.forest1110)));
            _backgroundCache.Add(31, CreateArray(new BackgroundDrawer(Icons.Background.forest1111)));

            /*for (int i = 4; i <= 7; i++)
                _backgroundCache.Add(i, null);
            _backgroundCache.Add(13, null);
            _backgroundCache.Add(14, null);
            _backgroundCache.Add(15, null);*/

            //0-3 Gras
            //8-11 Berg
            //12 See
            //16-31 Wald
        }

        private DrawerBase CreateArray(DrawerBase backgroundDrawer)
        {
            //return new DrawerBase[] { backgroundDrawer };
            return backgroundDrawer;
        }
        #endregion

        #region Public Methods
        protected override DrawerBase CreateDrawerCore(DrawerData data, MarkerGroup colors, DrawerData mainData)
        {
            Bitmap icon = null;
            if (mainData == null)
            {
                // No mainData means we are not talking about a decorator
                icon = (Bitmap)Icons.Villages.ResourceManager.GetObject(data.IconDrawer);
                if (icon != null) return new IconDrawer(icon, colors);
            }

            if (!string.IsNullOrEmpty(data.IconDrawer))
            {
                icon = (Bitmap)Icons.Other.ResourceManager.GetObject(data.IconDrawer);
                if (icon == null) throw new ArgumentException(string.Format("Unable to find icon {0}.", data.IconDrawer));
            }
            return new IconDrawerDecorator((VillageType)data.Value, icon);
        }

        protected override Data CreateData(DrawerData data, MarkerGroup colors, DrawerData mainData)
        {
            return new Data(data.IconDrawer, colors.Color, colors.ExtraColor, data.Value);
        }

        public override int GetVillageHeightSpacing(int zoom)
        {
            return StandardIconHeight;
        }

        public override int GetVillageWidthSpacing(int zoom)
        {
            return StandardIconWidth;
        }

        protected override DrawerBase CreateNonVillageDrawerCore(Point game)
        {
            _background.Position = game.Y * 1000 + game.X;
            return _backgroundCache[_background.ReadByte()];
        }

        public override int GetVillageWidth(int zoom)
        {
            return StandardIconWidth;
        }

        public override int GetVillageHeight(int zoom)
        {
            return StandardIconHeight;
        }

        public override string ToString()
        {
            return string.Format("IconDisplay ({0}x{1})", StandardIconWidth, StandardIconHeight);
        }
        #endregion
    }
}
