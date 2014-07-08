#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;
using TribalWars.Data;
using TribalWars.Data.Maps;
using TribalWars.Data.Maps.Markers;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using TribalWars.Tools;
#endregion

namespace TribalWars.Controls.TWContextMenu
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
            _menu.AddChangeColorCommand("Inner color", marker.Settings.ExtraColor, OnChangeExtraColor);
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

        #region Public Methods
        public UICommand GetMainCommand(UIContextMenu menu)
        {
            var marker = GetMarker();
            var cmd = menu.AddCommand(GetMainCommandText(marker));
            cmd.Image = GetMainCommandImage(marker);
            return cmd;
        }

        private Image GetMainCommandImage(Marker marker)
        {
            if (marker.Empty)
            {
                return null;
            }

            return Tools.Janus.DrawContextIcon(marker.Settings.Color, marker.Settings.ExtraColor);
        }
        
        private string GetMainCommandText(Marker marker)
        {
            return marker.Empty ? "Mark " + (_tribe == null ? _player.Name : _tribe.Tag) : "Marker";
        }

        public IEnumerable<UICommand> GetCommands()
        {
            return _menu.Commands.OfType<UICommand>();
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
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnChangeEnabled(object sender, CommandEventArgs e)
        {
            var marker = GetMarker();
            UpdateMarker(MarkerSettings.ChangeEnabled(marker.Settings, e.Command.IsChecked));
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnChangeColor(object sender, EventArgs e)
        {
            var marker = GetMarker();
            Color selectedColor = ((UIColorPicker)sender).SelectedColor;
            UpdateMarker(MarkerSettings.ChangeColor(marker.Settings, selectedColor));
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnChangeExtraColor(object sender, EventArgs e)
        {
            var marker = GetMarker();
            Color selectedColor = ((UIColorPicker)sender).SelectedColor;
            UpdateMarker(MarkerSettings.ChangeExtraColor(marker.Settings, selectedColor));
        }
        #endregion
    }
}
