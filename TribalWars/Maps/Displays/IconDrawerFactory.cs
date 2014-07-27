#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.IO;
using TribalWars.Maps.Drawers;
using TribalWars.Maps.Drawers.OtherDrawers;
using TribalWars.Maps.Drawers.VillageDrawers;
using TribalWars.Maps.Markers;
using TribalWars.Tools;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Displays
{
    /// <summary>
    /// Create and cache TW image IconDrawers
    /// </summary>
    public sealed class IconDrawerFactory : DrawerFactoryBase
    {
        #region Constants & Fields
        private const int StandardIconWidth = 53;
        private const int StandardIconHeight = 38;

        /// <summary>
        /// When zooming out on IconDisplay after reaching MaxZoomLevel
        /// auto switch to Icon display with this rectangle size
        /// </summary>
        public const int MinVillageSide = 11;

        /// <summary>
        /// When zooming in on ShapeDisplay after reaching MaxZoomLevel
        /// auto switch to Shape index with this index in VillageSizes
        /// </summary>
        public const int AutoSwitchFromShapeIndex = 4;

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
                {15, MinVillageSide}
            };

        private static readonly MemoryStream _background;
        private static readonly MemoryStream _background2;
        private static readonly Dictionary<int, DrawerBase> _backgroundCache;

        private readonly MemoryStream _activeBackground;

        private const int BackgroundGrasCount = 4;
        private const int BackgroundSea = 12;
        #endregion

        #region Properties
        public override bool AllowText
        {
            get { return true; }
        }

        public override DisplayTypes Type
        {
            get { return DisplayTypes.Icon; }
        }
        #endregion

        #region Constructors
        static IconDrawerFactory()
        {
            _background = new MemoryStream(WorldData.WorldBackgroundData);
            _background2 = new MemoryStream(WorldData.WorldBackgroundData2);

            _backgroundCache = new Dictionary<int, DrawerBase>();
            _backgroundCache.Add(0, new BackgroundDrawer(Icons.Background.gras1));
            _backgroundCache.Add(1, new BackgroundDrawer(Icons.Background.gras2));
            _backgroundCache.Add(2, new BackgroundDrawer(Icons.Background.gras3));
            _backgroundCache.Add(3, new BackgroundDrawer(Icons.Background.gras4));

            _backgroundCache.Add(8, new BackgroundDrawer(Icons.Background.berg1));
            _backgroundCache.Add(9, new BackgroundDrawer(Icons.Background.berg2));
            _backgroundCache.Add(10, new BackgroundDrawer(Icons.Background.berg3));
            _backgroundCache.Add(11, new BackgroundDrawer(Icons.Background.berg4));

            _backgroundCache.Add(BackgroundSea, new BackgroundDrawer(Icons.Background.see));

            _backgroundCache.Add(16, new BackgroundDrawer(Icons.Background.forest0000));
            _backgroundCache.Add(17, new BackgroundDrawer(Icons.Background.forest0001));
            _backgroundCache.Add(18, new BackgroundDrawer(Icons.Background.forest0010));
            _backgroundCache.Add(19, new BackgroundDrawer(Icons.Background.forest0011));
            _backgroundCache.Add(20, new BackgroundDrawer(Icons.Background.forest0100));
            _backgroundCache.Add(21, new BackgroundDrawer(Icons.Background.forest0101));
            _backgroundCache.Add(22, new BackgroundDrawer(Icons.Background.forest0110));
            _backgroundCache.Add(23, new BackgroundDrawer(Icons.Background.forest0111));
            _backgroundCache.Add(24, new BackgroundDrawer(Icons.Background.forest1000));
            _backgroundCache.Add(25, new BackgroundDrawer(Icons.Background.forest1001));
            _backgroundCache.Add(26, new BackgroundDrawer(Icons.Background.forest1010));
            _backgroundCache.Add(27, new BackgroundDrawer(Icons.Background.forest1011));
            _backgroundCache.Add(28, new BackgroundDrawer(Icons.Background.forest1100));
            _backgroundCache.Add(29, new BackgroundDrawer(Icons.Background.forest1101));
            _backgroundCache.Add(30, new BackgroundDrawer(Icons.Background.forest1110));
            _backgroundCache.Add(31, new BackgroundDrawer(Icons.Background.forest1111));

            //0-3 Gras
            //8-11 Berg
            //12 See
            //16-31 Wald
        }

        public IconDrawerFactory(int zoomLevel, Scenery scenery)
            : base(CreateZoom(zoomLevel))
        {
            if (scenery == Scenery.Old)
            {
                _activeBackground = _background;
            }
            else
            {
                Debug.Assert(scenery == Scenery.New);
                _activeBackground = _background2;
            }
        }

        public static ZoomInfo CreateZoom(int zoomLevel)
        {
            return new ZoomInfo(1, VillageSizes.Count - 1, zoomLevel);
        }
        #endregion

        #region Public Methods
        public override int GetMinimumZoomLevel(Size maxVillageSize, bool tryStayInCurrentZoom, out bool couldSatisfyVillageSize)
        {
            int newZoom = 0;

            // Start at 1: index 0 is empty
            for (int i = 1; i < VillageSizes.Count; i++)
            {
                var current = VillageSizes.ElementAt(i);
                if (current.Item1 <= maxVillageSize.Width && current.Item2 <= maxVillageSize.Height)
                {
                    newZoom = i;
                    break;
                }
            }

            if (newZoom == 0)
            {
                couldSatisfyVillageSize = false;
                bool hackForToShapeDisplaySwitching;
                return base.GetMinimumZoomLevel(maxVillageSize, tryStayInCurrentZoom, out hackForToShapeDisplaySwitching);
            }

            couldSatisfyVillageSize = true;
            return Math.Max(newZoom, Zoom.Current);
        }

        protected override DrawerBase CreateVillageDrawerCore(Village.BonusType villageBonus, DrawerData data, Marker marker)
        {
            string iconName = villageBonus == Village.BonusType.None ? data.IconDrawer : data.BonusIconDrawer;
            if (string.IsNullOrEmpty(iconName))
            {
                return null;
            }

            var icon = (Bitmap)Icons.Villages.ResourceManager.GetObject(iconName);
            Debug.Assert(icon != null);
            return new IconDrawer(icon, marker.Settings);
        }

        /// <summary>
        /// A VillageType decorator (off, def, ... icons)
        /// </summary>
        protected override DrawerBase CreateVillageDecoratorDrawerCore(DrawerData data, Marker colors, DrawerData mainData)
        {
            if ((VillageType)data.Value == VillageType.None || Zoom.Current != 1)
            {
                return null;
            }

            Bitmap icon = null;
            if (!string.IsNullOrEmpty(data.IconDrawer))
            {
                icon = (Bitmap)Icons.Other.ResourceManager.GetObject(data.IconDrawer);
            }
            return new IconDrawerDecorator((VillageType)data.Value, icon);
        }

        /// <summary>
        /// Gets the size of a village
        /// </summary>
        protected override VillageDimensions CalculateVillageDimensions()
        {
            return new VillageDimensions(new Size(VillageSizes[Zoom.Current].Item1, VillageSizes[Zoom.Current].Item2));
        }

        protected override DrawerBase CreateNonVillageDrawerCore(Point game, Rectangle village)
        {
            _activeBackground.Position = game.Y * 1000 + game.X;
            int villageType = _activeBackground.ReadByte();

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

        public override string ToString()
        {
            return string.Format("IconDisplay ({0}x{1})", StandardIconWidth, StandardIconHeight);
        }
        #endregion

        #region Scenery Enum
        /// <summary>
        /// Which world.dat to use for displaying seas, mountains, ...
        /// </summary>
        public enum Scenery
        {
            Old = 0,
            New = 1
        }
        #endregion
    }
}
