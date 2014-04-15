#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using TribalWars.Data.Players;
using TribalWars.Data.Villages;
using System.Drawing;
using TribalWars.Data.Tribes;
using System.Windows.Forms;
#endregion

namespace TribalWars
{
    public partial class World
    {
        /// <summary>
        /// Handles the Path and files stuff for the World type
        /// </summary>
        public class InternalStructure
        {
            #region Constants
            public const string FileVillageString = @"village.txt";
            public const string FileTribeString = @"ally.txt";
            public const string FilePlayerString = @"tribe.txt";

            public const string DirectorySettingsString = "Settings";
            public const string DirectoryDataString = "Data";
            public const string DirectoryWorldDataString = "WorldData";
            public const string DirectoryReportsString = "Reports";
            public const string DirectoryMonitorString = "Monitor";
            public const string DirectoryScreenshotString = "Screenshot";

            public const string WorldString = "World";
            public const string DefaultSettingsString = "default.sets";
            public const string SettingsWildcardString = "*.sets";
            public const string WorldXMLString = "world.xml";
            public const string VillageTypesString = "villagetypes.dat";

            public const int WorldVillageCount = 50000;
            public const int WorldPlayerCount = 10000;
            public const int WorldTribeCount = 5000;
            #endregion

            #region Fields
            private string _downloadVillage;
            private string _downloadPlayer;
            private string _downloadTribe;

            private static string _currentDirectory;
            private string _currentWorld;
            private string _currentData;
            private string _previousData;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the the URL to village.txt
            /// </summary>
            public string DownloadVillage
            {
                get { return _downloadVillage; }
                set { _downloadVillage = value; }
            }

            /// <summary>
            /// Gets or sets the the URL to tribe.txt
            /// </summary>
            public string DownloadPlayer
            {
                get { return _downloadPlayer; }
                set { _downloadPlayer = value; }
            }

            /// <summary>
            /// Gets or sets the the URL to ally.txt
            /// </summary>
            public string DownloadTribe
            {
                get { return _downloadTribe; }
                set { _downloadTribe = value; }
            }

            /// <summary>
            /// Gets the date of the current data
            /// </summary>
            public DateTime? CurrentData
            {
                get
                {
                    DateTime test;
                    if (DateTime.TryParseExact(_currentData, "yyyyMMddHH", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out test))
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
                    if (DateTime.TryParseExact(_previousData, "yyyyMMddHH", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out test))
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
                        using (FileStream stream = File.Create(CurrentWorldDirectory + VillageTypesString))
                        {
                            for (int i = 1; i <= 999999; i++)
                                stream.WriteByte(0);
                        }
                    }
                    return File.Open(CurrentWorldDirectory + VillageTypesString, FileMode.Open, FileAccess.ReadWrite);
                }
            }
            #endregion

            #region Constructors
            public InternalStructure()
            {
                
            }

