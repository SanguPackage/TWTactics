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
using TribalWars.Maps.Manipulators.Managers;
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

            _menu = JanusContextMenu.Create();
            _menu.ShowToolTips = InheritableBoolean.True;

            if (map.Display.IsVisible(player))
            {
                _menu.AddCommand("Pinpoint", OnPinPoint);
            }
            _menu.AddCommand("Pinpoint && Center", OnPinpointAndCenter, Properties.Resources.TeleportIcon);
            _menu.AddSeparator();

            if (World.Default.You.Empty)
            {
                _menu.AddCommand("This is me!", OnPlayerYouSet, Properties.Resources.Player);
                _menu.AddSeparator();
            }

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

        public bool IsVisible()
        {
            return _menu.IsVisible;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Pinpoints and centers the target player
        /// </summary>
        private void OnPinpointAndCenter(object sender, EventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Default);
            World.Default.Map.EventPublisher.SelectVillages(VillageContextMenu.OnDetailsHack, _player, VillageTools.PinPoint);
            World.Default.Map.SetCenter(_player);
        }

        /// <summary>
        /// Open quick details for the player
        /// </summary>
        private void OnPinPoint(object sender, EventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Default);
            World.Default.Map.EventPublisher.SelectPlayer(VillageContextMenu.OnDetailsHack, _player, VillageTools.PinPoint);
        }

        private void OnPlayerYouSet(object sender, EventArgs e)
        {
            World.Default.You = _player;
            World.Default.InvalidateMarkers();
        }

        /// <summary>
        /// Put target player name on clipboard
        /// </summary>
        private void OnToClipboard(object sender, EventArgs e)
        {
            WinForms.ToClipboard(_player.Name);
        }

        /// <summary>
        /// Put target player BBCoded on clipboard
        /// </summary>
        private void OnBbCode(object sender, EventArgs e)
        {
            WinForms.ToClipboard(_player.BbCode());
        }

        /// <summary>
        /// Put target player operation BBCoded on clipboard
        /// </summary>
        private void OnBbCodeOperation(object sender, EventArgs e)
        {
            WinForms.ToClipboard(_player.BbCodeMatt());
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
        #endregion
    }
}
