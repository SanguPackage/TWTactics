using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using TribalWars.Controls.Finders;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds;

namespace TribalWars.Maps.Markers
{
    public partial class MarkersControl : UserControl
    {
        private readonly VillagePlayerTribeFinderTextBox _playerTribeSelector;

        public MarkersControl()
        {
            InitializeComponent();

            World.Default.EventPublisher.SettingsLoaded += WorldEventPublisher_SettingsLoaded;

            _playerTribeSelector = new VillagePlayerTribeFinderTextBox();
            _playerTribeSelector.AllowPlayer = true;
            _playerTribeSelector.AllowTribe = true;
            _playerTribeSelector.AllowVillage = false;
        }

        private void WorldEventPublisher_SettingsLoaded(object sender, EventArgs e)
        {
            EnemyMarker.SetMarker(World.Default.Map.MarkerManager.EnemyMarkerSettings);
            AbandonedMarker.SetMarker(World.Default.Map.MarkerManager.AbandonedMarkerSettings);

            var views = new GridEXValueListItemCollection();
            views.AddRange(World.Default.GetBackgroundViews().Select(x => new GridEXValueListItem(x, x)).ToArray());
            MarkersGrid.RootTable.Columns["View"].EditValueList = views;

            SetMarkersGridDataSource();
        }

        private void MarkersControl_Load(object sender, EventArgs e)
        {
            MarkersGrid.Configure(true, false);

            MarkersGrid.RootTable.Columns["Color"].ConfigureAsColor();
            MarkersGrid.RootTable.Columns["ExtraColor"].ConfigureAsColor();
            //MarkersGrid.RootTable.Columns["Name"].ConfigureAsPlayerTribeSelector();

        }

        private void MarkersGrid_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.Record || e.Row.RowType == RowType.NewRecord)
            {
                var marker = e.Row.DataRow as MarkerGridRow;
                if (marker != null)
                {
                    e.Row.Cells["Type"].Image = marker.GetTypeImage();
                    e.Row.Cells["Type"].ToolTipText = marker.GetTooltip();
                    e.Row.Cells["Name"].ToolTipText = marker.GetTooltip();
                }
            }
        }



        private List<MarkerGridRow> _markers = new List<MarkerGridRow>();
        private void SetMarkersGridDataSource()
        {
            _markers = new List<MarkerGridRow>();
            foreach (var marker in World.Default.Map.MarkerManager.GetMarkers())
            {
                _markers.Add(marker);
            }

            MarkersGrid.DataSource = _markers;
        }

        private void MarkersGrid_RecordAdded(object sender, EventArgs e)
        {
            var x = 5;
        }

        private void MarkersGrid_RecordsDeleted(object sender, EventArgs e)
        {
            var x = 5;
        }

        private void MarkersGrid_RecordUpdated(object sender, EventArgs e)
        {

        }

        private void MarkersGrid_CellValueChanged(object sender, ColumnActionEventArgs e)
        {

        }

        private void MarkersGrid_InitCustomEdit(object sender, InitCustomEditEventArgs e)
        {
            if (e.Column.Key == "Name")
            {
                var rowData = e.Row.DataRow as MarkerGridRow;
                if (rowData != null)
                {
                    if (rowData.Player != null)
                    {
                        _playerTribeSelector.SetPlayer(rowData.Player);
                    }
                    else
                    {
                        _playerTribeSelector.SetTribe(rowData.Tribe);
                    }
                }
                else
                {
                    _playerTribeSelector.EmptyTextBox(false);
                }
                e.EditControl = _playerTribeSelector;
            }
        }

        private void MarkersGrid_EndCustomEdit(object sender, EndCustomEditEventArgs e)
        {
            if (e.Column.Key == "Name")
            {
                var rowData = e.Row.DataRow as MarkerGridRow;
                if (rowData != null)
                {
                    rowData.Player = _playerTribeSelector.Player;
                    rowData.Tribe = _playerTribeSelector.Tribe;
                    e.DataChanged = true;
                }
                else
                {
                    object selected = _playerTribeSelector.Tribe ?? (object) _playerTribeSelector.Player;
                    e.Value = selected;
                    e.DataChanged = true;
                }
            }
        }

        private void MarkersGrid_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "Delete")
            {
                if (MarkersGrid.CurrentRow != null && MarkersGrid.CurrentRow.RowType == RowType.Record)
                {
                    MarkersGrid.CurrentRow.Delete();
                }
            }
        }

        private void MarkersGrid_AddingRecord(object sender, CancelEventArgs e)
        {

        }
    }

    /// <summary>
    /// A marker as displayed in the GridEX
    /// </summary>
    public class MarkerGridRow
    {
        public bool Enabled { get; set; }
        public Color Color { get; set; }
        public Color ExtraColor { get; set; }
        public Tribe Tribe { get; set; }
        public Player Player { get; set; }
        public string View { get; set; }

        private string _uh;

        public string Name
        {
            get
            {
                if (Tribe != null)
                {
                    return Tribe.Tag;
                }
                if (Player != null)
                {
                    return Player.Name;
                }
                return _uh ?? "";
            }
            set { _uh = value; }
        }

        public MarkerGridRow(Marker marker)
        {
            Enabled = marker.Settings.Enabled;
            Color = marker.Settings.Color;
            ExtraColor = marker.Settings.ExtraColor;
            View = marker.Settings.View;
            Tribe = marker.Tribe;
            Player = marker.Player;
        }

        public MarkerGridRow()
        {
            
        }

        public Image GetTypeImage()
        {
            if (Tribe != null)
            {
                return Properties.Resources.Tribe;
            }
            if (Player != null)
            {
                return Properties.Resources.Player;
            }
            return null;
        }

        public string GetTooltip()
        {
            if (Tribe != null)
            {
                return Tribe.Tooltip;
            }
            if (Player != null)
            {
                return Player.Tooltip;
            }
            return null;
        }

        public override string ToString()
        {
            return string.Format(
                "{0}, Colors=({1}+{2}), View={3}, Enabled={4}", 
                Player == null ? Tribe.Tag : Player.Name, 
                Color.Description().Replace("=", ":"), 
                ExtraColor.Description().Replace("=", ":"), 
                View, 
                Enabled);
        }
    }
}
