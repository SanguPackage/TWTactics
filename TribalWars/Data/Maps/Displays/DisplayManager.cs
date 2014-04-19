#region Using

using System.Collections.Generic;
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
    public class DisplayManager
    {
        #region Fields
        private readonly Map _map;

        private readonly Dictionary<DisplayTypes, DisplayBase> _displays;
        private DisplayBase _currentDisplay;
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
        public DisplayBase CurrentDisplay
        {
            get { return _currentDisplay; }
        }
        #endregion

        #region Constructors
        public DisplayManager(Map map)
            : this(map, DisplayTypes.None)
        {
            
        }

        public DisplayManager(Map map, DisplayTypes type)
        {
            _displays = new Dictionary<DisplayTypes, DisplayBase>();
            _displays[DisplayTypes.Icon] = new IconDisplay();
            _displays[DisplayTypes.Shape] = new ShapeDisplay();
            _displays[DisplayTypes.MiniMap] = new MiniMapDisplay();

            _map = map;

            // TODO: De DisplayManager zit hier nog in de Views Namespace.. Die in de Displays namespace stoppen
            // Alle displays zitten nog in de Views Namespace blijkbaar.
            // --> DisplayManager property noemt ook nog de ViewManager -> Hernoemen
            // De _views komt in World.Default te zitten (en wordt gelezen uit World.xml)

            // Dan alles eens opnieuw uittekenen om te zien waar we juist verkeerd zitten. (het is ingewikkelder dan het moet zijn denk ik)
            // Bekijken nut van de DrawerData/ViewData want daarvan weet ik het niet zo goed meer :(

            // we hebben dan een IconDisplay, MiniMapDisplay en ShapeDisplay
            // --> De Displays beslissen welke "Views" actief zijn voor welke DisplayBase
            // --> Dus de Views verdwijnen niet!

            
            _markPlayer = new SortedDictionary<int, MarkerGroup>();
            _markTribe = new SortedDictionary<int, MarkerGroup>();
            _currentDisplayType = type;
            if (type != DisplayTypes.None)
                _currentDisplay = _displays[type];
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Draws a village on the map
        /// </summary>
        /// <param name="g">The graphics object</param>
        /// <param name="game">The game location of the village</param>
        /// <param name="mapX">The map X coordinate</param>
        /// <param name="mapY">The map Y coordinate</param>
        /// <param name="width">The width of the village</param>
        /// <param name="height">The height of the village</param>
        public void Paint(Graphics g, Point game, int mapX, int mapY, int width, int height)
        {
            /*if (game.X == 411 && game.Y == 448)
            {
                // with decorator
                int q = 10;
            }*/
            /*if (game.X.ToString() + '|' + game.Y.ToString() == "622|624") // && game.Y == 472)
            {
                // error
                int q = 10;
            }*/

            // TODO
            // momenteel zijn de Decorators verbonden aan de settings files
            // maar een VillageType - die moet sowieso altijd getekend worden
            // decorator wordt dan iets exclusiever: euh?
            // mss moet er helemaal geen decorator zijn...

            // TODO:
            // misschien moet dit ook al in de _currentDisplay zitten? (ie abstract op de DisplayBase)
            // ShapeDisplay heeft geen NonVillageDrawer
            // MiniShapeDisplay moet geen decorators tonen
            DrawerBase finalCache;
            Village village;
            if (World.Default.Villages.TryGetValue(game, out village))
            {
                MarkerGroup markerGroup;
                if (!village.HasPlayer) markerGroup = _map.MarkerManager.AbandonedMarker;
                else
                {
                    Player ply = village.Player;
                    if (!_markPlayer.TryGetValue(ply.Id, out markerGroup))
                    {
                        if (!(ply.HasTribe && _markTribe.TryGetValue(ply.Tribe.ID, out markerGroup)))
                            markerGroup = _map.MarkerManager.EnemyMarker;
                    }
                }

                DrawerData mainData = World.Default.Views[markerGroup.View].GetDrawer(village);
                finalCache = _currentDisplay.CreateDrawer(mainData, markerGroup, null);
                finalCache.PaintVillage(g, mapX, mapY, width, height);

                if (_currentDisplay.SupportDecorators)
                {
                    // Allows show VillageType
                    if (village.Type != VillageType.None)
                    {
                        DrawerData data = World.Default.Views[Types.VillageType.ToString()].GetDrawer(village);
                        if (data != null)
                        {
                            DrawerBase decoratorVillageType = _currentDisplay.CreateDrawer(data, markerGroup, mainData);
                            if (decoratorVillageType != null)
                                decoratorVillageType.PaintVillage(g, mapX, mapY, width, height);
                        }
                    }

                    // Show extra decorators
                    if (markerGroup.HasDecorator)
                    {
                        DrawerData data = World.Default.Views[markerGroup.Decorator].GetDrawer(village);
                        if (data != null)
                        {
                            DrawerBase drawer = _currentDisplay.CreateDrawer(data, markerGroup, mainData);
                            drawer.PaintVillage(g, mapX, mapY, width, height);
                        }
                    }
                }
            }
            else //if (_currentDisplay.SupportsBackground)
            {
                finalCache = _currentDisplay.CreateNonVillageDrawer(game);
                if (finalCache != null)
                    finalCache.PaintVillage(g, mapX, mapY, width, height);
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
                        _markPlayer.Add(player.Id, markerGroup);
                }

                foreach (Tribe tribe in markerGroup.Tribes)
                {
                    if (!_markTribe.ContainsKey(tribe.ID))
                        _markTribe.Add(tribe.ID, markerGroup);
                }
            }
        }

        /// <summary>
        /// Cache you and your tribe markers
        /// </summary>
        private void CacheYouMarkers()
        {
            Player you = World.Default.You.Player;
            if (you != null)
            {
                _markPlayer.Add(you.Id, _map.MarkerManager.YourMarker);
                Tribe youTribe = World.Default.You.Player.Tribe;
                if (youTribe != null)
                {
                    _markTribe.Add(youTribe.ID, _map.MarkerManager.YourTribeMarker);
                }
            }
        }

        /// <summary>
        /// TODO: ERROR on reading new settings, loading a new world, ..
        /// </summary>
        public void Reset(DisplayTypes type)
        {
            _currentDisplayType = type;
            _currentDisplay = _displays[type];
        }

        public override string ToString()
        {
            return string.Format("DisplayManager: {0}", CurrentDisplayType.ToString());
        }
        #endregion
    }
}