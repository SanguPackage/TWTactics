using System;
using System.Collections.Generic;
using System.Drawing;
using TribalWars.Maps;
using TribalWars.Villages;
using TribalWars.Villages.ContextMenu;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;
using XPTable.Models;

namespace TribalWars.Controls.XPTables
{
    #region Enums
    /// <summary>
    /// Lists the different columns in the PlayerTableRow columns
    /// </summary>
    [Flags]
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
    public class PlayerTableRow : Row, ITwContextMenu
    {
        #region Fields
        private readonly Player _player;
        private readonly Map _map;
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
        public PlayerTableRow(Map map, Player ply)
        {
            _player = ply;
            _map = map;
            _map.EventPublisher.LocationChanged += EventPublisher_LocationChanged;

            // player is currently visible?
            Cells.Add(new Cell
            {
                Image = GetVisibleImage()
            });

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
        private void EventPublisher_LocationChanged(object sender, MapLocationEventArgs e)
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
                var context = new PlayerContextMenu(_map, _player, true);
                context.Show(TableModel.Table, p);
            }
        }

        public IEnumerable<Village> GetVillages()
        {
            return _player;
        }

        public void DisplayDetails()
        {
            _map.EventPublisher.SelectPlayer(null, _player, VillageTools.PinPoint);
        }
        #endregion

        #region Private
        private Image GetVisibleImage()
        {
            if (_map.Display.IsVisible(_player))
            {
                return Properties.Resources.Visible;
            }
            return null;
        }

        public override string ToString()
        {
            return string.Format("XPTableRow Player = {0}", _player.Name);
        }
        #endregion
    }
}
