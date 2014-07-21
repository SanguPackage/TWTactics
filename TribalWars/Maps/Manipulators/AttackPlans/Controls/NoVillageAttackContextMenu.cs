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
using TribalWars.Villages;
using TribalWars.Villages.Buildings;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

#endregion

namespace TribalWars.Maps.Manipulators.AttackPlans.Controls
{
    /// <summary>
    /// ContextMenu with general AttackPlan operations
    /// </summary>
    public class NoVillageAttackContextMenu : IContextMenu
    {
        #region Fields
        private readonly UIContextMenu _menu;
        private readonly AttackManipulatorManager _manipulator;
        #endregion

        #region Constructor
        public NoVillageAttackContextMenu(AttackManipulatorManager manipulator)
        {
            _manipulator = manipulator;

            _menu = JanusContextMenu.Create();
            _menu.AddToggleCommand("Show non-active plan targets", _manipulator.Settings.ShowOtherTargets, OnShowOtherTargets);
            _menu.AddToggleCommand("Show non-active plan attackers", _manipulator.Settings.ShowOtherAttackers, OnShowOtherAttackers);
            _menu.AddSeparator();
            _menu.AddToggleCommand("Always show attack plans", _manipulator.Settings.ShowIfNotActiveManipulator, OnShowIfNotActiveManipulator);
        }
        #endregion

        #region EventHandlers
        private void OnShowIfNotActiveManipulator(object sender, CommandEventArgs e)
        {
            _manipulator.Settings.ShowIfNotActiveManipulator = !_manipulator.Settings.ShowIfNotActiveManipulator;
            World.Default.Map.Invalidate(false);
        }

        private void OnShowOtherTargets(object sender, CommandEventArgs e)
        {
            _manipulator.Settings.ShowOtherTargets = !_manipulator.Settings.ShowOtherTargets;
            World.Default.Map.Invalidate(false);
        }

        private void OnShowOtherAttackers(object sender, CommandEventArgs e)
        {
            _manipulator.Settings.ShowOtherAttackers = !_manipulator.Settings.ShowOtherAttackers;
            World.Default.Map.Invalidate(false);
        }
        #endregion

        #region Public
        public void Show(Control control, Point pos)
        {
            _menu.Show(control, pos);
        }

        public bool IsVisible()
        {
            return _menu.IsVisible;
        }
        #endregion
    }
}
