using System;
using System.Collections.Generic;
using System.Text;

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
        private static CellStyle _youStyle;
        private static CellStyle _tribeStyle;
        private static CellStyle _friendStyle;
        private static CellStyle _differenceStyle;

        private static bool _youSet;
        private static Player _you;
        private static Tribe _yourTribe;

        private static Font _boldFont;
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
        public static CellStyle YouStyle
        {
            get { return _youStyle; }
        }

        /// <summary>
        /// Gets the style for displaying your tribe
        /// </summary>
        public static CellStyle TribeStyle
        {
            get { return _tribeStyle; }
        }

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
            _boldFont = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Bold);

            ColumnDisplay._youStyle = new CellStyle();
            _youStyle.ForeColor = Color.Red;
            _youStyle.Font = _boldFont;
            _youStyle.BackColor = Color.Transparent;

            _tribeStyle = new CellStyle();
            _tribeStyle.ForeColor = Color.Blue;
            _tribeStyle.BackColor = Color.Empty;
            _tribeStyle.Font = _boldFont;

            _friendStyle = new CellStyle();
            _friendStyle.ForeColor = Color.LightBlue;
            _friendStyle.BackColor = Color.Empty;
            _friendStyle.Font = _boldFont;

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
            ImageColumn VisibleColumn = ColumnDisplay.CreateImageColumn(string.Empty, 20);
            ImageColumn VillageImageColumn = ColumnDisplay.CreateImageColumn(string.Empty, 20);
            TextColumn VillageCoordColumn = ColumnDisplay.CreateTextColumn("XY", 50);
            TextColumn VillageNameColumn = ColumnDisplay.CreateTextColumn("Name", 114);
            NumberColumn VillagePointsColumn = ColumnDisplay.CreateNumberColumn("Points", 55);
            NumberColumn VillagePointsDifferenceColumn = ColumnDisplay.CreateNumberColumn("Diff.", 45);
            CheckBoxColumn VillageHasReportsColumn = ColumnDisplay.CreateCheckBoxColumn(string.Empty, 30);
            TextColumn VillagePlayerColumn = ColumnDisplay.CreateTextColumn("New owner", 85);
            TextColumn VillagePlayerDifferenceColumn = ColumnDisplay.CreateTextColumn("Player", 85);
            NumberColumn VillagePlayerPoints = ColumnDisplay.CreateNumberColumn("Points", 60);
            NumberColumn VillagePlayerPointsDifference = ColumnDisplay.CreateNumberColumn("Diff.", 45);
            NumberColumn VillageVillagesColumn = ColumnDisplay.CreateNumberColumn("Villages", 60);
            TextColumn VillageVillagesDifferenceColumn = ColumnDisplay.CreateTextColumn("Diff.", 45);
            TextColumn VillageTribeColumn = ColumnDisplay.CreateTextColumn("Tribe", 50);
            NumberColumn VillageTribeRankColumn = ColumnDisplay.CreateNumberColumn("Rank", 50);

            VillageImageColumn.Visible = (fields & VillageFields.Type) != 0;
            VillageHasReportsColumn.Visible = (fields & VillageFields.HasReport) != 0;
            VillageNameColumn.Visible = (fields & VillageFields.Name) != 0;
            VillagePlayerColumn.Visible = (fields & VillageFields.Player) != 0;
            VillagePlayerDifferenceColumn.Visible = (fields & VillageFields.PlayerDifference) != 0;
            VillagePlayerPoints.Visible = (fields & VillageFields.PlayerPoints) != 0;
            VillagePlayerPointsDifference.Visible = (fields & VillageFields.PlayerPointsDifference) != 0;
            VillagePointsColumn.Visible = (fields & VillageFields.Points) != 0;
            VillagePointsDifferenceColumn.Visible = (fields & VillageFields.PointsDifference) != 0;
            VillageTribeColumn.Visible = (fields & VillageFields.Tribe) != 0;
            VillageTribeRankColumn.Visible = (fields & VillageFields.TribeRank) != 0;
            VillageVillagesColumn.Visible = (fields & VillageFields.PlayerVillages) != 0;
            VillageVillagesDifferenceColumn.Visible = (fields & VillageFields.PlayerVillagesDifference) != 0;

            return new ColumnModel(new XPTable.Models.Column[] {
                VisibleColumn,
                VillageImageColumn,
                VillageCoordColumn,
                VillageNameColumn,
                VillagePointsColumn,
                VillagePointsDifferenceColumn,
                VillageHasReportsColumn,
                VillagePlayerDifferenceColumn,
                VillagePlayerColumn,
                VillagePlayerPoints,
                VillagePlayerPointsDifference,
                VillageVillagesColumn,
                VillageVillagesDifferenceColumn,
                VillageTribeColumn,
                VillageTribeRankColumn});
        }

        /// <summary>
        /// Creates a column model for a player
        /// </summary>
        /// <param name="fields">The visible columns</param>
        public static ColumnModel CreateColumnModel(PlayerFields fields)
        {
            ImageColumn VisibleColumn = ColumnDisplay.CreateImageColumn(string.Empty, 20);
            TextColumn PlayerNameColumn = ColumnDisplay.CreateTextColumn("Name", 85);
            TextColumn PlayerTribeColumn = ColumnDisplay.CreateTextColumn("New tribe", 85);
            NumberColumn PlayerPointsColumn = ColumnDisplay.CreateNumberColumn("Points", 60);
            NumberColumn PlayerVillagesColumn = ColumnDisplay.CreateNumberColumn("Villages", 55);
            TextColumn PlayerVillagesDifferenceColumn = ColumnDisplay.CreateTextColumn("Diff.", 60);
            TextColumn PlayerTribeDifferenceColumn = ColumnDisplay.CreateTextColumn("Tribe", 60);
            NumberColumn PlayerPointsDifferenceColumn = ColumnDisplay.CreateNumberColumn("Diff.", 50);

            PlayerNameColumn.Visible = (fields & PlayerFields.Name) != 0;
            PlayerPointsColumn.Visible = (fields & PlayerFields.Points) != 0;
            PlayerPointsDifferenceColumn.Visible = (fields & PlayerFields.PointsDifference) != 0;
            PlayerTribeColumn.Visible = (fields & PlayerFields.Tribe) != 0;
            PlayerTribeDifferenceColumn.Visible = (fields & PlayerFields.TribeDifference) != 0;
            PlayerVillagesColumn.Visible = (fields & PlayerFields.Villages) != 0;
            PlayerVillagesDifferenceColumn.Visible = (fields & PlayerFields.VillagesDifference) != 0;

            return new ColumnModel(new XPTable.Models.Column[] {
                VisibleColumn,
                PlayerNameColumn,
                PlayerTribeDifferenceColumn,
                PlayerTribeColumn,
                PlayerPointsColumn,
                PlayerPointsDifferenceColumn,
                PlayerVillagesColumn,
                PlayerVillagesDifferenceColumn});
        }

        /// <summary>
        /// Creates a column model for a tribe
        /// </summary>
        /// <param name="fields">The visible columns</param>
        public static ColumnModel CreateColumnModel(TribeFields fields)
        {
            ImageColumn VisibleColumn = ColumnDisplay.CreateImageColumn(string.Empty, 20);
            NumberColumn TribeRankColumn = ColumnDisplay.CreateNumberColumn("Rank", 50);
            TextColumn TribeTagColumn = ColumnDisplay.CreateTextColumn("Tag", 55);
            TextColumn TribeNameColumn = ColumnDisplay.CreateTextColumn("Name", 130);
            NumberColumn TribePlayersColumn = ColumnDisplay.CreateNumberColumn("Players", 45);
            TextColumn TribePlayersDifferenceColumn = ColumnDisplay.CreateTextColumn("Diff.", 50);
            NumberColumn TribePointsColumn = ColumnDisplay.CreateNumberColumn("Points", 70);
            NumberColumn TribePointsDifferenceColumn = ColumnDisplay.CreateNumberColumn("Diff.", 60);
            NumberColumn TribeVillagesColumn = ColumnDisplay.CreateNumberColumn("Villages", 55);
            NumberColumn TribeVillagesDifferenceColumn = ColumnDisplay.CreateNumberColumn("Diff.", 50);

            TribeRankColumn.Visible = (fields & TribeFields.Rank) != 0;
            TribeTagColumn.Visible = (fields & TribeFields.Tag) != 0;
            TribeNameColumn.Visible = (fields & TribeFields.Name) != 0;
            TribePlayersColumn.Visible = (fields & TribeFields.Players) != 0;
            TribePlayersDifferenceColumn.Visible = (fields & TribeFields.PlayersDifference) != 0;
            TribePointsColumn.Visible = (fields & TribeFields.Points) != 0;
            TribePointsDifferenceColumn.Visible = (fields & TribeFields.PointsDifference) != 0;
            TribeVillagesColumn.Visible = (fields & TribeFields.Villages) != 0;
            TribeVillagesDifferenceColumn.Visible = (fields & TribeFields.VillagesDifference) != 0;

            return new ColumnModel(new XPTable.Models.Column[] {
                VisibleColumn,
                TribeRankColumn,
                TribeTagColumn,
                TribeNameColumn,
                TribePlayersColumn,
                TribePlayersDifferenceColumn,
                TribePointsColumn,
                TribePointsDifferenceColumn,
                TribeVillagesColumn,
                TribeVillagesDifferenceColumn});
        }

        /// <summary>
        /// Creates a column model for a report
        /// </summary>
        /// <param name="fields">The visible columns</param>
        public static ColumnModel CreateColumnModel(ReportFields fields)
        {
            ImageColumn ReportTypeImageColumn = ColumnDisplay.CreateImageColumn(string.Empty, 20);
            ImageColumn ReportStatusImageColumn = ColumnDisplay.CreateImageColumn(string.Empty, 20);
            TextColumn ReportVillageColumn = ColumnDisplay.CreateTextColumn("Village", 50);
            TextColumn ReportPlayerColumn = ColumnDisplay.CreateTextColumn("Player", 100);
            DateTimeColumn ReportDateColumn = ColumnDisplay.CreateDateTimeColumn("Date", 70);
            ImageColumn ReportFlagImageColumn = ColumnDisplay.CreateImageColumn(string.Empty, 20);

            ReportTypeImageColumn.Visible = (fields & ReportFields.Type) != 0;
            ReportStatusImageColumn.Visible = (fields & ReportFields.Status) != 0;
            ReportVillageColumn.Visible = (fields & ReportFields.Village) != 0;
            ReportPlayerColumn.Visible = (fields & ReportFields.Player) != 0;
            ReportDateColumn.Visible = (fields & ReportFields.Date) != 0;
            ReportFlagImageColumn.Visible = (fields & ReportFields.Flag) != 0;

            return new ColumnModel(new XPTable.Models.Column[] {
                ReportTypeImageColumn,
                ReportStatusImageColumn,
                ReportVillageColumn,
                ReportPlayerColumn,
                ReportDateColumn,
                ReportFlagImageColumn});
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates a text column
        /// </summary>
        private static TextColumn CreateTextColumn(string header, int width)
        {
            TextColumn col = new TextColumn(header, width);
            col.Editable = false;
            return col;
        }

        /// <summary>
        /// Creates a numeric column
        /// </summary>
        private static NumberColumn CreateNumberColumn(string header, int width)
        {
            NumberColumn col = new NumberColumn(header, width);
            col.Editable = false;
            col.Alignment = ColumnAlignment.Right;
            col.Format = "#,0";
            return col;
        }

        /// <summary>
        /// Creates a numeric column with pretty formatting
        /// </summary>
        /// <remarks>Currently not in use</remarks>
        private static TextColumn CreateNumericTextColumn(string header, int width)
        {
            TextColumn col = new TextColumn(header, width);
            col.Editable = false;
            col.Alignment = ColumnAlignment.Right;
            return col;
        }

        /// <summary>
        /// Creates a boolean column
        /// </summary>
        private static CheckBoxColumn CreateCheckBoxColumn(string header, int width)
        {
            CheckBoxColumn col = new CheckBoxColumn(header, width);
            col.DrawText = false;
            col.Alignment = ColumnAlignment.Center;
            col.Editable = false;
            return col;
        }

        /// <summary>
        /// Creates an Image column
        /// </summary>
        private static ImageColumn CreateImageColumn(string header, int width)
        {
            ImageColumn col = new ImageColumn(header, width);
            col.DrawText = false;
            col.Editable = false;
            return col;
        }

        private static DateTimeColumn CreateDateTimeColumn(string header, int width)
        {
            DateTimeColumn col = new DateTimeColumn(header, width);
            col.CustomDateTimeFormat = "d-MM H:mm";
            col.DateTimeFormat = System.Windows.Forms.DateTimePickerFormat.Custom;
            col.Editable = false;
            col.ShowDropDownButton = false;
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
            Cell cell = new Cell(player.Name);
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
            Cell cell = new Cell(difference);
            cell.CellStyle = DifferenceStyle;
            return cell;
        }

        /// <summary>
        /// Create a grayed out cell
        /// </summary>
        public static Cell CreateDifferenceCell(string difference)
        {
            Cell cell = new Cell(difference);
            cell.CellStyle = DifferenceStyle;
            return cell;
        }
        #endregion
    }
}
