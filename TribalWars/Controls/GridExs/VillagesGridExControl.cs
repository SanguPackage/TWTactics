using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using TribalWars.Controls.Common;
using TribalWars.Controls.XPTables;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Villages;
using TribalWars.Villages.ContextMenu;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.GridExs
{
    /// <summary>
    /// The Janus replacement for <see cref="XPTable "/> <see cref="TribalWars.Controls.XPTables.VillageTableRow"/>
    /// </summary>
    public partial class VillagesGridExControl : UserControl
    {
        #region Fields
        private readonly Dictionary<VillageFields, GridEXColumn> _columns;
        private readonly ImageCombobox _villageTypeBox;
        #endregion

        #region Properties
        /// <summary>
        /// Show/Hide the Player name column
        /// </summary>
        public bool ShowPlayer { get; set; }
        #endregion

        #region Constructor
        public VillagesGridExControl()
        {
            ShowPlayer = true;
            _columns = new Dictionary<VillageFields, GridEXColumn>();
            InitializeComponent();

            _villageTypeBox = new ImageCombobox();
            _villageTypeBox.ImageList = VillageTypeHelper.GetImageList();
        }

        private void VillagesGridControl_Load(object sender, EventArgs e)
        {
            GridExVillage.Configure(false, true);

            foreach (var column in GridExVillage.RootTable.Columns.OfType<GridEXColumn>())
            {
                VillageFields type = GetVillageColumnType(column);
                _columns.Add(type, column);

				//column.Name = ColumnDisplay
                column.HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.GetTooltip(type);
            }

            _columns[VillageFields.Points].ConfigureAsNumeric();
            _columns[VillageFields.Kingdom].ConfigureAsNumeric();

            _columns[VillageFields.Player].Visible = ShowPlayer;
            _columns[VillageFields.Church].Visible = World.Default.Settings.Church;

            World.Default.Map.EventPublisher.LocationChanged += EventPublisherOnLocationChanged;
        }

        private void EventPublisherOnLocationChanged(object sender, MapLocationEventArgs mapLocationEventArgs)
        {
            GridExVillage.Refresh();
        }

        private VillageFields GetVillageColumnType(GridEXColumn column)
        {
            VillageFields type;
            if (Enum.TryParse(column.Key, out type))
            {
                return type;
            }
            throw new Exception("Column should be in VillageFields enum!");
        }
        #endregion

        #region Event Handlers
        private void GridExVillage_CurrentCellChanging(object sender, CurrentCellChangingEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == RowType.Record)
            {
                VillageGridExRow row = GetVillageRow(e.Row);
                World.Default.Map.EventPublisher.SelectVillages(null, row.Village, VillageTools.PinPoint);
            }
            else
            {
                GridExVillage.ContextMenu = null;
            }
        }

        private void GridExVillage_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var rowCount = GridExVillage.SelectedItems.Count;
                if (rowCount == 1)
                {
                    var row = GridExVillage.CurrentRow;
                    if (row != null && row.RowType == RowType.Record)
                    {
                        VillageGridExRow record = GetVillageRow(row);

                        var contextMenu = new VillageContextMenu(World.Default.Map, record.Village, () => GridExVillage.Refresh());
                        contextMenu.Show(GridExVillage, e.Location);
                    }
                }
                else if (rowCount > 1)
                {
                    IEnumerable<Village> villages = GridExVillage.SelectedItems.GetRows<VillageGridExRow>().Select(x => x.Village);

                    var contextMenu = new VillagesContextMenu(World.Default.Map, villages.ToArray(), type => GridExVillage.Refresh());
                    contextMenu.Show(GridExVillage, e.Location);
                }
            }
        }

        private void GridExVillage_RowDoubleClick(object sender, RowActionEventArgs e)
        {
            if (e.Row.RowType == RowType.Record)
            {
                var row = GetVillageRow(e.Row);
                World.Default.Map.EventPublisher.SelectVillages(null, row.Village, VillageTools.PinPoint);
                World.Default.Map.SetCenter(row.Village.Location);
            }
        }

        private void GridExVillage_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.Record)
            {
                var record = GetVillageRow(e.Row);

                // SetVillageVisibility()
                if (record.Visible)
                {
                    e.Row.Cells["Visible"].Image = Properties.Resources.Visible;
                    e.Row.Cells["Visible"].ToolTipText = VillageGridExRes.VisibleCellTooltip;
                }

                // SetVillageType()
                if (record.Village.Type != VillageType.None)
                {
                    e.Row.Cells["Type"].Image = record.Village.Type.GetImage(true);
                    if (record.Village.HasComments)
                    {
                        e.Row.Cells["Type"].ToolTipText = record.Village.Comments;
                    }
                    else
                    {
                        e.Row.Cells["Type"].ToolTipText = record.Village.Type.GetDescription();
                    }
                }

                // Display You and your tribe in special color
                if (ShowPlayer)
                {
                    if (record.Village.HasPlayer)
                    {
                        var you = World.Default.You;
                        if (record.Village.Player == you)
                        {
                            var style = new GridEXFormatStyle();
                            style.ForeColor = Color.Red;
                            style.FontBold = TriState.True;
                            e.Row.Cells["Player"].FormatStyle = style;
                        }
                        else if (you != null && record.Village.Player.Tribe == you.Tribe)
                        {
                            var style = new GridEXFormatStyle();
                            style.ForeColor = Color.Blue;
                            style.FontBold = TriState.True;
                            e.Row.Cells["Player"].FormatStyle = style;
                        }
                    }
                }
            }
        }

        private void GridExVillage_LoadingRow(object sender, RowLoadEventArgs e)
        {

        }

        private void GridExVillage_KeyDown(object sender, KeyEventArgs e)
        {
            // Select all rows
            if (e.Control && e.KeyCode == Keys.A)
            {
                for (int i = 0; i < GridExVillage.RowCount; i++)
                {
                    GridExVillage.SelectedItems.Add(i);
                }
            }
        }

        private VillageGridExRow GetVillageRow(GridEXRow row)
        {
            Debug.Assert(row.RowType == RowType.Record);
            return (VillageGridExRow) row.DataRow;
        }
        #endregion

        public void Bind(IEnumerable<Village> villages)
        {
            IEnumerable<VillageGridExRow> villageRows = villages.Select(x => new VillageGridExRow(World.Default.Map, x));
            GridExVillage.DataSource = villageRows.ToList();
            GridExVillage.MoveFirst();
        }

        public IEnumerable<Village> GetSelectedVillages()
        {
            return GridExVillage.SelectedItems.GetRows<VillageGridExRow>().Select(x => x.Village);
        }

        private void GridExVillage_InitCustomEdit(object sender, InitCustomEditEventArgs e)
        {
            if (e.Column == _columns[VillageFields.Type] && e.Row.RowType == RowType.FilterRow)
            {
                e.EditControl = _villageTypeBox;
            }
        }

        private void GridExVillage_EndCustomEdit(object sender, EndCustomEditEventArgs e)
        {
            if (e.Column == _columns[VillageFields.Type])
            {
                int selected = _villageTypeBox.SelectedIndex;
                if (selected > 0)
                {
                    e.Value = (int)VillageTypeHelper.GetVillageType(selected);
                }
                else
                {
                    e.Value = null;
                }
                e.DataChanged = true;
            }
        }
    }
}
