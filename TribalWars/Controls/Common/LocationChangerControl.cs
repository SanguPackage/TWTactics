#region Imports
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data.Maps;
#endregion

namespace TribalWars.Controls.Common
{
    /// <summary>
    /// A VillageTextBox with button to move the map center
    /// </summary>
    public partial class LocationChangerControl : UserControl
    {
        #region Fields
        private Map _map;
        private bool _updatingZoom;
        #endregion

        #region Constructors
        public LocationChangerControl()
        {
            InitializeComponent();
            VillageTextBox.AllowPlayer = true;
            VillageTextBox.AllowTribe = true;
            VillageTextBox.ShowButton = true;
        }

        public void Initialize(Map map)
        {
            ZoomControl.Value = map.Location.Zoom;
            _map = map;
            VillageTextBox.Initialize(map);
            _map.EventPublisher.LocationChanged += new EventHandler<TribalWars.Data.Events.MapLocationEventArgs>(EventPublisher_LocationChanged);
        }
        #endregion

        #region Properties
        public VillageTextBox TextBox
        {
            get { return VillageTextBox; }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Changes the zoom level of the map
        /// </summary>
        private void ZoomControl_ValueChanged(object sender, EventArgs e)
        {
            if (_map != null && !_updatingZoom)
            {
                int Value = ZoomControl.Value;
                _map.SetCenter(Value);
            }
        }

        /// <summary>
        /// New location
        /// </summary>
        private void EventPublisher_LocationChanged(object sender, TribalWars.Data.Events.MapLocationEventArgs e)
        {
            _updatingZoom = true;
            ZoomControl.Minimum = e.ZoomInfo.Minimum;
            ZoomControl.Maximum = e.ZoomInfo.Maximum;
            ZoomControl.Value = e.NewLocation.Zoom;
            _updatingZoom = false;
        }
        #endregion

        #region Public Methods

        #endregion
    }
}
