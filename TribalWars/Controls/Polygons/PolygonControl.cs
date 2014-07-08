#region Using
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using TribalWars.Controls.TWContextMenu;
using Janus.Windows.GridEX;
using TribalWars.Controls.XPTables;
using TribalWars.Maps.Manipulators.Helpers;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Controls.Polygons
{
    /// <summary>
    /// Manages the villages inside the BBCodeArea polygons
    /// </summary>
    public partial class PolygonControl : UserControl
    {
        #region Fields
        private readonly UIColorButton _colorControl;
        #endregion

        #region Constructors
        public PolygonControl()
        {
            InitializeComponent();
            _colorControl = new UIColorButton();
        }

        public void Initialize()
        {
            World.Default.Map.EventPublisher.PolygonActivated += EventPublisher_PolygonActivated;
            World.Default.Map.EventPublisher.LocationChanged += EventPublisher_LocationChanged;
            SetGridExVillageTooltips();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// BBCodeArea polygon(s) have been ported to this control
        /// </summary>
        private void EventPublisher_PolygonActivated(object sender, PolygonEventArgs e)
        {
            Polygon[] polygons = e.Polygons.ToArray();
            if (polygons.Length == 1)
            {
                // Polygon management grid: jump to the selected polygon row
                foreach (GridEXRow row in GridExPolygon.GetRows())
                {
                    if (row.RowType == RowType.Record)
                    {
                        var polygon = (Polygon) row.DataRow;
                        if (polygon.Equals(polygons[0]))
                        {
                            GridExPolygon.MoveTo(row);
                            break;
                        }
                    }
                }
            }

            GridExVillage.DataSource = PolygonDataSet.CreateDataSet(e.Polygons);
            GridExVillage.MoveFirst();
        }

        /// <summary>
        /// Update visibility of villages after map move
        /// </summary>
        private void EventPublisher_LocationChanged(object sender, MapLocationEventArgs e)
        {
            var villageDs = (PolygonDataSet) GridExVillage.DataSource;
            foreach (var record in villageDs.VILLAGE.Rows.OfType<PolygonDataSet.VILLAGERow>())
            {
                record.ISVISIBLE = World.Default.Map.Display.IsVisible(record.Village);
            }
        }

        /// <summary>
        /// Load all villages from all polygons
        /// </summary>
        private void LoadPolygonData_Click(object sender, EventArgs e)
        {
            Polygon[] polygons = World.Default.Map.Manipulators.PolygonManipulator.GetAllPolygons().ToArray();
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
                GridExPolygon.MoveFirst();
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
                            if (!string.IsNullOrWhiteSpace(villageRow.Village.Type.GetDescription()))
                            {
                                str.AppendLine(villageRow.BBCODE + " (" + villageRow.Village.Type.GetDescription() + ")");
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
                World.Default.Map.EventPublisher.SelectVillages(null, row.Village, VillageTools.PinPoint);
            }
            else
            {
                GridExVillage.ContextMenu = null;
            }
        }

        /// <summary>
        /// Provide right click context menu
        /// </summary>
        private void GridExVillage_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var row = GridExVillage.CurrentRow;
                if (row != null && row.RowType == RowType.Record)
                {
                    PolygonDataSet.VILLAGERow record = GetVillageRow(row);

                    var contextMenu = new VillageContextMenu(World.Default.Map, record.Village, () => GridExVillage.Refresh());
                    contextMenu.Show(GridExVillage, e.Location);
                }
            }
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
                 World.Default.Map.SetCenter(row.Village.Location);
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
                    e.Row.Cells["TYPE"].Image = record.Village.Type.GetImage();
                    if (record.Village.Type.HasFlag(VillageType.Comments))
                    {
                        e.Row.Cells["TYPE"].ToolTipText = record.Village.Comments;
                    }
                    else
                    {
                        e.Row.Cells["TYPE"].ToolTipText = record.Village.Type.GetDescription();
                    }
                }

                // Display You and your tribe in special color
                if (record.Village.HasPlayer)
                {
                    var you = World.Default.You;
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
        /// Check visible polygons by default
        /// </summary>
        private void GridExVillage_LoadingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.GroupHeader && e.Row.Parent != null)
            {
                GridEXRow row = e.Row.GetChildRecords().FirstOrDefault();
                if (row != null)
                {
                    var polygon = GetVillageRow(row).Polygon;
                    if (polygon.Visible)
                    {
                        e.Row.CheckState = RowCheckState.Checked;
                    }
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
        private void GridExPolygon_InitCustomEdit(object sender, InitCustomEditEventArgs e)
        {
            if (e.Column.Key == "LineColor")
            {
                _colorControl.SelectedColor = (Color)e.Value;
                e.EditControl = _colorControl;
            }
        }

        private void GridExPolygon_EndCustomEdit(object sender, EndCustomEditEventArgs e)
        {
            if (e.Column.Key == "LineColor")
            {
                e.Value = _colorControl.SelectedColor;
                e.DataChanged = true;
            }
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

            CurrentModusGroupbox.Text = string.Format("Current modus: {0}", isVillageModus ? ModusVillage.Text : ModusPolygon.Text);
            GridExVillage.Visible = isVillageModus;
            GridExVillageShowFieldChooser.Visible = isVillageModus;
            GridExPolygon.Visible = !isVillageModus;

            ButtonGenerate.Enabled = isVillageModus;

            LoadPolygonData.PerformClick();
        }
        #endregion
    }
}