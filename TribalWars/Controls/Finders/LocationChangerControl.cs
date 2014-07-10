#region Imports
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX.EditControls;
using TribalWars.Maps;
using TribalWars.Worlds;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Controls.Finders
{
    /// <summary>
    /// A VillageTextBox with button to move the map center
    /// </summary>
    public partial class LocationChangerControl : UserControl
    {
        private const string DefaultPlaceHolderText = "Search village, player, tribe...";

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
            SelectorControl.Initialize(map);
            _map.EventPublisher.LocationChanged += EventPublisher_LocationChanged;
            SelectorControl.PlaceHolderText = DefaultPlaceHolderText;
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
                _map.SetZoomLevel(value);
            }
        }

        /// <summary>
        /// New location
        /// </summary>
        private void EventPublisher_LocationChanged(object sender, MapLocationEventArgs e)
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
