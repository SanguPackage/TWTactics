#region Using
using System;
using System.Collections.Generic;
using System.Globalization;
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
            _menu.AddCommand("Details", OnDetails);
            _menu.AddCommand("Center", OnCenter, Properties.Resources.TeleportIcon);
            _menu.AddSeparator();
            _menu.AddCommand("TWStats", OnTwStats);
            _menu.AddCommand("To clipboard", OnToClipboard, Properties.Resources.clipboard);
            _menu.AddCommand("BBCode", OnBbCode, Properties.Resources.clipboard);

            _menu.AddSeparator();

            if (village.HasPlayer)
            {
                var playerCommand = _menu.AddCommand(village.Player.Name);

                //playerCommand.Commands.Add(new UICommand("", "test"));
                //playerCommand.Commands.Add(new UICommand("", "test 2"));


            }
            //UICommand player = new UICommand(ContextMenuKeys.VillageKeys.Player, "Player", CommandType.Command);
            ////player.Commands.AddRange(World.Default.PlayerContextMenu);

            //UICommand tribe = new UICommand(ContextMenuKeys.VillageKeys.Tribe, "Tribe", CommandType.Command);


            //if (_village.HasPlayer)
            //{
            //    _menu.Commands[ContextMenuKeys.VillageKeys.Player].Text = _village.Player.Name;
            //    _menu.Commands[ContextMenuKeys.VillageKeys.Player].ToolTipText = _village.Player.Tooltip;
            //    if (_village.HasTribe)
            //    {
            //        _menu.Commands[ContextMenuKeys.VillageKeys.Tribe].Text = _village.Player.Tribe.Tag;
            //        _menu.Commands[ContextMenuKeys.VillageKeys.Tribe].ToolTipText = _village.Player.Tribe.Tooltip;
            //    }
            //}
            //_menu.Commands[ContextMenuKeys.VillageKeys.Tribe].Visible = _village.HasTribe ? InheritableBoolean.True : InheritableBoolean.False;
            //_menu.Commands[ContextMenuKeys.VillageKeys.Player].Visible = _village.HasPlayer ? InheritableBoolean.True : InheritableBoolean.False;
            //_menu.Commands[ContextMenuKeys.VillageKeys.Playerseperator].Visible = _village.HasPlayer ? InheritableBoolean.True : InheritableBoolean.False;
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
            World.Default.Map.EventPublisher.SelectVillages(OnDetailsHack, _village, VillageTools.SelectVillage);
        }





        /// <summary>
        /// Puts the pinpoint on the target village
        /// </summary>
        private void OnPinpoint(object sender, CommandEventArgs e)
        {
            World.Default.Map.EventPublisher.SelectVillages(null, _village, VillageTools.PinPoint);
        }

        /// <summary>
        /// Start a new marker for the target village
        /// </summary>
        private void OnMark(object sender, CommandEventArgs e)
        {
            //World.Default.EventPublisher.Publish(null, Village, VillageTools.Default);
        }

        /// <summary>
        /// Puts the bullseye on the target village
        /// </summary>
        /// <remarks>Bullseye is used for distance calculation</remarks>
        private void OnDistance(object sender, CommandEventArgs e)
        {
            World.Default.Map.EventPublisher.SelectVillages(null, _village, VillageTools.Distance);
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
