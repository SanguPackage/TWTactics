#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Markers
{
    /// <summary>
    /// ContextMenu for marking a player or tribe
    /// </summary>
    public class MarkerContextMenu : IContextMenu
    {
        #region Fields
        private readonly Player _player;
        private readonly Tribe _tribe;

        private readonly UIContextMenu _menu;
        private readonly Map _map;

        private UICommand _mainCommand;
        #endregion

        #region Constructors
        public MarkerContextMenu(Map map, Player player)
        {
            _menu = new UIContextMenu();
            _map = map;
            _player = player;
            InitializeMenu();
        }

        public MarkerContextMenu(Map map, Tribe tribe)
        {
            _menu = new UIContextMenu();
            _map = map;
            _tribe = tribe;
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            Marker marker = GetMarker();

            _menu.AddChangeColorCommand("Main color", marker.Settings.Color, OnChangeColor);
            _menu.AddChangeColorCommand("Inner color", marker.Settings.ExtraColor, Color.Transparent, OnChangeExtraColor);

            IEnumerable<string> views = World.Default.GetBackgroundViews(false);
            _menu.AddComboBoxCommand("View", views, marker.Settings.View, OnChangeView);
            _menu.AddToggleCommand(marker.Settings.Enabled ? "Disable marker" : "Activate marker", marker.Settings.Enabled, OnChangeEnabled);
        }

        private Marker GetMarker()
        {
            if (_tribe != null)
            {
                return _map.MarkerManager.GetMarker(_tribe);
            }
            else
            {
                return _map.MarkerManager.GetMarker(_player);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the command that owns GetCommands()
        /// </summary>
        public UICommand GetMainCommand(UIContextMenu menu)
        {
            Debug.Assert(_mainCommand == null);
            var marker = GetMarker();
            var cmd = menu.AddCommand(GetMainCommandText(marker));
            if (marker.Settings.Enabled)
            {
                cmd.Image = GetMainCommandImage(marker);
            }

            _mainCommand = cmd;
            return cmd;
        }

        /// <summary>
        /// Gets the commands to edit the marker
        /// </summary>
        public IEnumerable<UICommand> GetCommands()
        {
            return _menu.Commands.OfType<UICommand>();
        }

        private Image GetMainCommandImage(Marker marker)
        {
            if (marker.Empty)
            {
                return null;
            }

            return JanusContextMenu.DrawContextIcon(marker.Settings.Color, marker.Settings.ExtraColor);
        }
        
        private string GetMainCommandText(Marker marker)
        {
            return marker.Empty ? "Mark " + (_tribe == null ? _player.Name : _tribe.Tag) : "Marker";
        }

        public void Show(Control control, Point position)
        {
            _menu.Show(control, position);
        }
        #endregion

        #region EventHandlers
        private void UpdateMarker(MarkerSettings settings)
        {
            if (_tribe != null)
            {
                _map.MarkerManager.UpdateMarker(_map, _tribe, settings);
            }
            else
            {
                _map.MarkerManager.UpdateMarker(_map, _player, settings);
            }
            _mainCommand.Image = GetMainCommandImage(GetMarker());
        }

        private void OnChangeEnabled(object sender, CommandEventArgs e)
        {
            var marker = GetMarker();
            UpdateMarker(MarkerSettings.ChangeEnabled(marker.Settings, e.Command.IsChecked));
        }

        private void OnChangeView(object sender, EventArgs e)
        {
            var marker = GetMarker();
            UpdateMarker(MarkerSettings.ChangeView(marker.Settings, ((UIComboBox)sender).SelectedValue.ToString()));
        }

        private void OnChangeColor(object sender, Color selectedColor)
        {
            var marker = GetMarker();
            UpdateMarker(MarkerSettings.ChangeColor(marker.Settings, selectedColor));
        }

        private void OnChangeExtraColor(object sender, Color selectedColor)
        {
            var marker = GetMarker();
            UpdateMarker(MarkerSettings.ChangeExtraColor(marker.Settings, selectedColor));
        }
        #endregion
    }
}
