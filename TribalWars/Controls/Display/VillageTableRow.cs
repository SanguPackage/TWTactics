using System;
using System.Collections.Generic;
using System.Drawing;
using TribalWars.Data;
using TribalWars.Data.Villages;

using XPTable.Models;
using TribalWars.Data.Players;

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
    public class VillageTableRow : Row, TWContextMenu.ITWContextMenu
    {
        #region Fields
        private readonly Village _village;
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
        public VillageTableRow(Village village)
        {
            _village = village;

            // Village is currently visible?
            if (World.Default.Map.Display.IsVisible(village))
            {
                Cells.Add(new Cell(string.Empty, Properties.Resources.Visible));
            }
            else
            {
                Cells.Add(new Cell());
            }

            // General village info columns
            if (village.Reports != null && village.Type != VillageType.None)
            {
                Cells.Add(new Cell(string.Empty, village.TypeImage));
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
            var checkBoxCell = new Cell(village.Reports != null && village.Reports.Count > 0);
            Cells.Add(checkBoxCell);

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
            Cells.Add(new Cell(points));
            Cells.Add(ColumnDisplay.CreateDifferenceCell(prevPoints));
            Cells.Add(new Cell(villages));
            Cells.Add(ColumnDisplay.CreateDifferenceCell(prevVillages));
            Cells.Add(new Cell(tribe));
            Cells.Add(new Cell(tribeRank));
        }
        #endregion

        #region Event Handlers
        #endregion

        #region Public Methods
        #endregion

        #region Private Implementation
        #endregion

        #region ITWContextMenu Members
        public void ShowContext(Point p)
        {
            //if (this.TableModel != null)
            //    World.Default.Map.Manipulators.CurrentManipulator.VillageContextMenu.Show(this.TableModel.Table, p, _village);
        }

        public IEnumerable<Village> GetVillages()
        {
            return _village;
        }

        public void DisplayDetails()
        {
            World.Default.Map.EventPublisher.SelectVillages(null, _village, VillageTools.SelectVillage);
        }

        #endregion
    }
}
