#region Using
using System;
using System.Globalization;
using System.Windows.Forms;
using TribalWars.Browsers.Control;
using Janus.Windows.UI.CommandBars;
using System.Drawing;
using Janus.Windows.UI;
using TribalWars.Controls;
using TribalWars.Maps;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

#endregion

namespace TribalWars.Villages.ContextMenu
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

            _menu = JanusContextMenu.Create();

            if (World.Default.You == _village.Player || (World.Default.You.HasTribe && _village.HasTribe && World.Default.You.Tribe == _village.Player.Tribe))
            {
                _menu.AddCommand("Defend", OnAttack, Properties.Resources.Defense);
            }
            else
            {
                _menu.AddCommand("Attack", OnAttack, Buildings.BuildingImages.Barracks);
            }
            if (map.Display.IsVisible(village))
            {
                _menu.AddCommand("Pinpoint", OnPinPoint);
            }
            _menu.AddCommand("Pinpoint && Center", OnCenter, Properties.Resources.TeleportIcon);
            
            _menu.AddSeparator();
            UICommand villageTypes =_menu.AddCommand("Set purpose", null, village.Type.GetImage(true));
            AddVillageTypeCommand(villageTypes, VillageType.Attack, village.Type);
            AddVillageTypeCommand(villageTypes, VillageType.Defense, village.Type);
            AddVillageTypeCommand(villageTypes, VillageType.Noble, village.Type);
            AddVillageTypeCommand(villageTypes, VillageType.Scout, village.Type);
            AddVillageTypeCommand(villageTypes, VillageType.Farm, village.Type);

            if (village.HasPlayer)
            {
                _menu.AddSeparator();

                _menu.AddPlayerContextCommands(map, village.Player, false);

                if (village.HasTribe)
                {
                    _menu.AddTribeContextCommands(map, village.Player.Tribe);
                }

                if (village.PreviousVillageDetails != null && village.PreviousVillageDetails.Player != village.Player && village.PreviousVillageDetails.Player != null)
                {
                    var oldPlayer = World.Default.GetPlayer(village.PreviousVillageDetails.Player.Name);
                    _menu.AddPlayerNobledContextCommands(map, oldPlayer == null ? village.PreviousVillageDetails.Player : oldPlayer, true);
                }
            }

            _menu.AddSeparator();
            _menu.AddCommand("TWStats", OnTwStats);
            _menu.AddCommand("To clipboard", OnToClipboard, Properties.Resources.clipboard);
            _menu.AddCommand("BBCode", OnBbCode, Properties.Resources.clipboard);
        }

        /// <summary>
        /// Allow change between VillageTypes (Offensive, Defensive, Nobles, ...)
        /// </summary>
        private void AddVillageTypeCommand(UICommand menu, VillageType typeToSet, VillageType typeCurrent)
        {
            bool isCurrentlySet = typeCurrent.HasFlag(typeToSet);

            var command = new UICommand("", typeToSet.GetDescription());
            command.Tag = typeToSet;
            command.Image = typeToSet.GetImage(true);
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

        public bool IsVisible()
        {
            return _menu.IsVisible;
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
            WinForms.ToClipboard(_village.LocationString);
        }

        /// <summary>
        /// Put village BBCode on clipboard
        /// </summary>
        private void OnBbCode(object sender, CommandEventArgs e)
        {
            WinForms.ToClipboard(_village.BbCode());
        }

        /// <summary>
        /// Browses to the target village
        /// </summary>
        private void OnTwStats(object sender, CommandEventArgs e)
        {
            World.Default.EventPublisher.BrowseUri(null, DestinationEnum.TwStatsVillage, _village.Id.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Open quick details for the village
        /// </summary>
        private void OnPinPoint(object sender, CommandEventArgs e)
        {
            World.Default.Map.EventPublisher.SelectVillages(OnDetailsHack, _village, VillageTools.PinPoint);
        }

        private void OnAttack(object sender, CommandEventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Attack);
            World.Default.Map.EventPublisher.AttackAddTarget(this, _village);
        }
        #endregion
    }
}
