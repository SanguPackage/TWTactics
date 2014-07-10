#region Using
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Markers
{
    /// <summary>
    /// Control for editing a Map Marker
    /// </summary>
    public partial class MarkerSettingsControl : UserControl
    {
        #region Fields
        private Player _player;
        private Tribe _tribe;
        private MarkerSettings _settings;
        private bool _settingProperties;
        #endregion

        #region Properties
        /// <summary>
        /// Can the marker be deactivated
        /// </summary>
        [Category("Appearance")]
        public bool CanDeactivate
        {
            get { return MarkerActive.Visible; }
            set
            {
                BilliardMarkerPicturebox.Visible = value;
                MarkerActive.Visible = value;
                MarkerActivePanel.Left = value ? 52 : 5;
                MarkerActivePanel.Width += (52 - 5) * (value ? -1 : 1);
            }
        }

        [Category("Appearance")]
        public Color DefaultMarkerColor
        {
            get { return MarkerColor.ColorPicker.AutomaticColor; }
            set
            {
                _settingProperties = true;
                MarkerColor.ColorPicker.AutomaticColor = value;
                _settingProperties = false;
            }
        }

        [Category("Appearance")]
        public Color DefaultExtraMarkerColor
        {
            get { return MarkerExtraColor.ColorPicker.AutomaticColor; }
            set
            {
                _settingProperties = true;
                MarkerExtraColor.ColorPicker.AutomaticColor = value;
                _settingProperties = false;
            }
        }

        /// <summary>
        /// Automatically update marker settings back
        /// to the World
        /// </summary>
        public bool AutoUpdateMarkers { get; set; }
        #endregion

        #region Constructors
        public MarkerSettingsControl()
        {
            InitializeComponent();
            AutoUpdateMarkers = true;
        }
        #endregion

        #region Public Methods
        public void SetMarker(MarkerSettings settings)
        {
            _settings = settings;
            _player = null;
            _tribe = null;

            SetControlProperties(settings);
        }

        public void SetMarker(Player player)
        {
            _settings = null;
            _player = player;
            _tribe = null;

            Marker marker = World.Default.Map.MarkerManager.GetMarker(player);
            SetControlProperties(marker.Settings);
        }

        public void SetMarker(Tribe tribe)
        {
            _settings = null;
            _player = null;
            _tribe = tribe;

            Marker marker = World.Default.Map.MarkerManager.GetMarker(tribe);
            SetControlProperties(marker.Settings);
        }

        public MarkerSettings GetMarkerSettings()
        {
            if (_settings != null)
            {
                return _settings;
            }
            if (_tribe != null)
            {
                return World.Default.Map.MarkerManager.GetMarker(_tribe).Settings;
            }
            else
            {
                return World.Default.Map.MarkerManager.GetMarker(_player).Settings;
            }
        }
        #endregion

        #region EventHandlers
        private void MarkerSettingsControl_Load(object sender, EventArgs e)
        {
            _settingProperties = true;
            MarkerColor.Configure();
            MarkerExtraColor.Configure();
            MarkerExtraColor.ColorPicker.AutomaticColor = Color.Transparent;
            _settingProperties = false;
        }

        private void MarkerActive_CheckedChanged(object sender, EventArgs e)
        {
            MarkerActivePanel.Enabled = MarkerActive.Checked;

            if (!_settingProperties)
            {
                var settings = GetMarkerSettings();
                UpdateMarker(MarkerSettings.ChangeEnabled(settings, MarkerActive.Checked));
            }
        }

        private void MarkerColor_SelectedColorChanged(object sender, EventArgs e)
        {
            if (!_settingProperties)
            {
                var settings = GetMarkerSettings();
                UpdateMarker(MarkerSettings.ChangeColor(settings, MarkerColor.SelectedColor));
            }
        }

        private void MarkerExtraColor_SelectedColorChanged(object sender, EventArgs e)
        {
            if (!_settingProperties)
            {
                var settings = GetMarkerSettings();
                UpdateMarker(MarkerSettings.ChangeExtraColor(settings, MarkerExtraColor.SelectedColor));
            }
        }

        private void MarkerView_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!_settingProperties)
            {
                var settings = GetMarkerSettings();
                UpdateMarker(MarkerSettings.ChangeView(settings, MarkerView.SelectedValue.ToString()));
            }
        }
        #endregion

        #region Private
        private void UpdateMarker(MarkerSettings settings)
        {
            if (_settings != null)
            {
                if (AutoUpdateMarkers)
                {
                    World.Default.Map.MarkerManager.UpdateDefaultMarker(World.Default.Map, settings);
                }
                _settings = settings;
            }
            else if (_tribe != null)
            {
                Debug.Assert(AutoUpdateMarkers);
                World.Default.Map.MarkerManager.UpdateMarker(World.Default.Map, _tribe, settings);
            }
            else
            {
                Debug.Assert(AutoUpdateMarkers);
                World.Default.Map.MarkerManager.UpdateMarker(World.Default.Map, _player, settings);
            }
        }

        /// <summary>
        /// Make the control reflect the MarkerSettings
        /// </summary>
        private void SetControlProperties(MarkerSettings settings)
        {
            _settingProperties = true;

            MarkerView.SelectedValue = settings.View;
            MarkerView.DataSource = World.Default.GetBackgroundViews().ToArray();

            MarkerActive.Checked = settings.Enabled;
            MarkerColor.SelectedColor = settings.Color;
            MarkerExtraColor.SelectedColor = settings.ExtraColor;
            
            _settingProperties = false;
        }
        #endregion
    }
}
