#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Data.Maps;
using TribalWars.Data.Maps.Views;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using TribalWars.Data.Villages;
using TribalWars.Tools;
using Monitor = TribalWars.Data.Monitoring.Monitor;

#endregion

namespace TribalWars.Data
{
    /// <summary>
    /// Defines a TW world
    /// </summary>
    public partial class World : IDisposable
    {
        #region Fields
        private CultureInfo _culture;

        private Dictionary<string, Player> _players;
        private WorldVillagesCollection _villages;
        private Dictionary<string, Tribe> _tribes;
        private FileStream _villageTypes;

        private static Regex _villagePattern;
        #endregion

        #region Properties
        /// <summary>
        /// Gets all map views
        /// </summary>
        public Dictionary<string, ViewBase> Views { get; private set; }

        /// <summary>
        /// Gets the main WorldMap
        /// </summary>
        public Map Map { get; private set; }

        /// <summary>
        /// Gets the main WorldMiniMap
        /// </summary>
        public Map MiniMap { get; private set; }

        /// <summary>
        /// Gets a value indicating if there is an active player
        /// </summary>
        public bool PlayerSelected
        {
            get { return You != null; }
        }

        /// <summary>
        /// Gets or sets the active player
        /// </summary>
        public Player You { get; set; }

        /// <summary>
        /// Gets a value indicating if world data has been loaded
        /// </summary>
        public bool HasLoaded { get; private set; }

        /// <summary>
        /// Gets the URL of the Tribal Wars server
        /// </summary>
        public Uri Server { get; set; }

        /// <summary>
        /// Gets the culture of the TW server
        /// </summary>
        public CultureInfo Culture
        {
            get { return _culture; }
            set
            {
                Translations.TWWords.Culture = value;

                Thread.CurrentThread.CurrentCulture = value;
                Thread.CurrentThread.CurrentUICulture = value;

                _culture = value;
            }
        }

        /// <summary>
        /// Gets the name of the World
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the internal directory structure wrapper
        /// </summary>
        internal InternalStructure Structure { get; private set; }

        /// <summary>
        /// Gets or sets the time difference between local and server
        /// </summary>
        public TimeSpan ServerOffset { get; set; }

        /// <summary>
        /// Gets the current Tribal Wars server time
        /// </summary>
        public DateTime ServerTime
        {
            get { return DateTime.Now.Add(ServerOffset); }
        }

        /// <summary>
        /// Gets the world speed
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Gets the world unit speed
        /// </summary>
        public float UnitSpeed { get; set; }

        /// <summary>
        /// Gets the TW Uris
        /// </summary>
        public TwStatsLinks TwStats { get; private set; }

        /// <summary>
        /// Gets the gameplay link
        /// </summary>
        /// <remarks>Replace {0} by village id, {1} by the screen</remarks>
        public string GameLink { get; set; }

        /// <summary>
        /// Gets the dictionary with all players
        /// </summary>
        /// <remarks>Key is the name is uppercase</remarks>
        public Dictionary<string, Player> Players
        {
            get { return _players; }
        }

        /// <summary>
        /// Gets the dictionary with all tribes
        /// </summary>
        /// <remarks>Key is the tag in uppercase</remarks>
        public Dictionary<string, Tribe> Tribes
        {
            get { return _tribes; }
        }

        /// <summary>
        /// Gets the dictionary with all villages
        /// </summary>
        /// <remarks>Key is the X|Y coordinate</remarks>
        public WorldVillagesCollection Villages
        {
            get { return _villages; }
        }

        /// <summary>
        /// Gets the player monitoring object
        /// </summary>
        public Monitor Monitor { get; private set; }

        /// <summary>
        /// Gets the object that encapsulates event raising
        /// </summary>
        public Publisher EventPublisher { get; private set; }

        /// <summary>
        /// Gets the date of the current data
        /// </summary>
        public DateTime? CurrentData
        {
            get { return Structure.CurrentData; }
        }

        /// <summary>
        /// Gets the date of the previous data
        /// </summary>
        public DateTime? PreviousData
        {
            get { return Structure.PreviousData; }
        }

        /// <summary>
        /// Gets or sets the settings filename
        /// </summary>
        public string SettingsName { get; private set; }

        /// <summary>
        /// Gets the RegEx pattern to recognize valid Village input
        /// </summary>
        public static Regex VillagePattern
        {
            get
            {
                if (_villagePattern == null)
                {
                    // Does not only match 546|694 but also something like
                    // 546.265, 546-985 etc...
                    _villagePattern = new Regex(@"\(?(\d{1,3})\D(\d{1,3})\)?");
                }
                return _villagePattern;
            }
        }
        #endregion

