#region Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using TribalWars.Data.Players;
using TribalWars.Data.Villages;
using System.Drawing;
using TribalWars.Data.Tribes;
using System.Windows.Forms;
using TribalWars.Tools;

#endregion

namespace TribalWars.Data
{
    public partial class World
    {
        /// <summary>
        /// Handles the Path and files stuff for the World type
        /// </summary>
        public sealed class InternalStructure
        {
            #region Constants
            private const string AvailableWorlds = "http://www.tribalwars.nl/backend/get_servers.php";
            private const string NormalWorldPattern = @"nl(\d+)";
            private static readonly Regex NormalWorldRegex = new Regex(NormalWorldPattern);
            private const string NormalWorldPrefix = "nl";

            #region TribalWars API
            private const string ServerSettingsUrl = "http://{0}.tribalwars.nl/interface.php?func={1}";

            /// <summary>
            /// Downloads the global world settings and extracts the world speeds
            /// </summary>
            private static WorldSettings DownloadWorldSettings(string worldName)
            {
                var xdoc = XDocument.Load(string.Format(ServerSettingsUrl, worldName, "get_config"));
                Debug.Assert(xdoc.Root != null, "xdoc.Root != null");
                var worldSpeed = decimal.Parse(xdoc.Root.Element("speed").Value.Trim(), CultureInfo.InvariantCulture);
                var worldUnitSpeed = decimal.Parse(xdoc.Root.Element("unit_speed").Value.Trim(), CultureInfo.InvariantCulture);

                return new WorldSettings(worldSpeed, worldUnitSpeed);
            }

            private static object DownloadWorldUnitSettings(string worldName)
            {
                // TODO: load if Archers are present on this world!
                var xdoc = XDocument.Load(string.Format(ServerSettingsUrl, worldName, "get_unit_info"));
                //var worldSpeed = decimal.Parse(xdoc.Root.Element("speed").Value.Trim(), CultureInfo.InvariantCulture);
                //var worldUnitSpeed = decimal.Parse(xdoc.Root.Element("unit_speed").Value.Trim(), CultureInfo.InvariantCulture);

                return null;
            }

            private class WorldSettings
            {
                public decimal WorldUnitSpeed { get; private set; }
                public decimal UnitSpeed { get; private set; }

                public WorldSettings(decimal worldSpeed, decimal worldUnitSpeed)
                {
                    WorldUnitSpeed = worldSpeed;
                    UnitSpeed = worldUnitSpeed;
                }
            }
            #endregion

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
                        using (FileStream stream = File.Create(Path.Combine(WorldTemplateDirectory, VillageTypesString)))
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
                            TimeSpan? lastDownload = DateTime.Now - CurrentData;
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
                            Download();
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
                var sets = new XmlReaderSettings
                {
                    IgnoreWhitespace = true,
                    CloseInput = true
                };
                using (XmlReader worldXml = XmlReader.Create(File.Open(Path.Combine(worldPath, WorldXmlString), FileMode.Open, FileAccess.Read), sets))
                {
                    Builder.ReadWorld(worldXml, Default.Map, Default.MiniMap);
                }
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

            /// <summary>
            /// Create required infrastructure for a new world
            /// </summary>
            public static void CreateWorld(string path)
            {
                var dir = new DirectoryInfo(path);
                string worldName = dir.Name;
                if (!dir.Exists)
                {
                    dir.Create();
                }

                // villagetypes.dat
                File.Copy(Path.Combine(WorldTemplateDirectory, VillageTypesString), Path.Combine(path, VillageTypesString));

                // world.xml
                var worldXmlTemplate = new StringBuilder(File.ReadAllText(Path.Combine(WorldTemplateDirectory, WorldXmlTemplateString)));
                worldXmlTemplate.Replace("{WorldName}", worldName);
                WorldSettings worldSettings = DownloadWorldSettings(worldName);
                worldXmlTemplate.Replace("{WorldSpeed}", worldSettings.WorldUnitSpeed.ToString(CultureInfo.InvariantCulture));
                worldXmlTemplate.Replace("{WorldUnitSpeed}", worldSettings.UnitSpeed.ToString(CultureInfo.InvariantCulture));

                File.WriteAllText(Path.Combine(path, WorldXmlString), worldXmlTemplate.ToString());

                // TODO: also get the units so that archers are added if necessary

                // default.sets
                Directory.CreateDirectory(Path.Combine(path, DirectorySettingsString));
                var settingsTemplatePath = Path.Combine(WorldTemplateDirectory, DefaultSettingsString);
                

                string targetPath = Path.Combine(path, DirectorySettingsString, DefaultSettingsString);
                File.Copy(settingsTemplatePath, targetPath);
            }
            #endregion

