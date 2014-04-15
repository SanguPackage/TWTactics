#region Imports
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace TribalWars.Controls.TWContextMenu
{
    /// <summary>
    /// Implement this interface to show the Village, Player or Tribe context menu
    /// </summary>
    public interface ITWContextMenu
    {
        /// <summary>
        /// Shows the context menu
        /// </summary>
        /// <param name="p">The location to show the menu</param>
        void ShowContext(Point p);

        /// <summary>
        /// Gets a list of all villages
        /// </summary>
        IEnumerable<TribalWars.Data.Villages.Village> GetVillages();

        /// <summary>
        /// Displays the details for the context
        /// </summary>
        void DisplayDetails();
    }
}
