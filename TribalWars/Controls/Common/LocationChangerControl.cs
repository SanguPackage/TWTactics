#region Imports
using System;
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
        }

        public void Initialize(Map map)
        {
            ZoomControl.Value = map.Location.Zoom;
            _map = map;
            PlayerTribeFinderTextBox.Initialize(map);
            _map.EventPublisher.LocationChanged += EventPublisher_LocationChanged;
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
                int value = ZoomControl.Value;
                _map.SetCenter(value);
            }
        }

        /// <summary>
        /// New location
        /// </summary>
        private void EventPublisher_LocationChanged(object sender, Data.Events.MapLocationEventArgs e)
        {
            _updatingZoom = true;
            ZoomControl.Minimum = e.ZoomInfo.Minimum;
            ZoomControl.Maximum = e.ZoomInfo.Maximum;
            ZoomControl.Value = e.NewLocation.Zoom;
            _updatingZoom = false;
        }
        #endregion
    }
}
