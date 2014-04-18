using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using TribalWars.Tools;

namespace TribalWars
{
    public partial class LoadWorldForm : Form
    {
        #region Fields
        private string[] _existingWorlds;
        #endregion

        #region Constants
        const int ImageWorld = 0;
        const int ImageData = 1;
        const int ImageSettings = 2;

        const string PathDataWorldFormat = "yyyyMMddHH";
        const string PathDataWorldFormatShort = "yyyyMMdd";
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
            if (Worlds.Nodes.ContainsKey(TribalWars.Properties.Settings.Default.LastWorld))
            {
                Worlds.SelectedNode = Worlds.Nodes[TribalWars.Properties.Settings.Default.LastWorld];
                Worlds.SelectedNode.Expand();
            }

            // Add a new world
            string[] worlds = World.GetAllWorlds();
            AvailableWorlds.DataSource = worlds.Where(x => !_existingWorlds.Contains(x)).ToArray();
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
                    settings = WorldSettings.SelectedItems[0].Tag.ToString();
                }

                // save last selected world
                TribalWars.Properties.Settings.Default.LastWorld = path;
                TribalWars.Properties.Settings.Default.LastSettings = settings;
                TribalWars.Properties.Settings.Default.Save();

                World.Default.LoadWorld(pathData, settings);
                this.Close();
            }
        }

        private void Worlds_AfterSelect(object sender, TreeViewEventArgs e)
        {
            WorldSettings.Items.Clear();
            string pathSettings = FindSelectedWorldPath();
            if (pathSettings.Length > 0)
            {
                pathSettings += World.InternalStructure.DirectorySettingsString + "\\";

                // add all setting files
                foreach (string sets in Directory.GetFiles(pathSettings, World.InternalStructure.SettingsWildcardString))
                {
                    string fileName = Path.GetFileNameWithoutExtension(sets);
                    var itm = new ListViewItem(fileName);
                    var fileInfo = new FileInfo(sets);
                    itm.Tag = fileName;
                    itm.SubItems.Add(fileInfo.CreationTime.ToString());
                    WorldSettings.Items.Add(itm);
                }
            }
        }

        private void WorldSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WorldSettings.SelectedIndices.Count != 0)
            {
                ListViewItem itm = WorldSettings.SelectedItems[0];
                //SettingsGrid.SelectedObject = World.GetSettings(new FileInfo(itm.Tag.ToString()));
            }
        }

        private void btnNewWorld_Click(object sender, EventArgs e)
        {
            string world = AvailableWorlds.SelectedItem.ToString();
            string path = World.InternalStructure.WorldDataDirectory + world;
            World.Default.CreateNewWorld(path);
            World.Default.LoadWorld(path);
            Close();
        }
        #endregion

        #region Private Implementation
        private string FindSelectedWorldDataPath()
        {
            if (Worlds.SelectedNode != null)
            {
                if (Worlds.SelectedNode.ImageIndex == ImageWorld)
                {
                    if (Worlds.SelectedNode.Nodes.Count != 0)
                        return Worlds.SelectedNode.Nodes[0].Name;
                }
                else if (Worlds.SelectedNode.ImageIndex == ImageData)
                    return Worlds.SelectedNode.Name;
            }

            return string.Empty;
        }

        private string FindSelectedWorldPath()
        {
            if (Worlds.SelectedNode != null)
            {
                if (Worlds.SelectedNode.ImageIndex == ImageWorld)
                    return Worlds.SelectedNode.Name;
                else if (Worlds.SelectedNode.ImageIndex == ImageData)
                    return Worlds.SelectedNode.Parent.Name;
            }

            return string.Empty;
        }

        private void FillTree()
        {
            Worlds.Nodes.Clear();
            string path = World.InternalStructure.WorldDataDirectory;
            var worlds = Directory.GetDirectories(path);
            _existingWorlds = worlds.Select(x => new DirectoryInfo(x).Name).ToArray();

            // create tree
            foreach (string world in worlds)
            {
                string pathData = world + @"\" + World.InternalStructure.DirectoryDataString + @"\";
                if (world.StartsWith(path, StringComparison.InvariantCultureIgnoreCase))
                {
                    var worldInfo = new DirectoryInfo(world);
                    TreeNode WorldNode = Worlds.Nodes.Add(world + @"\", worldInfo.Name, ImageWorld, ImageWorld);
                    if (Directory.Exists(pathData))
                    {
                        // add all data for selected world
                        string[] worldSorted = Directory.GetDirectories(pathData);
                        Array.Sort<string>(worldSorted, delegate(string l, string r) { return r.CompareTo(l); });
                        foreach (string dir in worldSorted)
                        {
                            if (World.InternalStructure.IsValidDataPath(dir))
                            {
                                DateTime dirDate;
                                var dirInfo = new DirectoryInfo(dir);
                                if (DateTime.TryParseExact(dirInfo.Name, PathDataWorldFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dirDate))
                                {
                                    WorldNode.Nodes.Add(dir + @"\", dirDate.ToString(), ImageData, ImageData);
                                }
                                else if (DateTime.TryParseExact(dirInfo.Name, PathDataWorldFormatShort, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dirDate))
                                {
                                    WorldNode.Nodes.Add(dir + @"\", dirDate.ToShortDateString(), ImageData, ImageData);
                                }
                                else
                                {
                                    WorldNode.Nodes.Add(dir + @"\", dirInfo.Name, ImageData, ImageData);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}