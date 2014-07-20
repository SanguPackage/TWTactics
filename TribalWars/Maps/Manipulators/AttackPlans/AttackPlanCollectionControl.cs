using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Forms.Small;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.Worlds;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// Control with all <see cref="AttackPlan" />s
    /// </summary>
    public partial class AttackPlanCollectionControl : UserControl
    {
        #region Constants
        /// <summary>
        /// When searching for the fastest villages that can still make it for
        /// the given travel time, add this many possible attackers per click
        /// </summary>
        private const int AutoFindAmountOfAttackers = 10;
        #endregion

        #region Fields
        private readonly Dictionary<AttackPlan, Tuple<ToolStripMenuItem, AttackPlanControl>> _plans = 
            new Dictionary<AttackPlan, Tuple<ToolStripMenuItem, AttackPlanControl>>();

        private AttackPlanControl _activePlan;

        private readonly ToolStripItem[] _visibleWhenNoPlans;
        #endregion

        #region Properties
        private AttackPlanControl ActivePlan
        {
            get { return _activePlan; }
            set
            {
                if (_activePlan != null) _activePlan.Visible = false;
                if (value != null) value.Visible = true;
                _activePlan = value;
            }
        }
        #endregion

        #region Constructors
        public AttackPlanCollectionControl()
        {
            InitializeComponent();

            _visibleWhenNoPlans = new ToolStripItem[] { VillageInput, cmdAddTarget };

            World.Default.EventPublisher.SettingsLoaded += Default_SettingsLoaded;
            World.Default.Map.EventPublisher.TargetAdded += EventPublisherOnTargetAdded;
            World.Default.Map.EventPublisher.TargetUpdated += EventPublisherOnTargetUpdated;
            World.Default.Map.EventPublisher.TargetSelected += EventPublisherOnTargetSelected;
            World.Default.Map.EventPublisher.TargetRemoved += EventPublisherOnTargetRemoved;
        }
        #endregion

        #region World/Map Event Handlers
        private void EventPublisherOnTargetSelected(object sender, AttackEventArgs e)
        {
            foreach (var attackDropDownItem in AttackDropDown.DropDownItems.OfType<ToolStripMenuItem>())
            {
                attackDropDownItem.Checked = false;
            }

            if (e.Plan != null)
            {
                var selectedPlan = _plans[e.Plan];
                selectedPlan.Item1.Checked = true;
                ActivePlan = selectedPlan.Item2;
            }
            else
            {
                ActivePlan = null;
            }
        }

        private void EventPublisherOnTargetUpdated(object sender, AttackUpdateEventArgs e)
        {
            switch (e.Action)
            {
                case AttackUpdateEventArgs.ActionKind.Add:
                    e.AttackFrom.ForEach(x => ActivePlan.AddAttacker(x));
                    break;

                case AttackUpdateEventArgs.ActionKind.Delete:
                    e.AttackFrom.ForEach(x => ActivePlan.RemoveAttacker(x));
                    break;

                case AttackUpdateEventArgs.ActionKind.Update:
                    if (ActivePlan != null)
                    {
                        Debug.Assert(!e.AttackFrom.Any() || ActivePlan.Plan == e.AttackFrom.First().Plan);
                        ActivePlan.UpdateDisplay();
                    }
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private void EventPublisherOnTargetRemoved(object sender, AttackEventArgs e)
        {
            RemovePlan(e.Plan);
        }

        private void EventPublisherOnTargetAdded(object sender, AttackEventArgs e)
        {
            AddPlan(e.Plan);
        }

        private void Default_SettingsLoaded(object sender, EventArgs e)
        {
            UnitInput.Combobox.ImageList = WorldUnits.Default.ImageList;
            UnitInput.Combobox.SelectedIndex = WorldUnits.Default[UnitTypes.Ram].Position;

            _plans.Clear();
            AttackDropDown.DropDownItems.Clear();
            ActivePlan = null;
            AllPlans.Controls.Clear();

            var plansFromXml = World.Default.Map.Manipulators.AttackManipulator.GetPlans();
            foreach (AttackPlan plan in plansFromXml)
            {
                AddPlan(plan);
            }
            if (_plans.Any())
            {
                World.Default.Map.EventPublisher.AttackSelect(this, _plans.Select(x => x.Key).First());
            }

            foreach (var toolbarItem in toolStrip1.Items.OfType<ToolStripItem>())
            {
                if (_plans.Any())
                {
                    toolbarItem.Visible = true;
                }
                else
                {
                    toolbarItem.Visible = _visibleWhenNoPlans.Contains(toolbarItem);
                }
            }
        }
        #endregion

        #region Local Event Handlers
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (ActivePlan != null) ActivePlan.UpdateDisplay();
        }

        private void cmdAddVillage_Click(object sender, EventArgs e)
        {
            Village village = VillageInput.Village;
            if (village != null)
            {
                var attackEventArgs = AttackUpdateEventArgs.AddAttackFrom(new AttackPlanFrom(ActivePlan.Plan, village, WorldUnits.Default[UnitTypes.Ram]));
                World.Default.Map.EventPublisher.AttackUpdateTarget(this, attackEventArgs);
            }
        }

        private void cmdAddTarget_Click(object sender, EventArgs e)
        {
            Village village = VillageInput.Village;
            if (village != null)
            {
                World.Default.Map.EventPublisher.AttackAddTarget(this, village);
            }
        }

        private void cmdFind_Click(object sender, EventArgs e)
        {
            if (World.Default.You.Empty)
            {
                if (MessageBox.Show(
                    "You have not yet selected yourself.\nSet yourself now?", 
                    "Select Active Player", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    ActivePlayerForm.UpdateDefaultWorld();    
                }
            }
            else if (ActivePlan != null)
            {
                if (UnitInput.Unit != null)
                {
                    Village[] villagesAlreadyUsed = _plans.Keys.SelectMany(x => x.Attacks)
                                                          .Select(x => x.Attacker)
                                                          .ToArray();

                    var villagesWithTimeLeft = 
                        from village in World.Default.You
                        where !villagesAlreadyUsed.Contains(village)
                        let travelTime = Village.TravelTime(ActivePlan.Plan.Target, village, UnitInput.Unit)
                        let timeBeforeNeedToSend = ActivePlan.Plan.ArrivalTime - World.Default.Settings.ServerTime.Add(travelTime)
                        where timeBeforeNeedToSend.TotalSeconds > 0
                        select new
                            {
                                Village = village,
                                TimeBeforeNeedToSend = timeBeforeNeedToSend
                            };

                    foreach (var village in villagesWithTimeLeft.OrderBy(x => x.TimeBeforeNeedToSend).Take(AutoFindAmountOfAttackers))
                    {
                        var attackEventArgs = AttackUpdateEventArgs.AddAttackFrom(new AttackPlanFrom(ActivePlan.Plan, village.Village, UnitInput.Unit));
                        World.Default.Map.EventPublisher.AttackUpdateTarget(this, attackEventArgs);
                    }

                    ActivePlan.SortOnTimeLeft();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            if (ActivePlan != null)
            {
                World.Default.Map.EventPublisher.AttackUpdateTarget(this, AttackUpdateEventArgs.DeleteAttacksFrom(ActivePlan.Plan.Attacks));
            }
        }

        private void cmdSort_Click(object sender, EventArgs e)
        {
            if (ActivePlan != null)
                ActivePlan.SortOnTimeLeft();
        }
        #endregion

        #region AttackPlans
        private void AddPlan(AttackPlan plan)
        {
            toolStrip1.Items.OfType<ToolStripItem>().ForEach(x => x.Visible = true);

            Village vil = plan.Target;
            var newItm = new ToolStripMenuItem(string.Format("{0} {1} ({2}pts)", vil.LocationString, vil.Name, Common.GetPrettyNumber(vil.Points)), null, SelectPlan);
            if (vil.HasPlayer) newItm.Text += " (" + vil.Player.Name + ")";
            AttackDropDown.DropDownItems.Add(newItm);

            var distance = new AttackPlanControl(WorldUnits.Default.ImageList, plan);
            distance.Dock = DockStyle.Fill;
            _plans.Add(plan, new Tuple<ToolStripMenuItem, AttackPlanControl>(newItm, distance));
            AllPlans.Controls.Add(distance);

            foreach (AttackPlanFrom attack in plan.Attacks)
            {
                distance.AddAttacker(attack);
            }

            Timer.Enabled = true;
        }

        private void RemovePlan(AttackPlan plan)
        {
            var attackControls = _plans[plan];

            AllPlans.Controls.Remove(attackControls.Item2);
            AttackDropDown.DropDownItems.Remove(attackControls.Item1);

            if (ActivePlan == attackControls.Item2)
            {
                if (AttackDropDown.DropDownItems.Count > 0)
                {
                    SelectPlan(AttackDropDown.DropDownItems[0], EventArgs.Empty);
                }
                else
                {
                    SelectPlan(null, EventArgs.Empty);
                }
            }
        }

        private void SelectPlan(object sender, EventArgs e)
        {
            var selectedPlan =_plans.Where(x => x.Value.Item1 == sender).ToArray();
            World.Default.Map.EventPublisher.AttackSelect(this, !selectedPlan.Any() ? null : selectedPlan.First().Key);
        }
        #endregion

        #region TextOutput
        private void cmdClipboardText_Click(object sender, EventArgs e)
        {
            if (ActivePlan != null)
            {
                string export = AttackPlanExporter.GetSinglePlanTextExport(ActivePlan.Plan);
                WinForms.ToClipboard(export);
            }
        }

        private void cmdClipboardBBCode_Click(object sender, EventArgs e)
        {
            if (ActivePlan != null)
            {
                string export = AttackPlanExporter.GetSinglePlanBbCodeExport(ActivePlan.Plan);
                WinForms.ToClipboard(export);
            }
        }

        private void cmdClipboardTextAll_Click(object sender, EventArgs e)
        {
            IEnumerable<AttackPlanFrom> plans = GetAllAttacks();
            string export = AttackPlanExporter.GetMultiPlanTextExport(plans);
            WinForms.ToClipboard(export);
        }

        private void cmdClipboardBBCodeAll_Click(object sender, EventArgs e)
        {
            IEnumerable<AttackPlanFrom> plans = GetAllAttacks();
            string export = AttackPlanExporter.GetMultiPlanBbCodeExport(plans);
            WinForms.ToClipboard(export);
        }

        private IEnumerable<AttackPlanFrom> GetAllAttacks()
        {
            return _plans.Keys.SelectMany(x => x.Attacks);
        }
        #endregion        
    }
}
