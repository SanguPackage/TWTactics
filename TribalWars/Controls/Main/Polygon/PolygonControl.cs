#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Controls.Display;
using TribalWars.Data;
using TribalWars.Data.Events;
using Janus.Windows.GridEX;
using TribalWars.Data.Maps.Manipulators.Managers;
using TribalWars.Data.Villages;
using TribalWars.Tools;
#endregion

namespace TribalWars.Controls.Main.Polygon
{
    /// <summary>
    /// Manages the villages inside the BBCodeArea polygons
    /// </summary>
    public partial class PolygonControl : UserControl
    {
        #region Constructors
        public PolygonControl()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            World.Default.Map.EventPublisher.PolygonActivated += EventPublisher_PolygonActivated;
            SetGridExPolygonTooltips();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// BBCodeArea polygon(s) have been ported to this control
        /// </summary>
        private void EventPublisher_PolygonActivated(object sender, PolygonEventArgs e)
        {
            GridExPolygon.DataSource = PolygonDataSet.CreateDataSet(e.Polygons);
            GridExPolygon.CheckAllRecords();
            GridExPolygon.MoveFirst();
        }

        /// <summary>
        /// Load all villages from all polygons
        /// </summary>
        private void LoadPolygonData_Click(object sender, EventArgs e)
        {
            GridExPolygon.RemoveFilters();

            var polygons = World.Default.Map.Manipulators.PolygonManipulator.GetAllPolygons().ToArray();
            if (polygons.Any())
            {
                World.Default.Map.EventPublisher.ActivatePolygon(this, polygons);
            }
            else
            {
                World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Polygon);
                MessageBox.Show(@"You have not yet defined any polygons.
I have activated polygon drawing for you, you can go back to the main map and create some now!

Click and hold left mouse button to start drawing!
Or... Right click on the map for more help.", "No polygons!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// The selected villages are exported to the clipboard
        /// </summary>
        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            if (GridExPolygon.RowCount == 0)
            {
                MessageBox.Show(string.Format("There is nothing in the grid yet. Try '{0}'?", LoadPolygonData.Text), "No data!");
                return;
            }

            var str = new StringBuilder();
            int villagesExported = 0;
            foreach (GridEXRow groupRow in GridExPolygon.GetRows())
            {
                if (groupRow.RowType == RowType.GroupHeader)
                {
                    str.AppendLine();
                    str.AppendLine();
                    str.AppendLine(groupRow.GroupValue.ToString());
                    foreach (GridEXRow row in groupRow.GetChildRecords())
                    {
                        if (row.CheckState == RowCheckState.Checked)
                        {
                            villagesExported++;

                            var villageRow = (PolygonDataSet.VILLAGERow)((DataRowView)row.DataRow).Row;
                            if (!string.IsNullOrWhiteSpace(villageRow.Village.TypeString))
                            {
                                str.AppendLine(villageRow.BBCODE + " (" + villageRow.Village.TypeString + ")");
                            }
                            else
                            {
                                str.AppendLine(villageRow.BBCODE);
                            }
                        }
                    }
                }
            }

            try
            {
                Clipboard.SetText(str.ToString().Trim());
                MessageBox.Show(string.Format("BBCodes for {0} villages have been placed on the clipboard!", villagesExported), "BBCodes on clipboard", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                
            }
        }
        #endregion

        #region GridExPolygon
        private void SetGridExPolygonTooltips()
        {
            GridExPolygon.RootTable.Columns["NAME"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Name;
            GridExPolygon.RootTable.Columns["LOCATION"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Location;
            GridExPolygon.RootTable.Columns["KINGDOM"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Kingdom;
            GridExPolygon.RootTable.Columns["POINTS"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Points;
            GridExPolygon.RootTable.Columns["POINTSDIFF"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.PointsDifference;
            GridExPolygon.RootTable.Columns["PLAYER"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.PlayerName;
            GridExPolygon.RootTable.Columns["TRIBE"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.TribeTag;
            GridExPolygon.RootTable.Columns["TYPE"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Type;
            GridExPolygon.RootTable.Columns["ISVISIBLE"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Visible;
        }

        private void GridExPolygon_FormattingRow(object sender, RowLoadEventArgs e)
        {
            // GroupHeaders
            UpdateGroupRecordText(e.Row);

            // Normal Rows
            if (e.Row.RowType == RowType.Record)
            {
                PolygonDataSet.VILLAGERow record = GetVillageRow(e.Row);

                // SetVillageVisibility()
                if (record.ISVISIBLE)
                {
                    e.Row.Cells["ISVISIBLE"].Image = VisibleImageList.Images[0];
                    e.Row.Cells["ISVISIBLE"].ToolTipText = "Currently visible on the map";
                }

                // SetVillageType()
                if (record.Village.Type != VillageType.None)
                {
                    e.Row.Cells["TYPE"].Image = record.Village.TypeImage;
                    if (record.Village.Type.HasFlag(VillageType.Comments))
                    {
                        e.Row.Cells["TYPE"].ToolTipText = record.Village.Comments;
                    }
                    else
                    {
                        e.Row.Cells["TYPE"].ToolTipText = record.Village.TypeString;
                    }
                }

                // Display You and your tribe in special color
                if (record.Village.HasPlayer)
                {
                    var you = World.Default.You.Player;
                    if (record.Village.Player == you)
                    {
                        var style = new GridEXFormatStyle();
                        style.ForeColor = Color.Red;
                        style.FontBold = TriState.True;
                        e.Row.Cells["PLAYER"].FormatStyle = style;
                    }
                    else if (you != null && record.Village.Player.Tribe == you.Tribe)
                    {
                        var style = new GridEXFormatStyle();
                        style.ForeColor = Color.Blue;
                        style.FontBold = TriState.True;
                        e.Row.Cells["PLAYER"].FormatStyle = style;
                    }
                }
            }
        }

        private void GridExPolygon_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
        {
            GridEXRow groupToUpdate = e.Row;
            if (e.Row.RowType == RowType.Record)
            {
                groupToUpdate = e.Row.Parent;
            }

            if (groupToUpdate.RowType == RowType.GroupHeader)
            {
                UpdateGroupRecordText(groupToUpdate);
                if (groupToUpdate.Parent != null)
                {
                    UpdateGroupRecordText(groupToUpdate.Parent);
                }
            }
        }

        /// <summary>
        /// Column chooser
        /// </summary>
        private void GridExPolygonShowFieldChooser_Click(object sender, EventArgs e)
        {
            GridExPolygon.ShowFieldChooser();
        }

        /// <summary>
        /// PinPoint the village
        /// </summary>
        private void GridExPolygon_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            if (e.Row.RowType == RowType.Record)
            {
                PolygonDataSet.VILLAGERow row = GetVillageRow(e.Row);
                World.Default.Map.EventPublisher.SelectVillages(null, row.Village, VillageTools.PinPoint);
            }
        }

        /// <summary>
        /// Select the village
        /// </summary>
        private void GridExPolygon_CurrentCellChanging(object sender, CurrentCellChangingEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == RowType.Record)
            {
                PolygonDataSet.VILLAGERow row = GetVillageRow(e.Row);
                World.Default.Map.EventPublisher.SelectVillages(null, row.Village, VillageTools.SelectVillage);
            }
        }

        /// <summary>
        /// Don't allow changing the default grouping
        /// </summary>
        private void GridExPolygon_GroupsChanging(object sender, GroupsChangingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Cast GridEx row to TypedDataSet row
        /// </summary>
        private PolygonDataSet.VILLAGERow GetVillageRow(GridEXRow row)
        {
            Debug.Assert(row.RowType == RowType.Record);
            return (PolygonDataSet.VILLAGERow)row.GetDataRow();
        }

        /// <summary>
        /// Update the group text with the amount of checked villages / total villages
        /// </summary>
        private void UpdateGroupRecordText(GridEXRow row)
        {
            if (row.RowType == RowType.GroupHeader)
            {
                // Set group totals
                int totalRecords = row.GetRecordCount();
                int totalChecked = row.GetChildRecords().Count(x => x.CheckState == RowCheckState.Checked);
                row.GroupCaption = string.Format("{0} ({1} / {2} villages)", row.GroupValue, totalChecked, totalRecords);
            }
        }
        #endregion
    }
}
