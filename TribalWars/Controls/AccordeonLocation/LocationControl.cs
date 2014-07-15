#region Using
using System;
using System.Windows.Forms;
using TribalWars.Villages.ContextMenu;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Controls.AccordeonLocation
{
    public partial class LocationControl : UserControl
    {
        public LocationControl()
        {
            InitializeComponent();
        }

        private void PlayerTribeSelectorButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (PlayerTribeSelector.Player != null)
                {
                    var cm = new PlayerContextMenu(World.Default.Map, PlayerTribeSelector.Player, true);
                    cm.Show(PlayerTribeSelectorButton, e.Location);
                }
                else if (PlayerTribeSelector.Tribe != null)
                {
                    var cm = new TribeContextMenu(World.Default.Map, PlayerTribeSelector.Tribe);
                    cm.Show(PlayerTribeSelectorButton, e.Location);
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (PlayerTribeSelector.Player != null)
            {
                World.Default.Map.SetCenter(PlayerTribeSelector.Player);
                World.Default.Map.EventPublisher.SelectPlayer(null, PlayerTribeSelector.Player, VillageTools.PinPoint);
            }
            else if (PlayerTribeSelector.Tribe != null)
            {
                World.Default.Map.SetCenter(PlayerTribeSelector.Tribe);
                World.Default.Map.EventPublisher.SelectTribe(null, PlayerTribeSelector.Tribe, VillageTools.PinPoint);
            }
            }
        }
    }
}
