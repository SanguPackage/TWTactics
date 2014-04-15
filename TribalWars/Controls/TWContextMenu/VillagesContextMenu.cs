#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data.Villages;

using TribalWars.Data.Maps;
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
            : base()
        {
            Items.Add("Pinpoint", null, OnPinpoint);
            Items.Add("Pinpoint && Center", null, OnCenter);
            Items.Add("BBCode", null, OnBBCode);
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
        /// Puts the pinpoint on the target villages
        /// </summary>
        private void OnPinpoint(object sender, EventArgs e)
        {
            if (Villages != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, Villages, VillageTools.PinPoint);
            }
        }

        /// <summary>
        /// Pinpoints and centers the target villages
        /// </summary>
        private void OnCenter(object sender, EventArgs e)
        {
            if (Villages != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, Villages, VillageTools.PinPoint);
                World.Default.Map.SetCenter(Data.Maps.Display.GetSpan(Villages));
            }
        }

        /// <summary>
        /// Create BB code for the villages and put on clipboard
        /// </summary>
        private void OnBBCode(object sender, EventArgs e)
        {
            if (Villages != null)
            {
                StringBuilder str = new StringBuilder();
                foreach (Village village in Villages)
                    str.AppendFormat("{0}{1}", village.BBCode(), Environment.NewLine);

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
