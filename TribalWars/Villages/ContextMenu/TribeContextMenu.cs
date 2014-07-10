using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using TribalWars.Browsers.Control;
using TribalWars.Controls;
using TribalWars.Maps;
using TribalWars.Maps.Markers;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

namespace TribalWars.Villages.ContextMenu
{
    /// <summary>
    /// ContextMenu with general Tribe operations
    /// </summary>
    public class TribeContextMenu : IContextMenu
    {
        #region Fields
        private readonly Tribe _tribe;

        private readonly UIContextMenu _menu;
        #endregion

        #region Constructors
        public TribeContextMenu(Map map, Tribe tribe)
        {
            _tribe = tribe;

            _menu = new UIContextMenu();

            if (map.Display.IsVisible(tribe))
            {
                _menu.AddCommand("Pinpoint", OnDetails);
            }
            _menu.AddCommand("Pinpoint && Center", OnCenter, Properties.Resources.TeleportIcon);
            _menu.AddSeparator();

            var markerContext = new MarkerContextMenu(map, tribe);
            _menu.AddMarkerContextCommands(markerContext);
            _menu.AddSeparator();

            _menu.AddCommand("TWStats", OnTwStats);
            _menu.AddCommand("TW Guest", OnTwGuest);

            _menu.AddSeparator();

            _menu.AddCommand("To clipboard", OnToClipboard, Properties.Resources.clipboard);
            _menu.AddCommand("BBCode", OnBbCode, Properties.Resources.clipboard);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Shows the ContextMenuStrip
        /// </summary>
        public void Show(Control control, Point position)
        {
            _menu.Show(control, position);
        }

        public IEnumerable<UICommand> GetCommands()
        {
            return _menu.Commands.OfType<UICommand>();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Open quick details for the tribe
        /// </summary>
        private void OnDetails(object sender, EventArgs e)
        {
            World.Default.Map.EventPublisher.SelectTribe(VillageContextMenu.OnDetailsHack, _tribe, VillageTools.PinPoint);
        }

        /// <summary>
        /// Pinpoints and centers the target tribe
        /// </summary>
        private void OnCenter(object sender, EventArgs e)
        {
            World.Default.Map.EventPublisher.SelectVillages(VillageContextMenu.OnDetailsHack, _tribe, VillageTools.PinPoint);
            World.Default.Map.SetCenter(_tribe);
        }

        /// <summary>
        /// Browse to TWStats for the target tribe
        /// </summary>
        private void OnTwStats(object sender, EventArgs e)
        {
            World.Default.EventPublisher.BrowseUri(null, DestinationEnum.TwStatsTribe, _tribe.Id.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Browse to TW guest page for the target tribe
        /// </summary>
        private void OnTwGuest(object sender, EventArgs e)
        {
            World.Default.EventPublisher.BrowseUri(null, DestinationEnum.GuestTribe, _tribe.Id.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Put target tribe tag on clipboard
        /// </summary>
        private void OnToClipboard(object sender, EventArgs e)
        {
            SetClipboard(_tribe.Tag);
        }

        /// <summary>
        /// Put target tribe BBCoded on clipboard
        /// </summary>
        private void OnBbCode(object sender, EventArgs e)
        {
            SetClipboard(_tribe.BbCode());
        }
        #endregion

        #region Private
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
