#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Data.Maps;
using TribalWars.Data.Maps.Views;
using TribalWars.Data.Monitoring;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using TribalWars.Data.Villages;

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
        /// Gets the ContextMenu with standard village operations
        /// </summary>
        public VillageContextMenu VillageContextMenu { get; private set; }

        /// <summary>
        /// Gets the ContextMenu with standard player operations
        /// </summary>
        public PlayerContextMenu PlayerContextMenu { get; private set; }

        /// <summary>
        /// Gets the ContextMenu with standard tribe operations
        /// </summary>
        public TribeContextMenu TribeContextMenu { get; private set; }

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
            get { return PlayerYou.Default.Player != null; }
        }

        /// <summary>
        /// Gets or sets the active player
        /// </summary>
        public PlayerYou You
        {
            [DebuggerStepThrough] get { return PlayerYou.Default; }
        }

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
                TribalWars.Translations.TWWords.Culture = value;
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
            get { return DateTime.UtcNow.Add(ServerOffset); }
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
        public TWStatsLinks TwStats { get; private set; }

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
        public string SettingsName { get; set; }

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
            PlayerContextMenu = new PlayerContextMenu();
            TribeContextMenu = new TribeContextMenu();
            VillageContextMenu = new VillageContextMenu();

            EventPublisher = new Publisher();
            Map = new Map();
            MiniMap = new Map(Map);

            _players = new Dictionary<string, Player>();
            _tribes = new Dictionary<string, Tribe>();
            _villages = new WorldVillagesCollection();

            Views = new Dictionary<string, ViewBase>();
            TwStats = new TWStatsLinks();
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
        public bool LoadWorld(string dataPath)
        {
            return LoadWorld(dataPath, InternalStructure.DefaultSettingsString);
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
            Structure.SetPath(dataPath, settings);
            Structure.LoadDictionaries(out _villages, out _players, out _tribes);

            // Settings
            Monitor = new TribalWars.Data.Monitoring.Monitor();
            Data.Builder.ReadSettings(new FileInfo(Structure.CurrentWorldSettingsDirectory + settings), Map, MiniMap);

            if (_villageTypes != null) _villageTypes.Close();
            _villageTypes = Structure.CurrentVillageTypes;

            SettingsName = settings;
            HasLoaded = true;

            EventPublisher.InformLoaded(this, EventArgs.Empty);
            EventPublisher.InformSettingsLoaded(this, EventArgs.Empty);
            Map.EventPublisher.PaintMap(this);

            return true;
        }

        /// <summary>
        /// Saves the user settings
        /// </summary>
        public void SaveWorld()
        {
            FileInfo sets = new FileInfo(Structure.CurrentWorldSettingsDirectory + SettingsName);
            Data.Builder.WriteSettings(sets, Map);
        }

        ///// <summary>
        ///// Saves the current settings
        ///// </summary>
        ///// <param name="name">Filename of the settings file</param>
        //public void Save(string name)
        //{
        //    FileInfo file = new FileInfo(World.Default.Structure.CurrentWorldSettingsDirectory + name);
        //    Save(file);
        //}

        ///// <summary>
        ///// Saves the current settings
        ///// </summary>
        ///// <param name="file">FileInfo of the settings file</param>
        //public void Save(FileInfo file)
        //{
        //    using (XmlTextWriter worldXml = new XmlTextWriter(System.IO.File.Open(file.FullName, FileMode.Create, FileAccess.Write), System.Text.Encoding.UTF8))
        //    {
        //        worldXml.Indentation = 3;
        //        worldXml.IndentChar = ' ';
        //        worldXml.Formatting = Formatting.Indented;
        //        WriteXml(worldXml);
        //    }
        //}

        ///// <summary>
        ///// Loads the specified settings
        ///// </summary>
        ///// <param name="name">Name of the settings file</param>
        //public void Load(string name)
        //{
        //    FileInfo file = new FileInfo(World.Default.Structure.CurrentWorldSettingsDirectory + name);
        //    Load(file);
        //}

        ///// <summary>
        ///// Loads the specified settings
        ///// </summary>
        ///// <param name="file">FileInfo of the settings file</param>
        //public void Load(FileInfo file)
        //{
        //    if (file != null && file.Exists)
        //    {
        //        using (XmlReader worldXml = XmlReader.Create(System.IO.File.Open(file.FullName, FileMode.Open, FileAccess.Read), sets))
        //        {
        //            ReadXml(worldXml);
        //        }
        //        World.Default.EventPublisher.InformSettingsLoaded(this, EventArgs.Empty);
        //        World.Default.Map.EventPublisher.PaintMap(null);
        //    }
        //}
        #endregion

        #region New World
        /// <summary>
        /// Downloads available worlds on the server
        /// </summary>
        public static string[] GetAllWorlds()
        {
            return InternalStructure.DownloadWorlds();
        }

        public void CreateNewWorld(string path)
        {
            InternalStructure.CreateWorld(path);
        }
        #endregion

        #region Public Methods
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
        #endregion

        #region Finders
        /// <summary>
        /// Finds a tribe based on tw id
        /// </summary>
        public Tribe GetTribe(int id)
        {
            foreach (Tribe tribe in Tribes.Values)
            {
                if (tribe.ID == id)
                    return tribe;
            }
            return null;
        }

        /// <summary>
        /// Finds a player based on tw id
        /// </summary>
        public Player GetPlayer(int id)
        {
            foreach (Player player in Players.Values)
            {
                if (player.Id == id)
                    return player;
            }
            return null;
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
                    Point loc = new Point(x, y);
                    if (World.Default.Villages.ContainsKey(loc))
                    {
                        return World.Default.Villages[loc];
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
            Village village = null;
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
            if (World.Default.Players.ContainsKey(input.ToUpper(CultureInfo.InvariantCulture)))
            {
                return World.Default.Players[input.ToUpper(CultureInfo.InvariantCulture)];
            }
            return null;
        }

        /// <summary>
        /// Checks if the input string is a tribe
        /// </summary>
        public Tribe GetTribe(string input)
        {
            if (World.Default.Tribes.ContainsKey(input.ToUpper(CultureInfo.InvariantCulture)))
            {
                return World.Default.Tribes[input.ToUpper(CultureInfo.InvariantCulture)];
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
        /// <summary>
        /// Contains all the villages in the world
        /// </summary>
        /// <remarks>Making an int, Village appear as a Point, Village collection for increased performance</remarks>
        public class WorldVillagesCollection
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
                return _v.ContainsKey(c(p));
            }

            /// <summary>
            /// Tries to get a village from the given location
            /// </summary>
            public bool TryGetValue(Point p, out Village value)
            {
                return _v.TryGetValue(c(p), out value);
            }

            /// <summary>
            /// Gets one village
            /// </summary>
            public Village this[Point p]
            {
                get { return _v[c(p)]; }
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
                _v.Add(c(p), v);
            }
            #endregion

            #region Private Methods
            private int c(Point p)
            {
                return p.X*1000 + p.Y;
            }
            #endregion
        }

        /*public class WorldVillagesCollection
        {
            #region Fields
            private System.Collections.Hashtable _v;
            #endregion

            #region Constructors
            public WorldVillagesCollection()
            {
                _v = new System.Collections.Hashtable();
            }

            public WorldVillagesCollection(int x)
            {
                _v = new System.Collections.Hashtable(x);
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// Checks if there is a village
            /// </summary>
            public bool ContainsKey(Point p)
            {
                return _v.ContainsKey(c(p));
            }

            /// <summary>
            /// Tries to get a village from the given location
            /// </summary>
            public bool TryGetValue(Point p, out Village value)
            {
                if (_v.ContainsKey(c(p)))
                {
                    value = (Village)_v[c(p)];
                    return true;
                }
                value = null;
                return false;
            }

            /// <summary>
            /// Gets one village
            /// </summary>
            public Village this[Point p]
            {
                get { return (Village)_v[c(p)]; }
            }

            /// <summary>
            /// Gets all villages
            /// </summary>
            public IEnumerable<Village> Values
            {
                get
                {
                    foreach (object obj in _v.Values)
                    {
                        yield return (Village)obj;
                    }
                }
            }

            /// <summary>
            /// Adds a village
            /// </summary>
            public void Add(Point p, Village v)
            {
                _v.Add(c(p), v);
            }
            #endregion

            #region Private Methods
            private int c(Point p)
            {
                return p.X * 1000 + p.Y;
            }
            #endregion
        }*/
        #endregion

        #region TWStats
        /// <summary>
        /// Contains direct links to differnt TW Stats pages
        /// </summary>
        public class TWStatsLinks
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
            private Uri _twStats;
            #endregion

            #region Properties
            /// <summary>
            /// Gets the general TW Stats link
            /// </summary>
            public Uri Default
            {
                get { return _twStats; }
                set { _twStats = value; }
            }

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