#region Using
using System.Windows.Forms;
using System.Drawing;

#endregion

namespace TribalWars.Controls
{
    /// <summary>
    /// Defines a show method
    /// </summary>
    public interface IContextMenu
    {
        /// <summary>
        /// Show the contextmenu
        /// </summary>
        void Show(Control c, Point p);

        /// <summary>
        /// Is the contextmenu currently visible
        /// </summary>
        bool IsVisible();
    }
}
