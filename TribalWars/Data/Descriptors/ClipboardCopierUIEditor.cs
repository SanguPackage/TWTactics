#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
#endregion

namespace TribalWars.Data.Descriptors
{
    /// <summary>
    /// UI Editor to copy the value to the clipboard
    /// </summary>
    public class ClipboardCopierUIEditor : System.Drawing.Design.UITypeEditor
    {
        #region Overriden Methods
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(value.ToString());
            }
            catch (Exception)
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
