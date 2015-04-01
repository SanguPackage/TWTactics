using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using TribalWars.Forms.Small;
using TribalWars.Maps.AttackPlans.EventArg;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.Worlds;

namespace TribalWars.Maps.AttackPlans.Controls
{
    /// <summary>
    /// Control with all <see cref="AttackPlan" />s
    /// </summary>
    public partial class AttackPlanCollectionControl : UserControl
    {
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
                if (_activePlan != value)
                {
                    if (_activePlan != null)
                    {
                        _activePlan.SetActiveAttacker(null);
                        _activePlan.Visible = false;
                    }
                    if (value != null)
                    {
                        value.Visible = true;
                    }
                    _activePlan = value;
                }
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
            if (ActivePlan == null || ActivePlan.Plan != e.Plan)
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

            if (ActivePlan != null)
            {
                ActivePlan.SetActiveAttacker(e.Attacker);
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
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            if (e.AttackFrom.Any())
            {
                var plan = e.AttackFrom.First().Plan;
                _plans[plan].Item1.Text = GetAttackToolstripText(plan);
            }

            if (ActivePlan != null)
            {
                Debug.Assert(!e.AttackFrom.Any() || ActivePlan.Plan == e.AttackFrom.First().Plan);
                ActivePlan.SetActiveAttacker(e.AttackFrom.FirstOrDefault());
                ActivePlan.UpdateDisplay();
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
            World.Default.Map.Manipulators.AttackManipulator.DefaultSpeed = UnitTypes.Ram;

            VillageTypeInput.Combobox.ImageList = VillageTypeHelper.GetImageList();

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
            else
            {
                ShowHelp();
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

        private void ShowHelp()
        {
            var attackHelpControl1 = new AttackHelpControl();
            attackHelpControl1.Dock = DockStyle.Fill;
            attackHelpControl1.Location = new System.Drawing.Point(0, 0);
            attackHelpControl1.Margin = new Padding(0);
            AllPlans.Controls.Add(attackHelpControl1);
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
                ActivePlayerForm.AskToSetSelf();
            }
            else if (ActivePlan != null && UnitInput.Unit != null)
            {
                IEnumerable<Village> searchIn = World.Default.Map.Manipulators.AttackManipulator.GetAttackersFromYou(ActivePlan.Plan, UnitInput.Unit);
                foreach (var village in searchIn)
                {
                    var attackEventArgs = AttackUpdateEventArgs.AddAttackFrom(new AttackPlanFrom(ActivePlan.Plan, village, UnitInput.Unit));
                    World.Default.Map.EventPublisher.AttackUpdateTarget(this, attackEventArgs);
                }

                ActivePlan.SortOnTimeLeft();
            }
        }

        private void cmdFindPool_Click(object sender, EventArgs e)
        {
            if (World.Default.Map.Manipulators.AttackManipulator.IsAttackersPoolEmpty)
            {
                MessageBox.Show("In the top menu, choose 'Windows' > 'Manage your villages' to create an attackers pool!", "Attackers pool");
            }
            else if (ActivePlan != null && UnitInput.Unit != null)
            {
                bool depleted;
                IEnumerable<Village> searchIn = World.Default.Map.Manipulators.AttackManipulator.GetAttackersFromPool(ActivePlan.Plan, UnitInput.Unit, out depleted);
                if (depleted)
                {
                    MessageBox.Show("Attackers pool depleted!", "Attackers pool");
                }

                foreach (var village in searchIn)
                {
                    var attackEventArgs = AttackUpdateEventArgs.AddAttackFrom(new AttackPlanFrom(ActivePlan.Plan, village, UnitInput.Unit));
                    World.Default.Map.EventPublisher.AttackUpdateTarget(this, attackEventArgs);
                }

                ActivePlan.SortOnTimeLeft();
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
        private string GetAttackToolstripText(AttackPlan plan)
        {
            Village vil = plan.Target;
            string attackDesc = string.Format("{0} {1} ({2}pts) - {3} attacks", vil.LocationString, vil.Name, Common.GetPrettyNumber(vil.Points), plan.Attacks.Count());
            if (vil.HasPlayer) attackDesc += " on " + vil.Player.Name;
            return attackDesc;
        }

        private void AddPlan(AttackPlan plan)
        {
            if (AllPlans.Controls.Count == 1 && AllPlans.Controls[0] is AttackHelpControl)
            {
                toolStrip1.Items.OfType<ToolStripItem>().ForEach(x => x.Visible = true);
                AllPlans.Controls.Clear();
            }

            var newPlanDropdownItm = new ToolStripMenuItem("", null, SelectPlan);
            newPlanDropdownItm.Text = GetAttackToolstripText(plan);
            AttackDropDown.DropDownItems.Add(newPlanDropdownItm);

            var newPlan = new AttackPlanControl(WorldUnits.Default.ImageList, plan);
            newPlan.Visible = false;
            newPlan.Dock = DockStyle.Fill;
            _plans.Add(plan, new Tuple<ToolStripMenuItem, AttackPlanControl>(newPlanDropdownItm, newPlan));
            AllPlans.Controls.Add(newPlan);

            foreach (AttackPlanFrom attack in plan.Attacks)
            {
                newPlan.AddAttacker(attack);
            }

            Timer.Enabled = true;
        }

        private void RemovePlan(AttackPlan plan)
        {
            var attackControls = _plans[plan];

            AllPlans.Controls.Remove(attackControls.Item2);
            AttackDropDown.DropDownItems.Remove(attackControls.Item1);
            _plans.Remove(plan);

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

            if (!_plans.Any())
            {
                ShowHelp();
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

        private void UnitInput_Click(object sender, EventArgs e)
        {
            World.Default.Map.Manipulators.AttackManipulator.DefaultSpeed = (UnitTypes)UnitInput.Combobox.SelectedIndex;
        }
    }
}
