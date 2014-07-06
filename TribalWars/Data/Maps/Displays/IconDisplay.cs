#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Maps.Drawers.OtherDrawers;
using TribalWars.Data.Maps.Drawers.VillageDrawers;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Markers;
using System.IO;
using TribalWars.Tools;
#endregion

namespace TribalWars.Data.Maps.Displays
{
    /// <summary>
    /// Create and cache IconDrawers
    /// </summary>
    public sealed class IconDisplay : DisplayBase
    {
        /// <summary>
        /// Which world.dat to use for displaying seas, mountains, ...
        /// </summary>
        public enum Scenery
        {
            Old = 0,
            New = 1
        }

        #region Fields
        public const int StandardIconWidth = 53;
        public const int StandardIconHeight = 38;

        /// <summary>
        /// Width x Height on different zoom levels (index is zoom level)
        /// </summary>
        private static readonly List<Tuple<int, int>> VillageSizes = new TupleList<int, int>
            {
                {0, 0}, // there is no zoom 0
                {StandardIconWidth, StandardIconHeight},
                {40, 29},
                {35, 25},
                {30, 22},
                {25, 18},
                {20, 14},
                {15, 11}
            };

        private readonly MemoryStream _background;
        private readonly Dictionary<int, DrawerBase> _backgroundCache;

        private const int BackgroundGrasCount = 4;
        private const int BackgroundSea = 12;
        #endregion

        #region Constructors
        public IconDisplay()
            : base(new ZoomInfo(1, VillageSizes.Count - 1, 1))
        {
            var scenery = Scenery.Old;
            if (scenery == Scenery.Old)
            {
                _background = new MemoryStream(WorldData.WorldBackgroundData);
            }
            else
            {
                Debug.Assert(scenery == Scenery.New);
                _background = new MemoryStream(WorldData.WorldBackgroundData2);
            }

            _backgroundCache = new Dictionary<int, DrawerBase>();
            _backgroundCache.Add(0, CreateArray(new BackgroundDrawer(Icons.Background.gras1)));
            _backgroundCache.Add(1, CreateArray(new BackgroundDrawer(Icons.Background.gras2)));
            _backgroundCache.Add(2, CreateArray(new BackgroundDrawer(Icons.Background.gras3)));
            _backgroundCache.Add(3, CreateArray(new BackgroundDrawer(Icons.Background.gras4)));

            _backgroundCache.Add(8, CreateArray(new BackgroundDrawer(Icons.Background.berg1)));
            _backgroundCache.Add(9, CreateArray(new BackgroundDrawer(Icons.Background.berg2)));
            _backgroundCache.Add(10, CreateArray(new BackgroundDrawer(Icons.Background.berg3)));
            _backgroundCache.Add(11, CreateArray(new BackgroundDrawer(Icons.Background.berg4)));

            _backgroundCache.Add(BackgroundSea, CreateArray(new BackgroundDrawer(Icons.Background.see)));

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

            //0-3 Gras
            //8-11 Berg
            //12 See
            //16-31 Wald
        }

        private DrawerBase CreateArray(DrawerBase backgroundDrawer)
        {
            return backgroundDrawer;
        }
        #endregion

        #region Public Methods
        protected override DrawerBase CreateVillageDrawerCore(Village.BonusType villageBonus, DrawerData data, MarkerGroup colors)
        {
            string iconName = villageBonus == Village.BonusType.None ? data.IconDrawer : data.BonusIconDrawer;
            if (string.IsNullOrEmpty(iconName))
            {
                return null;
            }

            var icon = (Bitmap)Icons.Villages.ResourceManager.GetObject(iconName);
            Debug.Assert(icon != null);
            return new IconDrawer(icon, colors);
        }

        /// <summary>
        /// A VillageType decorator (off, def, ... icons)
        /// </summary>
        protected override DrawerBase CreateVillageDecoratorDrawerCore(DrawerData data, MarkerGroup colors, DrawerData mainData)
        {
            if (string.IsNullOrEmpty(data.IconDrawer))
            {
                return null;
            }

            var icon = (Bitmap)Icons.Other.ResourceManager.GetObject(data.IconDrawer);
            Debug.Assert(icon != null);
            return new IconDrawerDecorator((VillageType)data.Value, icon);
        }

        private int GetVillageWidthCore(int zoom)
        {
            return VillageSizes[zoom].Item1;
        }

        private int GetVillageHeightCore(int zoom)
        {
            return VillageSizes[zoom].Item2;
        }

        public override int GetVillageHeightSpacing(int zoom)
        {
            return GetVillageHeightCore(zoom);
        }

        public override int GetVillageWidthSpacing(int zoom)
        {
            return GetVillageWidthCore(zoom);
        }

        protected override DrawerBase CreateNonVillageDrawerCore(Point game, Rectangle village)
        {
            _background.Position = game.Y * 1000 + game.X;
            int villageType = _background.ReadByte();

            if (village.Width < 28)
            {
                // Stop displaying mountains, forests and sea
                // BackgroundGrasCount = 0->3 = grass
                if (villageType < BackgroundGrasCount) return _backgroundCache[villageType];
                if (villageType == BackgroundSea) return _backgroundCache[BackgroundSea];
                return _backgroundCache[villageType % BackgroundGrasCount];
            }

            return _backgroundCache[villageType];
        }

        public override int GetVillageWidth(int zoom)
        {
            return GetVillageWidthCore(zoom);
        }

        public override int GetVillageHeight(int zoom)
        {
            return GetVillageHeightCore(zoom);
        }

        public override string ToString()
        {
            return string.Format("IconDisplay ({0}x{1})", StandardIconWidth, StandardIconHeight);
        }
        #endregion
    }
}
