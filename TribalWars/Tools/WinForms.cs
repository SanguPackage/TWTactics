using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TribalWars.Tools
{
    /// <summary>
    /// Extensions for WinForms controls
    /// </summary>
    public static class WinForms
    {
        /// <summary>
        /// Create a WinForms tooltip control with default properties set
        /// </summary>
        public static ToolTip CreateTooltip()
        {
            return new ToolTip()
                {
                    Active = true,
                    IsBalloon = true
                };
        }

        /// <summary>
        /// Update a control from different thread
        /// </summary>
        public static void InvokeIfRequired(this ISynchronizeInvoke obj, MethodInvoker action)
        {
            if (obj.InvokeRequired)
            {
                var args = new object[0];
                obj.Invoke(action, args);
            }
            else
            {
                action();
            }
        }
    }
}
