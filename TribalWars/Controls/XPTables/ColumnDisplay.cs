using System;
using System.Diagnostics;
using TribalWars.Villages;
using TribalWars.Worlds;
using XPTable.Models;
using System.Drawing;
using TribalWars.Controls.GridExs;
using XPTable.Sorting;

namespace TribalWars.Controls.XPTables
{
    /// <summary>
    /// Creates XPTable ColumnModels
    /// </summary>
    public static class ColumnDisplay
    {
        #region Fields
        /// <summary>
        /// Gets the style for displaying your changes in owners, points, ...
        /// </summary>
        private static readonly CellStyle DifferenceStyle;

        private static bool _youSet;
        private static Player _you;
        private static Tribe _yourTribe;

        private static readonly Font BoldFont;
        #endregion

        #region Properties
        /// <summary>
        /// Gets you
        /// </summary>
        private static Player You
        {
            get
            {
                if (!_youSet && World.Default.HasLoaded)
                {
                    _you = World.Default.You;
                    if (_you != null && _you.HasTribe)
                        _yourTribe = _you.Tribe;
                    _youSet = true;
                }
                return _you;
            }
        }

        /// <summary>
        /// Gets your tribe
        /// </summary>
        private static Tribe YourTribe
        {
            get
            {
                if (!_youSet && World.Default.HasLoaded)
                {
                    _you = World.Default.You;
                    if (_you != null && _you.HasTribe)
                        _yourTribe = _you.Tribe;
                    _youSet = true;
                }
                return _yourTribe;
            }
        }

        /// <summary>
        /// Gets the style for displaying you
        /// </summary>
        private static CellStyle YouStyle { get; set; }

        /// <summary>
        /// Gets the style for displaying your tribe
        /// </summary>
        private static CellStyle TribeStyle { get; set; }
        #endregion

        #region Static Constructor
        static ColumnDisplay()
        {
            BoldFont = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Bold);

            YouStyle = new CellStyle();
            YouStyle.ForeColor = Color.Red;
            YouStyle.Font = BoldFont;
            YouStyle.BackColor = Color.Transparent;

            TribeStyle = new CellStyle();
            TribeStyle.ForeColor = Color.Blue;
            TribeStyle.BackColor = Color.Empty;
            TribeStyle.Font = BoldFont;

            DifferenceStyle = new CellStyle();
            DifferenceStyle.ForeColor = Color.LightGray;
            DifferenceStyle.BackColor = Color.Empty;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Tooltips are shared between XPTable and Janus.GridEX
        /// </summary>
        public static class VillageHeaderTooltips
        {
            public static string GetTooltip(VillageFields type)
            {
                switch (type)
                {
                    case VillageFields.Coordinates:
                        return VillageGridExRes.LocationTooltip;

                   case VillageFields.Kingdom:
						return VillageGridExRes.KingdomTooltip;

                   case VillageFields.Name:
						return VillageGridExRes.NameTooltip;

                    case VillageFields.Points:
						return VillageGridExRes.PointsTooltip;

                    case VillageFields.Visible:
						return VillageGridExRes.VisibleTooltip;

                    case VillageFields.Type:
						return VillageGridExRes.TypeTooltip;

                    case VillageFields.Player:
						return VillageGridExRes.PlayerNameTooltip;

                    case VillageFields.Church:
                        return VillageGridExRes.ChurchTooltip;
                }

                throw new Exception("Add new header text here... (go to ColumnDisplay class)");
            }
        }

