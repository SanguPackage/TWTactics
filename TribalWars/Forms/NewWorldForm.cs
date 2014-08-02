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
    /// Start a new world
    /// </summary>
    public partial class NewWorldForm : Form
    {
        #region Fields
        private string[] _existingWorlds;
        #endregion

        #region Constructors
        public NewWorldForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void LoadWorldForm_Load(object sender, EventArgs e)
        {
            string path = World.InternalStructure.WorldDataDirectory;
            var worlds = Directory.GetDirectories(path);
            _existingWorlds = worlds.Select(x => new DirectoryInfo(x).Name).ToArray();

            // Existing servers
            World.InternalStructure.ServerInfo selectedServer = null;
            IEnumerable<World.InternalStructure.ServerInfo> existingServers = World.InternalStructure.GetAllServers();
            foreach (var server in existingServers)
            {
                Servers.Items.Add(server);
                if (server.ServerUrl == Properties.Settings.Default.DefaultServer)
                {
                    selectedServer = server;
                }
            }
            if (selectedServer != null)
            {
                Servers.SelectedItem = selectedServer;
            }
            else
            {
                Servers.SelectedIndex = 0;
            }
        }

        private void btnNewWorld_Click(object sender, EventArgs e)
        {
            string world = AvailableWorlds.SelectedItem.ToString();
            string path = World.InternalStructure.WorldDataDirectory + world;
            var server = (World.InternalStructure.ServerInfo) Servers.SelectedItem;

            Hide();

            World.CreateNewWorld(path, server);
            World.Default.LoadWorld(path);

            ActivePlayerForm.UpdateDefaultWorld();

            World.Default.DrawMaps();

            Properties.Settings.Default.DefaultServer = server.ServerUrl;
            Properties.Settings.Default.Save();

            string selectYouLater = "";
            if (World.Default.You.Empty)
            {
                selectYouLater = Environment.NewLine + Environment.NewLine + "You can later still specify yourself through the World -> 'Select Active Player' menu.";
                selectYouLater += Environment.NewLine + Environment.NewLine + "Note that for example planning attacks will work much easier when you have selected yourself.";
            }

            MessageBox.Show(@"A new world has been created!

Right click a village if you don't know where to start." + selectYouLater, "World Created!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }

        private void Servers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Add a new world: show available worlds on server
            var selectedServer = (World.InternalStructure.ServerInfo)Servers.SelectedItem;
            IEnumerable<string> worlds = World.GetAllWorlds(selectedServer.ServerUrl);
            AvailableWorlds.DataSource = worlds.Where(x => !_existingWorlds.Contains(x)).ToArray();
        }
        #endregion
    }
}