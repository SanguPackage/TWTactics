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
using TribalWars.Controls.GridExs;
using TribalWars.Controls.XPTables;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Villages.ContextMenu;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using XPTable.Models;
using XPTable.Sorting;

namespace TribalWars.Controls
{
    /// <summary>
    /// The Janus replacement for <see cref="XPTable "/> <see cref="TribalWars.Controls.XPTables.VillageTableRow"/>
    /// </summary>
    public partial class VillagesGridControl : UserControl
    {
        #region Fields
        private readonly Dictionary<VillageFields, GridEXColumn> _columns;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public VillagesGridControl()
        {
            _columns = new Dictionary<VillageFields, GridEXColumn>();
            InitializeComponent();
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
                VillageGridExData row = GetVillageRow(e.Row);
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
                var row = GridExVillage.CurrentRow;
                if (row != null && row.RowType == RowType.Record)
                {
                    VillageGridExData record = GetVillageRow(row);

                    var contextMenu = new VillageContextMenu(World.Default.Map, record.Village, () => GridExVillage.Refresh());
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

        }

        private void GridExVillage_LoadingRow(object sender, RowLoadEventArgs e)
        {

        }

        

        private VillageGridExData GetVillageRow(GridEXRow row)
        {
            Debug.Assert(row.RowType == RowType.Record);
            return (VillageGridExData) row.DataRow;
        }
        #endregion

    }
}
