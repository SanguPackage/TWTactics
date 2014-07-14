using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

namespace TribalWars.Controls.AttackPlan
{
    public partial class MapDistanceCollectionControl : UserControl
    {
        #region Fields
        private readonly Dictionary<ToolStripMenuItem, MapDistanceControl> _plans = new Dictionary<ToolStripMenuItem, MapDistanceControl>();
        private MapDistanceControl _activePlan;
        #endregion

        #region Properties
        public MapDistanceControl ActivePlan
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
        public MapDistanceCollectionControl()
        {
            InitializeComponent();

            World.Default.EventPublisher.SettingsLoaded += Default_SettingsLoaded;
            World.Default.Map.EventPublisher.VillagesSelected += EventPublisherOnVillagesSelected;
        }
        #endregion

        #region Event Handlers
        private void Default_SettingsLoaded(object sender, EventArgs e)
        {
            UnitInput.Combobox.ImageList = WorldUnits.Default.ImageList;
            UnitInput.Combobox.SelectedIndex = WorldUnits.Default[UnitTypes.Ram].Position;

            var plansFromXml = World.Default.Map.Manipulators.AttackManipulator.HackTogether(_plans, () => _activePlan);
            foreach (AttackPlanInfo plan in plansFromXml)
            {
                AddTarget(plan.Target);
                ActivePlan.AttackDate = plan.ArrivalTime;

                foreach (AttackPlanFrom attack in plan.Attacks)
                {
                    ActivePlan.AddVillage(attack.Attacker, attack.SlowestUnit);
                }
            }

        }

