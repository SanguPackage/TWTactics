using TribalWars.Data;
using XPTable.Models;
using TribalWars.Data.Players;
using System.Drawing;
using TribalWars.Data.Tribes;
using XPTable.Sorting;

namespace TribalWars.Controls.Display
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
                    _you = World.Default.You.Player;
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
                    _you = World.Default.You.Player;
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
            public const string Visible = "Show image if village is currently visible on the main map";
            public const string Type = "Show image for village type (Offense, Defense, Scout, Noble or Farm - Set in 'Quick Details')";
            public const string Location = "Coordinates of the village";
            public const string Name = "Name of the village";
            public const string Points = "Points of the village";
            public const string PointsDifference = "Difference in village points since previous data";
            public const string Kingdom = "The kingdom the village is located in";

            public const string PlayerName = "The owner of the village";
            public const string TribeTag = "The tribe of the player";
        }

        /// <summary>
        /// Creates a column model for a village
        /// </summary>
        /// <param name="fields">The visible columns</param>
        public static ColumnModel CreateColumnModel(VillageFields fields)
        {
            ImageColumn visibleColumn = CreateImageColumn(string.Empty, 20, VillageHeaderTooltips.Visible);
            ImageColumn villageImageColumn = CreateImageColumn(string.Empty, 20, VillageHeaderTooltips.Type);
            TextColumn villageCoordColumn = CreateTextColumn("XY", 50, VillageHeaderTooltips.Location);
            TextColumn villageNameColumn = CreateTextColumn("Name", 114, VillageHeaderTooltips.Name);
            NumberColumn villagePointsColumn = CreateNumberColumn("Points", 55, VillageHeaderTooltips.Points);
            NumberColumn villagePointsDifferenceColumn = CreateNumberColumn("Diff.", 45, VillageHeaderTooltips.PointsDifference);

            TextColumn villagePlayerColumn = CreateTextColumn("Player", 85, VillageHeaderTooltips.PlayerName);
            TextColumn villagePlayerDifferenceColumn = CreateTextColumn("Old owner", 85, "The player the village has been nobled from.");
            NumberColumn villagePlayerPoints = CreateNumberColumn("Points", 65, "The points of the player owning the village");
            NumberColumn villagePlayerPointsDifference = CreateNumberColumn("Diff.", 55, "The difference in player points since previous data");
            NumberColumn villageVillagesColumn = CreateNumberColumn("Villages", 60, "The amount of villages the player has");
            TextColumn villageVillagesDifferenceColumn = CreateTextColumn("Diff.", 45, "The villages the player gained and/or lost since previous data");
            TextColumn villageTribeColumn = CreateTextColumn("Tribe", 50, VillageHeaderTooltips.TribeTag);
            NumberColumn villageTribeRankColumn = CreateNumberColumn("Rank", 50, "The rank of the tribe of the player");

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
            ImageColumn visibleColumn = CreateImageColumn(string.Empty, 20, "Show image if at least one village of the player is currently visible on the main map.");
            TextColumn playerNameColumn = CreateTextColumn("Name", 85, "The name of the player");
            TextColumn playerTribeColumn = CreateTextColumn("Tribe", 60, "The tribe the player belongs to");
            NumberColumn playerPointsColumn = CreateNumberColumn("Points", 75, "Points of the player");
            NumberColumn playerVillagesColumn = CreateNumberColumn("Villages", 60, "Villages of the player");
            TextColumn playerVillagesDifferenceColumn = CreateTextColumn("Diff.", 60, "Villages gained and/or lost since previous data");
            TextColumn playerTribeDifferenceColumn = CreateTextColumn("Old tribe", 65, "The tribe the player switched from since previous data");
            NumberColumn playerPointsDifferenceColumn = CreateNumberColumn("Diff.", 55, "The difference in player points since previous data");

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
            ImageColumn visibleColumn = CreateImageColumn(string.Empty, 20, "Show image if at least one village of the tribe is currently visible on the main map.");
            NumberColumn tribeRankColumn = CreateNumberColumn("Rank", 50, "World rank of the tribe");
            TextColumn tribeTagColumn = CreateTextColumn("Tag", 55, "Tribe tag");
            TextColumn tribeNameColumn = CreateTextColumn("Name", 130, "Tribe name");
            NumberColumn tribePlayersColumn = CreateNumberColumn("Players", 55, "Amount of players in the tribe");
            TextColumn tribePlayersDifferenceColumn = CreateTextColumn("Diff.", 50, "The difference in players since previous data");
            NumberColumn tribePointsColumn = CreateNumberColumn("Points", 75, "Total points of the tribe");
            NumberColumn tribePointsDifferenceColumn = CreateNumberColumn("Diff.", 70, "The difference in total tribe points since previous data");
            NumberColumn tribeVillagesColumn = CreateNumberColumn("Villages", 55, "The total amount of villages in the tribe");
            NumberColumn tribeVillagesDifferenceColumn = CreateNumberColumn("Diff.", 55, "The difference in villages since previous data");

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
                tribeNameColumn,
                tribePlayersColumn,
                tribePlayersDifferenceColumn,
                tribePointsColumn,
                tribePointsDifferenceColumn,
                tribeVillagesColumn,
                tribeVillagesDifferenceColumn});
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
