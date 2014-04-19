using TribalWars.Data;
using XPTable.Models;
using TribalWars.Data.Players;
using System.Drawing;
using TribalWars.Data.Tribes;

namespace TribalWars.Controls.Display
{
    /// <summary>
    /// Creates XPTable ColumnModels
    /// </summary>
    public static class ColumnDisplay
    {
        #region Fields

        private static readonly CellStyle _friendStyle;
        private static readonly CellStyle _differenceStyle;

        private static bool _youSet;
        private static Player _you;
        private static Tribe _yourTribe;

        private static readonly Font BoldFont;
        #endregion

        #region Properties
        /// <summary>
        /// Gets you
        /// </summary>
        public static Player You
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
        public static Tribe YourTribe
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
        public static CellStyle YouStyle { get; private set; }

        /// <summary>
        /// Gets the style for displaying your tribe
        /// </summary>
        public static CellStyle TribeStyle { get; private set; }

        /// <summary>
        /// Gets the style for displaying your friends
        /// </summary>
        public static CellStyle FriendStyle
        {
            get { return _friendStyle; }
        }

        /// <summary>
        /// Gets the style for displaying your changes in owners, points, ...
        /// </summary>
        public static CellStyle DifferenceStyle
        {
            get { return _differenceStyle; }
        }
        #endregion

        #region Static Constructor
        static ColumnDisplay()
        {
            BoldFont = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Bold);

            ColumnDisplay.YouStyle = new CellStyle();
            YouStyle.ForeColor = Color.Red;
            YouStyle.Font = BoldFont;
            YouStyle.BackColor = Color.Transparent;

            TribeStyle = new CellStyle();
            TribeStyle.ForeColor = Color.Blue;
            TribeStyle.BackColor = Color.Empty;
            TribeStyle.Font = BoldFont;

            _friendStyle = new CellStyle();
            _friendStyle.ForeColor = Color.LightBlue;
            _friendStyle.BackColor = Color.Empty;
            _friendStyle.Font = BoldFont;

