#region Using
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Manipulators;
using TribalWars.Data.Maps;
using Janus.Windows.UI.CommandBars;
using System.Drawing;
using Janus.Windows.UI;
using TribalWars.Tools;

#endregion

namespace TribalWars.Controls.TWContextMenu
{
    /// <summary>
    /// ContextMenu with general Village operations
    /// </summary>
    public class VillageContextMenu : IContextMenu
    {
        #region Constants
        public const string OnDetailsHack = "HACK_SWITCH_TO_DETAILS_PANE";
        #endregion

        #region Fields
        private readonly Village _village;

        private readonly UIContextMenu _menu;
        #endregion

        #region Constructors
        public VillageContextMenu(Village village)
        {
            _village = village;

            _menu = new UIContextMenu();
            _menu.AddCommand("Pinpoint", OnDetails);
            _menu.AddCommand("Pinpoint && Center", OnCenter, Properties.Resources.TeleportIcon);
            _menu.AddSeparator();
            _menu.AddCommand("TWStats", OnTwStats);
            _menu.AddCommand("To clipboard", OnToClipboard, Properties.Resources.clipboard);
            _menu.AddCommand("BBCode", OnBbCode, Properties.Resources.clipboard);

            if (village.HasPlayer)
            {
                _menu.AddSeparator();

                var playerCommand = _menu.AddCommand(village.Player.Name);
                var playerContext = new PlayerContextMenu(village.Player, false);
                playerCommand.Commands.AddRange(playerContext.GetCommands().ToArray());

                if (village.HasTribe)
                {
                    var tribeCommand = _menu.AddCommand(village.Player.Tribe.Tag);
                    var tribeContext = new TribeContextMenu(village.Player.Tribe);
                    tribeCommand.Commands.AddRange(tribeContext.GetCommands().ToArray());

                }
            }
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
        #endregion

        #region EventHandlers
        /// <summary>
        /// Pinpoints and centers the target village
        /// </summary>
        private void OnCenter(object sender, CommandEventArgs e)
        {
            World.Default.Map.EventPublisher.SelectVillages(null, _village, VillageTools.PinPoint);
            World.Default.Map.SetCenter(Data.Maps.Display.GetSpan(_village));
        }

        /// <summary>
        /// Put village location on clipboard
        /// </summary>
        private void OnToClipboard(object sender, CommandEventArgs e)
        {
            SetClipboard(_village.LocationString);
        }

        /// <summary>
        /// Put village BBCode on clipboard
        /// </summary>
        private void OnBbCode(object sender, CommandEventArgs e)
        {
            SetClipboard(_village.BBCode());
        }

        /// <summary>
        /// Browses to the target village
        /// </summary>
        private void OnTwStats(object sender, CommandEventArgs e)
        {
            World.Default.EventPublisher.BrowseUri(null, Main.Browser.DestinationEnum.TwStatsVillage, _village.Id.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Open quick details for the village
        /// </summary>
        private void OnDetails(object sender, CommandEventArgs e)
        {
            World.Default.Map.EventPublisher.SelectVillages(OnDetailsHack, _village, VillageTools.PinPoint);
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
