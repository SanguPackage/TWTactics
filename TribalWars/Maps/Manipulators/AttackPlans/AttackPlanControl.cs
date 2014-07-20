using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Villages.ContextMenu;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// Control with for one <see cref="AttackPlan" />
    /// </summary>
    public partial class AttackPlanControl : UserControl
    {
        #region Fields
        private readonly ImageList _unitImageList;
        private bool _settingControlValues;
        #endregion

        #region Properties
        public AttackPlan Plan { get; private set; }
        #endregion

        #region Constructors
        public AttackPlanControl(ImageList imageList, AttackPlan plan)
        {
            InitializeComponent();
            _unitImageList = imageList;

            Plan = plan;
            SetControlProperties();
        }
        #endregion

        #region EventHandlers
        private void Close_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            World.Default.Map.EventPublisher.AttackRemoveTarget(this, Plan);
        }

        private void Date_DateSelected(object sender, TribalWars.Controls.TimeConverter.DateEventArgs e)
        {
            if (!_settingControlValues)
            {
                Plan.ArrivalTime = e.SelectedDate;
                World.Default.Map.EventPublisher.AttackUpdateTarget(this, AttackUpdateEventArgs.Update());
            }
        }

        private void Coords_VillageSelected(object sender, Worlds.Events.Impls.VillageEventArgs e)
        {
            if (!_settingControlValues)
            {
                Plan.Target = e.FirstVillage;
                SetControlProperties();
                World.Default.Map.EventPublisher.AttackUpdateTarget(this, AttackUpdateEventArgs.Update());
            }
        }

        private void _Village_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var cm = new VillageContextMenu(World.Default.Map, Plan.Target);
                cm.Show(_Village, e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {
                World.Default.Map.EventPublisher.SelectVillages(null, Plan.Target, VillageTools.PinPoint);
            }
        }

        private void _Village_DoubleClick(object sender, EventArgs e)
        {
            World.Default.Map.EventPublisher.SelectVillages(null, Plan.Target, VillageTools.PinPoint);
            World.Default.Map.SetCenter(Plan.Target.Location);
        }

        private void _Player_MouseClick(object sender, MouseEventArgs e)
        {
            if (Plan.Target.HasPlayer)
            {
                if (e.Button == MouseButtons.Right)
                {
                    var cm = new PlayerContextMenu(World.Default.Map, Plan.Target.Player, false);
                    cm.Show(_Player, e.Location);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    World.Default.Map.EventPublisher.SelectVillages(null, Plan.Target.Player, VillageTools.PinPoint);
                }
            }
        }

        private void Player_DoubleClick(object sender, EventArgs e)
        {
            if (Plan.Target.HasPlayer)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, Plan.Target.Player, VillageTools.PinPoint);
                World.Default.Map.SetCenter(Plan.Target.Player);
            }
        }

        private void _Tribe_MouseClick(object sender, MouseEventArgs e)
        {
            if (Plan.Target.HasTribe)
            {
                if (e.Button == MouseButtons.Right)
                {
                    var cm = new TribeContextMenu(World.Default.Map, Plan.Target.Player.Tribe);
                    cm.Show(_Tribe, e.Location);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    World.Default.Map.EventPublisher.SelectVillages(null, Plan.Target.Player.Tribe, VillageTools.PinPoint);
                }
            }
        }

        private void Tribe_DoubleClick(object sender, EventArgs e)
        {
            if (Plan.Target.HasTribe)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, Plan.Target.Player.Tribe, VillageTools.PinPoint);
                World.Default.Map.SetCenter(Plan.Target.Player.Tribe);
            }
        }
        #endregion

        #region Update Display
        /// <summary>
        /// Update the display times for all attacking villages
        /// </summary>
        public void UpdateDisplay()
        {
            foreach (var attackFrom in DistanceContainer.Controls.OfType<AttackPlanFromControl>())
            {
                attackFrom.UpdateDisplay();
            }
        }

        /// <summary>
        /// Sort attackers, with attackers that need to be send first on top
        /// </summary>
        public void SortOnTimeLeft()
        {
            var list = DistanceContainer.Controls
                .OfType<AttackPlanFromControl>()
                .Select(x => x.Attacker)
                .OrderBy(x => x.GetTimeLeftBeforeSendDate())
                .ToArray();

            for (int i = 0; i < DistanceContainer.Controls.Count; i++)
            {
                var mdv = DistanceContainer.Controls[i] as AttackPlanFromControl;
                if (mdv != null)
                {
                    mdv.SetVillage(list[i]);
                }
            }
        }

        private void SetControlProperties()
        {
            _settingControlValues = true;
            Date.Value = Plan.ArrivalTime;
            Coords.Text = Plan.Target.LocationString;
            _Village.Text = Plan.Target.Name;
            _Player.Text = Plan.Target.HasPlayer ? Plan.Target.Player.ToString() : "";
            _Tribe.Text = Plan.Target.HasTribe ? Plan.Target.Player.Tribe.ToString() : "";
            _settingControlValues = false;
        }
        #endregion

        #region Attacker Changes
        public AttackPlanFromControl AddAttacker(AttackPlanFrom attackFrom)
        {
            var ctl = new AttackPlanFromControl(_unitImageList, attackFrom);
            DistanceContainer.Controls.Add(ctl);
            return ctl;
        }

        public void RemoveAttacker(AttackPlanFrom attacker)
        {
            AttackPlanFromControl villageControl =
                DistanceContainer.Controls
                                 .OfType<AttackPlanFromControl>()
                                 .SingleOrDefault(x => x.Attacker == attacker);
            
            if (villageControl != null)
            {
                DistanceContainer.Controls.Remove(villageControl);
            }
        }
        #endregion

        #region Export
        public string GetExport(bool bbCodes)
        {
            return GetExport(bbCodes, true);
        }

        private string GetExport(bool bbCodes, bool standAlone)
        {
            SortOnTimeLeft();

            if (Plan.Target != null)
            {
                var str = new StringBuilder();
                if (standAlone)
                {
                    str.AppendLine("*** Attack Plan ***");
                    str.AppendLine(!bbCodes ? Plan.Target.ToString() : Plan.Target.BbCode());
                    str.AppendLine();

                    str.AppendLine("Arrival time: " + Date.Value.ToString(Date.CustomFormat));
                    str.AppendLine("Current time: " + World.Default.Settings.ServerTime.ToString(Date.CustomFormat));
                    str.AppendLine();
                }

                foreach (var control in DistanceContainer.Controls.OfType<AttackPlanFromControl>())
                {
                    str.Append(control.Attacker.GetExport(bbCodes, standAlone));
                }
                   
                return str.ToString().Trim();
            }
            return string.Empty;
        }
        #endregion
    }
}