        /// <summary>
        /// Creates a column model for a village
        /// </summary>
        /// <param name="fields">The visible columns</param>
        public static ColumnModel CreateColumnModel(VillageFields fields)
        {
			ImageColumn visibleColumn = CreateImageColumn(string.Empty, 20, VillageGridExRes.VisibleTooltip);
			ImageColumn villageImageColumn = CreateImageColumn(string.Empty, 20, VillageGridExRes.TypeTooltip);
			TextColumn villageCoordColumn = CreateTextColumn(VillageGridExRes.Location, 50, VillageGridExRes.LocationTooltip);
			TextColumn villageNameColumn = CreateTextColumn(VillageGridExRes.Name, 114, VillageGridExRes.NameTooltip);
			NumberColumn villagePointsColumn = CreateNumberColumn(VillageGridExRes.Points, 55, VillageGridExRes.PointsTooltip);
			NumberColumn villagePointsDifferenceColumn = CreateNumberColumn(VillageGridExRes.Difference, 45, VillageGridExRes.PointsDifferenceTooltip);

			TextColumn villagePlayerColumn = CreateTextColumn(VillageGridExRes.Player, 85, VillageGridExRes.PlayerNameTooltip);
            TextColumn villagePlayerDifferenceColumn = CreateTextColumn(VillageGridExRes.PlayerOld, 85, VillageGridExRes.PlayerDifferenceTooltip);
            NumberColumn villagePlayerPoints = CreateNumberColumn(VillageGridExRes.Points, 65, VillageGridExRes.PlayerPointsTooltip);
			NumberColumn villagePlayerPointsDifference = CreateNumberColumn(VillageGridExRes.Difference, 55, VillageGridExRes.PlayerPointsDiffTooltip);
			NumberColumn villageVillagesColumn = CreateNumberColumn(VillageGridExRes.Villages, 60, VillageGridExRes.VillagesTooltip);
			TextColumn villageVillagesDifferenceColumn = CreateTextColumn(VillageGridExRes.Difference, 45, VillageGridExRes.VillagesDiffTooltip);
			TextColumn villageTribeColumn = CreateTextColumn(VillageGridExRes.Tribe, 50, VillageGridExRes.TribeTagTooltip);
            NumberColumn villageTribeRankColumn = CreateNumberColumn(VillageGridExRes.Rank, 50, VillageGridExRes.TribeRankTooltip);

            villageImageColumn.Visible = (fields & VillageFields.Type) != 0;
            villageNameColumn.Visible = (fields & VillageFields.Name) != 0;
            villagePlayerColumn.Visible = (fields & VillageFields.Player) != 0;
            villagePlayerDifferenceColumn.Visible = (fields & VillageFields.PlayerDifference) != 0;
            villagePlayerPoints.Visible = (fields & VillageFields.PlayerPoints) != 0;
            villagePlayerPointsDifference.Visible = (fields & VillageFields.PlayerPointsDifference) != 0;
            villagePointsColumn.Visible = (fields & VillageFields.Points) != 0;
            villagePointsDifferenceColumn.Visible = (fields & VillageFields.PointsDifference) != 0;
            villageTribeColumn.Visible = (fields & VillageFields.Tribe) != 0;
            villageTribeRankColumn.Visible = (fields & VillageFields.TribeRank) != 0;
            villageVillagesColumn.Visible = (fields & VillageFields.PlayerVillages) != 0;
            villageVillagesDifferenceColumn.Visible = (fields & VillageFields.PlayerVillagesDifference) != 0;

            return new ColumnModel(new Column[] {
                visibleColumn,
                villageImageColumn,
                villageCoordColumn,
                villageNameColumn,
                villagePointsColumn,
                villagePointsDifferenceColumn,
                villagePlayerDifferenceColumn,
                villagePlayerColumn,
                villagePlayerPoints,
                villagePlayerPointsDifference,
                villageVillagesColumn,
                villageVillagesDifferenceColumn,
                villageTribeColumn,
                villageTribeRankColumn});
        }

        /// <summary>
        /// Creates a column model for a player
        /// </summary>
        /// <param name="fields">The visible columns</param>
        public static ColumnModel CreateColumnModel(PlayerFields fields)
        {
            ImageColumn visibleColumn = CreateImageColumn(string.Empty, 20, VillageGridExRes.Player_VisibleTooltip);
			TextColumn playerNameColumn = CreateTextColumn(VillageGridExRes.Name, 85, VillageGridExRes.Player_NameTooltip);
			TextColumn playerTribeColumn = CreateTextColumn(VillageGridExRes.Tribe, 60, VillageGridExRes.Player_TribeTooltip);
			NumberColumn playerPointsColumn = CreateNumberColumn(VillageGridExRes.Points, 72, VillageGridExRes.Player_PointsTooltip);
			NumberColumn playerVillagesColumn = CreateNumberColumn(VillageGridExRes.Villages, 60, VillageGridExRes.Player_VillagesTooltip);
			TextColumn playerVillagesDifferenceColumn = CreateTextColumn(VillageGridExRes.Difference, 60, VillageGridExRes.Player_VillagesDiffTooltip);
			TextColumn playerTribeDifferenceColumn = CreateTextColumn(VillageGridExRes.TribeOld, 65, VillageGridExRes.Player_TribeOldTooltip);
			NumberColumn playerPointsDifferenceColumn = CreateNumberColumn(VillageGridExRes.Difference, 55, VillageGridExRes.Player_PointsDiffTooltip);

            playerNameColumn.Visible = (fields & PlayerFields.Name) != 0;
            playerPointsColumn.Visible = (fields & PlayerFields.Points) != 0;
            playerPointsDifferenceColumn.Visible = (fields & PlayerFields.PointsDifference) != 0;
            playerTribeColumn.Visible = (fields & PlayerFields.Tribe) != 0;
            playerTribeDifferenceColumn.Visible = (fields & PlayerFields.TribeDifference) != 0;
            playerVillagesColumn.Visible = (fields & PlayerFields.Villages) != 0;
            playerVillagesDifferenceColumn.Visible = (fields & PlayerFields.VillagesDifference) != 0;

            return new ColumnModel(new Column[] {
                visibleColumn,
                playerNameColumn,
                playerTribeDifferenceColumn,
                playerTribeColumn,
                playerPointsColumn,
                playerPointsDifferenceColumn,
                playerVillagesColumn,
                playerVillagesDifferenceColumn});
        }

