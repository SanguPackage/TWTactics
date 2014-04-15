using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

using TribalWars.Data.Players;
using TribalWars.Data.Villages;

using XPTable.Models;

namespace TribalWars.Controls.Display
{
    #region Enums
    /// <summary>
    /// Lists the different columns in the PlayerTableRow columns
    /// </summary>
    [Flags()]
    public enum PlayerFields
    {
        None = 0,
        Name = 1,
        Tribe = 2,
        TribeDifference = 64,
        Points = 4,
        PointsDifference = 8,
        Villages = 16,
        VillagesDifference = 32,
        Default = 23,
        All = 127
    }
    #endregion

    /// <summary>
    /// Represents a player row in an XPTable
    /// </summary>
    public class PlayerTableRow : XPTable.Models.Row, TribalWars.Controls.TWContextMenu.ITWContextMenu
    {
        #region Fields
        private Player _player;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the player the row represents
        /// </summary>
        public Player Player
        {
            get { return _player; }
        }
        #endregion

        #region Constructors
        public PlayerTableRow(Player ply)
        {
            _player = ply;

            // player is currently visible?
            if (World.Default.Map.Display.IsVisible(ply))
            {
                Cells.Add(new Cell(string.Empty, TribalWars.Properties.Resources.Visible));
            }
            else
            {
                Cells.Add(new Cell());
            }

            Cells.Add(ColumnDisplay.CreatePlayerCell(ply));
            string tribe = ply.HasTribe ? ply.Tribe.Tag : string.Empty;
            if (ply.TribeChange && ply.PreviousPlayerDetails.Tribe != null)
            {
                Cells.Add(ColumnDisplay.CreateDifferenceCell(ply.PreviousPlayerDetails.Tribe.Tag));
            }
            else
            {
                Cells.Add(new Cell());
            }
            Cells.Add(new Cell(tribe));
            Cells.Add(new Cell(ply.Points));
            if (ply.PreviousPlayerDetails != null)
            {
                Cells.Add(ColumnDisplay.CreateDifferenceCell(ply.Points - ply.PreviousPlayerDetails.Points));
            }
            else
            {
                Cells.Add(new Cell());
            }
            Cells.Add(new Cell(ply.Villages.Count));
            if (ply.PreviousPlayerDetails != null)
            {
                Cells.Add(new Cell(ply.ConquerString));
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
            //    World.Default.Map.Manipulators.CurrentManipulator.PlayerContextMenu.Show(this.TableModel.Table, p, _player);
        }

        public IEnumerable<TribalWars.Data.Villages.Village> GetVillages()
        {
            return _player;
        }
        public void DisplayDetails()
        {
            World.Default.Map.EventPublisher.SelectPlayer(null, _player, VillageTools.SelectVillage);
        }
        #endregion
    }
}
