using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.Common;
using Janus.Windows.UI.CommandBars;

namespace TribalWars.Tools
{
    /// <summary>
    /// Extensions for WinForms controls
    /// </summary>
    public static class WinForms
    {
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