        #region Constructors
        private World()
        {
            UnitSpeed = 1;

            EventPublisher = new Publisher();
            Map = new Map();
            MiniMap = new Map(Map);

            _players = new Dictionary<string, Player>();
            _tribes = new Dictionary<string, Tribe>();
            _villages = new WorldVillagesCollection();

            Views = new Dictionary<string, ViewBase>();
            TwStats = new TwStatsLinks();

            You = new Player();
        }

        ~World()
        {
            if (_villageTypes != null)
            {
                _villageTypes.Close();
            }
        }
        #endregion

        #region Singleton
        private static readonly World world = new World();
        

        /// <summary>
        /// Gets the currently loaded world
        /// </summary>
        public static World Default
        {
            [DebuggerStepThrough] get { return world; }
        }
        #endregion

        #region LoadWorld
        /// <summary>
        /// Loads world data for the specified path
        /// </summary>
        /// <param name="dataPath">The path to the World directory</param>
        public void LoadWorld(string dataPath)
        {
            LoadWorld(dataPath, InternalStructure.DefaultSettingsString);
        }

        /// <summary>
        /// Loads world data for the specified path &amp; settings file
        /// </summary>
        /// <param name="dataPath">The path to the World directory</param>
        /// <param name="settings">The settings filename</param>
        public bool LoadWorld(string dataPath, string settings)
        {
            HasLoaded = false;
            if (string.IsNullOrEmpty(dataPath) || !Directory.Exists(dataPath))
                return false;

            Structure = new InternalStructure();
            Structure.SetPath(dataPath);
            Structure.LoadDictionaries(out _villages, out _players, out _tribes);

            if (_villageTypes != null) _villageTypes.Close();
            _villageTypes = Structure.CurrentVillageTypes;

            bool settingLoadSuccess = LoadSettingsCore(settings, false);
            if (!settingLoadSuccess)
                return false;

            HasLoaded = true;

            EventPublisher.InformLoaded(this, EventArgs.Empty);
            EventPublisher.InformSettingsLoaded(this, EventArgs.Empty);
            Map.EventPublisher.PaintMap(this);

            return true;
        }

        /// <summary>
        /// Loads settings (markers, ...) for the specified path
        /// </summary>
        /// <param name="settings">The settings filename</param>
        public bool LoadSettings(string settings)
        {
            return LoadSettingsCore(settings, true);
        }

        private bool LoadSettingsCore(string settings, bool publishLoad)
        {
            var settingsFile = new FileInfo(Structure.CurrentWorldSettingsDirectory + settings);
            if (!settingsFile.Exists)
                return false;

            Monitor = new Monitor();
            Builder.ReadSettings(settingsFile, Map, Monitor);
            SettingsName = settings;

            InvalidateMarkers();

            if (publishLoad)
            {
                EventPublisher.InformSettingsLoaded(this, EventArgs.Empty);
            }

            return true;
        }
        #endregion

        #region SaveSettings
        /// <summary>
        /// Saves the user and world.xml settings
        /// </summary>
        public void SaveSettings()
        {
            // .sets file
            SaveSettings(SettingsName);

            // world.xml
            Debug.Assert(ServerOffset.Hours == (int)ServerOffset.TotalHours);

            var worldSettings = WorldTemplate.World.LoadFromFile(Structure.CurrentWorldXmlPath.FullName);
            worldSettings.Offset = ServerOffset.Hours.ToString(CultureInfo.InvariantCulture);
            worldSettings.SaveToFile(Structure.CurrentWorldXmlPath.FullName);
        }

        /// <summary>
        /// Saves the user settings (not the world.xml)
        /// </summary>
        /// <param name="settingsName">Just the settings file name, no path information</param>
        public void SaveSettings(string settingsName)
        {
            var sets = new FileInfo(Structure.CurrentWorldSettingsDirectory + settingsName);
            Builder.WriteSettings(sets, Map, Monitor);

            SettingsName = new FileInfo(settingsName).Name;
        }
        #endregion

        #region New World
        /// <summary>
        /// Downloads available worlds on the server
        /// </summary>
        public static IEnumerable<string> GetAllWorlds(string serverName)
        {
            return InternalStructure.DownloadWorlds(serverName);
        }