        private void EventPublisherOnVillagesSelected(object sender, VillagesEventArgs e)
        {
            if (e.Tool == VillageTools.DistanceCalculationTarget)
            {
                AddTarget(e.FirstVillage);
            }
            else if (e.Tool == VillageTools.DistanceCalculation)
            {
                if (ActivePlan != null)
                {
                    ActivePlan.AddVillage(e.FirstVillage);
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (ActivePlan != null) ActivePlan.Calculate();
        }

        private void cmdAddVillage_Click(object sender, EventArgs e)
        {
            Village village = VillageInput.Village;
            if (village != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, village, VillageTools.DistanceCalculation);
            }
        }

        private void cmdAddTarget_Click(object sender, EventArgs e)
        {
            Village village = VillageInput.Village;
            if (village != null)
            {
                World.Default.Map.EventPublisher.SelectVillages(this, village, VillageTools.DistanceCalculationTarget);
            }
        }

        private void cmdFind_Click(object sender, EventArgs e)
        {
            if (ActivePlan != null && World.Default.HasLoaded && World.Default.You != null)
            {
                foreach (Village village in World.Default.You)
                {
                    Unit unit = UnitInput.Unit;
                    if (unit != null)
                    {
                        TimeSpan travelTime = Village.TravelTime(ActivePlan.Target, village, unit);
                        TimeSpan left = ActivePlan.AttackDate - World.Default.Settings.ServerTime.Add(travelTime);
                        if (left.TotalSeconds > 0 && left.TotalHours < 3)
                        {
                            MapDistanceVillageControl ctl = ActivePlan.AddVillage(village);
                            ctl.UnitSelectedIndex = unit.Position;
                        }
                    }
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            if (ActivePlan != null)
            {
                ActivePlan.Clear();
            }
        }

        private void cmdSort_Click(object sender, EventArgs e)
        {
            if (ActivePlan != null)
                ActivePlan.Sort();
        }
        #endregion

        #region Public Methods
        private void AddTarget(Village vil)
        {
            var newItm = new ToolStripMenuItem(string.Format("{0} {1} ({2}pts)", vil.LocationString, vil.Name, Tools.Common.GetPrettyNumber(vil.Points)), null, SelectPlan);
            if (vil.HasPlayer) newItm.Text += " (" + vil.Player.Name + ")";
            AttackDropDown.DropDownItems.Add(newItm);
            if (AttackDropDown.DropDownItems.Count == 1)
                newItm.Checked = true;

            var distance = new MapDistanceControl(this, WorldUnits.Default.ImageList);
            distance.Target = vil;
            distance.Dock = DockStyle.Fill;
            _plans.Add(newItm, distance);
            AllPlans.Controls.Add(distance);

            SelectPlan(AttackDropDown.DropDownItems[AttackDropDown.DropDownItems.Count - 1], EventArgs.Empty);
            Timer.Enabled = true;
        }

        public void Remove(MapDistanceControl target)
        {
            Collection.Controls.Remove(target);
            ToolStripMenuItem menuItm = null;
            foreach (KeyValuePair<ToolStripMenuItem, MapDistanceControl> pair in _plans)
            {
                if (pair.Value == target)
                {
                    menuItm = pair.Key;
                }
            }
            if (menuItm != null)
            {
                _plans.Remove(menuItm);
                AttackDropDown.DropDownItems.Remove(menuItm);
            }

            if (ActivePlan == target)
            {
                if (AttackDropDown.DropDownItems.Count > 0)
                    SelectPlan(AttackDropDown.DropDownItems[0], EventArgs.Empty);
                else
                {
                    SelectPlan(null, EventArgs.Empty);
                }
            }

            World.Default.Map.Invalidate(false);
        }

        private void SelectPlan(object sender, EventArgs e)
        {
            // select new active plan
            if (sender != null)
            {
                for (int i = 0; i < AttackDropDown.DropDownItems.Count; i++)
                {
                    ((ToolStripMenuItem)AttackDropDown.DropDownItems[i]).Checked = false;
                }

                var selectedItem = (ToolStripMenuItem)sender;
                selectedItem.Checked = true;
                ActivePlan = _plans[selectedItem];
            }
            else
            {
                ActivePlan = null;
            }
            World.Default.Map.Invalidate(false);
        }

        public void SetPlan(IEnumerable<Village> villages)
        {
            if (villages == null)
            {
                ActivePlan = null;
            }
            else
            {
                // clumsy :p
                // should've made a new toolstripmenuitem...
                Village last = null;
                foreach (Village vil in villages)
                {
                    last = vil;
                }

                if (last != null)
                {
                    MapDistanceControl plan = null;
                    foreach (KeyValuePair<ToolStripMenuItem, MapDistanceControl> pair in _plans)
                    {
                        if (pair.Value.Target == last)
                        {
                            pair.Key.Checked = true;
                            plan = pair.Value;
                        }
                        else
                        {
                            pair.Key.Checked = false;
                        }
                    }
                    if (plan == null)
                    {
                        AddTarget(last);
                    }
                    //ActivePlan = plan;
                }
            }
        }
        #endregion

        #region TextOutput
        private void cmdClipboardText_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActivePlan != null)
                    Clipboard.SetText(ActivePlan.GetPlan(false));
            }
            catch
            {
                
            }
        }

        private void cmdClipboardBBCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActivePlan != null)
                    Clipboard.SetText(ActivePlan.GetPlan(true));
            }
            catch
            {

            }
        }

        private void cmdClipboardTextAll_Click(object sender, EventArgs e)
        {
            try
            {
                string str = GetPlans(false);
                if (str.Length != 0) Clipboard.SetText(str);
            }
            catch
            {
                
            }
        }

        private void cmdClipboardBBCodeAll_Click(object sender, EventArgs e)
        {
            try
            {
                string str = GetPlans(true);
                if (str.Length != 0) Clipboard.SetText(str);
            }
            catch
            {
                
            }
        }

        private string GetPlans(bool bbCodes)
        {
            var list = new List<MapDistanceVillageComparor>();
            foreach (MapDistanceControl distance in _plans.Values)
            {
                if (distance != null)
                    list.AddRange(distance.GetVillageList());
            }
            list.Sort();

            var str = new StringBuilder();
            foreach (MapDistanceVillageComparor comp in list)
            {
                str.Append(comp.MapDistanceVillage.ToString(bbCodes, comp.MapDistanceVillage.TargetControl.Target));
            }
            return str.ToString().Trim();
        }
        #endregion        
    }
}
