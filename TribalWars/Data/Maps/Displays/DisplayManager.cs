#region Using

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Markers;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using TribalWars.Data.Maps.Views;
#endregion

namespace TribalWars.Data.Maps.Displays
{
    /// <summary>
    /// Manages the different view collections
    /// </summary>
    /// <remarks>
    /// Currently the <see cref="DisplayManager"/> allows us to switch between
    /// Shape and Icon displays
    /// </remarks>
    public sealed class DisplayManager
    {
        #region Fields
        private readonly Map _map;

        private readonly Dictionary<DisplayTypes, DisplayBase> _displays;
        private DisplayTypes _currentDisplayType = DisplayTypes.None; 

        private SortedDictionary<int, MarkerGroup> _markTribe;
        private SortedDictionary<int, MarkerGroup> _markPlayer;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current view of the map
        /// </summary>
        public DisplayTypes CurrentDisplayType
        {
            get { return _currentDisplayType; }
        }

        /// <summary>
        /// Gets the current view of the map
        /// </summary>
        public DisplayBase CurrentDisplay { get; private set; }
        #endregion

        #region Constructors
        public DisplayManager(Map map, DisplayTypes type)
        {
            _displays = new Dictionary<DisplayTypes, DisplayBase>();
            if (type == DisplayTypes.MiniMap)
            {
                _displays[DisplayTypes.MiniMap] = new MiniMapDisplay();
            }
            else
            {
                _displays[DisplayTypes.Icon] = new IconDisplay();
                _displays[DisplayTypes.Shape] = new ShapeDisplay();
            }
            
            _map = map;

            _markPlayer = new SortedDictionary<int, MarkerGroup>();
            _markTribe = new SortedDictionary<int, MarkerGroup>();
            _currentDisplayType = type;
            if (type != DisplayTypes.None)
                CurrentDisplay = _displays[type];
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Draws a village on the map
        /// </summary>
        /// <param name="g">The graphics object</param>
        /// <param name="game">The game location of the village</param>
        /// <param name="mapVillage">Where and how big to draw the village</param>
        public void Paint(Graphics g, Point game, Rectangle mapVillage)
        {
            if (!(game.X > 0 && game.X < 1000 && game.Y > 0 && game.Y < 1000))
                return;

            Village village;
            if (World.Default.Villages.TryGetValue(game, out village))
            {
                MarkerGroup markerGroup = GetMarkerGroup(village);

                // Paint village icon/shape
                DrawerData mainData = World.Default.Views[markerGroup.View].GetDrawer(village);
                DrawerBase finalCache = CurrentDisplay.CreateVillageDrawer(village.Bonus, mainData, markerGroup);
                if (finalCache != null)
                {
                    finalCache.PaintVillage(g, mapVillage);

                    if (CurrentDisplay.SupportDecorators && village.Type != VillageType.None)
                    {
                        // Paint extra village decorators
                        DrawerData data = World.Default.Views["VillageType"].GetDrawer(village);
                        DrawerBase decoratorVillageType = CurrentDisplay.CreateVillageDecoratorDrawer(data, markerGroup, mainData);
                        if (decoratorVillageType != null)
                        {
                            decoratorVillageType.PaintVillage(g, mapVillage);
                        }
                    }
                }
                else
                {
                    PaintNonVillage(g, game, mapVillage);
                }
            }
            else
            {
                PaintNonVillage(g, game, mapVillage);
            }
        }

        /// <summary>
        /// Paint grass, mountains, ...
        /// </summary>
        private void PaintNonVillage(Graphics g, Point game, Rectangle mapVillage)
        {
            DrawerBase finalCache = CurrentDisplay.CreateNonVillageDrawer(game, mapVillage);
            if (finalCache != null)
            {
                finalCache.PaintVillage(g, mapVillage);
            }
        }

        /// <summary>
        /// Find out which marker to use for the village
        /// </summary>
        private MarkerGroup GetMarkerGroup(Village village)
        {
            if (!village.HasPlayer)
            {
                return _map.MarkerManager.AbandonedMarker;
            }
            else
            {
                MarkerGroup markerGroup;
                Player ply = village.Player;
                if (!_markPlayer.TryGetValue(ply.Id, out markerGroup))
                {
                    if (!(ply.HasTribe && _markTribe.TryGetValue(ply.Tribe.Id, out markerGroup)))
                    {
                        markerGroup = _map.MarkerManager.EnemyMarker;
                    }
                }
                return markerGroup;
            }
        }

        /// <summary>
        /// Cache all special markers
        /// </summary>
        public void CacheSpecialMarkers()
        {
            _markPlayer = new SortedDictionary<int, MarkerGroup>();
            _markTribe = new SortedDictionary<int, MarkerGroup>();

            CacheYouMarkers();

            foreach (MarkerGroup markerGroup in _map.MarkerManager.Markers)
            {
                foreach (Player player in markerGroup.Players)
                {
                    if ( !_markPlayer.ContainsKey(player.Id))
                    {
                        _markPlayer.Add(player.Id, markerGroup);
                    }
                }

                foreach (Tribe tribe in markerGroup.Tribes)
                {
                    if (!_markTribe.ContainsKey(tribe.Id))
                    {
                        _markTribe.Add(tribe.Id, markerGroup);
                    }
                }
            }
        }

        /// <summary>
        /// Cache you and your tribe markers
        /// </summary>
        private void CacheYouMarkers()
        {
            Player you = World.Default.You;
            if (you != null)
            {
                _markPlayer.Add(you.Id, _map.MarkerManager.YourMarker);
                Tribe youTribe = World.Default.You.Tribe;
                if (youTribe != null)
                {
                    _markTribe.Add(youTribe.Id, _map.MarkerManager.YourTribeMarker);
                }
            }
        }

        public void Reset(DisplayTypes type)
        {
            _currentDisplayType = type;
            CurrentDisplay = _displays[type];
        }

        public override string ToString()
        {
            return string.Format("DisplayManager: {0}", CurrentDisplayType);
        }
        #endregion
    }
}