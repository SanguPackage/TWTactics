using System;
using System.Windows.Forms;
using TribalWars.Maps.Markers;
using TribalWars.Villages;
using TribalWars.Worlds;

namespace TribalWars.Forms.Small
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

        private void ActivePlayerForm_Load(object sender, EventArgs e)
        {
            if (You.Player == null || You.Player.Empty)
            {
                You.SelectorControl.Focus();
                You.SelectorControl.DroppedDown = true;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
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
                youChooser.You.SortOnText();
                var result = youChooser.ShowDialog();

                if (result == DialogResult.OK)
                {
                    World.Default.Map.MarkerManager.UpdateDefaultMarker(World.Default.Map, youChooser.YourMarkerSettings);
                    World.Default.Map.MarkerManager.UpdateDefaultMarker(World.Default.Map, youChooser.YourTribeMarkerSettings);

                    if (World.Default.You != youChooser.YouPlayer)
                    {
                        World.Default.You = youChooser.YouPlayer;
                        World.Default.Map.SetCenter(World.Default.You);
                        World.Default.Map.SaveHome();
                    }

                    World.Default.InvalidateMarkers();
                }
            }
        }
    }
}
