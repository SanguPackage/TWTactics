using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Maps.Markers;
using TribalWars.Villages;
using TribalWars.Worlds;

namespace TribalWars.Forms
{
    /// <summary>
    /// Change the active player and also his and his tribe markers
    /// </summary>
    public partial class ActivePlayerForm : Form
    {
        public Player YouPlayer
        {
            get { return You.Player; }
            set { You.SetPlayer(value); }
        }

        public MarkerSettings YourMarkerSettings
        {
            get { return YourMarker.GetMarkerSettings(); }
            set { YourMarker.SetMarker(value); }
        }

        public MarkerSettings YourTribeMarkerSettings
        {
            get { return YourTribeMarker.GetMarkerSettings(); }
            set { YourTribeMarker.SetMarker(value); }
        }

        public ActivePlayerForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Show the form as a dialog and update World.Default after closing
        /// </summary>
        public static void UpdateDefaultWorld()
        {
            using (var youChooser = new ActivePlayerForm())
            {
                youChooser.YouPlayer = World.Default.You;
                youChooser.YourMarkerSettings = World.Default.Map.MarkerManager.YourMarkerSettings;
                youChooser.YourTribeMarkerSettings = World.Default.Map.MarkerManager.YourTribeMarkerSettings;
                youChooser.ShowDialog();

                World.Default.You = youChooser.YouPlayer;
                World.Default.Map.MarkerManager.UpdateDefaultMarker(World.Default.Map, youChooser.YourMarkerSettings);
                World.Default.Map.MarkerManager.UpdateDefaultMarker(World.Default.Map, youChooser.YourTribeMarkerSettings);

                World.Default.InvalidateMarkers();
                World.Default.Map.SetCenter(World.Default.You);
            }
        }
    }
}