        /// <summary>
        /// Creates a column model for a tribe
        /// </summary>
        /// <param name="fields">The visible columns</param>
        public static ColumnModel CreateColumnModel(TribeFields fields)
        {
            ImageColumn visibleColumn = CreateImageColumn(string.Empty, 20, VillageGridExRes.Tribe_VisibleTooltip);
			NumberColumn tribeRankColumn = CreateNumberColumn(VillageGridExRes.Rank, 40, VillageGridExRes.Tribe_RankTooltip);
			TextColumn tribeTagColumn = CreateTextColumn(VillageGridExRes.TribeTag, 50, VillageGridExRes.Tribe_TagTooltip);
			NumberColumn tribePlayersColumn = CreateNumberColumn(VillageGridExRes.Players, 55, VillageGridExRes.Tribe_PlayersTooltip);
			TextColumn tribePlayersDifferenceColumn = CreateTextColumn(VillageGridExRes.Difference, 50, VillageGridExRes.Tribe_PlayersDiffTooltip);
			NumberColumn tribePointsColumn = CreateNumberColumn(VillageGridExRes.Points, 75, VillageGridExRes.Tribe_PointsTooltip);
			NumberColumn tribePointsDifferenceColumn = CreateNumberColumn(VillageGridExRes.Difference, 70, VillageGridExRes.Tribe_PointsDiffTooltip);
			NumberColumn tribeVillagesColumn = CreateNumberColumn(VillageGridExRes.Villages, 55, VillageGridExRes.Tribe_VillagesTooltip);
			NumberColumn tribeVillagesDifferenceColumn = CreateNumberColumn(VillageGridExRes.Difference, 55, VillageGridExRes.Tribe_VillagesDiffTooltip);
			TextColumn tribeNameColumn = CreateTextColumn(VillageGridExRes.Name, 130, VillageGridExRes.Tribe_NameTooltip);

            tribeRankColumn.Visible = (fields & TribeFields.Rank) != 0;
            tribeTagColumn.Visible = (fields & TribeFields.Tag) != 0;
            tribeNameColumn.Visible = (fields & TribeFields.Name) != 0;
            tribePlayersColumn.Visible = (fields & TribeFields.Players) != 0;
            tribePlayersDifferenceColumn.Visible = (fields & TribeFields.PlayersDifference) != 0;
            tribePointsColumn.Visible = (fields & TribeFields.Points) != 0;
            tribePointsDifferenceColumn.Visible = (fields & TribeFields.PointsDifference) != 0;
            tribeVillagesColumn.Visible = (fields & TribeFields.Villages) != 0;
            tribeVillagesDifferenceColumn.Visible = (fields & TribeFields.VillagesDifference) != 0;

            return new ColumnModel(new Column[] {
                visibleColumn,
                tribeRankColumn,
                tribeTagColumn,
                tribePlayersColumn,
                tribePlayersDifferenceColumn,
                tribePointsColumn,
                tribePointsDifferenceColumn,
                tribeVillagesColumn,
                tribeVillagesDifferenceColumn,
                tribeNameColumn});
        }