            _differenceStyle = new CellStyle();
            _differenceStyle.ForeColor = Color.LightGray;
            _differenceStyle.BackColor = Color.Empty;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates a column model for a village
        /// </summary>
        /// <param name="fields">The visible columns</param>
        public static ColumnModel CreateColumnModel(VillageFields fields)
        {
            ImageColumn visibleColumn = CreateImageColumn(string.Empty, 20);
            ImageColumn villageImageColumn = CreateImageColumn(string.Empty, 20);
            TextColumn villageCoordColumn = CreateTextColumn("XY", 50);
            TextColumn villageNameColumn = CreateTextColumn("Name", 114);
            NumberColumn villagePointsColumn = CreateNumberColumn("Points", 55);
            NumberColumn villagePointsDifferenceColumn = CreateNumberColumn("Diff.", 45);
            CheckBoxColumn villageHasReportsColumn = CreateCheckBoxColumn(string.Empty, 30);
            TextColumn villagePlayerColumn = CreateTextColumn("New owner", 85);
            TextColumn villagePlayerDifferenceColumn = CreateTextColumn("Player", 85);
            NumberColumn villagePlayerPoints = CreateNumberColumn("Points", 60);
            NumberColumn villagePlayerPointsDifference = CreateNumberColumn("Diff.", 45);
            NumberColumn villageVillagesColumn = CreateNumberColumn("Villages", 60);
            TextColumn villageVillagesDifferenceColumn = CreateTextColumn("Diff.", 45);
            TextColumn villageTribeColumn = CreateTextColumn("Tribe", 50);
            NumberColumn villageTribeRankColumn = CreateNumberColumn("Rank", 50);

            villageImageColumn.Visible = (fields & VillageFields.Type) != 0;
            villageHasReportsColumn.Visible = (fields & VillageFields.HasReport) != 0;
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

            return new ColumnModel(new XPTable.Models.Column[] {
                visibleColumn,
                villageImageColumn,
                villageCoordColumn,
                villageNameColumn,
                villagePointsColumn,
                villagePointsDifferenceColumn,
                villageHasReportsColumn,
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
            ImageColumn visibleColumn = CreateImageColumn(string.Empty, 20);
            TextColumn playerNameColumn = CreateTextColumn("Name", 85);
            TextColumn playerTribeColumn = CreateTextColumn("New tribe", 85);
            NumberColumn playerPointsColumn = CreateNumberColumn("Points", 60);
            NumberColumn playerVillagesColumn = CreateNumberColumn("Villages", 55);
            TextColumn playerVillagesDifferenceColumn = CreateTextColumn("Diff.", 60);
            TextColumn playerTribeDifferenceColumn = CreateTextColumn("Tribe", 60);
            NumberColumn playerPointsDifferenceColumn = CreateNumberColumn("Diff.", 50);

            playerNameColumn.Visible = (fields & PlayerFields.Name) != 0;
            playerPointsColumn.Visible = (fields & PlayerFields.Points) != 0;
            playerPointsDifferenceColumn.Visible = (fields & PlayerFields.PointsDifference) != 0;
            playerTribeColumn.Visible = (fields & PlayerFields.Tribe) != 0;
            playerTribeDifferenceColumn.Visible = (fields & PlayerFields.TribeDifference) != 0;
            playerVillagesColumn.Visible = (fields & PlayerFields.Villages) != 0;
            playerVillagesDifferenceColumn.Visible = (fields & PlayerFields.VillagesDifference) != 0;

            return new ColumnModel(new XPTable.Models.Column[] {
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
            ImageColumn visibleColumn = CreateImageColumn(string.Empty, 20);
            NumberColumn tribeRankColumn = CreateNumberColumn("Rank", 50);
            TextColumn tribeTagColumn = CreateTextColumn("Tag", 55);
            TextColumn tribeNameColumn = CreateTextColumn("Name", 130);
            NumberColumn tribePlayersColumn = CreateNumberColumn("Players", 45);
            TextColumn tribePlayersDifferenceColumn = CreateTextColumn("Diff.", 50);
            NumberColumn tribePointsColumn = CreateNumberColumn("Points", 70);
            NumberColumn tribePointsDifferenceColumn = CreateNumberColumn("Diff.", 60);
            NumberColumn tribeVillagesColumn = CreateNumberColumn("Villages", 55);
            NumberColumn tribeVillagesDifferenceColumn = CreateNumberColumn("Diff.", 50);

            tribeRankColumn.Visible = (fields & TribeFields.Rank) != 0;
            tribeTagColumn.Visible = (fields & TribeFields.Tag) != 0;
            tribeNameColumn.Visible = (fields & TribeFields.Name) != 0;
            tribePlayersColumn.Visible = (fields & TribeFields.Players) != 0;
            tribePlayersDifferenceColumn.Visible = (fields & TribeFields.PlayersDifference) != 0;
            tribePointsColumn.Visible = (fields & TribeFields.Points) != 0;
            tribePointsDifferenceColumn.Visible = (fields & TribeFields.PointsDifference) != 0;
            tribeVillagesColumn.Visible = (fields & TribeFields.Villages) != 0;
            tribeVillagesDifferenceColumn.Visible = (fields & TribeFields.VillagesDifference) != 0;

            return new ColumnModel(new XPTable.Models.Column[] {
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
            ImageColumn reportTypeImageColumn = CreateImageColumn(string.Empty, 20);
            ImageColumn reportStatusImageColumn = CreateImageColumn(string.Empty, 20);
            TextColumn reportVillageColumn = CreateTextColumn("Village", 50);
            TextColumn reportPlayerColumn = CreateTextColumn("Player", 100);
            DateTimeColumn reportDateColumn = CreateDateTimeColumn("Date", 70);
            ImageColumn reportFlagImageColumn = CreateImageColumn(string.Empty, 20);

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
        private static TextColumn CreateTextColumn(string header, int width)
        {
            var col = new TextColumn(header, width) {Editable = false};
            return col;
        }

        /// <summary>
        /// Creates a numeric column
        /// </summary>
        private static NumberColumn CreateNumberColumn(string header, int width)
        {
            var col = new NumberColumn(header, width)
                {
                    Editable = false,
                    Alignment = ColumnAlignment.Right,
                    Format = "#,0"
                };
            return col;
        }

        /// <summary>
        /// Creates a numeric column with pretty formatting
        /// </summary>
        /// <remarks>Currently not in use</remarks>
        private static TextColumn CreateNumericTextColumn(string header, int width)
        {
            var col = new TextColumn(header, width) {Editable = false, Alignment = ColumnAlignment.Right};
            return col;
        }

        /// <summary>
        /// Creates a boolean column
        /// </summary>
        private static CheckBoxColumn CreateCheckBoxColumn(string header, int width)
        {
            var col = new CheckBoxColumn(header, width)
                {
                    DrawText = false,
                    Alignment = ColumnAlignment.Center,
                    Editable = false
                };
            return col;
        }

        /// <summary>
        /// Creates an Image column
        /// </summary>
        private static ImageColumn CreateImageColumn(string header, int width)
        {
            var col = new ImageColumn(header, width) {DrawText = false, Editable = false};
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
