using System;
using System.Collections.Generic;
using System.Drawing;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Data;
using TribalWars.Data.Maps;
using TribalWars.Villages;
using XPTable.Models;

namespace TribalWars.Controls.Display
{
    #region Enums
    /// <summary>
    /// Lists the different columns in the VillageTableRow columns
    /// </summary>
    [Flags]
    public enum VillageFields
    {
        /// <summary>
        /// Different images depending on the type of village
        /// </summary>
        Type = 1,
        Coordinates = 2,
        Name = 4,
        Points = 8,
        PointsDifference = 16,
        HasReport = 32,
        Player = 64,
        PlayerDifference = 128,
        PlayerPoints = 256,
        PlayerPointsDifference = 512,
        PlayerVillages = 1024,
        PlayerVillagesDifference = 2048,
        Tribe = 4096,
        TribeRank = 8192,
        Default = 127,
        All = 16383
    }
    #endregion

    /// <summary>
    /// Represents a village row in an XPTable
    /// </summary>
    public class VillageTableRow : Row, ITwContextMenu
    {
        #region Fields
        private readonly Village _village;
        private readonly Map _map;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the village the row represents
        /// </summary>
        public Village Village
        {
            get { return _village; }
        }
        #endregion

        #region Constructors
        public VillageTableRow(Map map, Village village)
        {
            _village = village;
            _map = map;
            _map.EventPublisher.LocationChanged += EventPublisher_LocationChanged;

            // Village is currently visible?
            Cells.Add(new Cell
                {
                    Image = GetVisibleImage()
                });

            // General village info columns
            if (village.Reports != null && village.Type != VillageType.None)
            {
                Cells.Add(new Cell(string.Empty, village.Type.GetImage()));
            }
            else
            {
                Cells.Add(new Cell());
            }
            Cells.Add(new Cell(village.LocationString));
            Cells.Add(new Cell(village.Name));
            Cells.Add(new Cell(village.Points));
            if (village.PreviousVillageDetails != null)
            {
                Cells.Add(ColumnDisplay.CreateDifferenceCell(village.Points - village.PreviousVillageDetails.Points));
            }
            else
            {
                Cells.Add(new Cell());
            }

            // village owner details
            Player owner = null; Player prevOwner = null; int points = 0; int prevPoints = 0;
            int villages = 0; string prevVillages = null; string tribe = null; int tribeRank = 0;
            if (village.HasPlayer)
            {
                owner = village.Player;
                points = village.Player.Points;
                villages = village.Player.Villages.Count;
                if (village.Player.HasTribe)
                {
                    tribe = village.Player.Tribe.Tag;
                    tribeRank = village.Player.Tribe.Rank;
                }
                if (village.Player.PreviousPlayerDetails != null)
                {
                    prevPoints = village.Player.Points - village.Player.PreviousPlayerDetails.Points;
                }
                prevVillages = village.Player.ConquerString;
            }
            if (village.PreviousVillageDetails != null && village.PreviousVillageDetails.Player != null
                && !village.PreviousVillageDetails.Player.Equals(village.Player))
            {
                prevOwner = village.PreviousVillageDetails.Player;
            }

            Cells.Add(ColumnDisplay.CreatePlayerCell(prevOwner, true));
            Cells.Add(ColumnDisplay.CreatePlayerCell(owner));
            Cells.Add(points != 0 ? new Cell(points) : new Cell());
            Cells.Add(ColumnDisplay.CreateDifferenceCell(prevPoints));
            Cells.Add(villages != 0 ? new Cell(villages) : new Cell());
            Cells.Add(ColumnDisplay.CreateDifferenceCell(prevVillages));
            Cells.Add(new Cell(tribe));
            Cells.Add(tribeRank != 0 ? new Cell(tribeRank) : new Cell());
        }
        #endregion

        #region Event Handlers
        private void EventPublisher_LocationChanged(object sender, Data.Events.MapLocationEventArgs e)
        {
            var image = GetVisibleImage();
            Cells[0].Image = image;
        }
        #endregion

        #region ITWContextMenu Members
        public void ShowContext(Point p)
        {
            if (TableModel != null)
            {
                var context = new VillageContextMenu(_map, _village, () => Cells[1].Image = _village.Type.GetImage());
                context.Show(TableModel.Table, p);
            }
        }

        public IEnumerable<Village> GetVillages()
        {
            return _village;
        }

        public void DisplayDetails()
        {
            _map.EventPublisher.SelectVillages(null, _village, VillageTools.PinPoint);
        }
        #endregion

        #region Private
        private Image GetVisibleImage()
        {
            if (_map.Display.IsVisible(_village))
            {
                return Properties.Resources.Visible;
            }
            return null;
        }

        public override string ToString()
        {
            return string.Format("XPTableRow Village = {0} {1}", _village.LocationString, _village.Name);
        }
        #endregion
    }
}
