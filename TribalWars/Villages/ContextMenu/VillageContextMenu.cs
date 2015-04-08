#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using TribalWars.Browsers.Control;
using Janus.Windows.UI.CommandBars;
using System.Drawing;
using TribalWars.Controls;
using TribalWars.Maps;
using TribalWars.Maps.AttackPlans;
using TribalWars.Maps.AttackPlans.EventArg;
using TribalWars.Maps.Manipulators.Implementations.Church;
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
        private readonly AttackPlan _attackPlan;
        private readonly AttackPlanFrom _attacker;
        private readonly bool _isActiveAttackPlan;
        #endregion

        #region Constructors
        public VillageContextMenu(Map map, Village village, Action onVillageTypeChangeDelegate = null)
        {
            _village = village;
            _map = map;
            _onVillageTypeChangeDelegate = onVillageTypeChangeDelegate;
            _attackPlan = World.Default.Map.Manipulators.AttackManipulator.GetPlan(_village, out _isActiveAttackPlan, out _attacker, false);

            _menu = JanusContextMenu.Create();

            AddAttackPlanItems();

            if (World.Default.You == _village.Player || (World.Default.You.HasTribe && _village.HasTribe && World.Default.You.Tribe == _village.Player.Tribe))
            {
                _menu.AddCommand("New attack plan", OnAttack, Properties.Resources.Defense);
            }
            else
            {
                _menu.AddCommand("New attack plan", OnAttack, Buildings.BuildingImages.Barracks);
            }
            _menu.AddSeparator();

            if (map.Display.IsVisible(village))
            {
                _menu.AddCommand("Quick Details", OnPinPoint, Properties.Resources.LeftNavigation_QuickFind);
            }
            _menu.AddCommand("Pinpoint && Center", OnPinpointAndCenter, Properties.Resources.TeleportIcon);
            
            _menu.AddSeparator();
            _menu.AddSetVillageTypeCommand(OnVillageTypeChange, village);

            if (World.Default.Settings.Church)
            {
                var church = _map.Manipulators.ChurchManipulator.GetChurch(_village);
                AddChurchCommands(_menu, church, ChurchChange_Click);
            }

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
                    _menu.AddPlayerNobledContextCommands(map, oldPlayer ?? village.PreviousVillageDetails.Player, true);
                }
            }

            _menu.AddSeparator();
            _menu.AddCommand("TWStats", OnTwStats);
            _menu.AddCommand("To clipboard", OnToClipboard, Properties.Resources.clipboard);
            _menu.AddCommand("BBCode", OnBbCode, Properties.Resources.clipboard);
        }

        private void AddAttackPlanItems()
        {
            if (_attackPlan == null) return;

            if (_isActiveAttackPlan)
            {
                if (_attacker == null)
                {
                    _menu.AddCommand("Delete attack plan", OnDeleteAttackPlan, Properties.Resources.Delete);
                }
                else
                {
                    _menu.AddCommand("Delete from plan", OnDeleteAttacker, Properties.Resources.Delete);
                }
            }
            else
            {
                _menu.AddCommand("Select attack plan", OnSelectAttackPlan, Properties.Resources.FlagGreen);
            }
        }

        public static void AddChurchCommands(UIContextMenu menu, ChurchInfo church, CommandEventHandler handler)
        {
            Debug.Assert(World.Default.Settings.Church);
            string commandText = "Church" + (church == null ? "" : string.Format(" ({0})", church.ChurchLevel));
            var containerCommand = new UICommand("", commandText)
                {
                    Image = Properties.Resources.Church
                };
            containerCommand.Commands.AddRange(CreateChurchLevelCommands(handler, church));

            if (church != null)
            {
                containerCommand.Commands.AddSeparator();
                containerCommand.Commands.AddChangeColorCommand("Color", church.Color, ChurchInfo.DefaultColor, (sender, selectedColor) => church.Color = selectedColor);
            }

            menu.Commands.Add(containerCommand);
        }

        private static UICommand[] CreateChurchLevelCommands(CommandEventHandler handler, ChurchInfo church)
        {
            var churchLevelCommands = new List<UICommand>();
            for (int i = 0; i <= 3; i++)
            {
                churchLevelCommands.Add(CreateChurchLevelCommand(handler, i, church == null ? -1 : church.ChurchLevel));
            }

            return churchLevelCommands.ToArray();
        }

        private static UICommand CreateChurchLevelCommand(CommandEventHandler handler, int level, int churchLevel)
        {
            string text = level == 0 ? "No church" : string.Format("Level {0}", level);
            var cmd = new UICommand("", text)
            {
                IsChecked = level == churchLevel,
                Tag = level
            };

            cmd.Click += handler;

            return cmd;
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
        private void ChurchChange_Click(object sender, CommandEventArgs e)
        {
            var level = (int) e.Command.Tag;
            _map.EventPublisher.ChurchChange(_village, level);

            if (_onVillageTypeChangeDelegate != null)
            {
                _onVillageTypeChangeDelegate();
            }
        }

        private void OnVillageTypeChange(object sender, CommandEventArgs e)
        {
            var changeTo = (VillageType)e.Command.Tag;
            _village.TogglePurpose(changeTo);
            _map.Invalidate();

            if (_onVillageTypeChangeDelegate != null)
            {
                _onVillageTypeChangeDelegate();
            }
        }

        /// <summary>
        /// Pinpoints and centers the target village
        /// </summary>
        private void OnPinpointAndCenter(object sender, CommandEventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Default);
            World.Default.Map.EventPublisher.SelectVillages(OnDetailsHack, _village, VillageTools.PinPoint);
            World.Default.Map.SetCenter(_village.Location);
        }

        /// <summary>
        /// Select village on the map
        /// </summary>
        private void OnPinPoint(object sender, CommandEventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Default);
            World.Default.Map.EventPublisher.SelectVillages(OnDetailsHack, _village, VillageTools.PinPoint);
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

        private void OnAttack(object sender, CommandEventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Attack);
            World.Default.Map.EventPublisher.AttackAddTarget(this, _village);
        }

        private void OnDeleteAttackPlan(object sender, CommandEventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Attack);
            World.Default.Map.EventPublisher.AttackRemoveTarget(this, _attackPlan);
        }

        private void OnDeleteAttacker(object sender, CommandEventArgs e)
        {
            Debug.Assert(_attacker != null);
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Attack);
            World.Default.Map.EventPublisher.AttackUpdateTarget(this, AttackUpdateEventArgs.DeleteAttackFrom(_attacker));
        }

        private void OnSelectAttackPlan(object sender, CommandEventArgs e)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Attack);
            World.Default.Map.EventPublisher.AttackSelect(this, _attackPlan);
        }
        #endregion
    }
}
