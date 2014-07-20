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

        /// <summary>
        /// Put given text onto the clipboard
        /// </summary>
        /// <remarks>
        /// Returns true if the operation succeeded
        /// </remarks>
        public static bool ToClipboard(string content)
        {
            try
            {
                Clipboard.SetText(content);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
