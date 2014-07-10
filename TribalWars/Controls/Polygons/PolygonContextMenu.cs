using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.UI.CommandBars;
using TribalWars.Maps.Manipulators.Implementations;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;

namespace TribalWars.Controls.Polygons
{
    /// <summary>
    /// Right mouse click functionality for polygons when a polygon is selected
    /// </summary>
    public class PolygonContextMenu : IContextMenu
    {
        private readonly BbCodeManipulator _bbCode;
        private readonly UIContextMenu _menu;

        public PolygonContextMenu(BbCodeManipulator bbCode)
        {
            _bbCode = bbCode;
            _menu = new UIContextMenu();

            Debug.Assert(_bbCode.ActivePolygon != null);

            _menu.AddCommand(string.Format("Generate \"{0}\"", _bbCode.ActivePolygon.Name), OnGenerate);
            _menu.AddSeparator();

            _menu.AddCommand("Delete", OnDelete, Shortcut.Del);
            _menu.AddTextBoxCommand("Name", _bbCode.ActivePolygon.Name, NameChanged);
            _menu.AddTextBoxCommand("Group", _bbCode.ActivePolygon.Group, GroupChanged);
            _menu.AddChangeColorCommand("Color", _bbCode.ActivePolygon.LineColor, SelectedColorChanged);

            _menu.AddCommand(_bbCode.ActivePolygon.Visible ? "Hide" : "Show", ToggleVisibility);
        }

        public void Show(Control control, Point pos)
        {
            _menu.Show(control, pos);
        }

        /// <summary>
        /// Deletes a polygon
        /// </summary>
        private void OnDelete(object sender, CommandEventArgs e)
        {
            _bbCode.Delete();
        }

        private void NameChanged(object sender, EventArgs e)
        {
            var nameChanger = (TextBox)sender;
            _bbCode.ActivePolygon.Name = nameChanger.Text;
        }

        private void GroupChanged(object sender, EventArgs e)
        {
            var groupChanger = (TextBox)sender;
            _bbCode.ActivePolygon.Group = groupChanger.Text;
        }

        private void SelectedColorChanged(object sender, Color selectedColor)
        {
            _bbCode.ActivePolygon.LineColor = selectedColor;
            World.Default.DrawMaps(false);
        }

        /// <summary>
        /// Raise the polygon event for the villages inside
        /// the selected polygon
        /// </summary>
        private void OnGenerate(object sender, CommandEventArgs e)
        {
            World.Default.Map.EventPublisher.ActivatePolygon(this, Enumerable.Repeat(_bbCode.ActivePolygon, 1));
        }

        /// <summary>
        /// Hides/Shows one polygon
        /// </summary>
        private void ToggleVisibility(object sender, CommandEventArgs e)
        {
            _bbCode.ToggleVisibility();
        }
    }
}
