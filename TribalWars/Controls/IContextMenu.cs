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
        void Show(Control c, Point p);
    }
}
