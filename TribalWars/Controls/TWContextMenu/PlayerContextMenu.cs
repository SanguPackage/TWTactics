#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using TribalWars.Data;
using TribalWars.Data.Players;
using TribalWars.Tools;
#endregion

namespace TribalWars.Controls.TWContextMenu
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
        public PlayerContextMenu(Player player, bool addTribeCommands)
        {
            _player = player;

            _menu = new UIContextMenu();

            _menu.AddCommand("Details", OnDetails);
            _menu.AddCommand("Center", OnCenter, Properties.Resources.TeleportIcon);

            _menu.AddSeparator();

            _menu.AddCommand("TWStats", OnTwStats);
            _menu.AddCommand("To clipboard", OnToClipboard, Properties.Resources.clipboard);
            _menu.AddCommand("BBCode", OnBbCode, Properties.Resources.clipboard);
            _menu.AddCommand("Operation", OnBbCodeOperation, Properties.Resources.clipboard);

            if (addTribeCommands)
            {
                // TODO: PlayerContextMenu to implement
                _menu.AddSeparator();

            }
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
            World.Default.Map.EventPublisher.SelectVillages(null, _player, VillageTools.PinPoint);
            World.Default.Map.SetCenter(Data.Maps.Display.GetSpan(_player));
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
            World.Default.EventPublisher.BrowseUri(null, Main.Browser.DestinationEnum.TwStatsPlayer, _player.Id.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Open quick details for the player
        /// </summary>
        private void OnDetails(object sender, EventArgs e)
        {
            World.Default.Map.EventPublisher.SelectPlayer(VillageContextMenu.OnDetailsHack, _player, VillageTools.SelectVillage);
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