        public static void CreateNewWorld(string path, InternalStructure.ServerInfo server)
        {
            InternalStructure.CreateWorld(path, server);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Rebuilds all markers on all maps
        /// </summary>
        public void InvalidateMarkers()
        {
            Map.Display.DisplayManager.CacheSpecialMarkers();
            MiniMap.Display.DisplayManager.CacheSpecialMarkers();
        }

        /// <summary>
        /// Gets the type of a certain village
        /// </summary>
        /// <remarks>Offense, Defense, ...</remarks>
        public VillageType GetVillageType(Village village)
        {
            _villageTypes.Position = village.Y*1000 + village.X;
            return (VillageType) _villageTypes.ReadByte();
        }

        /// <summary>
        /// Sets the type of a village
        /// </summary>
        /// <remarks>Offense, Defense, ...</remarks>
        public void SetVillageType(Village village, VillageType value)
        {
            _villageTypes.Position = village.Y*1000 + village.X;
            _villageTypes.WriteByte((byte) value);
        }

        /// <summary>
        /// Force redrawing the maps
        /// </summary>
        public void DrawMaps(bool resetBackgroundCache = true)
        {
            if (resetBackgroundCache)
            {
                Map.Display.ResetCache();
                MiniMap.Display.ResetCache();
            }

            Map.Control.Invalidate();
            MiniMap.Control.Invalidate();
        }
        #endregion

        #region Finders
        /// <summary>
        /// Finds a tribe based on tw id
        /// </summary>
        public Tribe GetTribe(int id)
        {
            return Tribes.Values.FirstOrDefault(tribe => tribe.Id == id);
        }

        /// <summary>
        /// Finds a player based on tw id
        /// </summary>
        public Player GetPlayer(int id)
        {
            return Players.Values.FirstOrDefault(player => player.Id == id);
        }

        /// <summary>
        /// Checks if the input string is a TW coordinate
        /// </summary>
        public Point? GetCoordinates(string input)
        {
            Match match = VillagePattern.Match(input.Trim());
            if (match.Success)
            {
                int x;
                int y;
                if (int.TryParse(match.Groups[1].Value, out x) && int.TryParse(match.Groups[2].Value, out y)
                    && x > 0 && x < 1000 && y > 0 && y < 1000)
                {
                    return new Point(x, y);
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if the input string is a village
        /// </summary>
        public Village GetVillage(string input)
        {
            Match match = VillagePattern.Match(input.Trim());
            if (match.Success)
            {
                int x;
                int y;
                if (int.TryParse(match.Groups[1].Value, out x) && int.TryParse(match.Groups[2].Value, out y))
                {
                    var loc = new Point(x, y);
                    if (Default.Villages.ContainsKey(loc))
                    {
                        return Default.Villages[loc];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the village at the specified location
        /// </summary>
        public Village GetVillage(Point location)
        {
            Village village;
            Villages.TryGetValue(location, out village);
            return village;
        }

        /// <summary>
        /// Gets the village at the specified location
        /// </summary>
        public Village GetVillage(int x, int y)
        {
            return GetVillage(new Point(x, y));
        }

        /// <summary>
        /// Checks if the input string is a player
        /// </summary>
        public Player GetPlayer(string input)
        {
            if (Default.Players.ContainsKey(input.ToUpper(CultureInfo.InvariantCulture)))
            {
                return Default.Players[input.ToUpper(CultureInfo.InvariantCulture)];
            }
            return null;
        }

        /// <summary>
        /// Checks if the input string is a tribe
        /// </summary>
        public Tribe GetTribe(string input)
        {
            if (Default.Tribes.ContainsKey(input.ToUpper(CultureInfo.InvariantCulture)))
            {
                return Default.Tribes[input.ToUpper(CultureInfo.InvariantCulture)];
            }
            return null;
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
        }
        #endregion

        #region WorldVillagesCollection
        ///// <summary>
        ///// Contains all the villages in the world
        ///// </summary>
        ///// <remarks>Making an int, Village appear as a Point, Village collection for increased performance</remarks>
        //public sealed class WorldVillagesCollection
        //{
        //    #region Fields
        //    //private readonly Dictionary<int, Village> _v;
        //    private readonly Village[] _v;
        //    #endregion

        //    #region Constructors
        //    public WorldVillagesCollection()
        //    {
        //        //_v = new Dictionary<int, Village>();
        //        _v = new Village[1000000];
        //    }

        //    public WorldVillagesCollection(int x)
        //    {
        //        //_v = new Dictionary<int, Village>(x);
        //        _v = new Village[1000000];
        //    }
        //    #endregion

        //    #region Public Methods
        //    /// <summary>
        //    /// Checks if there is a village
        //    /// </summary>
        //    public bool ContainsKey(Point p)
        //    {
        //        //return _v.ContainsKey(C(p));
        //        return _v[C(p)] != null;
        //    }

        //    /// <summary>
        //    /// Tries to get a village from the given location
        //    /// </summary>
        //    public bool TryGetValue(Point p, out Village value)
        //    {
        //        //return _v.TryGetValue(C(p), out value);
        //        value = _v[C(p)];
        //        return value != null;
        //    }

        //    /// <summary>
        //    /// Gets one village
        //    /// </summary>
        //    public Village this[Point p]
        //    {
        //        get { return _v[C(p)]; }
        //    }

        //    /// <summary>
        //    /// Gets all villages
        //    /// </summary>
        //    public IEnumerable<Village> Values
        //    {
        //        //get { return _v.Values; }
        //        get { return _v; }
        //    }

        //    /// <summary>
        //    /// Adds a village
        //    /// </summary>
        //    public void Add(Point p, Village v)
        //    {
        //        //_v.Add(C(p), v);
        //        _v[C(p)] = v;
        //    }
        //    #endregion

        //    #region Private Methods
        //    private static int C(Point p)
        //    {
        //        return p.X * 1000 + p.Y;
        //    }
        //    private static int C(int x, int y)
        //    {
        //        return x * 1000 + y;
        //    }
        //    #endregion
        //}

        /// <summary>
        /// Contains all the villages in the world
        /// </summary>
        /// <remarks>Making an int, Village appear as a Point, Village collection for increased performance</remarks>
        public sealed class WorldVillagesCollection
        {
            #region Fields
            private readonly Dictionary<int, Village> _v;
            #endregion

            #region Constructors
            public WorldVillagesCollection()
            {
                _v = new Dictionary<int, Village>();
            }

            public WorldVillagesCollection(int x)
            {
                _v = new Dictionary<int, Village>(x);
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// Checks if there is a village
            /// </summary>
            public bool ContainsKey(Point p)
            {
                return _v.ContainsKey(C(p));
            }

            /// <summary>
            /// Tries to get a village from the given location
            /// </summary>
            public bool TryGetValue(Point p, out Village value)
            {
                return _v.TryGetValue(C(p), out value);
            }

            /// <summary>
            /// Gets one village
            /// </summary>
            public Village this[Point p]
            {
                get { return _v[C(p)]; }
            }

            /// <summary>
            /// Gets all villages
            /// </summary>
            public IEnumerable<Village> Values
            {
                get { return _v.Values; }
            }

            /// <summary>
            /// Adds a village
            /// </summary>
            public void Add(Point p, Village v)
            {
                _v.Add(C(p), v);
            }
            #endregion

            #region Private Methods
            private static int C(Point p)
            {
                return p.X * 1000 + p.Y;
            }
            private static int C(int x, int y)
            {
                return x * 1000 + y;
            }
            #endregion
        }
        #endregion

        #region TWStats
        /// <summary>
        /// Contains direct links to differnt TW Stats pages
        /// </summary>
        public class TwStatsLinks
        {
            #region Enums
            /// <summary>
            /// The different types of graphs offered by TWStats
            /// </summary>
            public enum Graphs
            {
                points,
                villages,
                od,
                oda,
                odd,
                rank,
                members
            }
            #endregion

            #region Fields
            #endregion

            #region Properties
            /// <summary>
            /// Gets the general TW Stats link
            /// </summary>
            public Uri Default { get; set; }

            /// <summary>
            /// Gets the direct link to TW stats village overview
            /// </summary>
            /// <remarks>Replace {0} by the village id</remarks>
            public string Village { get; set; }

            /// <summary>
            /// Gets the direct link to TW stats player overview
            /// </summary>
            /// <remarks>Replace {0} by the player id</remarks>
            public string Player { get; set; }

            /// <summary>
            /// Gets the direct link to TW stats tribe overview
            /// </summary>
            /// <remarks>Replace {0} by the tribe id</remarks>
            public string Tribe { get; set; }

            /// <summary>
            /// Gets the direct link to the TW stats player graphs
            /// </summary>
            /// <remarks>Replace {0} with the player ID, {1} with the tpye of graph</remarks>
            public string PlayerGraph { get; set; }

            /// <summary>
            /// Gets the direct link to the TW stats player graphs
            /// </summary>
            /// <remarks>Replace {0} with the tribe ID, {1} with the tpye of graph</remarks>
            public string TribeGraph { get; set; }
            #endregion
        }
        #endregion
    }
}