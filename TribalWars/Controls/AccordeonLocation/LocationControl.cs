#region Using
using System;
using System.Windows.Forms;
using TribalWars.Worlds;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Controls.AccordeonLocation
{
    /// <summary>
    /// UI control with Location, You (=current player) setting,
    /// continent center and grid with search options for village, player
    /// and tribe
    /// </summary>
    public partial class LocationControl : UserControl
    {
        private bool _worldLoaded;

        public LocationControl()
        {
            InitializeComponent();
        }

        private void LocationControl_Load(object sender, EventArgs e)
        {
            World.Default.EventPublisher.SettingsLoaded += World_SettingsLoaded;
        }

        private void World_SettingsLoaded(object sender, EventArgs e)
        {
            if (World.Default.PlayerSelected)
            {
                You.SetPlayer(World.Default.You);
            }

            _worldLoaded = true;
        }

        private void cmdCenterKingdom_Click(object sender, EventArgs e)
        {
            int continent;
            if (int.TryParse(txtK.Text, out continent))
            {
                World.Default.Map.SetCenterContinent(continent);
            }
        }

        private void GoHome_Click(object sender, EventArgs e)
        {
            if (World.Default.HasLoaded)
            {
                DialogResult result = MessageBox.Show("Use the current position as your home?", "Set Homepage", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    World.Default.Map.SaveHome();
                }
            }
        }

        private void You_PlayerSelected(object sender, PlayerEventArgs e)
        {
            if (_worldLoaded)
            {
                World.Default.You = e.SelectedPlayer;
                World.Default.InvalidateMarkers();
                World.Default.Map.SetCenter(e.SelectedPlayer);
            }
        }

        public void FocusYouControl()
        {
            // When starting out, select the You control
            // it's probably a good idea to start by providing
            // your player name
            You.Focus();
        }
    }
}