            #region Download
            /// <summary>
            /// Get all available worlds on the server
            /// </summary>
            public static string[] DownloadWorlds()
            {
                var request = new System.Net.WebClient();
                var file = request.DownloadString(AvailableWorlds);
                var worldsObject = (Hashtable)new Serializer().Deserialize(file);

                string[] worlds = worldsObject.Keys.OfType<string>().ToArray();
                IEnumerable<string> specialWorlds = worlds.Where(x => !NormalWorldRegex.IsMatch(x));

                // Only the worlds where one can still start are returned from the server (or something)
                int lastStartedWorld = 
                    (from world in worlds
                     where NormalWorldRegex.IsMatch(world)
                     let worldNumber = int.Parse(NormalWorldRegex.Match(world).Groups[1].Value)
                     orderby worldNumber descending 
                     select worldNumber).First();

                // So we add all worlds from 1 to the last one
                IEnumerable<string> normalWorlds =
                    Enumerable.Range(1, lastStartedWorld)
                              .Select(x => NormalWorldPrefix + x.ToString(CultureInfo.InvariantCulture));

                return normalWorlds.Union(specialWorlds).ToArray();
            }

            /// <summary>
            /// Downloads the latest Tribal Wars data
            /// </summary>
            public void Download()
            {
                _previousData = _currentData;
                _currentData = DateTime.Now.ToString("yyyyMMddHH", CultureInfo.InvariantCulture);
                string dirName = CurrentWorldDataDirectory + _currentData;
                if (Directory.Exists(dirName))
                {
                    MessageBox.Show("You can only download the data once per hour!");
                }
                else
                {
                    // Download data
                    Directory.CreateDirectory(dirName);
                    var client = new System.Net.WebClient();

                    DownloadFile(client, DownloadVillage, dirName + "\\" + FileVillageString);
                    DownloadFile(client, DownloadTribe, dirName + "\\" + FileTribeString);
                    DownloadFile(client, DownloadPlayer, dirName + "\\" + FilePlayerString);
                }
            }

            /// <summary>
            /// Downloads one TW file
            /// </summary>
            private void DownloadFile(System.Net.WebClient client, string urlFile, string outputFile)
            {
                using (Stream stream = client.OpenRead(urlFile))
                {
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

            #region Dictionaries
            private void LoadDictionary(string dataPath, out WorldVillagesCollection villages, out Dictionary<string, Player> players, out Dictionary<string, Tribe> tribes)
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
            /// Loads the village, player and tribe files into memory
            /// </summary>
            /// <param name="villages">Output villages dictionary</param>
            /// <param name="players">Output players dictionary</param>
            /// <param name="tribes">Output tribes dictionary</param>
            public void LoadDictionaries(out WorldVillagesCollection villages, out Dictionary<string, Player> players, out Dictionary<string, Tribe> tribes)
            {
                LoadDictionary(CurrentWorldDataDateDirectory, out villages, out players, out tribes);
                LoadPreviousDictionary(null, villages, players, tribes);
            }

            /// <summary>
            /// Load previous data async
            /// </summary>
            /// <param name="previousPath">The directory name with the previous data to download</param>
            public void LoadPreviousDictionary(string previousPath, WorldVillagesCollection villages, Dictionary<string, Player> players, Dictionary<string, Tribe> tribes)
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
                LoadDictionary(wrapper.Path, out villages, out players, out tribes);

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

                World.Default.EventPublisher.InformMonitoringLoaded(null);
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
            #endregion
        }
    }
}
