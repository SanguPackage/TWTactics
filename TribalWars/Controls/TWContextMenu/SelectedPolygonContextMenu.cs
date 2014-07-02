using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;
using TribalWars.Data;
using TribalWars.Data.Maps.Manipulators.Helpers;
using TribalWars.Data.Maps.Manipulators.Implementations;
using TribalWars.Data.Villages;
using TribalWars.Tools;

namespace TribalWars.Controls.TWContextMenu
{
    /// <summary>
    /// Right mouse click functionality for polygons when a polygon is selected
    /// </summary>
    public class SelectedPolygonContextMenu : IContextMenu
    {
        private readonly BbCodeManipulator _bbCode;
        private readonly UIContextMenu _menu;

        public SelectedPolygonContextMenu(BbCodeManipulator bbCode)
        {
            _bbCode = bbCode;
            _menu = new UIContextMenu();

            Debug.Assert(_bbCode.ActivePolygon != null);

            _menu.AddCommand(ContextMenuKeys.Polygons.Generate, string.Format("Generate \"{0}\"", _bbCode.ActivePolygon.Name), OnGenerate);
            _menu.AddSeparator();
            _menu.AddCommand(ContextMenuKeys.Polygons.Delete, "Delete", OnDelete, Shortcut.Del);

            AddChangeNameCommand();
            _menu.AddTextBoxCommand("ChangeGroup", "Group", _bbCode.ActivePolygon.Group, GroupChanged);
            AddChangeColorCommand();

            _menu.AddCommand(ContextMenuKeys.Polygons.Edit, _bbCode.ActivePolygon.Visible ? "Hide" : "Show", ToggleVisibility);
        }

        private void AddChangeNameCommand()
        {
            _menu.AddTextBoxCommand("ChangeName", "Name", _bbCode.ActivePolygon.Name, NameChanged);
        }

        private void AddChangeColorCommand()
        {
            var colorPicker = new UIColorPicker();
            colorPicker.SelectedColor = _bbCode.ActivePolygon.LineColor;
            colorPicker.SelectedColorChanged += SelectedColorChanged;

            var cmd = new UICommand("ChangeColor", "Color", CommandType.ColorPickerCommand);
            cmd.Control = colorPicker;
            _menu.Commands.Add(cmd);
        }

        public void Show(Control control, Point pos, Village village)
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

        private void SelectedColorChanged(object sender, EventArgs e)
        {
            _bbCode.ActivePolygon.LineColor = ((UIColorPicker)sender).SelectedColor;
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
