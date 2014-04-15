using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using TribalWars.Tools;

namespace TribalWars
{
    public partial class LoadWorldForm : Form
    {
        #region Fields
        //ListViewSorter listviewsorter = new ListViewSorter();
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
            //listviewsorter.ListView = this.WorldSettings;

            // Register Comparer for each column
            //listviewsorter.ColumnComparerCollection["Name"] = new TribalWars.Tools.StringComparer();
            //listviewsorter.ColumnComparerCollection["Created"] = new TribalWars.Tools.DateComparer();

            // Load last selected world
            FillTree();
            if (Worlds.Nodes.ContainsKey(TribalWars.Properties.Settings.Default.LastWorld))
            {
                Worlds.SelectedNode = Worlds.Nodes[TribalWars.Properties.Settings.Default.LastWorld];
                Worlds.SelectedNode.Expand();
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
                    settings = WorldSettings.SelectedItems[0].Tag.ToString();
                }

                // save last selected world
                TribalWars.Properties.Settings.Default.LastWorld = path;
                TribalWars.Properties.Settings.Default.LastSettings = settings;
                TribalWars.Properties.Settings.Default.Save();

                World.Default.LoadWorld(pathData, settings);
                this.Close();
            }

            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show("The following error occured:" + Environment.NewLine + ex.ToString());
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
                    ListViewItem itm = new ListViewItem(fileName);
                    FileInfo fileInfo = new FileInfo(sets);
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
            string world = Microsoft.VisualBasic.Interaction.InputBox("Give the name of the new world", "New World", "World ", this.Left + this.ClientSize.Width / 3, this.Top + this.ClientSize.Height / 3);
            if (world.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                foreach (char c in Path.GetInvalidPathChars())
                {
                    world = world.Replace(c, '-');
                }
            }
            string path = World.InternalStructure.WorldDataDirectory + world;
            if (world.StartsWith(World.InternalStructure.WorldString, StringComparison.InvariantCultureIgnoreCase))
            {
                World.Default.LoadWorld(path);
            }
            else
            {
                MessageBox.Show("A new world has to start with 'World' :)");
            }
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

        private string FindSelectedWorldName()
        {
            if (Worlds.SelectedNode.ImageIndex == ImageWorld)
                return Worlds.SelectedNode.Text;
            else if (Worlds.SelectedNode.ImageIndex == ImageData)
                return Worlds.SelectedNode.Parent.Text;

            return string.Empty;
        }

        private void FillTree()
        {
            Worlds.Nodes.Clear();
            string path = World.InternalStructure.WorldDataDirectory;
            string[] worldsSorted = Directory.GetDirectories(path); // Sort on world ID instead of on world string

            // sort world dirs
            Array.Sort<string>(worldsSorted,
                delegate(string left, string right)
                {
                    // sorting by number
                    // there is also "World Classic/Speed": string comparison is used for those
                    string l;
                    if (left.LastIndexOf(" ") != -1) l = left.Substring(left.LastIndexOf(" "));
                    else l = left;
                    string r;
                    if (right.LastIndexOf(" ") != -1) r = right.Substring(right.LastIndexOf(" "));
                    else r = right;
                    int il, ir;
                    if (int.TryParse(l, out il) && int.TryParse(r, out ir))
                    {
                        return il - ir;
                    }
                    else
                    {
                        return l.CompareTo(r);
                    }
                });

            // create tree
            foreach (string worlds in worldsSorted)
            {
                string pathData = worlds + @"\" + World.InternalStructure.DirectoryDataString + @"\";
                if (worlds.StartsWith(path/* + World.PathWorldName*/, StringComparison.InvariantCultureIgnoreCase))
                {
                    DirectoryInfo worldInfo = new DirectoryInfo(worlds);
                    TreeNode WorldNode = Worlds.Nodes.Add(worlds + @"\", worldInfo.Name, ImageWorld, ImageWorld);
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
                                DirectoryInfo dirInfo = new DirectoryInfo(dir);
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