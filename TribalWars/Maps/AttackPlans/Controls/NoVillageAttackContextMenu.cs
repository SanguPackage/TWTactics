#region Using
using System.Windows.Forms;
using Janus.Windows.UI.CommandBars;
using System.Drawing;
using TribalWars.Controls;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.AttackPlans.Controls
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

			_menu.AddToggleCommand(ControlsRes.NoVillageAttackContextMenu_ShowNonActivePlanTargets, _manipulator.Settings.ShowOtherTargets, OnShowOtherTargets);
			_menu.AddToggleCommand(ControlsRes.NoVillageAttackContextMenu_ShowNonActivePlanAttackers, _manipulator.Settings.ShowOtherAttackers, OnShowOtherAttackers);
            _menu.AddSeparator();
			_menu.AddToggleCommand(ControlsRes.NoVillageAttackContextMenu_AlwaysShowAttackPlans, _manipulator.Settings.ShowIfNotActiveManipulator, OnShowIfNotActiveManipulator);
            _menu.AddSeparator();
			_menu.AddToggleCommand(ControlsRes.NoVillageAttackContextMenu_ShowArrivalTimesWhenSentNow, _manipulator.Settings.ShowArrivalTimeWhenSentNow, OnShowArrivalTimeWhenSentNow);
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

        private void OnShowArrivalTimeWhenSentNow(object sender, CommandEventArgs e)
        {
            _manipulator.Settings.ShowArrivalTimeWhenSentNow = !_manipulator.Settings.ShowArrivalTimeWhenSentNow;
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
