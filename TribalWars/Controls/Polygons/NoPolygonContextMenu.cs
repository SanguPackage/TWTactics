using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using TribalWars.Maps.Manipulators.Implementations;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;

namespace TribalWars.Controls.Polygons
{
    /// <summary>
    /// Right mouse click functionality for polygons when no polygon is selected
    /// </summary>
    public class NoPolygonContextMenu : IContextMenu
    {
        private readonly BbCodeManipulator _bbCode;
        private readonly UIContextMenu _menu;

        public NoPolygonContextMenu(BbCodeManipulator bbCode)
        {
            _bbCode = bbCode;
            _menu = JanusContextMenu.Create();

            if (_bbCode.Polygons.Count > 0)
            {
                _menu.AddCommand("Generate all", OnGenerate);
                _menu.AddSeparator();
                _menu.AddCommand(string.Format("Delete all ({0})", _bbCode.Polygons.Count), OnClearAll);

                int visiblePolygons = _bbCode.Polygons.Count(x => x.Visible);
                if (visiblePolygons > 0)
                {
                    _menu.AddCommand(string.Format("Hide all visible ({0})", visiblePolygons), OnHideAll);
                }
                int hiddenPolygons = _bbCode.Polygons.Count(x => !x.Visible);
                if (hiddenPolygons > 0)
                {
                    _menu.AddCommand(string.Format("Show all hidden ({0})", hiddenPolygons), OnShowAll);
                }

                _menu.AddSeparator();
            }

            _menu.AddCommand("Help", OnHelp);
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
            _bbCode.Clear();
        }

        /// <summary>
        /// Raise the polygon event for the villages inside the polygons
        /// </summary>
        private void OnGenerate(object sender, CommandEventArgs e)
        {
            World.Default.Map.EventPublisher.ActivatePolygon(this, _bbCode.Polygons);
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
Click on a polygon to select it. Use Del to remove the selected Polygon. Use the arrow keys to move it.

Right click inside/outside a polygon for more options.";
            MessageBox.Show(caption, "Polygon Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
