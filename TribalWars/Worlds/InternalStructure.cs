#region Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Linq;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Forms;
using TribalWars.Forms.Small;
using TribalWars.Maps.Displays;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.WorldTemplate;

#endregion

namespace TribalWars.Worlds
{
    public partial class World
    {
        /// <summary>
        /// Handles the Path and files stuff for the World type
        /// </summary>
        public sealed class InternalStructure
        {
            #region Constants
            private const string AvailableWorlds = "http://www.{0}/backend/get_servers.php";
            private const string UnitGraphicsUrlFormat = "http://dsen.innogamescdn.com/8.24/20950/graphic/unit/unit_{0}.png";
            private const string ServerSettingsUrl = "http://{0}.{1}/interface.php?func={2}";

            /// <summary>
            /// Match input like nl1, de5, en9, ...
            /// </summary>
            private static readonly Regex WorldSplitterRegex = new Regex(@"(\D*)(\d*)");

            private const string FileVillageString = @"village.txt";
            private const string FileTribeString = @"ally.txt";
            private const string FilePlayerString = @"tribe.txt";

            public const string DirectorySettingsString = "Settings";
            public const string DirectoryDataString = "Data";
            private const string DirectoryWorldDataString = "WorldData";
            private const string DirectoryReportsString = "Reports";
            private const string DirectoryScreenshotString = "Screenshot";

            public const string DefaultSettingsString = "default.sets";
            /// <summary>
            /// With .
            /// </summary>
            public const string SettingsExtensionString = ".sets";
            private const string WorldXmlString = "world.xml";
            private const string WorldXmlTemplateString = "WorldSettings.xml";
            
            /// <summary>
            /// Contains the VillageTypes (Off, Def, ...) of all villages
            /// </summary>
            private const string VillageTypesString = "villagetypes.dat";

            private const int WorldPlayerCount = 10000;
            private const int WorldTribeCount = 5000;
            #endregion

            #region Fields
            private static string _currentDirectory;
            private string _currentWorld;
            private string _currentData;
            private string _previousData;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the the URL to village.txt
            /// </summary>
            public string DownloadVillage { get; set; }

            /// <summary>
            /// Gets or sets the the URL to tribe.txt
            /// </summary>
            public string DownloadPlayer { get; set; }

            /// <summary>
            /// Gets or sets the the URL to ally.txt
            /// </summary>
            public string DownloadTribe { get; set; }

            /// <summary>
            /// Gets the date of the current data
            /// </summary>
            public DateTime? CurrentData
            {
                get
                {
                    DateTime test;
                    if (DateTime.TryParseExact(_currentData, "yyyyMMddHH", CultureInfo.InvariantCulture, DateTimeStyles.None, out test))
                    {
                        return test;
                    }
                    return null;
                }
            }

            /// <summary>
            /// Gets the date of the previous data
            /// </summary>
            public DateTime? PreviousData
            {
                get
                {
                    DateTime test;
                    if (DateTime.TryParseExact(_previousData, "yyyyMMddHH", CultureInfo.InvariantCulture, DateTimeStyles.None, out test))
                    {
                        return test;
                    }
                    return null;
                }
            }

            /// <summary>
            /// Gets a MemoryStream containing the user defined village types
            /// </summary>
            public FileStream CurrentVillageTypes
            {
                get
                {
                    if (!File.Exists(CurrentWorldDirectory + VillageTypesString))
                    {
                        using (FileStream stream = File.Create(Path.Combine(CurrentWorldDirectory, VillageTypesString)))
                        {
                            for (int i = 1; i <= 999999; i++)
                                stream.WriteByte(0);
                        }
                    }
                    return File.Open(CurrentWorldDirectory + VillageTypesString, FileMode.Open, FileAccess.ReadWrite);
                }
            }

            /// <summary>
            /// Template directory with files required for creating a new world
            /// </summary>
            private static string WorldTemplateDirectory
            {
                get
                {
                    return AppDomain.CurrentDomain.BaseDirectory + @"\WorldTemplate";
                }
            }
            #endregion

