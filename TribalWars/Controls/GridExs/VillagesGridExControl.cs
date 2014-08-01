using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        #endregion

        #region Constructor
        public VillagesGridExControl()
        {
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

                column.HeaderToolTip = ColumnDisplay.VillageHeaderTooltips.GetTooltip(type);
            }

            _columns[VillageFields.Points].ConfigureAsNumeric();
            _columns[VillageFields.Kingdom].ConfigureAsNumeric();

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

                    var contextMenu = new VillagesContextMenu(World.Default.Map, villages, type => GridExVillage.Refresh());
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
                    e.Row.Cells["Visible"].ToolTipText = "Currently visible on the map";
                }

                // SetVillageType()
                if (record.Village.Type != VillageType.None)
                {
                    e.Row.Cells["Type"].Image = record.Village.Type.GetImage(true);
                    if (record.Village.Type.HasFlag(VillageType.Comments))
                    {
                        e.Row.Cells["Type"].ToolTipText = record.Village.Comments;
                    }
                    else
                    {
                        e.Row.Cells["Type"].ToolTipText = record.Village.Type.GetDescription();
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

        public void Bind(IList<VillageGridExRow> villages)
        {
            GridExVillage.DataSource = villages;
            GridExVillage.MoveFirst();
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
                if (selected != 0)
                {
                    e.Value = selected;
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
