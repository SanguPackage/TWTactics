using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;

namespace TribalWars.Maps.Polygons
{
    /// <summary>
    /// Right mouse click functionality for polygons when no polygon is selected
    /// </summary>
    public class NoPolygonContextMenu : IContextMenu
    {
        private readonly PolygonDrawerManipulator _polygonDrawer;
        private readonly UIContextMenu _menu;

        public NoPolygonContextMenu(PolygonDrawerManipulator polygonDrawer)
        {
            _polygonDrawer = polygonDrawer;
            _menu = JanusContextMenu.Create();

            if (_polygonDrawer.Polygons.Count > 0)
            {
				_menu.AddCommand(ControlsRes.NoPolygonContextMenu_ManipulateAll, OnGenerate);
                _menu.AddSeparator();
				_menu.AddCommand(string.Format(ControlsRes.NoPolygonContextMenu_DeleteAll, _polygonDrawer.Polygons.Count), OnClearAll);

                int visiblePolygons = _polygonDrawer.Polygons.Count(x => x.Visible);
                if (visiblePolygons > 0)
                {
					_menu.AddCommand(string.Format(ControlsRes.NoPolygonContextMenu_HideAll, visiblePolygons), OnHideAll);
                }
                int hiddenPolygons = _polygonDrawer.Polygons.Count(x => !x.Visible);
                if (hiddenPolygons > 0)
                {
					_menu.AddCommand(string.Format(ControlsRes.NoPolygonContextMenu_ShowAll, hiddenPolygons), OnShowAll);
                }

                _menu.AddSeparator();
            }

			_menu.AddCommand(ControlsRes.NoPolygonContextMenu_HelpCaption, OnHelp);
        }

        public void Show(Control control, Point pos)
        {
            _menu.Show(control, pos);
        }

        public bool IsVisible()
        {
            return _menu.IsVisible;
        }

        /// <summary>
        /// Clears all the polygons
        /// </summary>
        private void OnClearAll(object sender, CommandEventArgs e)
        {
			if (MessageBox.Show(ControlsRes.NoPolygonContextMenu_DeleteAllConfirm, ControlsRes.NoPolygonContextMenu_Clusters, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                _polygonDrawer.Clear();
            }
        }

        /// <summary>
        /// Raise the polygon event for the villages inside the polygons
        /// </summary>
        private void OnGenerate(object sender, CommandEventArgs e)
        {
            World.Default.Map.EventPublisher.ActivatePolygon(this, _polygonDrawer.Polygons);
        }

        /// <summary>
        /// Hides the polygons
        /// </summary>
        private void OnHideAll(object sender, CommandEventArgs e)
        {
            _polygonDrawer.ToggleVisibility(false);
        }

        /// <summary>
        /// Shows the polygons
        /// </summary>
        private void OnShowAll(object sender, CommandEventArgs e)
        {
            _polygonDrawer.ToggleVisibility(true);
        }

        private void OnHelp(object sender, CommandEventArgs e)
        {
			MessageBox.Show(ControlsRes.NoPolygonContextMenu_Help, ControlsRes.NoPolygonContextMenu_Clusters, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
