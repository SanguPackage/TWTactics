using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using TribalWars.Controls.XPTables;
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
        private VillageFields _fields;

        private GridEXColumn _visibleColumn;
        private GridEXColumn _villageImageColumn;
        private GridEXColumn _villageCoordColumn;
        private GridEXColumn _villageNameColumn;
        private GridEXColumn _villagePointsColumn;
        private GridEXColumn _villagePointsDifferenceColumn;
                             
        private GridEXColumn _villagePlayerColumn;
        private GridEXColumn _villagePlayerDifferenceColumn;
        private GridEXColumn _villagePlayerPoints;
        private GridEXColumn _villagePlayerPointsDifference;
        private GridEXColumn _villageVillagesColumn;
        private GridEXColumn _villageVillagesDifferenceColumn;
        private GridEXColumn _villageTribeColumn;
        private GridEXColumn _villageTribeRankColumn;
        #endregion

        #region Properties
        public VillageFields VisibleFields
        {
            get { return _fields; }
            set { _fields = value; }
        }
        #endregion

        #region Constructor
        public VillagesGridControl()
        {
            _fields = VillageFields.You;

            InitializeComponent();

            //AddColumns();
        }

        private void VillagesGridControl_Load(object sender, EventArgs e)
        {
            //GridExVillage.RootTable.Columns.Add();
        }
        #endregion
        
        //#region Add Columns
        //private void AddColumns()
        //{
        //    _visibleColumn = CreateImageColumn(string.Empty, 20, ColumnDisplay.VillageHeaderTooltips.Visible);
        //    _villageImageColumn = CreateImageColumn(string.Empty, 20, ColumnDisplay.VillageHeaderTooltips.Type);
        //    _villageCoordColumn = CreateTextColumn("XY", 50, ColumnDisplay.VillageHeaderTooltips.Location);
        //    _villageNameColumn = CreateTextColumn("Name", 114, ColumnDisplay.VillageHeaderTooltips.Name);
        //    _villagePointsColumn = CreateNumberColumn("Points", 55, ColumnDisplay.VillageHeaderTooltips.Points);
        //    _villagePointsDifferenceColumn = CreateNumberColumn("Diff.", 45, ColumnDisplay.VillageHeaderTooltips.PointsDifference);

        //    _villagePlayerColumn = CreateTextColumn("Player", 85, ColumnDisplay.VillageHeaderTooltips.PlayerName);
        //    _villagePlayerDifferenceColumn = CreateTextColumn("Old owner", 85, "The player the village has been nobled from.");
        //    _villagePlayerPoints = CreateNumberColumn("Points", 65, "The points of the player owning the village");
        //    _villagePlayerPointsDifference = CreateNumberColumn("Diff.", 55, "The difference in player points since previous data");
        //    _villageVillagesColumn = CreateNumberColumn("Villages", 60, "The amount of villages the player has");
        //    _villageVillagesDifferenceColumn = CreateTextColumn("Diff.", 45, "The villages the player gained and/or lost since previous data");
        //    _villageTribeColumn = CreateTextColumn("Tribe", 50, ColumnDisplay.VillageHeaderTooltips.TribeTag);
        //    _villageTribeRankColumn = CreateNumberColumn("Rank", 50, "The rank of the tribe of the player");

        //    _villageImageColumn.Visible = (_fields & VillageFields.Type) != 0;
        //    _villageNameColumn.Visible = (_fields & VillageFields.Name) != 0;
        //    _villagePlayerColumn.Visible = (_fields & VillageFields.Player) != 0;
        //    _villagePlayerDifferenceColumn.Visible = (_fields & VillageFields.PlayerDifference) != 0;
        //    _villagePlayerPoints.Visible = (_fields & VillageFields.PlayerPoints) != 0;
        //    _villagePlayerPointsDifference.Visible = (_fields & VillageFields.PlayerPointsDifference) != 0;
        //    _villagePointsColumn.Visible = (_fields & VillageFields.Points) != 0;
        //    _villagePointsDifferenceColumn.Visible = (_fields & VillageFields.PointsDifference) != 0;
        //    _villageTribeColumn.Visible = (_fields & VillageFields.Tribe) != 0;
        //    _villageTribeRankColumn.Visible = (_fields & VillageFields.TribeRank) != 0;
        //    _villageVillagesColumn.Visible = (_fields & VillageFields.PlayerVillages) != 0;
        //    _villageVillagesDifferenceColumn.Visible = (_fields & VillageFields.PlayerVillagesDifference) != 0;
        //}

        ///// <summary>
        ///// Creates a text column
        ///// </summary>
        //private static GridEXColumn CreateTextColumn(string header, int width, string toolTipText = null)
        //{
        //    var column = new GridEXColumn("", ColumnType.Text);
        //    if (!string.IsNullOrWhiteSpace(toolTipText))
        //    {
        //        column.HeaderToolTip = toolTipText;
        //    }
        //    return column;

        //    var col = new TextColumn(header, width) { Editable = false };
        //    if (!string.IsNullOrWhiteSpace(toolTipText))
        //    {
        //        col.ToolTipText = toolTipText;
        //    }
        //    return col;
        //}

        ///// <summary>
        ///// Creates a numeric column
        ///// </summary>
        //private static GridEXColumn CreateNumberColumn(string header, int width, string toolTipText = null)
        //{
        //    var col = new NumberColumn(header, width)
        //    {
        //        Editable = false,
        //        Alignment = ColumnAlignment.Right,
        //        Format = "#,0"
        //    };

        //    if (!string.IsNullOrWhiteSpace(toolTipText))
        //    {
        //        col.ToolTipText = toolTipText;
        //    }

        //    return col;
        //}

        ///// <summary>
        ///// Creates an Image column
        ///// </summary>
        //private static GridEXColumn CreateImageColumn(string header, int width, string toolTipText)
        //{
        //    var col = new ImageColumn(header, width) { DrawText = false, Editable = false, ToolTipText = toolTipText };
        //    col.Comparer = typeof(TextComparer);
        //    return col;
        //}

        //private static GridEXColumn CreateDateTimeColumn(string header, int width)
        //{
        //    var col = new DateTimeColumn(header, width)
        //    {
        //        CustomDateTimeFormat = "d-MM H:mm",
        //        DateTimeFormat = System.Windows.Forms.DateTimePickerFormat.Custom,
        //        Editable = false,
        //        ShowDropDownButton = false
        //    };
        //    return col;
        //}
        //#endregion

        
    }
}
