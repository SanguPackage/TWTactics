using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using TribalWars.Forms.Small;
using TribalWars.Tools;
using TribalWars.Worlds;

namespace TribalWars.Forms
{
    /// <summary>
    /// Load a world or select a settings file
    /// for a previously created world
    /// </summary>
    public partial class LoadWorldForm : Form
    {
        #region Fields
        private string _lastSelectedWorld;
        #endregion

        #region Constants
        private const int ImageWorld = 0;
        private const int ImageData = 1;
        //private const int ImageSettings = 2;

        private const string PathDataWorldFormat = "yyyyMMddHH";
        #endregion

        #region Constructors
        public LoadWorldForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void LoadWorldForm_Load(object sender, EventArgs e)
        {
            // Load last selected world
            FillTree();
            if (Worlds.Nodes.ContainsKey(Properties.Settings.Default.LastWorld))
            {
                Worlds.SelectedNode = Worlds.Nodes[Properties.Settings.Default.LastWorld];
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string path = FindSelectedWorldPath();
            string pathData = FindSelectedWorldDataPath();
            if (path.Length == 0)
            {
                MessageBox.Show("No world selected!");
            }
            else
            {
                string settings = World.InternalStructure.DefaultSettingsString;
                if (WorldSettings.SelectedItems.Count == 1)
                {
                    settings = WorldSettings.SelectedItems[0].Tag + World.InternalStructure.SettingsExtensionString;
                }

                // save last selected world
                Properties.Settings.Default.LastWorld = path;
                Properties.Settings.Default.LastSettings = settings;
                Properties.Settings.Default.Save();

                World.Default.LoadWorld(pathData, settings);
                Close();
            }
        }

        private void Worlds_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string pathSettings = FindSelectedWorldPath();
            if (pathSettings.Length > 0 && _lastSelectedWorld != pathSettings)
            {
                _lastSelectedWorld = pathSettings;
                pathSettings += World.InternalStructure.DirectorySettingsString + "\\";

                // add all setting files
                WorldSettings.Items.Clear();
                foreach (string sets in Directory.GetFiles(pathSettings, "*" + World.InternalStructure.SettingsExtensionString))
                {
                    string fileName = Path.GetFileNameWithoutExtension(sets);
                    var itm = new ListViewItem(fileName);
                    var fileInfo = new FileInfo(sets);
                    itm.Tag = fileName;
                    itm.SubItems.Add(fileInfo.CreationTime.ToString(CultureInfo.InvariantCulture));
                    WorldSettings.Items.Add(itm);
                }
            }
        }
        #endregion

        #region Private Implementation
        private string FindSelectedWorldDataPath()
        {
            if (Worlds.SelectedNode != null)
            {
                switch (Worlds.SelectedNode.ImageIndex)
                {
                    case ImageWorld:
                        if (Worlds.SelectedNode.Nodes.Count != 0)
                        {
                            return Worlds.SelectedNode.Nodes[0].Name;
                        }
                        break;

                    case ImageData:
                        return Worlds.SelectedNode.Name;
                }
            }

            return string.Empty;
        }

        private string FindSelectedWorldPath()
        {
            if (Worlds.SelectedNode != null)
            {
                if (Worlds.SelectedNode.ImageIndex == ImageWorld)
                    return Worlds.SelectedNode.Name;

                if (Worlds.SelectedNode.ImageIndex == ImageData)
                    return Worlds.SelectedNode.Parent.Name;
            }

            return string.Empty;
        }

        private void FillTree()
        {
            Worlds.Nodes.Clear();
            string path = World.InternalStructure.WorldDataDirectory;
            var worlds = Directory.GetDirectories(path);

            // create tree
            foreach (string world in worlds)
            {
                string pathData = world + @"\" + World.InternalStructure.DirectoryDataString + @"\";
                if (world.StartsWith(path, StringComparison.InvariantCultureIgnoreCase))
                {
                    var worldInfo = new DirectoryInfo(world);
                    TreeNode worldNode = Worlds.Nodes.Add(world + @"\", worldInfo.Name, ImageWorld, ImageWorld);
                    if (Directory.Exists(pathData))
                    {
                        // add all data for selected world
                        IOrderedEnumerable<string> worldSorted = Directory.GetDirectories(pathData).OrderByDescending(x => x);
                        foreach (string dir in worldSorted)
                        {
                            if (World.InternalStructure.IsValidDataPath(dir))
                            {
                                DateTime dirDate;
                                var dirInfo = new DirectoryInfo(dir);
                                if (DateTime.TryParseExact(dirInfo.Name, PathDataWorldFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dirDate))
                                {
                                    worldNode.Nodes.Add(dir + @"\", dirDate.PrintWorldDate(), ImageData, ImageData);
                                }
                                else
                                {
                                    worldNode.Nodes.Add(dir + @"\", dirInfo.Name, ImageData, ImageData);
                                }
                            }
                        }
                    }
                }
            }

            if (Worlds.Nodes.Count == 0)
            {
                btnLoad.Enabled = false;
            }
        }
        #endregion
    }
}