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
			_menu.AddCommand(ControlsRes.NoVillageContextMenu_CenterHere, OnMapCenter, Properties.Resources.TeleportIcon);
			_menu.AddCommand(ControlsRes.NoVillageContextMenu_SetHome, OnSetHome, Properties.Resources.HomeIcon);
            _menu.AddSeparator();

			_menu.AddChangeColorCommand(ControlsRes.NoVillageContextMenu_BackgroundColor, World.Default.Map.Display.Settings.BackgroundColor, Color.Green, OnBackgroundColor);
			_menu.AddToggleCommand(ControlsRes.NoVillageContextMenu_ContinentLines, World.Default.Map.Display.Settings.ContinentLines, OnContinentLines);
            _menu.AddToggleCommand(ControlsRes.NoVillageContextMenu_ProvinceLines, World.Default.Map.Display.Settings.ProvinceLines, OnProvinceLines);
            _menu.AddSeparator();

			_menu.AddToggleCommand(ControlsRes.NoVillageContextMenu_HideAbandoned, World.Default.Map.Display.Settings.HideAbandoned, OnHideAbandoned);
			_menu.AddToggleCommand(ControlsRes.NoVillageContextMenu_ShowMarkedOnly, World.Default.Map.Display.Settings.MarkedOnly, OnMarkedOnly);
            _menu.AddSeparator();

			var showTooltip = _menu.AddCommand(ControlsRes.NoVillageContextMenu_ShowVillageTooltip, OnShowTooltip);
            showTooltip.IsChecked = World.Default.Map.Manipulators.CurrentManipulator.TooltipActive;
        }

        private void OnBackgroundColor(object sender, Color selectedColor)
        {
            World.Default.Map.Display.Settings.BackgroundColor = selectedColor;
            World.Default.DrawMaps();
        }

        private void OnMarkedOnly(object sender, CommandEventArgs e)
        {
            World.Default.Map.Display.Settings.MarkedOnly = !World.Default.Map.Display.Settings.MarkedOnly;
            World.Default.DrawMaps();
        }

        private void OnHideAbandoned(object sender, CommandEventArgs e)
        {
            World.Default.Map.Display.Settings.HideAbandoned = !World.Default.Map.Display.Settings.HideAbandoned;
            World.Default.DrawMaps();
        }

        private void OnContinentLines(object sender, CommandEventArgs e)
        {
            World.Default.Map.Display.Settings.ContinentLines = !World.Default.Map.Display.Settings.ContinentLines;
            World.Default.DrawMaps();
        }

        private void OnProvinceLines(object sender, CommandEventArgs e)
        {
            World.Default.Map.Display.Settings.ProvinceLines = !World.Default.Map.Display.Settings.ProvinceLines;
            World.Default.DrawMaps();
        }

        public void Show(Control control, Point pos)
        {
            _menu.Show(control, pos);
        }

        public bool IsVisible()
        {
            return _menu.IsVisible;
        }

        private void OnMapCenter(object sender, CommandEventArgs e)
        {
            World.Default.Map.SetCenter(_gameLocation);
        }

        private void OnSetHome(object sender, CommandEventArgs e)
        {
            World.Default.Map.SaveHome();
        }

        private void OnShowTooltip(object sender, CommandEventArgs e)
        {
            World.Default.Map.Manipulators.CurrentManipulator.TooltipActive = !World.Default.Map.Manipulators.CurrentManipulator.TooltipActive;
        }
    }
}
