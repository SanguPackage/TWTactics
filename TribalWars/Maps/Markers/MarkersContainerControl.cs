#region Using
using System;
using System.Windows.Forms;

#endregion

namespace TribalWars.Maps.Markers
{
    public partial class MarkersContainerControl : UserControl
    {
        public MarkersContainerControl()
        {
            InitializeComponent();
        }

        private void MarkersContainerControl_Load(object sender, EventArgs e)
        {
            //World.Default.EventPublisher.SettingsLoaded += World_SettingsLoaded;
        }

        //private void World_SettingsLoaded(object sender, EventArgs e)
        //{

        //}

    }
}
