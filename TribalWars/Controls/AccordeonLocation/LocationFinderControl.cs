using System;
using System.Windows.Forms;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.AccordeonLocation
{
    /// <summary>
    /// Search the world for villages, players and tribes
    /// and display them in an XPTable
    /// </summary>
    public partial class LocationFinderControl : UserControl
    {
        #region Constructors
        public LocationFinderControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void finderOptionsControl1_SizeChanged(object sender, EventArgs e)
        {
            MainTable.RowStyles[0].Height = Options.Height;
        }

        /// <summary>
        /// Display found players
        /// </summary>
        private void Options_PlayersFound(object sender, PlayersEventArgs e)
        {
            Table.DisplayPlayers(e.Players);
        }

        /// <summary>
        /// Display found tribes
        /// </summary>
        private void Options_TribesFound(object sender, TribesEventArgs e)
        {
            Table.DisplayTribes(e.Tribes);
        }

        /// <summary>
        /// Display found villages
        /// </summary>
        private void Options_VillagesFound(object sender, VillagesEventArgs e)
        {
            Table.DisplayVillages(e.Villages);
        }
        #endregion
    }
}
