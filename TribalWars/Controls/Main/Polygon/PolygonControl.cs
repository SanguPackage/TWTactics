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
            SetGridExVillageTooltips();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// BBCodeArea polygon(s) have been ported to this control
        /// </summary>
        private void EventPublisher_PolygonActivated(object sender, PolygonEventArgs e)
        {
            GridExVillage.DataSource = PolygonDataSet.CreateDataSet(e.Polygons);
            GridExVillage.MoveFirst();
        }

        /// <summary>
        /// Load all villages from all polygons
        /// </summary>
        private void LoadPolygonData_Click(object sender, EventArgs e)
        {
            Data.Maps.Manipulators.Helpers.Polygon[] polygons = World.Default.Map.Manipulators.PolygonManipulator.GetAllPolygons().ToArray();
            if (!polygons.Any())
            {
                World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Polygon);
                MessageBox.Show(@"You have not yet defined any polygons.
I have activated polygon drawing for you, you can go back to the main map and create some now!

Click and hold left mouse button to start drawing!
Or... Right click on the map for more help.", "No polygons!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (ModusPolygon.Enabled)
            {
                // BBCode export: load villages
                GridExVillage.RemoveFilters();
                World.Default.Map.EventPublisher.ActivatePolygon(this, polygons);
            }
            else
            {
                // Polygon management
                IEnumerable<string> groups = polygons.Select(x => x.Group).Distinct().OrderBy(x => x);
                var valueList = new GridEXValueListItemCollection();
                foreach (string group in groups)
                {
                    valueList.Add(group, group);
                }

                GridExPolygon.RootTable.Columns["GROUP"].EditValueList = valueList;

                GridExPolygon.DataSource = polygons;
            }
        }
        #endregion

        #region GridExVillage
        /// <summary>
        /// The selected villages are exported to the clipboard
        /// </summary>
        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            if (GridExVillage.RowCount == 0)
            {
                MessageBox.Show(string.Format("There is nothing in the grid yet. Try '{0}'?", LoadPolygonData.Text), "No data!");
                return;
            }

            var str = new StringBuilder();
            int villagesExported = 0;
            foreach (GridEXRow groupRow in GridExVillage.GetRows())
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

        /// <summary>
        /// Select the village
        /// </summary>
        private void GridExVillage_CurrentCellChanging(object sender, CurrentCellChangingEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == RowType.Record)
            {
                PolygonDataSet.VILLAGERow row = GetVillageRow(e.Row);
                World.Default.Map.EventPublisher.SelectVillages(null, row.Village, VillageTools.SelectVillage);
            }
        }

        /// <summary>
        /// Special formatting, image display, tooltips
        /// </summary>
        private void GridExVillage_FormattingRow(object sender, RowLoadEventArgs e)
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

        /// <summary>
        /// Update group header totals
        /// </summary>
        private void GridExVillage_RowCheckStateChanged(object sender, RowCheckStateChangeEventArgs e)
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
        /// Don't allow changing the default grouping
        /// </summary>
        private void GridExVillage_GroupsChanging(object sender, GroupsChangingEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// PinPoint the village
        /// </summary>
        private void GridExVillage_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            if (e.Row.RowType == RowType.Record)
            {
                PolygonDataSet.VILLAGERow row = GetVillageRow(e.Row);
                World.Default.Map.EventPublisher.SelectVillages(null, row.Village, VillageTools.PinPoint);
            }
        }

        /// <summary>
        /// Check visible polygons by default
        /// </summary>
        private void GridExVillage_LoadingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.GroupHeader && e.Row.Parent != null)
            {
                var polygon = GetVillageRow(e.Row.GetChildRecords().First()).Polygon;
                if (polygon.Visible)
                {
                    e.Row.CheckState = RowCheckState.Checked;
                }
            }
        }

        /// <summary>
        /// Column chooser
        /// </summary>
        private void GridExVillageShowFieldChooser_Click(object sender, EventArgs e)
        {
            GridExVillage.ShowFieldChooser();
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

        private void SetGridExVillageTooltips()
        {
            GridExVillage.RootTable.Columns["NAME"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Name;
            GridExVillage.RootTable.Columns["LOCATION"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Location;
            GridExVillage.RootTable.Columns["KINGDOM"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Kingdom;
            GridExVillage.RootTable.Columns["POINTS"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Points;
            GridExVillage.RootTable.Columns["POINTSDIFF"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.PointsDifference;
            GridExVillage.RootTable.Columns["PLAYER"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.PlayerName;
            GridExVillage.RootTable.Columns["TRIBE"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.TribeTag;
            GridExVillage.RootTable.Columns["TYPE"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Type;
            GridExVillage.RootTable.Columns["ISVISIBLE"].HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.Visible;
        }
        #endregion

        #region GridExPolygon
        private void GridExPolygon_CellEdited(object sender, ColumnActionEventArgs e)
        {
            var polygon = GridExPolygon.CurrentRow;

            
            
        }

        private void GridExPolygon_FormattingRow(object sender, RowLoadEventArgs e)
        {
            //if (e.Row.RowType == RowType.Record)
            //{
                
            //}
        }

        private void GridExPolygon_CellUpdated(object sender, ColumnActionEventArgs e)
        {

        }

        private void GridExPolygon_CellValueChanged(object sender, ColumnActionEventArgs e)
        {

        }

        private void GridExPolygon_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {

        }

        private void GridExPolygon_CurrentCellChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Modus Switch
        private void ModusVillage_Click(object sender, EventArgs e)
        {
            SetModus(true);
        }

        private void ModusPolygon_Click(object sender, EventArgs e)
        {
            SetModus(false);
        }

        /// <summary>
        /// Change control enablement
        /// </summary>
        /// <param name="isVillageModus">VillageModus=Export bbcodes OR PolygonModus=Manage polygons</param>
        private void SetModus(bool isVillageModus)
        {
            ModusVillage.Enabled = !isVillageModus;
            ModusPolygon.Enabled = isVillageModus;

            GridExVillage.Visible = isVillageModus;
            GridExVillageShowFieldChooser.Visible = isVillageModus;
            GridExPolygon.Visible = !isVillageModus;
            ButtonGenerate.Enabled = isVillageModus;

            LoadPolygonData.PerformClick();
        }

        #endregion
    }
}

//ButtonEnabled property in GridEXCell allows you to enable/disable buttons in individual cells.
//- ButtonStyle property in GridEXCell allows you to have different buttons styles in cells from the same column.
//- CellDisplayType property in GridEXCell allows you to have different display types for cells in the same column. With this property, you now can have text, image or checkboxes cells under the same column.
//- EditType property in GridEXCell allows you to have different edit types for cells in the same column. With this property you now can have cells that can be edited as text, combo or as a checkbox under the same column.
//- Enabled property in GridEXCell allows you to enable/disable individual cells in the control.
//- ValueList property in GridEXCell allows you to define a valuelist for value replacement and or as the source for the dropdown in a combo editor on each cell
