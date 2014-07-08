#region Using
using System;
using System.Diagnostics;
using System.ComponentModel;

#endregion

namespace TribalWars.Tools.PropertyGrid
{
    /// <summary>
    /// UI Editor to copy the value to the clipboard
    /// </summary>
    public class ClipboardCopierUiEditor : System.Drawing.Design.UITypeEditor
    {
        #region Overriden Methods
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                Debug.Assert(value != null, "value != null");
                System.Windows.Forms.Clipboard.SetText(value.ToString());
            }
            catch
            {

            }
            return base.EditValue(context, provider, value);
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
        #endregion
    }
}
