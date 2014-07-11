#region Using
using System;
using System.Windows.Forms;
using TribalWars.Worlds;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Controls.AccordeonLocation
{
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
    }
}
