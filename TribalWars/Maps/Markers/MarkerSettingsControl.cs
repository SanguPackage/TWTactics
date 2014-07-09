#region Using
using System;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Markers
{
    public partial class MarkerSettingsControl : UserControl
    {
        private Player _player;
        private Tribe _tribe;
        private bool _settingProperties;

        public MarkerSettingsControl()
        {
            InitializeComponent();
        }

        private Marker GetMarker()
        {
            if (_tribe != null)
            {
                return World.Default.Map.MarkerManager.GetMarker(_tribe);
            }
            else
            {
                return World.Default.Map.MarkerManager.GetMarker(_player);
            }
        }

        private void UpdateMarker(MarkerSettings settings)
        {
            if (_tribe != null)
            {
                World.Default.Map.MarkerManager.UpdateMarker(World.Default.Map, _tribe, settings);
            }
            else
            {
                World.Default.Map.MarkerManager.UpdateMarker(World.Default.Map, _player, settings);
            }
        }

        private void MarkerActive_CheckedChanged(object sender, EventArgs e)
        {
            MarkerActivePanel.Enabled = MarkerActive.Checked;

            if (!_settingProperties)
            {
                var marker = GetMarker();
                UpdateMarker(MarkerSettings.ChangeEnabled(marker.Settings, MarkerActive.Checked));
            }
        }

        private void MarkerColor_SelectedColorChanged(object sender, EventArgs e)
        {
            if (!_settingProperties)
            {
                var marker = GetMarker();
                UpdateMarker(MarkerSettings.ChangeColor(marker.Settings, MarkerColor.SelectedColor));
            }
        }

        private void MarkerExtraColor_SelectedColorChanged(object sender, EventArgs e)
        {
            if (!_settingProperties)
            {
                var marker = GetMarker();
                UpdateMarker(MarkerSettings.ChangeExtraColor(marker.Settings, MarkerExtraColor.SelectedColor));
            }
        }

        private void MarkerView_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!_settingProperties)
            {
                var marker = GetMarker();
                UpdateMarker(MarkerSettings.ChangeView(marker.Settings, MarkerView.SelectedValue.ToString()));
            }
        }

        public void SetMarker(Player player)
        {
            _player = player;
            _tribe = null;

            Marker marker = World.Default.Map.MarkerManager.GetMarker(player);
            SetMarker(marker.Settings);
        }

        public void SetMarker(Tribe tribe)
        {
            _player = null;
            _tribe = tribe;

            Marker marker = World.Default.Map.MarkerManager.GetMarker(tribe);
            SetMarker(marker.Settings);
        }

        /// <summary>
        /// Make the control reflect the MarkerSettings
        /// </summary>
        private void SetMarker(MarkerSettings settings)
        {
            MarkerView.DataSource = World.Default.GetBackgroundViews().ToArray();

            _settingProperties = true;
            MarkerActive.Checked = settings.Enabled;
            MarkerColor.SelectedColor = settings.Color;
            MarkerExtraColor.SelectedColor = settings.ExtraColor;
            MarkerView.SelectedValue = settings.View;
            _settingProperties = false;
        }
    }
}
