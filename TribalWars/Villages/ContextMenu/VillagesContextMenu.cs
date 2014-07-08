#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TribalWars.Villages;

#endregion

namespace TribalWars.Controls.TWContextMenu
{
    /// <summary>
    /// ContextMenu with general Villages operations
    /// </summary>
    public class VillagesContextMenu : ContextMenuStrip
    {
        #region Fields
        private IEnumerable<Village> _villages;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the village s
        /// </summary>
        public IEnumerable<Village> Villages
        {
            get { return _villages; }
            set { _villages = value; }
        }
        #endregion

        #region Constructors
        public VillagesContextMenu()
        {
            Items.Add("BBCode", null, OnBbCode);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Shows the ContextMenuStrip
        /// </summary>
        public void Show(Control control, System.Drawing.Point position, IEnumerable<Village> target)
        {
            _villages = target;

            Show(control, position);
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Create BB code for the villages and put on clipboard
        /// </summary>
        private void OnBbCode(object sender, EventArgs e)
        {
            if (Villages != null)
            {
                var str = new StringBuilder();
                foreach (Village village in Villages)
                {
                    str.AppendFormat("{0}{1}", village.BbCode(), Environment.NewLine);
                }

                try
                {
                    Clipboard.SetText(str.ToString());
                }
                catch (Exception)
                {
                    
                }
            }
        }
        #endregion
    }
}
