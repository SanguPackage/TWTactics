using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls;
using TribalWars.Maps.AttackPlans.Controls;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;

namespace TribalWars.Maps.Polygons
{
    /// <summary>
    /// Right mouse click functionality for polygons when a polygon is selected
    /// </summary>
    public class PolygonContextMenu : IContextMenu
    {
        private readonly PolygonDrawerManipulator _polygonDrawer;
        private readonly UIContextMenu _menu;

        public PolygonContextMenu(PolygonDrawerManipulator polygonDrawer)
        {
            _polygonDrawer = polygonDrawer;
            _menu = JanusContextMenu.Create();

            Debug.Assert(_polygonDrawer.ActivePolygon != null);

			_menu.AddCommand(string.Format(ControlsRes.PolygonContextMenu_Manipulate, _polygonDrawer.ActivePolygon.Name), OnGenerate);
            AttackersPoolContextMenuCommandCreator.Add(_menu, _polygonDrawer.ActivePolygon.GetVillages().Where(x => x.Player == World.Default.You));
            _menu.AddSeparator();

			_menu.AddCommand(ControlsRes.PolygonContextMenu_Delete, OnDelete, Shortcut.Del);
			_menu.AddTextBoxCommand(ControlsRes.PolygonContextMenu_Name, _polygonDrawer.ActivePolygon.Name, NameChanged);
			_menu.AddTextBoxCommand(ControlsRes.PolygonContextMenu_Group, _polygonDrawer.ActivePolygon.Group, GroupChanged);
			_menu.AddChangeColorCommand(ControlsRes.PolygonContextMenu_Color, _polygonDrawer.ActivePolygon.LineColor, SelectedColorChanged);

			_menu.AddCommand(_polygonDrawer.ActivePolygon.Visible ? ControlsRes.PolygonContextMenu_Hide : ControlsRes.PolygonContextMenu_Show, ToggleVisibility);
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
        /// Deletes a polygon
        /// </summary>
        private void OnDelete(object sender, CommandEventArgs e)
        {
            _polygonDrawer.Delete();
        }

        private void NameChanged(object sender, System.EventArgs e)
        {
            var nameChanger = (TextBox)sender;
            _polygonDrawer.ActivePolygon.Name = nameChanger.Text;
        }

        private void GroupChanged(object sender, System.EventArgs e)
        {
            var groupChanger = (TextBox)sender;
            _polygonDrawer.ActivePolygon.Group = groupChanger.Text;
        }

        private void SelectedColorChanged(object sender, Color selectedColor)
        {
            _polygonDrawer.ActivePolygon.LineColor = selectedColor;
            World.Default.DrawMaps(false);
        }

        /// <summary>
        /// Raise the polygon event for the villages inside
        /// the selected polygon
        /// </summary>
        private void OnGenerate(object sender, CommandEventArgs e)
        {
            World.Default.Map.EventPublisher.ActivatePolygon(this, Enumerable.Repeat(_polygonDrawer.ActivePolygon, 1));
        }

        /// <summary>
        /// Hides/Shows one polygon
        /// </summary>
        private void ToggleVisibility(object sender, CommandEventArgs e)
        {
            _polygonDrawer.ToggleVisibility();
        }
    }
}
