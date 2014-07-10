#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;
using TribalWars.Browsers.Control;
using TribalWars.Controls;
using TribalWars.Maps;
using TribalWars.Maps.Markers;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

#endregion

namespace TribalWars.Villages.ContextMenu
{
    /// <summary>
    /// ContextMenu with general Player operations
    /// </summary>
    public class PlayerContextMenu : IContextMenu
    {
        #region Fields
        private readonly Player _player;

        private readonly UIContextMenu _menu;
        #endregion

        #region Constructors
        public PlayerContextMenu(Map map, Player player, bool addTribeCommands)
        {
            _player = player;

            _menu = new UIContextMenu();
            _menu.ShowToolTips = InheritableBoolean.True;

            if (map.Display.IsVisible(player))
            {
                _menu.AddCommand("Pinpoint", OnDetails);
            }
            _menu.AddCommand("Pinpoint && Center", OnCenter, Properties.Resources.TeleportIcon);
            _menu.AddSeparator();

            var markerContext = new MarkerContextMenu(map, player);
            _menu.AddMarkerContextCommands(markerContext);

            if (addTribeCommands && player.HasTribe)
            {
                _menu.AddTribeContextCommands(map, player.Tribe);
            }

            _menu.AddSeparator();

            _menu.AddCommand("TWStats", OnTwStats);
            _menu.AddCommand("TW Guest", OnTwGuest);

            _menu.AddSeparator();

            _menu.AddCommand("To clipboard", OnToClipboard, Properties.Resources.clipboard);
            _menu.AddCommand("BBCode", OnBbCode, Properties.Resources.clipboard);
            _menu.AddCommand("Operation", OnBbCodeOperation, Properties.Resources.clipboard);
        }
        #endregion

        #region Public Methods
        public IEnumerable<UICommand> GetCommands()
        {
            return _menu.Commands.OfType<UICommand>();
        }

        public void Show(Control control, Point position)
        {
            _menu.Show(control, position);
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Pinpoints and centers the target player
        /// </summary>
        private void OnCenter(object sender, EventArgs e)
        {
            World.Default.Map.EventPublisher.SelectVillages(VillageContextMenu.OnDetailsHack, _player, VillageTools.PinPoint);
            World.Default.Map.SetCenter(_player);
        }

        /// <summary>
        /// Put target player name on clipboard
        /// </summary>
        private void OnToClipboard(object sender, EventArgs e)
        {
            SetClipboard(_player.Name);
        }

        /// <summary>
        /// Put target player BBCoded on clipboard
        /// </summary>
        private void OnBbCode(object sender, EventArgs e)
        {
            SetClipboard(_player.BbCode());
        }

        /// <summary>
        /// Put target player operation BBCoded on clipboard
        /// </summary>
        private void OnBbCodeOperation(object sender, EventArgs e)
        {
            SetClipboard(_player.BbCodeMatt());
        }

        /// <summary>
        /// Browse to TWStats for the target player
        /// </summary>
        private void OnTwStats(object sender, EventArgs e)
        {
            World.Default.EventPublisher.BrowseUri(null, DestinationEnum.TwStatsPlayer, _player.Id.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Browse to TW guest page for the target player
        /// </summary>
        private void OnTwGuest(object sender, EventArgs e)
        {
            World.Default.EventPublisher.BrowseUri(null, DestinationEnum.GuestPlayer, _player.Id.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Open quick details for the player
        /// </summary>
        private void OnDetails(object sender, EventArgs e)
        {
            World.Default.Map.EventPublisher.SelectPlayer(VillageContextMenu.OnDetailsHack, _player, VillageTools.PinPoint);
        }

        private void SetClipboard(string text)
        {
            try
            {
                Clipboard.SetText(text);
            }
            catch (Exception)
            {

            }
        }
        #endregion
    }
}
