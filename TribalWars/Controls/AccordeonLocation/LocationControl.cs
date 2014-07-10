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
    }
}