            /// <summary>
            /// Sets up the paths
            /// Reads world.xml
            /// Downloads the data if necessary
            /// </summary>
            /// <param name="world"></param>
            /// <param name="settings"></param>
            public bool SetPath(string world, string settings)
            {
                try
                {
                    _currentData = string.Empty;
                    _previousData = string.Empty;

                    DirectoryInfo worldPath = new DirectoryInfo(world);
                    if (worldPath.Name.StartsWith(InternalStructure.WorldString, StringComparison.InvariantCultureIgnoreCase) && worldPath.Name != DirectoryWorldDataString) // god how long did it say <> instead of != and me not understanding why it was a syntax error :)
                    {
                        // general world selected
                        _currentWorld = worldPath.Name;

                        // World stats (server sets, Buildings & units)
                        XmlReaderSettings sets = new XmlReaderSettings();
                        sets.IgnoreWhitespace = true;
                        sets.CloseInput = true;
                        using (XmlReader worldXml = XmlReader.Create(File.Open(worldPath + InternalStructure.WorldXMLString, FileMode.Open, FileAccess.Read), sets))
                        {
                            Data.Builder.ReadWorld(worldXml, World.Default.Map, World.Default.MiniMap);
                        }

                        // If there is no datapath, read the last directory
                        string[] dirs = Directory.GetDirectories(CurrentWorldDataDirectory);
                        if (dirs.Length != 0)
                        {
                            _currentData = new DirectoryInfo(dirs[dirs.Length - 1]).Name;
                            if (dirs.Length != 1) _previousData = new DirectoryInfo(dirs[dirs.Length - 2]).Name;
                        }
                        else
                        {
                            // no data found: download!
                            this.Download();
                            dirs = Directory.GetDirectories(CurrentWorldDataDirectory);
                            _currentData = new DirectoryInfo(dirs[0]).Name;
                        }
                    }
                    else
                    {
                        _currentData = worldPath.Name;
                        _currentWorld = worldPath.Parent.Parent.Name;
                        string[] dirs = Directory.GetDirectories(CurrentWorldDataDirectory);
                        if (dirs.Length > 1)
                        {
                            string lastDir = string.Empty;
                            foreach (string dir in dirs)
                            {
                                if (dir + "\\" == worldPath.FullName && !string.IsNullOrEmpty(lastDir))
                                {
                                    _previousData = new FileInfo(lastDir).Name;
                                }
                                lastDir = dir;
                            }
                        }

                        // World stats (server sets, Buildings & units)
                        XmlReaderSettings sets = new XmlReaderSettings();
                        sets.IgnoreWhitespace = true;
                        sets.CloseInput = true;
                        using (XmlReader worldXml = XmlReader.Create(File.Open(worldPath.Parent.Parent.FullName + "\\" + InternalStructure.WorldXMLString, FileMode.Open, FileAccess.Read), sets))
                        {
                            Data.Builder.ReadWorld(worldXml, World.Default.Map, World.Default.MiniMap);
                        }

                        worldPath = new DirectoryInfo(CurrentWorldDirectory);
                        if (!worldPath.Name.StartsWith(InternalStructure.WorldString, StringComparison.InvariantCultureIgnoreCase))
                        {
                            _currentWorld = string.Empty;
                            _currentData = string.Empty;
                            _previousData = string.Empty;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + "Loading world: " + world + " with settings " + settings);
                    throw;
                }

                return _currentData != string.Empty;
            }
            #endregion

            #region Path Properties
            /// <summary>
            /// Gets the executable directory
            /// </summary>
            /// <remarks>\</remarks>
            public static string MainDirectory
            {
                get
                {
                    if (InternalStructure._currentDirectory == null)
                        InternalStructure._currentDirectory = Environment.CurrentDirectory + "\\";

                    return InternalStructure._currentDirectory;
                }
            }

            /// <summary>
            /// Gets the WorldData directory
            /// </summary>
            /// <remarks>\WorldData\</remarks>
            public static string WorldDataDirectory
            {
                get { return InternalStructure.GetPath(InternalStructure.MainDirectory + DirectoryWorldDataString + "\\"); }
            }

            /// <summary>
            /// Gets the loaded world directory
            /// </summary>
            /// <remarks>\WorldData\World 1\</remarks>
            public string CurrentWorldDirectory
            {
                get { return InternalStructure.GetPath(InternalStructure.MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\"); }
            }

            /// <summary>
            /// Gets the reports directory
            /// </summary>
            /// <remarks>\WorldData\World 1\Reports\</remarks>
            public string CurrentWorldReportsDirectory
            {
                get { return InternalStructure.GetPath(InternalStructure.MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryReportsString + "\\"); }
            }

            /// <summary>
            /// Gets the directory with player monitorring data
            /// </summary>
            /// <remarks>\WorldData\World 1\Monitor\</remarks>
            public string CurrentWorldMonitorDirectory
            {
                get { return InternalStructure.GetPath(InternalStructure.MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryMonitorString + "\\"); }
            }

            /// <summary>
            /// Gets the directory with user screenshots
            /// </summary>
            /// <remarks>\WorldData\World 1\Screenshot\</remarks>
            public string CurrentWorldScreenshotDirectory
            {
                get { return InternalStructure.GetPath(InternalStructure.MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryScreenshotString + "\\"); }
            }

            /// <summary>
            /// Gets the directory with the different settings files
            /// </summary>
            /// <remarks>\WorldData\World 1\Settings\</remarks>
            public string CurrentWorldSettingsDirectory
            {
                get { return InternalStructure.GetPath(InternalStructure.MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectorySettingsString + "\\"); }
            }

            /// <summary>
            /// Gets the directory with all the downloaded TW data
            /// </summary>
            /// <remarks>\WorldData\World 1\Data\</remarks>
            public string CurrentWorldDataDirectory
            {
                get { return InternalStructure.GetPath(InternalStructure.MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryDataString + "\\"); }
            }

            /// <summary>
            /// Gets the directory with the currently loaded TW data
            /// </summary>
            /// <remarks>\WorldData\World 1\Data\20080101\</remarks>
            public string CurrentWorldDataDateDirectory
            {
                get { return InternalStructure.GetPath(InternalStructure.MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryDataString + "\\" + _currentData + "\\"); }
            }

            /// <summary>
            /// Gets the directory with the previously downloaded TW data
            /// </summary>
            public string PreviousWorldDataDateDirectory
            {
                get
                {
                    if (string.IsNullOrEmpty(_previousData)) return null;
                    return InternalStructure.GetPath(InternalStructure.MainDirectory + DirectoryWorldDataString + "\\" + _currentWorld + "\\" + DirectoryDataString + "\\" + _previousData + "\\");
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
            #endregion

            #region Download
            /// <summary>
            /// Downloads the latest Tribal Wars data
            /// </summary>
            public void Download()
            {
                _previousData = _currentData;
                _currentData = DateTime.Now.ToString("yyyyMMddHH", System.Globalization.CultureInfo.InvariantCulture);
                string dirName = CurrentWorldDataDirectory + _currentData;
                if (Directory.Exists(dirName))
                {
                    System.Windows.Forms.MessageBox.Show("You can only download the data once per hour!");
                }
                else
                {
                    // Download data
                    Directory.CreateDirectory(dirName);
                    System.Net.WebClient client = new System.Net.WebClient();

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
                    using (System.IO.Compression.GZipStream unzip = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                    {
                        using (FileStream writer = new FileStream(outputFile, FileMode.Create))
                        {
                            byte[] block = new byte[4096];
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
            public void LoadDictionary(string dataPath, out WorldVillagesCollection villages, out Dictionary<string, Player> players, out Dictionary<string, Tribe> tribes)
            {
                if (IsValidDataPath(dataPath))
                {
                    // load map files
                    string[] readVillages = File.ReadAllLines(dataPath + InternalStructure.FileVillageString);
                    string[] readPlayers = File.ReadAllLines(dataPath + InternalStructure.FilePlayerString);
                    string[] readTribes = File.ReadAllLines(dataPath + InternalStructure.FileTribeString);

                    // Temporary dictionaries for mapping
                    Dictionary<int, string> tempPlayers = new Dictionary<int, string>(InternalStructure.WorldPlayerCount);
                    Dictionary<int, string> tempTribes = new Dictionary<int, string>(InternalStructure.WorldTribeCount);

                    // Load tribes
                    tribes = new Dictionary<string, Tribe>(readTribes.Length + 1);
                    foreach (string tribeString in readTribes)
                    {
                        Tribe tribe = new Tribe(tribeString.Split(','));
                        tribes.Add(tribe.Tag.ToUpper(), tribe);
                        tempTribes.Add(tribe.ID, tribe.Tag.ToUpper());
                    }

                    // Load players
                    players = new Dictionary<string, Player>(readPlayers.Length + 1);
                    foreach (string playerString in readPlayers)
                    {
                        Player player = new Player(playerString.Split(','));

                        // Find tribe
                        if (player._TribeID != 0 && tempTribes.ContainsKey(player._TribeID))
                        {
                            Tribe tribe = tribes[tempTribes[player._TribeID]];
                            player.Tribe = tribe;
                            tribes[tribe.Tag.ToUpper()].AddPlayer(player);
                        }

                        players.Add(player.Name.ToUpper(), player);
                        tempPlayers.Add(player.ID, player.Name.ToUpper());
                    }

                    // Load villages
                    villages = new WorldVillagesCollection(readVillages.Length + 1);
                    foreach (string villageString in readVillages)
                    {
                        Village village = new Village(villageString.Split(','));

                        // links to and from players
                        if (village._PlayerID != 0 && tempPlayers.ContainsKey(village._PlayerID))
                        {
                            Player player = players[tempPlayers[village._PlayerID]];
                            village.Player = player;
                            players[player.Name.ToUpper()].AddVillage(village);
                        }

                        villages.Add(new Point(village.X, village.Y), village);
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
                    DictionaryLoaderWrapper wrapper = new DictionaryLoaderWrapper();
                    wrapper.Villages = villages;
                    wrapper.Tribes = tribes;
                    wrapper.Players = players;
                    wrapper.Path = PreviousWorldDataDateDirectory;

                    DictionaryLoader invoker = new DictionaryLoader(BeginDictionaryAsync);
                    invoker.BeginInvoke(wrapper, new AsyncCallback(EndDictionaryAsync), null);
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
                System.Runtime.Remoting.Messaging.AsyncResult result = (System.Runtime.Remoting.Messaging.AsyncResult)ar;
                DictionaryLoader invoker = (DictionaryLoader)result.AsyncDelegate;

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
