#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Browsers.Control;
using Janus.Windows.UI.CommandBars;
using System.Drawing;
using Janus.Windows.UI;
using TribalWars.Controls;
using TribalWars.Maps;
using TribalWars.Maps.AttackPlans.Controls;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

#endregion

namespace TribalWars.Villages.ContextMenu
{
    /// <summary>
    /// ContextMenu for multiple Village
    /// </summary>
    public class VillagesContextMenu : IContextMenu
    {
        private const int WarnWhenAddingMoreTargets = 10;

        #region Fields
        private readonly IEnumerable<Village> _villages;

        private readonly UIContextMenu _menu;
        private readonly Map _map;
        private readonly Action<VillageType> _onVillageTypeChangeDelegate;
        #endregion

        #region Constructors
        public VillagesContextMenu(Map map, ICollection<Village> villages, Action<VillageType> onVillageTypeChangeDelegate = null)
        {
            _villages = villages;
            _map = map;
            _onVillageTypeChangeDelegate = onVillageTypeChangeDelegate;

            _menu = JanusContextMenu.Create();
            // TODO: hehe, the ActiveVillageManipulator takes the first player and selects all his villages...
            //if (map.Display.IsVisible(_villages))
            //{
			//    _menu.AddCommand(ControlsRes.ContextMenu_Pinpoint, OnPinPoint);
            //}
			//_menu.AddCommand(ControlsRes.ContextMenu_PinpointAndCenter, OnPinpointAndCenter, Properties.Resources.TeleportIcon);
            //_menu.AddSeparator();

            _menu.AddSetVillageTypeCommand(OnVillageTypeChange, null);
			_menu.AddCommand(ControlsRes.VillageContextMenu_RemovePurpose, OnRemovePurpose);

            if (World.Default.Settings.Church)
            {
                VillageContextMenu.AddChurchCommands(_menu, null, ChurchChange_Click);
            }

            _menu.AddSeparator();
            if (!World.Default.You.Empty && villages.All(x => x.Player == World.Default.You))
            {
				_menu.AddCommand(ControlsRes.VillagesContextMenu_DefendThem, OnAddTargets, Properties.Resources.swordsman);

                AttackersPoolContextMenuCommandCreator.Add(_menu, _villages);
            }
            else
            {
				_menu.AddCommand(ControlsRes.VillagesContextMenu_AttackThem, OnAddTargets, Properties.Resources.barracks);
            }
            _menu.AddSeparator();

			_menu.AddCommand(ControlsRes.ContextMenu_ToBbCode, OnBbCode, Properties.Resources.clipboard);
        }

        #endregion

        #region Event Handlers
        private void ChurchChange_Click(object sender, CommandEventArgs e)
        {
            var level = (int)e.Command.Tag;
            foreach (Village village in _villages)
            {
                _map.EventPublisher.ChurchChange(village, level, false);
            }
            World.Default.DrawMaps(false);

            if (_onVillageTypeChangeDelegate != null)
            {
                _onVillageTypeChangeDelegate(VillageType.None);
            }
        }

        //private void OnPinpointAndCenter(object sender, CommandEventArgs e)
        //{
        //    World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Default);
        //    World.Default.Map.EventPublisher.SelectVillages(sender, _villages, VillageTools.PinPoint);
        //    World.Default.Map.SetCenter(_villages);
        //}

        //private void OnPinPoint(object sender, CommandEventArgs e)
        //{
        //    World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Default);
        //    World.Default.Map.EventPublisher.SelectVillages(sender, _villages, VillageTools.PinPoint);
        //}

        private void OnRemovePurpose(object sender, CommandEventArgs e)
        {
			string text = string.Format(ControlsRes.VillagesContextMenu_RemovePurposeWarning, _villages.Count());
			if (_villages.Count() > 1 && MessageBox.Show(text, ControlsRes.VillageContextMenu_SetPurpose, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }

            foreach (Village village in _villages)
            {
                village.RemovePurpose();
            }
            _map.Invalidate();

            if (_onVillageTypeChangeDelegate != null)
            {
                _onVillageTypeChangeDelegate(VillageType.None);
            }
        }

        private void OnVillageTypeChange(object sender, CommandEventArgs e)
        {
            var changeTo = (VillageType) e.Command.Tag;
			string text = string.Format(ControlsRes.VillagesContextMenu_ChangePurposeWarning, _villages.Count(), changeTo);
			if (_villages.Count() > 1 && MessageBox.Show(text, ControlsRes.VillageContextMenu_SetPurpose, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }

            foreach (Village village in _villages)
            {
                village.SetPurpose(changeTo);
            }
            _map.Invalidate();

            if (_onVillageTypeChangeDelegate != null)
            {
                _onVillageTypeChangeDelegate(changeTo);
            }
        }

        /// <summary>
        /// Create BB code for the villages and put on clipboard
        /// </summary>
        private void OnBbCode(object sender, EventArgs e)
        {
            var str = new StringBuilder();
            foreach (Village village in _villages)
            {
                str.AppendFormat("{0}{1}", village.BbCode(), Environment.NewLine);
            }

            WinForms.ToClipboard(str.ToString());
        }

        private void OnAddTargets(object sender, EventArgs e)
        {
            if (_villages.Count() > WarnWhenAddingMoreTargets)
            {
				var result = MessageBox.Show(string.Format(ControlsRes.VillagesContextMenu_AddTargetsWarning, _villages.Count()), ControlsRes.VillagesContextMenu_AttackThem, MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            foreach (Village village in _villages)
            {
                _map.EventPublisher.AttackAddTarget(sender, village);
            }
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Attack);
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
    }
}
