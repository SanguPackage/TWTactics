#region Using
using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

using TribalWars.Data.Tribes;

using XPTable.Models;
#endregion

namespace TribalWars.Controls.Display
{
    #region Enums
    /// <summary>
    /// Lists the different columns in the TribeTableRow columns
    /// </summary>
    [Flags()]
    public enum TribeFields
    {
        None = 0,
        Rank = 256,
        Tag = 1,
        Name = 2,
        Players = 4,
        PlayersDifference = 32,
        Points = 8,
        PointsDifference = 64,
        Villages = 16,
        VillagesDifference = 128,
        Default = 287,
        All = 511
    }
    #endregion

    /// <summary>
    /// Represents a tribe row in an XPTable
    /// </summary>
    public class TribeTableRow : XPTable.Models.Row, TribalWars.Controls.TWContextMenu.ITWContextMenu
    {
        #region Fields
        private Tribe _tribe;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tribe the row represents
        /// </summary>
        public Tribe Tribe
        {
            get { return _tribe; }
        }
        #endregion

        #region Constructors
        public TribeTableRow(Tribe tribe)
        {
            _tribe = tribe;

            // player is currently visible?
            if (World.Default.Map.Display.IsVisible(tribe))
            {
                Cells.Add(new Cell(string.Empty, TribalWars.Properties.Resources.Visible));
            }
            else
            {
                Cells.Add(new Cell());
            }

            Cells.Add(new Cell(tribe.Rank));
            Cells.Add(new Cell(tribe.Tag));
            Cells.Add(new Cell(tribe.Name));
            Cells.Add(new Cell(tribe.Players.Count));
            Cells.Add(new Cell(tribe.PlayerDifferenceString));
            Cells.Add(new Cell(tribe.AllPoints));
            if (tribe.PreviousTribeDetails != null)
            {
                Cells.Add(new Cell(tribe.AllPoints - tribe.PreviousTribeDetails.AllPoints));
            }
            else
            {
                Cells.Add(new Cell());
            }
            Cells.Add(new Cell(tribe.Villages));
            if (tribe.PreviousTribeDetails != null)
            {
                Cells.Add(new Cell(tribe.Villages - tribe.PreviousTribeDetails.Villages));
            }
            else
            {
                Cells.Add(new Cell());
            }
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
            //    World.Default.Map.Manipulators.CurrentManipulator.TribeContextMenu.Show(this.TableModel.Table, p, _tribe);
        }

        public IEnumerable<TribalWars.Data.Villages.Village> GetVillages()
        {
            return _tribe;
        }

        public void DisplayDetails()
        {
            World.Default.Map.EventPublisher.SelectTribe(null, _tribe, VillageTools.SelectVillage);
        }
        #endregion
    }
}
