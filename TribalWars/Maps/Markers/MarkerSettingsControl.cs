#region Using
using System;
using System.Windows.Forms;

#endregion

namespace TribalWars.Maps.Markers
{
    public partial class MarkerSettingsControl : UserControl
    {
        public MarkerSettingsControl()
        {
            InitializeComponent();
        }

        private void MarkersContainerControl_Load(object sender, EventArgs e)
        {
            //World.Default.EventPublisher.SettingsLoaded += World_SettingsLoaded;
        }

        private void MarkerActive_CheckedChanged(object sender, EventArgs e)
        {
            MarkerActivePanel.Enabled = MarkerActive.Checked;
        }

        private void MarkerColor_SelectedColorChanged(object sender, EventArgs e)
        {

        }

        private void MarkerExtraColor_SelectedColorChanged(object sender, EventArgs e)
        {

        }

        private void MarkerView_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Make the control reflect the MarkerSettings
        /// </summary>
        public void SetMarker(MarkerSettings settings)
        {
            MarkerActive.Checked = settings.Enabled;
            MarkerColor.SelectedColor = settings.Color;
            MarkerExtraColor.SelectedColor = settings.ExtraColor;
            MarkerView.SelectedValue = settings.View;
        }
    }
}
