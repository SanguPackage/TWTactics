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
        private readonly Marker _marker;

        private readonly UIContextMenu _menu;
        #endregion

        #region Constructors
        public MarkerContextMenu(Map map, Player player)
            : this()
        {
            _player = player;
            _marker = map.MarkerManager.GetMarker(player);
        }

        public MarkerContextMenu(Map map, Tribe tribe)
            : this()
        {
            _tribe = tribe;
            _marker = map.MarkerManager.GetMarker(tribe);
        }

        private MarkerContextMenu()
        {
            _menu = new UIContextMenu();

            _menu.AddTextBoxCommand("Name", _marker.Name, OnChangeName);
            _menu.AddChangeColorCommand("Color", _marker.Color, OnChangeColor);
            _menu.AddChangeColorCommand("Extra color", _marker.Color, OnChangeExtraColor);
        }
        #endregion

        #region Public Methods
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
        /// <summary>
        /// 
        /// </summary>
        private void OnChangeName(object sender, EventArgs e)
        {
            var nameChanger = (TextBox)sender;
            //_marker.Name = nameChanger;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnChangeColor(object sender, EventArgs e)
        {
            Color selectedColor = ((UIColorPicker)sender).SelectedColor;
            //_marker.Color = selectedColor;
            World.Default.InvalidateMarkers();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnChangeExtraColor(object sender, EventArgs e)
        {
            Color selectedColor = ((UIColorPicker)sender).SelectedColor;
            //_marker.ExtraColor = selectedColor;
            World.Default.InvalidateMarkers();
        }
        #endregion
    }
}
