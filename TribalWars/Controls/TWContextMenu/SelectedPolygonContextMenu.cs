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

            _menu.AddCommand(ContextMenuKeys.Polygon.Generate, string.Format("Generate \"{0}\"", _bbCode.ActivePolygon.Name), OnGenerate);
            _menu.AddSeparator();
            _menu.AddCommand(ContextMenuKeys.Polygon.Delete, "Delete", OnDelete, Shortcut.Del);

            AddChangeNameCommand();
            AddChangeColorCommand();

            _menu.AddCommand(ContextMenuKeys.Polygon.Edit, _bbCode.ActivePolygon.Visible ? "Hide" : "Show", ToggleVisibility);
        }

        private void AddChangeNameCommand()
        {
            var nameChanger = new TextBox();
            nameChanger.Text = _bbCode.ActivePolygon.Name;
            nameChanger.TextChanged += NameChanged;

            var cmd = new UICommand("ChangeName", "Name", CommandType.TextBoxCommand);
            cmd.Control = nameChanger;
            _menu.Commands.Add(cmd);
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
            var ds = new PolygonDataSet();
            foreach (Village v in _bbCode.ActivePolygon.GetVillages())
            {
                ds.AddVILLAGERow(v, _bbCode.ActivePolygon);
            }
            World.Default.Map.EventPublisher.ActivatePolygon(this, ds);
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
