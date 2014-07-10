using System.Drawing;
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;

namespace TribalWars.Villages.ContextMenu
{
    /// <summary>
    /// ContextMenu when right clicking but not on any village on the map
    /// </summary>
    public class NoVillageContextMenu : IContextMenu
    {
        private readonly UIContextMenu _menu;
        private readonly Point _gameLocation;

        public NoVillageContextMenu(Point gameLocation)
        {
            _gameLocation = gameLocation;

            _menu = JanusContextMenu.Create();
            _menu.AddCommand("Center here", OnMapCenter, Properties.Resources.TeleportIcon);
            _menu.AddSeparator();

            _menu.AddChangeColorCommand("Background color", World.Default.Map.Display.Settings.BackgroundColor, Color.Green, OnBackgroundColor);
            _menu.AddToggleCommand("Show continent lines", World.Default.Map.Display.Settings.ContinentLines, OnContinentLines);
            _menu.AddToggleCommand("Show province lines", World.Default.Map.Display.Settings.ProvinceLines, OnProvinceLines);
            _menu.AddSeparator();

            _menu.AddToggleCommand("Hide all abandoned", World.Default.Map.Display.Settings.HideAbandoned, OnHideAbandoned);
            _menu.AddToggleCommand("Show marked only", World.Default.Map.Display.Settings.MarkedOnly, OnMarkedOnly);
            _menu.AddSeparator();

            var showTooltip = _menu.AddCommand("Show village &tooltip", OnShowTooltip);
            showTooltip.IsChecked = World.Default.Map.Manipulators.CurrentManipulator.TooltipActive;
        }

        private void OnBackgroundColor(object sender, Color selectedColor)
        {
            World.Default.Map.Display.Settings.BackgroundColor = selectedColor;
        }

        private void OnMarkedOnly(object sender, CommandEventArgs e)
        {
            World.Default.Map.Display.Settings.MarkedOnly = !World.Default.Map.Display.Settings.MarkedOnly;
        }

        private void OnHideAbandoned(object sender, CommandEventArgs e)
        {
            World.Default.Map.Display.Settings.HideAbandoned = !World.Default.Map.Display.Settings.HideAbandoned;
        }

        private void OnContinentLines(object sender, CommandEventArgs e)
        {
            World.Default.Map.Display.Settings.ContinentLines = !World.Default.Map.Display.Settings.ContinentLines;
        }

        private void OnProvinceLines(object sender, CommandEventArgs e)
        {
            World.Default.Map.Display.Settings.ProvinceLines = !World.Default.Map.Display.Settings.ProvinceLines;
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
