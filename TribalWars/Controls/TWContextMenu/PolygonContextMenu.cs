using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using TribalWars.Data;
using TribalWars.Data.Maps.Manipulators.Helpers;
using TribalWars.Data.Maps.Manipulators.Implementations;
using TribalWars.Data.Villages;
using TribalWars.Tools;

namespace TribalWars.Controls.TWContextMenu
{
    /// <summary>
    /// Right mouse click functionality for polygons when no polygon is selected
    /// </summary>
    public class PolygonContextMenu : IContextMenu
    {
        private readonly BbCodeManipulator _bbCode;
        private readonly UIContextMenu _menu;

        public PolygonContextMenu(BbCodeManipulator bbCode)
        {
            _bbCode = bbCode;
            _menu = new UIContextMenu();

            if (_bbCode.Polygons.Count > 0)
            {
                _menu.AddCommand(ContextMenuKeys.Polygon.Generate, "Generate all", OnGenerate);
                _menu.AddSeparator();
                _menu.AddCommand(ContextMenuKeys.Polygon.Clearall, string.Format("Delete all ({0})", _bbCode.Polygons.Count), OnClearAll);

                int visiblePolygons = _bbCode.Polygons.Count(x => x.Visible);
                if (visiblePolygons > 0)
                {
                    _menu.AddCommand(ContextMenuKeys.Polygon.Hideall, string.Format("Hide all visible ({0})", visiblePolygons), OnHideAll);
                }
                int hiddenPolygons = _bbCode.Polygons.Count(x => !x.Visible);
                if (hiddenPolygons > 0)
                {
                    _menu.AddCommand(ContextMenuKeys.Polygon.Showall, string.Format("Show all hidden ({0})", hiddenPolygons), OnShowAll);
                }

                _menu.AddSeparator();
            }

            _menu.AddCommand("help", "Help", OnHelp);
        }

        public void Show(Control control, Point pos, Village village)
        {
            _menu.Show(control, pos);
        }

        /// <summary>
        /// Clears all the polygons
        /// </summary>
        private void OnClearAll(object sender, CommandEventArgs e)
        {
            _bbCode.Clear();
        }

        /// <summary>
        /// Raise the polygon event for the villages inside the polygons
        /// </summary>
        private void OnGenerate(object sender, CommandEventArgs e)
        {
            var ds = new PolygonDataSet();
            foreach (Polygon poly in _bbCode.Polygons)
            {
                foreach (Village v in poly.GetVillages())
                {
                    ds.AddVILLAGERow(v, poly.Name);
                }
            }
            World.Default.Map.EventPublisher.ActivatePolygon(this, ds);
        }

        /// <summary>
        /// Hides the polygons
        /// </summary>
        private void OnHideAll(object sender, CommandEventArgs e)
        {
            _bbCode.ToggleVisibility(false);
        }

        /// <summary>
        /// Shows the polygons
        /// </summary>
        private void OnShowAll(object sender, CommandEventArgs e)
        {
            _bbCode.ToggleVisibility(true);
        }

        private void OnHelp(object sender, CommandEventArgs e)
        {
            const string caption = @"Click and hold the left mouse button to draw the area (=Polygon) you want to generate BB codes for.
Use Control to force drawing vertically and Shift to force drawing horizontally. (in case you don't have a steady hand:)
Click on a polygon to select it. Use Del to remove the selected Polygon. Use the arrow keys to move it.";
            MessageBox.Show(caption, "Polygon Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