        /// <summary>
        /// Creates a column model for a report
        /// </summary>
        /// <param name="fields">The visible columns</param>
        public static ColumnModel CreateColumnModel(ReportFields fields)
        {
            ImageColumn reportTypeImageColumn = CreateImageColumn(string.Empty, 20, "");
            ImageColumn reportStatusImageColumn = CreateImageColumn(string.Empty, 20, "");
            TextColumn reportVillageColumn = CreateTextColumn("Village", 50);
            TextColumn reportPlayerColumn = CreateTextColumn("Player", 100);
            DateTimeColumn reportDateColumn = CreateDateTimeColumn("Date", 70);
            ImageColumn reportFlagImageColumn = CreateImageColumn(string.Empty, 20, "");

            reportTypeImageColumn.Visible = (fields & ReportFields.Type) != 0;
            reportStatusImageColumn.Visible = (fields & ReportFields.Status) != 0;
            reportVillageColumn.Visible = (fields & ReportFields.Village) != 0;
            reportPlayerColumn.Visible = (fields & ReportFields.Player) != 0;
            reportDateColumn.Visible = (fields & ReportFields.Date) != 0;
            reportFlagImageColumn.Visible = (fields & ReportFields.Flag) != 0;

            return new ColumnModel(new Column[] {
                reportTypeImageColumn,
                reportStatusImageColumn,
                reportVillageColumn,
                reportPlayerColumn,
                reportDateColumn,
                reportFlagImageColumn});
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates a text column
        /// </summary>
        private static TextColumn CreateTextColumn(string header, int width, string toolTipText = null)
        {
            var col = new TextColumn(header, width) {Editable = false};
            if (!string.IsNullOrWhiteSpace(toolTipText))
            {
                col.ToolTipText = toolTipText;
            }
            return col;
        }

        /// <summary>
        /// Creates a numeric column
        /// </summary>
        private static NumberColumn CreateNumberColumn(string header, int width, string toolTipText = null)
        {
            var col = new NumberColumn(header, width)
                {
                    Editable = false,
                    Alignment = ColumnAlignment.Right,
                    Format = "#,0"
                };

            if (!string.IsNullOrWhiteSpace(toolTipText))
            {
                col.ToolTipText = toolTipText;
            }

            return col;
        }

        /// <summary>
        /// Creates an Image column
        /// </summary>
        private static ImageColumn CreateImageColumn(string header, int width, string toolTipText)
        {
            var col = new ImageColumn(header, width) {DrawText = false, Editable = false, ToolTipText = toolTipText};
            col.Comparer = typeof(TextComparer);
            return col;
        }

        private static DateTimeColumn CreateDateTimeColumn(string header, int width)
        {
            var col = new DateTimeColumn(header, width)
                {
                    CustomDateTimeFormat = "d-MM H:mm",
                    DateTimeFormat = System.Windows.Forms.DateTimePickerFormat.Custom,
                    Editable = false,
                    ShowDropDownButton = false
                };
            return col;
        }
        #endregion

        #region Cell Creators
        /// <summary>
        /// Create a cell for a player
        /// </summary>
        public static Cell CreatePlayerCell(Player owner)
        {
            return CreatePlayerCell(owner, false);
        }

        /// <summary>
        /// Create a cell for a player
        /// </summary>
        /// <param name="player">The player to display</param>
        /// <param name="diffColumn">If the player is not important, the cell is grayed out</param>
        public static Cell CreatePlayerCell(Player player, bool diffColumn)
        {
            if (player == null) return new Cell();
            var cell = new Cell(player.Name);
            cell.ToolTipText = player.Tooltip;
            if (player.Equals(You))
            {
                cell.CellStyle = YouStyle;
            }
            else if (YourTribe != null && YourTribe.Players.Contains(player))
            {
                cell.CellStyle = TribeStyle;
            }
            else if (diffColumn)
            {
                cell.CellStyle = DifferenceStyle;
            }
            return cell;
        }

        /// <summary>
        /// Create a grayed out cell
        /// </summary>
        public static Cell CreateDifferenceCell(int difference)
        {
            if (difference == 0)
                return new Cell();

            var cell = new Cell(difference);
            cell.CellStyle = DifferenceStyle;
            return cell;
        }

        /// <summary>
        /// Create a grayed out cell
        /// </summary>
        public static Cell CreateDifferenceCell(string difference)
        {
            var cell = new Cell(difference);
            cell.CellStyle = DifferenceStyle;
            return cell;
        }
        #endregion
    }
}