            #region Constructors
            /// <summary>
            /// Sets up the paths
            /// Reads world.xml
            /// Downloads the data if necessary
            /// </summary>
            public bool SetPath(string world)
            {
                try
                {
                    _currentData = string.Empty;
                    _previousData = string.Empty;

                    var worldPath = new DirectoryInfo(world);
                    Debug.Assert(worldPath.Parent != null, "worldPath.Parent != null");
                    if (worldPath.Parent.Name == DirectoryWorldDataString && worldPath.Name != DirectoryWorldDataString)
                    {
                        // general world selected
                        _currentWorld = worldPath.Name;

                        ReadWorldConfiguration(worldPath.FullName);

                        // If there is no datapath, read the last directory
                        string[] dirs = Directory.GetDirectories(CurrentWorldDataDirectory);
                        if (dirs.Length != 0)
                        {
                            _currentData = new DirectoryInfo(dirs[dirs.Length - 1]).Name;
                            if (dirs.Length != 1) _previousData = new DirectoryInfo(dirs[dirs.Length - 2]).Name;

#if !DEBUG
                            // Redownload after x hours
                            TimeSpan? lastDownload = Default.Settings.ServerTime - CurrentData;
                            if (lastDownload.HasValue && lastDownload.Value.TotalHours >= 12)
                            {
                                string text = string.Format("It has been {0} hours since you downloaded the latest TW data.\nDownload now?", (int)lastDownload.Value.TotalHours);
                                DialogResult doDownload = MessageBox.Show(text, "Download latest data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (doDownload == DialogResult.Yes)
                                {
                                    Download();
                                }
                            }
#endif
                        }
                        else
                        {
                            // no data found: download!
                            DownloadNewTwSnapshot();
                            dirs = Directory.GetDirectories(CurrentWorldDataDirectory);
                            _currentData = new DirectoryInfo(dirs[0]).Name;
                        }
                    }
                    else
                    {
                        _currentData = worldPath.Name;
                        worldPath = worldPath.Parent.Parent;
                        Debug.Assert(worldPath != null, "worldPath != null");
                        _currentWorld = worldPath.Name;
                        string[] dirs = Directory.GetDirectories(CurrentWorldDataDirectory).OrderBy(x => x).ToArray();
                        if (dirs.Length > 1)
                        {
                            DirectoryInfo lastDir = null;
                            foreach (DirectoryInfo dir in dirs.Select(x => new DirectoryInfo(x)))
                            {
                                if (dir.Name == _currentData && lastDir != null)
                                {
                                    _previousData = lastDir.Name;
                                }
                                lastDir = dir;
                            }
                        }

                        ReadWorldConfiguration(worldPath.FullName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + "Loading world: " + world);
                    throw;
                }

                return _currentData != string.Empty;
            }

            /// <summary>
            /// World stats (server sets, Buildings & units)
            /// </summary>
            private void ReadWorldConfiguration(string worldPath)
            {
                Builder.ReadWorld(Path.Combine(worldPath, WorldXmlString));
            }
            #endregion

            #region Path Properties
            /// <summary>
            /// Gets the executable directory
            /// </summary>
            private static string MainDirectory
            {
                get
                {
                    if (_currentDirectory == null)
                        _currentDirectory = Environment.CurrentDirectory + "\\";

                    return _currentDirectory;
                }
            }

            /// <summary>
            /// Gets the WorldData directory
            /// </summary>
            /// <remarks>\WorldData\</remarks>
            public static string WorldDataDirectory
            {
                get { return GetPath(MainDirectory + DirectoryWorldDataString + "\\"); }
            }

            /// <summary>
            /// Gets the loaded world directory
            /// </summary>
            /// <remarks>\WorldData\World 1\</remarks>
            public string CurrentWorldDirectory
            {
                get { return GetPath(MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\"); }
            }

            /// <summary>
            /// Gets the location of world.xml
            /// </summary>
            public FileInfo CurrentWorldXmlPath
            {
                get { return new FileInfo(CurrentWorldDirectory + WorldXmlString); }
            }

            /// <summary>
            /// Gets the reports directory
            /// </summary>
            /// <remarks>\WorldData\World 1\Reports\</remarks>
            public string CurrentWorldReportsDirectory
            {
                get { return GetPath(MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryReportsString + "\\"); }
            }

            /// <summary>
            /// Gets the directory with user screenshots
            /// </summary>
            /// <remarks>\WorldData\World 1\Screenshot\</remarks>
            public string CurrentWorldScreenshotDirectory
            {
                get { return GetPath(MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryScreenshotString + "\\"); }
            }

            /// <summary>
            /// Gets the directory with the different settings files
            /// </summary>
            /// <remarks>\WorldData\World 1\Settings\</remarks>
            public string CurrentWorldSettingsDirectory
            {
                get { return GetPath(MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectorySettingsString + "\\"); }
            }

            /// <summary>
            /// Gets the directory with all the downloaded TW data
            /// </summary>
            /// <remarks>\WorldData\World 1\Data\</remarks>
            public string CurrentWorldDataDirectory
            {
                get { return GetPath(MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryDataString + "\\"); }
            }

            /// <summary>
            /// Gets the directory with the currently loaded TW data
            /// </summary>
            /// <remarks>\WorldData\World 1\Data\20080101\</remarks>
            private string CurrentWorldDataDateDirectory
            {
                get { return GetPath(MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryDataString + "\\" + _currentData + "\\"); }
            }

            /// <summary>
            /// Gets the directory with the previously downloaded TW data
            /// </summary>
            private string PreviousWorldDataDateDirectory
            {
                get
                {
                    if (string.IsNullOrEmpty(_previousData)) return null;
                    return GetPath(MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryDataString + "\\" + _previousData + "\\");
                }
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// Get image url for a unit
            /// </summary>
            public string GetUnitImageUrl(UnitTypes type)
            {
                return string.Format(UnitGraphicsUrlFormat, type.ToString().ToLowerInvariant());
            }

            /// <summary>
            /// Checks if a folder contains village, tribe and ally.txt
            /// </summary>
            public static bool IsValidDataPath(string path)
            {
                if (path.Length > 0 && Directory.Exists(path))
                {
                    if (File.Exists(path + @"\" + FileVillageString)
                        && File.Exists(path + @"\" + FileTribeString)
                        && File.Exists(path + @"\" + FilePlayerString))
                        return true;
                }
                return false;
            }
            #endregion

            #region TW Servers Information
            /// <summary>
            /// Gets all TW servers as defined in WorldTemplate
            /// </summary>
            public static IEnumerable<ServerInfo> GetAllServers()
            {
                var list = new List<ServerInfo>();

                var xDoc = XDocument.Load(Path.Combine(WorldTemplateDirectory, "TribalWarsServers.xml"));
                foreach (var server in xDoc.Root.Elements())
                {
                    list.Add(new ServerInfo(server.Element("ServerUrl").Value, server.Element("TWStatsPrefix").Value));
                }
                return list;
            }

            /// <summary>
            /// Get all available worlds on the server
            /// </summary>
            public static IEnumerable<string> GetWorlds(string serverName)
            {
                var file = Network.GetWebRequest(string.Format(AvailableWorlds, serverName));
                var worldsObject = (Hashtable)new Serializer().Deserialize(file);

                string[] worldsPlayerCanStart = worldsObject.Keys.OfType<string>().ToArray();

                var worldsPerWorldType = worldsPlayerCanStart.Select(SplitIntoServerPrefixAndWorldNumber).ToLookup(x => x.Key, x => x.Value);
                var worlds = new List<string>();
                foreach (var worldType in worldsPerWorldType)
                {
                    IGrouping<string, int> fixedType = worldType;
                    IEnumerable<string> allWorldsInType = Enumerable.Range(1, fixedType.Max()).Select(x => fixedType.Key + x);
                    worlds.AddRange(allWorldsInType);
                }

                return worlds.ToArray();
            }

            /// <summary>
            /// Splits TW worlds like (en15, nl39) into the server prefix (en, nl)
            /// and world number (15, 39)
            /// </summary>
            private static KeyValuePair<string, int> SplitIntoServerPrefixAndWorldNumber(string world)
            {
                Match match = WorldSplitterRegex.Match(world);
                return new KeyValuePair<string, int>(match.Groups[1].Value, Convert.ToInt32(match.Groups[2].Value));
            }

            /// <summary>
            /// Simple holder class with server info
            /// that runs TribalWars
            /// </summary>
            public class ServerInfo
            {
                public string ServerUrl { get; private set; }
                public string TwStatsPrefix { get; private set; }

                public ServerInfo(string url, string twStatsPrefix)
                {
                    ServerUrl = url;
                    TwStatsPrefix = twStatsPrefix;
                }

                public override string ToString()
                {
                    return string.Format("Url={0}, TWStats={1}", ServerUrl, TwStatsPrefix);
                }
            }
            #endregion

            #region Create New World
            /// <summary>
            /// Create required infrastructure for a new world
            /// </summary>
            public static void CreateWorld(string path, ServerInfo server)
            {
                var dir = new DirectoryInfo(path);
                string worldName = dir.Name;
                if (!dir.Exists)
                {
                    dir.Create();
                }

                // world.xml
                var worldInfo = WorldConfiguration.LoadFromFile(Path.Combine(WorldTemplateDirectory, WorldXmlTemplateString));
                worldInfo.Name = worldName;
                using (var timeZoneSetter = new TimeZoneForm())
                {
                    if (timeZoneSetter.ShowDialog() == DialogResult.OK)
                    {
                        worldInfo.Offset = timeZoneSetter.ServerOffset.Hours.ToString(CultureInfo.InvariantCulture);
                    }
                }

                TwWorldSettings twWorldSettings = DownloadWorldSettings(worldName, server.ServerUrl);
                worldInfo.Speed = twWorldSettings.Speed.ToString(CultureInfo.InvariantCulture);
                worldInfo.UnitSpeed = twWorldSettings.UnitSpeed.ToString(CultureInfo.InvariantCulture);
                worldInfo.WorldDatScenery = ((int)twWorldSettings.MapScenery).ToString(CultureInfo.InvariantCulture);

                // Fix URIs
                worldInfo.Server = ReplaceServerAndWorld(worldInfo.Server, worldName, server);

                worldInfo.DataVillage = ReplaceServerAndWorld(worldInfo.DataVillage, worldName, server);
                worldInfo.DataPlayer = ReplaceServerAndWorld(worldInfo.DataPlayer, worldName, server);
                worldInfo.DataTribe = ReplaceServerAndWorld(worldInfo.DataTribe, worldName, server);

                worldInfo.GameVillage = ReplaceServerAndWorld(worldInfo.GameVillage, worldName, server);
                worldInfo.GuestPlayer = ReplaceServerAndWorld(worldInfo.GuestPlayer, worldName, server);
                worldInfo.GuestTribe = ReplaceServerAndWorld(worldInfo.GuestTribe, worldName, server);

                worldInfo.TWStatsGeneral = ReplaceServerAndWorld(worldInfo.TWStatsGeneral, worldName, server);
                worldInfo.TWStatsPlayer = ReplaceServerAndWorld(worldInfo.TWStatsPlayer, worldName, server);
                worldInfo.TWStatsPlayerGraph = ReplaceServerAndWorld(worldInfo.TWStatsPlayerGraph, worldName, server);
                worldInfo.TWStatsTribe = ReplaceServerAndWorld(worldInfo.TWStatsTribe, worldName, server);
                worldInfo.TWStatsTribeGraph = ReplaceServerAndWorld(worldInfo.TWStatsTribeGraph, worldName, server);
                worldInfo.TWStatsVillage = ReplaceServerAndWorld(worldInfo.TWStatsVillage, worldName, server);

                worldInfo.Units.Clear();
                worldInfo.Units.AddRange(GetWorldUnitSettings(worldName, server));

                worldInfo.SaveToFile(Path.Combine(path, WorldXmlString));

                // default.sets
                Directory.CreateDirectory(Path.Combine(path, DirectorySettingsString));
                var settingsTemplatePath = Path.Combine(WorldTemplateDirectory, DefaultSettingsString);

                string targetPath = Path.Combine(path, DirectorySettingsString, DefaultSettingsString);
                File.Copy(settingsTemplatePath, targetPath);
            }

            #region TW World Information
            /// <summary>
            /// Downloads the global world settings and extracts the world speeds
            /// </summary>
            private static TwWorldSettings DownloadWorldSettings(string worldName, string worldServer)
            {
                var xdoc = Network.DownloadXml(string.Format(ServerSettingsUrl, worldName, worldServer, "get_config"));
                Debug.Assert(xdoc.Root != null, "xdoc.Root != null");
                var worldSpeed = float.Parse(xdoc.Root.Element("speed").Value.Trim(), CultureInfo.InvariantCulture);
                var worldUnitSpeed = float.Parse(xdoc.Root.Element("unit_speed").Value.Trim(), CultureInfo.InvariantCulture);

                bool isOldScenery = xdoc.Root.Element("coord").Element("legacy_scenery").Value == "1";

                return new TwWorldSettings(worldSpeed, worldUnitSpeed, isOldScenery);
            }

            /// <summary>
            /// Useful world information from the TW API
            /// </summary>
            private class TwWorldSettings
            {
                public float Speed { get; private set; }
                public float UnitSpeed { get; private set; }

                /// <summary>
                /// Gets a value indicating which world.dat to use
                /// </summary>
                public IconDrawerFactory.Scenery MapScenery { get; private set; }

                public TwWorldSettings(float worldSpeed, float worldUnitSpeed, bool isOldScenery)
                {
                    Speed = worldSpeed;
                    UnitSpeed = worldUnitSpeed;
                    MapScenery = isOldScenery ? IconDrawerFactory.Scenery.Old : IconDrawerFactory.Scenery.New;
                }
            }

            /// <summary>
            /// Gets the unit information for this world
            /// and combines it with TWTactics data
            /// </summary>
            private static IEnumerable<WorldConfigurationUnitsUnit> GetWorldUnitSettings(string worldName, ServerInfo server)
            {
                List<TacticsUnit> twTacticsUnits = TacticsUnit.GetUnitsFromXml(Path.Combine(WorldTemplateDirectory, "TacticsUnits.xml"));
                IEnumerable<TwUnit> twUnits = DownloadWorldUnitSettings(worldName, server.ServerUrl);

                var list = new List<WorldConfigurationUnitsUnit>();
                int position = 0;
                foreach (TwUnit twUnit in twUnits)
                {
                    TacticsUnit tacticsUnit = twTacticsUnits.SingleOrDefault(x => x.Type == twUnit.Type);
                    if (tacticsUnit != null)
                    {
                        list.Add(new WorldConfigurationUnitsUnit
                        {
                            Carry = twUnit.Carry.ToString(CultureInfo.InvariantCulture),
                            CostClay = twUnit.Clay.ToString(CultureInfo.InvariantCulture),
                            CostIron = twUnit.Iron.ToString(CultureInfo.InvariantCulture),
                            CostWood = twUnit.Wood.ToString(CultureInfo.InvariantCulture),
                            CostPeople = twUnit.Population.ToString(CultureInfo.InvariantCulture),
                            Farmer = tacticsUnit.Farmer.ToString(),
                            HideAttacker = tacticsUnit.HideAttacher.ToString(),
                            Offense = tacticsUnit.Offense.ToString(),
                            Name = tacticsUnit.Name,
                            Short = tacticsUnit.ShortName,
                            Speed = twUnit.Speed.ToString(CultureInfo.InvariantCulture),
                            Type = twUnit.Type.ToString(),
                            Position = position.ToString(CultureInfo.InvariantCulture)
                        });
                    }

                    position++;
                }
                return list;
            }

            private static IEnumerable<TwUnit> DownloadWorldUnitSettings(string worldName, string serverName)
            {
                var xdoc = Network.DownloadXml(string.Format(ServerSettingsUrl, worldName, serverName, "get_unit_info"));

                var list = new List<TwUnit>();
                foreach (var xmlUnit in xdoc.Root.Elements())
                {
                    UnitTypes type;
                    if (Enum.TryParse(xmlUnit.Name.LocalName, true, out type))
                    {
                        list.Add(new TwUnit
                        {
                            Type = type,
                            Wood = Convert.ToInt32(xmlUnit.Element("wood").Value),
                            Clay = Convert.ToInt32(xmlUnit.Element("stone").Value),
                            Iron = Convert.ToInt32(xmlUnit.Element("iron").Value),
                            Population = Convert.ToInt32(xmlUnit.Element("pop").Value),
                            Speed = Convert.ToSingle(xmlUnit.Element("speed").Value, CultureInfo.InvariantCulture),
                            Carry = Convert.ToInt32(xmlUnit.Element("carry").Value)
                        });
                    }
                }
                return list;
            }

            /// <summary>
            /// Holder class with data from TW API
            /// </summary>
            private class TwUnit
            {
                public UnitTypes Type { get; set; }
                public int Wood { get; set; }
                public int Clay { get; set; }
                public int Iron { get; set; }
                public int Population { get; set; }
                public float Speed { get; set; }
                public int Carry { get; set; }

                public override string ToString()
                {
                    return string.Format("Type={0}, Speed={1}, Population={2}", Type, Speed, Population);
                }
            }
            #endregion
            #endregion

            #region Download Snapshot
            /// <summary>
            /// Downloads the latest Tribal Wars data
            /// </summary>
            public void DownloadNewTwSnapshot()
            {
                _previousData = _currentData;
                _currentData = Default.Settings.ServerTime.ToString("yyyyMMddHH", CultureInfo.InvariantCulture);
                string dirName = CurrentWorldDataDirectory + _currentData;
                if (Directory.Exists(dirName))
                {
                    MessageBox.Show("You can only download the data once per hour!");
                }
                else
                {
                    // Download data
                    Directory.CreateDirectory(dirName);

                    DownloadFile(DownloadVillage, dirName + "\\" + FileVillageString);
                    DownloadFile(DownloadTribe, dirName + "\\" + FileTribeString);
                    DownloadFile(DownloadPlayer, dirName + "\\" + FilePlayerString);
                }

                // Keep statistics :)
                UpdateCounter();
            }

            private void UpdateCounter()
            {
                var data = new NameValueCollection();
                data["server"] = Default.Settings.Server.Host;
                data["world"] = Default.Settings.Name;
                data["player"] = Default.You.Name;
                data["tribe"] = Default.You.HasTribe ? Default.You.Tribe.Tag : "";
                Network.PostValues("http://sangu.be/api/twtacticsusage.php", data);
            }

            /// <summary>
            /// Downloads one TW file
            /// </summary>
            private void DownloadFile(string urlFile, string outputFile)
            {
                var client = Network.CreateWebRequest(urlFile);
                using (var response = client.GetResponse())
                {
                    var stream = response.GetResponseStream();
                    Debug.Assert(stream != null);
                    using (var unzip = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                    {
                        using (var writer = new FileStream(outputFile, FileMode.Create))
                        {
                            var block = new byte[4096];
                            int read;
                            while ((read = unzip.Read(block, 0, block.Length)) != 0)
                            {
                                writer.Write(block, 0, read);
                            }
                        }
                    }
                }
            }
            #endregion

            #region Load TW Snapshots
            /// <summary>
            /// Loads the village, player and tribe files into memory
            /// </summary>
            /// <param name="villages">Output villages dictionary</param>
            /// <param name="players">Output players dictionary</param>
            /// <param name="tribes">Output tribes dictionary</param>
            public void LoadCurrentAndPreviousTwSnapshot(out WorldVillagesCollection villages, out Dictionary<string, Player> players, out Dictionary<string, Tribe> tribes)
            {
                LoadTwSnapshot(CurrentWorldDataDateDirectory, out villages, out players, out tribes);
                LoadPreviousTwSnapshot(null, villages, players, tribes);
            }

            private void LoadTwSnapshot(string dataPath, out WorldVillagesCollection villages, out Dictionary<string, Player> players, out Dictionary<string, Tribe> tribes)
            {
                if (IsValidDataPath(dataPath))
                {
                    // load map files
                    string[] readVillages = File.ReadAllLines(dataPath + FileVillageString);
                    string[] readPlayers = File.ReadAllLines(dataPath + FilePlayerString);
                    string[] readTribes = File.ReadAllLines(dataPath + FileTribeString);

                    // Temporary dictionaries for mapping
                    var tempPlayers = new Dictionary<int, string>(WorldPlayerCount);
                    var tempTribes = new Dictionary<int, string>(WorldTribeCount);

                    // Load tribes
                    tribes = new Dictionary<string, Tribe>(readTribes.Length + 1);
                    foreach (string tribeString in readTribes)
                    {
                        var tribe = new Tribe(tribeString.Split(','));
                        tribes.Add(tribe.Tag.ToUpper(), tribe);
                        tempTribes.Add(tribe.Id, tribe.Tag.ToUpper());
                    }

                    // Load players
                    players = new Dictionary<string, Player>(readPlayers.Length + 1);
                    foreach (string playerString in readPlayers)
                    {
                        var player = new Player(playerString.Split(','));

                        // Find tribe
                        if (player.TribeId != 0 && tempTribes.ContainsKey(player.TribeId))
                        {
                            Tribe tribe = tribes[tempTribes[player.TribeId]];
                            player.Tribe = tribe;
                            tribes[tribe.Tag.ToUpper()].AddPlayer(player);
                        }

                        players.Add(player.Name.ToUpper(), player);
                        tempPlayers.Add(player.Id, player.Name.ToUpper());
                    }

                    // Load villages
                    villages = new WorldVillagesCollection(readVillages.Length + 1);
                    foreach (string villageString in readVillages)
                    {
                        var village = new Village(villageString.Split(','));

                        // links to and from players
                        if (village.PlayerId != 0 && tempPlayers.ContainsKey(village.PlayerId))
                        {
                            Player player = players[tempPlayers[village.PlayerId]];
                            village.Player = player;
                            players[player.Name.ToUpper()].AddVillage(village);
                        }

                        if (village.Location.IsValidGameCoordinate())
                        {
                            villages.Add(new Point(village.X, village.Y), village);
                        }
                    }
                }
                else
                {
                    tribes = new Dictionary<string, Tribe>();
                    players = new Dictionary<string, Player>();
                    villages = new WorldVillagesCollection();
                }
            }

            /// <summary>
            /// Load previous data async
            /// </summary>
            /// <param name="previousPath">The directory name with the previous data to download</param>
            public void LoadPreviousTwSnapshot(string previousPath, WorldVillagesCollection villages, Dictionary<string, Player> players, Dictionary<string, Tribe> tribes)
            {
                bool load = true;
                if (previousPath != null)
                {
                    string currentPath = PreviousWorldDataDateDirectory;
                    _previousData = previousPath;
                    if (currentPath == PreviousWorldDataDateDirectory) load = false;
                }
                if (load && PreviousWorldDataDateDirectory != null)
                {
                    var wrapper = new DictionaryLoaderWrapper();
                    wrapper.Villages = villages;
                    wrapper.Tribes = tribes;
                    wrapper.Players = players;
                    wrapper.Path = PreviousWorldDataDateDirectory;

                    DictionaryLoader invoker = BeginDictionaryAsync;
                    invoker.BeginInvoke(wrapper, EndDictionaryAsync, null);
                }
            }

            /// <summary>
            /// Async delegate for loading the previously download data
            /// </summary>
            private delegate void DictionaryLoader(DictionaryLoaderWrapper wrapper);

            /// <summary>
            /// Wrapper argument class for async dictionary loading
            /// </summary>
            private class DictionaryLoaderWrapper
            {
                public string Path;
                public WorldVillagesCollection Villages;
                public Dictionary<string, Player> Players;
                public Dictionary<string, Tribe> Tribes;
            }

            /// <summary>
            /// Starts filling the previous data of the villages, players and tribes
            /// </summary>
            /// <param name="wrapper">Wrapper object with path and current data</param>
            private void BeginDictionaryAsync(DictionaryLoaderWrapper wrapper)
            {
                // Load the data
                WorldVillagesCollection villages;
                Dictionary<string, Player> players;
                Dictionary<string, Tribe> tribes;
                LoadTwSnapshot(wrapper.Path, out villages, out players, out tribes);

                foreach (Village village in villages.Values)
                {
                    if (wrapper.Villages.ContainsKey(village.Location))
                    {
                        wrapper.Villages[village.Location].SetPreviousDetails(village);
                    }
                }

                foreach (KeyValuePair<string, Player> pair in players)
                {
                    if (wrapper.Players.ContainsKey(pair.Key))
                    {
                        wrapper.Players[pair.Key].SetPreviousDetails(pair.Value);
                    }
                }

                foreach (KeyValuePair<string, Tribe> pair in tribes)
                {
                    if (wrapper.Tribes.ContainsKey(pair.Key))
                    {
                        wrapper.Tribes[pair.Key].SetPreviousDetails(pair.Value);
                    }
                }
            }

            /// <summary>
            /// Raise the Monitor event when loading the previous data has been loaded
            /// </summary>
            private void EndDictionaryAsync(IAsyncResult ar)
            {
                var result = (System.Runtime.Remoting.Messaging.AsyncResult)ar;
                var invoker = (DictionaryLoader)result.AsyncDelegate;

                invoker.EndInvoke(ar);

                Default.EventPublisher.InformMonitoringLoaded(null);
            }
            #endregion

            #region Private Implementation
            /// <summary>
            /// Returns the path and creates the directory if it does not yet exist
            /// </summary>
            private static string GetPath(string str)
            {
                if (!Directory.Exists(str)) Directory.CreateDirectory(str);
                return str;
            }

            /// <summary>
            /// Replace {WorldName} and {WorldServer} with the given values
            /// </summary>
            private static string ReplaceServerAndWorld(string str, string worldName, ServerInfo server)
            {
                return str
                    .Replace("{WorldName}", worldName)
                    .Replace("{ServerUrl}", server.ServerUrl)
                    .Replace("{TWStatsPrefix}", server.TwStatsPrefix);
            }
            #endregion
        }
    }
}
