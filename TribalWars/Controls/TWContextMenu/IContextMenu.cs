#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

#endregion

namespace TribalWars.Controls.TWContextMenu
{
    /// <summary>
    /// Defines a show method
    /// </summary>
    public interface IContextMenu
    {
        void Show(Control c, Point p);
    }
}
