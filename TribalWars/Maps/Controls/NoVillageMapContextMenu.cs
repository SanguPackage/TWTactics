using System.Drawing;
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Tools;
using TribalWars.Worlds;

namespace TribalWars.Maps.Controls
{
    /// <summary>
    /// ContextMenu when right clicking but not on any village on the map
    /// </summary>
    public class NoVillageMapContextMenu : IContextMenu
    {
        private readonly UIContextMenu _menu;
        private readonly Point _gameLocation;

        public NoVillageMapContextMenu(Point gameLocation)
        {
            _gameLocation = gameLocation;

            _menu = new UIContextMenu();
            _menu.AddCommand("Center here", OnMapCenter, Properties.Resources.TeleportIcon);
            _menu.AddSeparator();

            var showTooltip = _menu.AddCommand("Show village &tooltip", OnShowTooltip);
            showTooltip.IsChecked = World.Default.Map.Manipulators.CurrentManipulator.TooltipActive;
        }

        public void Show(Control control, Point pos)
        {
            _menu.Show(control, pos);
        }

        private void OnMapCenter(object sender, CommandEventArgs e)
        {
            World.Default.Map.SetCenter(_gameLocation);
        }

        private void OnShowTooltip(object sender, CommandEventArgs e)
        {
            World.Default.Map.Manipulators.CurrentManipulator.TooltipActive = !World.Default.Map.Manipulators.CurrentManipulator.TooltipActive;
        }
    }
}
