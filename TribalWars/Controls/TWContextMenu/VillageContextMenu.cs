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
        /// <summary>
        /// Hack for forcing the MainForm to open the details quickpane
        /// </summary>
        public const string OnDetailsHack = "HACK_SWITCH_TO_DETAILS_PANE";
        #endregion

        #region Fields
        private readonly Village _village;

        private readonly UIContextMenu _menu;
        private readonly Map _map;
        private readonly Action _onVillageTypeChangeDelegate;
        #endregion

        #region Constructors
        public VillageContextMenu(Map map, Village village, Action onVillageTypeChangeDelegate = null)
        {
            _village = village;
            _map = map;
            _onVillageTypeChangeDelegate = onVillageTypeChangeDelegate;

            _menu = new UIContextMenu();
            _menu.ShowToolTips = InheritableBoolean.True;

            if (map.Display.IsVisible(village))
            {
                _menu.AddCommand("Pinpoint", OnDetails);
            }
            _menu.AddCommand("Pinpoint && Center", OnCenter, Properties.Resources.TeleportIcon);
            
            _menu.AddSeparator();
            UICommand villageTypes =_menu.AddCommand("Type", null, village.Type.GetImage());
            AddVillageTypeCommand(villageTypes, VillageType.Attack, village.Type);
            AddVillageTypeCommand(villageTypes, VillageType.Defense, village.Type);
            AddVillageTypeCommand(villageTypes, VillageType.Noble, village.Type);
            AddVillageTypeCommand(villageTypes, VillageType.Scout, village.Type);
            AddVillageTypeCommand(villageTypes, VillageType.Farm, village.Type);

            _menu.AddSeparator();
            _menu.AddCommand("TWStats", OnTwStats);
            _menu.AddCommand("To clipboard", OnToClipboard, Properties.Resources.clipboard);
            _menu.AddCommand("BBCode", OnBbCode, Properties.Resources.clipboard);

            if (village.HasPlayer)
            {
                _menu.AddSeparator();

                var playerCommand = _menu.AddCommand(village.Player.Name);
                playerCommand.ToolTipText = village.Player.Tooltip;
                var playerContext = new PlayerContextMenu(map, village.Player, false);
                playerCommand.Commands.AddRange(playerContext.GetCommands().ToArray());

                if (village.HasTribe)
                {
                    var tribeCommand = _menu.AddCommand(village.Player.Tribe.Tag);
                    tribeCommand.ToolTipText = village.Player.Tribe.Tooltip;
                    var tribeContext = new TribeContextMenu(map, village.Player.Tribe);
                    tribeCommand.Commands.AddRange(tribeContext.GetCommands().ToArray());
                }
            }
        }

        /// <summary>
        /// Allow change between VillageTypes (Offensive, Defensive, Nobles, ...)
        /// </summary>
        private void AddVillageTypeCommand(UICommand menu, VillageType typeToSet, VillageType typeCurrent)
        {
            bool isCurrentlySet = typeCurrent.HasFlag(typeToSet);

            var command = new UICommand("", typeToSet.GetDescription());
            command.Tag = typeToSet;
            command.Image = typeToSet.GetImage();
            command.Checked = isCurrentlySet ? InheritableBoolean.True : InheritableBoolean.False;
            command.Click += OnVillageTypeChange;
            menu.Commands.Add(command);
        }

        private void OnVillageTypeChange(object sender, CommandEventArgs e)
        {
            var changeTo = (VillageType) e.Command.Tag;
            if (_village.Type.HasFlag(changeTo))
            {
                _village.Type -= changeTo;
            }
            else
            {
                _village.Type |= changeTo;
            }
            _map.Invalidate();

            if (_onVillageTypeChangeDelegate != null)
            {
                _onVillageTypeChangeDelegate();
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
            World.Default.Map.EventPublisher.SelectVillages(OnDetailsHack, _village, VillageTools.PinPoint);
            World.Default.Map.SetCenter(_village.Location);
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
            SetClipboard(_village.BbCode());
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
