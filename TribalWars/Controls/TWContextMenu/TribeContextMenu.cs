using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Tribes;
using TribalWars.Data.Maps;

namespace TribalWars.Controls.TWContextMenu
{
    /// <summary>
    /// ContextMenu with general Tribe operations
    /// </summary>
    public class TribeContextMenu : ContextMenuStrip
    {
        #region Fields
        private Tribe _tribe;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the tribe 
        /// </summary>
        public Tribe Tribe
        {
            get { return _tribe; }
            set { _tribe = value; }
        }
        #endregion

        #region Constructors
        public TribeContextMenu()
            : base()
        {
            Items.Add("Pinpoint", null, OnPinpoint);
            Items.Add("Pinpoint && Center", null, OnCenter);
            Items.Add("Details", null, OnDetails);
            //Items.Add("Mark", null, OnMark);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Shows the ContextMenuStrip
        /// </summary>
        public void Show(Control control, System.Drawing.Point position, Tribe target)
        {
            _tribe = target;
            Show(control, position);
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Puts the pinpoint on the target village
        /// </summary>
        private void OnPinpoint(object sender, EventArgs e)
        {
            if (_tribe != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, _tribe, VillageTools.PinPoint);
            }
        }

        /// <summary>
        /// Pinpoints and centers the target village
        /// </summary>
        private void OnCenter(object sender, EventArgs e)
        {
            if (_tribe != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, _tribe, VillageTools.PinPoint);
                World.Default.Map.SetCenter(Data.Maps.Display.GetSpan(_tribe));
            }
        }

        /// <summary>
        /// Start a new marker for the owner of the target village
        /// </summary>
        private void OnMark(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Open quick details for the tribe
        /// </summary>
        private void OnDetails(object sender, EventArgs e)
        {
            if (_tribe != null)
            {
                World.Default.Map.EventPublisher.SelectTribe(null, _tribe, VillageTools.SelectVillage);
            }
        }
        #endregion
    }
}
