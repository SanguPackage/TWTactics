using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using TribalWars.Controls.Finders;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Villages;
using TribalWars.Worlds;

namespace TribalWars.Maps.Markers
{
    /// <summary>
    /// Edit control for Enemy, Abandoned and UserDefined Markers
    /// </summary>
    public partial class MarkersControl : UserControl
    {
        #region Fields
        private readonly VillagePlayerTribeFinderTextBox _playerTribeSelector;
        private List<MarkerGridRow> _markers = new List<MarkerGridRow>();
        #endregion

        #region Constructors
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

            var viewsList = new GridEXValueListItemCollection();
            string[] viewItems = World.Default.GetBackgroundViews().ToArray();
            viewsList.AddRange(viewItems.Select(x => new GridEXValueListItem(x, x)).ToArray());
            MarkersGrid.RootTable.Columns["View"].EditValueList = viewsList;
            MarkersGrid.RootTable.Columns["View"].DefaultValue = viewItems.FirstOrDefault();

            SetMarkersGridDataSource();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Configure grid
        /// </summary>
        private void MarkersControl_Load(object sender, EventArgs e)
        {
            MarkersGrid.Configure(true, true);

            MarkersGrid.RootTable.Columns["Color"].ConfigureAsColor();
            MarkersGrid.RootTable.Columns["ExtraColor"].ConfigureAsColor(Color.Transparent);
        }

        private void MarkersGrid_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            var currentRow = MarkersGrid.CurrentRow;
            if (currentRow != null && currentRow.RowType == RowType.Record)
            {
                var data = currentRow.DataRow as MarkerGridRow;
                if (data != null)
                {
                    MarkerSettings settings = data.GetMarkerSettings();

                    object newValue = e.Value;
                    if (e.Value != null)
                    {
                        switch (e.Column.Key)
                        {
                            case "Enabled":
                                settings = MarkerSettings.ChangeEnabled(settings, (bool)newValue);
                                data.Enabled = (bool)newValue;
                                break;

                            case "Name":
                                // Done in Init/EndCustomEdit
                                break;

                            case "Color":
                                settings = MarkerSettings.ChangeColor(settings, (Color)newValue);
                                data.Color = (Color)newValue;
                                break;

                            case "ExtraColor":
                                settings = MarkerSettings.ChangeExtraColor(settings, (Color)newValue);
                                data.ExtraColor = (Color)newValue;
                                break;

                            case "View":
                                settings = MarkerSettings.ChangeView(settings, (string)newValue);
                                data.View = (string)newValue;
                                break;
                        }
                    }

                    UpdateMarker(data, settings);
                }
            }
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
                    e.Row.Cells["Name"].Text = marker.Name;
                }
            }
        }

        /// <summary>
        /// Edit: PlayerTribeSelector
        /// </summary>
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

        /// <summary>
        /// Edit: PlayerTribeSelector
        /// </summary>
        private void MarkersGrid_EndCustomEdit(object sender, EndCustomEditEventArgs e)
        {
            if (e.Column.Key == "Name")
            {
                var oldMarker = e.Row.DataRow as MarkerGridRow;
                if (oldMarker == null || e.Row.RowType == RowType.NewRecord)
                {
                    //oldMarker = new MarkerGridRow();
                    //oldMarker.Enabled = (bool)e.Row.GridEX.GetValue("Enabled");

                    object selected = _playerTribeSelector.Tribe ?? (object)_playerTribeSelector.Player;
                    e.Value = selected;
                    e.DataChanged = true;
                }
                else
                {
                    Player newPlayer = _playerTribeSelector.Player;
                    Tribe newTribe = _playerTribeSelector.Tribe;

                    if (newPlayer != oldMarker.Player || newTribe != oldMarker.Tribe)
                    {
                        DeleteMarker(oldMarker);

                        oldMarker.Player = newPlayer;
                        oldMarker.Tribe = newTribe;
                        UpdateMarker(oldMarker, oldMarker.GetMarkerSettings());

                        object selected = _playerTribeSelector.Tribe ?? (object)_playerTribeSelector.Player;
                        e.Value = selected;
                        e.DataChanged = true;
                    }
                }
            }
        }

        /// <summary>
        /// Delete marker
        /// </summary>
        private void MarkersGrid_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "Delete" && MarkersGrid.CurrentRow != null)
            {
                if (MarkersGrid.CurrentRow.RowType == RowType.Record)
                {
                    var row = MarkersGrid.CurrentRow.DataRow as MarkerGridRow;
                    DeleteMarker(row);
                    MarkersGrid.CurrentRow.Delete();
                }
            }
        }

        private void MarkersGrid_DeletingRecord(object sender, RowActionCancelEventArgs e)
        {
            if (e.Row.RowType == RowType.Record)
            {
                var row = e.Row.DataRow as MarkerGridRow; 
                DeleteMarker(row);
            }
        }

        private void MarkersGrid_AddingRecord(object sender, CancelEventArgs e)
        {
            var currentRow = MarkersGrid.CurrentRow;
            if (currentRow != null && currentRow.RowType == RowType.NewRecord)
            {
                var marker = currentRow.DataRow as MarkerGridRow;
                if (marker != null)
                {
                    if (marker.IsValid())
                    {
                        UpdateMarker(marker, marker.GetMarkerSettings());
                    }
                    else
                    {
                        e.Cancel = true;
                        MarkersGrid.CancelCurrentEdit();
                    }
                }
            }
        }

        private void MarkersGrid_GetNewRow(object sender, GetNewRowEventArgs e)
        {
            e.NewRow = new MarkerGridRow();
        }

        private void RefreshMarkersButton_Click(object sender, EventArgs e)
        {
            SetMarkersGridDataSource();
        }
        #endregion

        #region Private
        private void SetMarkersGridDataSource()
        {
            _markers = new List<MarkerGridRow>();
            foreach (var marker in World.Default.Map.MarkerManager.GetMarkers())
            {
                _markers.Add(marker);
            }

            MarkersGrid.DataSource = _markers;
        }

        private void DeleteMarker(MarkerGridRow row)
        {
            if (row != null)
            {
                if (row.Player != null)
                {
                    World.Default.Map.MarkerManager.RemoveMarker(World.Default.Map, row.Player);
                }
                else if (row.Tribe != null)
                {
                    World.Default.Map.MarkerManager.RemoveMarker(World.Default.Map, row.Tribe);
                }
            }
        }

        private void UpdateMarker(MarkerGridRow data, MarkerSettings settings)
        {
            if (data.Player != null)
            {
                World.Default.Map.MarkerManager.UpdateMarker(World.Default.Map, data.Player, settings);
            }
            else if (data.Tribe != null)
            {
                World.Default.Map.MarkerManager.UpdateMarker(World.Default.Map, data.Tribe, settings);
            }
        }
        #endregion
    }
}
