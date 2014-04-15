using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data.Villages;
using TribalWars.Data.Units;

namespace TribalWars.Controls
{
    public partial class MapDistanceCollectionControl : UserControl
    {
        private Dictionary<ToolStripMenuItem, MapDistanceControl> _Plans = new Dictionary<ToolStripMenuItem, MapDistanceControl>();
        public MapDistanceControl this[ToolStripMenuItem itm]
        {
            get
            {
                if (_Plans.ContainsKey(itm)) return _Plans[itm];
                return null;
            }
        }

        #region Fields
        private MapDistanceControl _ActivePlan;
        private bool _sound;
        //private List<MapDistanceControl> _plans;

        /*public List<MapDistanceControl> Plans
        {
            get { return _plans; }
        }*/
	
        #endregion

        #region Properties
        public MapDistanceControl ActivePlan
        {
            get { return _ActivePlan; }
            set
            {
                if (_ActivePlan != null) _ActivePlan.Visible = false;
                if (value != null) value.Visible = true;
                _ActivePlan = value;
            }
        }

        public bool Sound
        {
            get { return _sound; }
            set { _sound = value; }
        }

        public Panel AllPlans
        {
            get { return AllContainer; }
        }
        #endregion

        #region Constructors
        public MapDistanceCollectionControl()
        {
            InitializeComponent();

            World.Default.EventPublisher.SettingsLoaded += new EventHandler<EventArgs>(Default_SettingsLoaded);
        }
        #endregion

        #region Event Handlers
        private void Default_SettingsLoaded(object sender, EventArgs e)
        {
            UnitInput.Combobox.ImageList = WorldUnits.Default.ImageList;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (ActivePlan != null) ActivePlan.Calculate();
        }

        private void AttackAllDropDown_Click(object sender, EventArgs e)
        {

        }

        private void cmdAddVillage_Click(object sender, EventArgs e)
        {
            Village village = VillageInput.Village;
            if (village != null)
            {
                //World.Default.Map.EventPublisher.SelectVillages(this, village, VillageTools.DistanceCalculation);
            }
        }

        private void cmdAddTarget_Click(object sender, EventArgs e)
        {
            Village village = VillageInput.Village;
            if (village != null)
            {
                //World.Default.Map.EventPublisher.SelectVillages(this, village, VillageTools.DistanceCalculationTarget);
            }
        }

        private void cmdFind_Click(object sender, EventArgs e)
        {
            if (ActivePlan != null && World.Default.HasLoaded && World.Default.You.Player != null)
            {
                foreach (Village village in World.Default.You.Player)
                {
                    Unit Unit = UnitInput.Unit;
                    if (Unit != null)
                    {
                        TimeSpan TravelTime = Village.TravelTime(ActivePlan.Target, village, Unit);
                        TimeSpan left = ActivePlan.AttackDate - World.Default.ServerTime.Add(TravelTime);
                        if (left.TotalSeconds > 0 && left.TotalHours < 3)
                        {
                            MapDistanceVillageControl ctl = ActivePlan.AddVillage(village);
                            ctl.UnitSelectedIndex = Unit.Position;
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

        private void cmdSound_Click(object sender, EventArgs e)
        {

        }

        private void cmdSort_Click(object sender, EventArgs e)
        {
            if (ActivePlan != null)
                ActivePlan.Sort();
        }
        #endregion

        #region Public Methods
        public void AddTarget(Village vil)
        {
            ToolStripMenuItem newItm = new ToolStripMenuItem(string.Format("{0} {1} ({2}pts)", vil.LocationString.ToString(), vil.Name, vil.Points.ToString("#,0")), null, SelectPlan);
            if (vil.HasPlayer) newItm.Text += " (" + vil.Player.Name + ")";
            AttackDropDown.DropDownItems.Add(newItm);
            if (AttackDropDown.DropDownItems.Count == 1)
                newItm.Checked = true;

            MapDistanceControl distance = new MapDistanceControl(this, WorldUnits.Default.ImageList);
            distance.Target = vil;
            distance.Dock = DockStyle.Fill;
            _Plans.Add(newItm, distance);
            AllContainer.Controls.Add(distance);

            SelectPlan(AttackDropDown.DropDownItems[AttackDropDown.DropDownItems.Count - 1], EventArgs.Empty);
            Timer.Enabled = true;
        }

        public void Remove(MapDistanceControl target)
        {
            Collection.Controls.Remove(target);
            ToolStripMenuItem menuItm = null;
            foreach (KeyValuePair<ToolStripMenuItem, MapDistanceControl> pair in _Plans)
            {
                if (pair.Value == target)
                {
                    menuItm = pair.Key;
                }
            }
            if (menuItm != null)
            {
                _Plans.Remove(menuItm);
                AttackDropDown.DropDownItems.Remove(menuItm);
            }

            if (ActivePlan == target)
            {
                if (AttackDropDown.DropDownItems.Count > 2)
                    SelectPlan((ToolStripMenuItem)AttackDropDown.DropDownItems[2], EventArgs.Empty);
                else
                {
                    SelectPlan(null, EventArgs.Empty);
                }
            }

            World.Default.Map.EventPublisher.PaintMap(false);
        }

        private void SelectPlan(object sender, EventArgs e)
        {
            // select new active plan
            if (sender != null)
            {
                for (int i = 2; i < AttackDropDown.DropDownItems.Count; i++)
                {
                    ((ToolStripMenuItem)AttackDropDown.DropDownItems[i]).Checked = false;
                }

                ToolStripMenuItem SelectedItem = (ToolStripMenuItem)sender;
                SelectedItem.Checked = true;
                ActivePlan = _Plans[SelectedItem];
            }
            else
            {
                ActivePlan = null;
            }
            World.Default.Map.EventPublisher.PaintMap(false);
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
                    foreach (KeyValuePair<ToolStripMenuItem, MapDistanceControl> pair in _Plans)
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
            {
                
            }
        }

        private string GetPlans(bool BBCodes)
        {
            List<MapDistanceVillageComparor> list = new List<MapDistanceVillageComparor>();
            foreach (MapDistanceControl distance in _Plans.Values)
            {
                if (distance != null)
                    list.AddRange(distance.GetVillageList());
            }
            list.Sort();

            StringBuilder str = new StringBuilder();
            foreach (MapDistanceVillageComparor comp in list)
            {
                str.Append(comp.MapDistanceVillage.ToString(BBCodes, comp.MapDistanceVillage.TargetControl.Target));
            }
            return str.ToString().Trim();
        }
        #endregion        

        #region MapDrawing
        public void Draw(Graphics g)
        {
            foreach (MapDistanceControl plan in _Plans.Values)
            {
                Point loc = World.Default.Map.Display.GetMapLocation(plan.Target.X, plan.Target.Y);
                int size = 1; // World.Default.Map.RectangleSize;
                if (plan == _ActivePlan)
                {
                    Bitmap bitmap = TribalWars.Properties.Resources.pin;
                    loc.Offset((int)(size / 2), (int)(size / 2));
                    loc.Offset(-3, -40);
                    g.DrawImage(bitmap, loc);

                    List<MapDistanceVillageComparor> list = plan.GetVillageList();
                    if (list != null)
                    {
                        foreach (MapDistanceVillageComparor itm in list)
                        {
                            loc = World.Default.Map.Display.GetMapLocation(itm.Village.X, itm.Village.Y);
                            loc.Offset((int)(size / 2), (int)(size / 2));
                            loc.Offset(-10, -17);
                            g.DrawImage(TribalWars.Properties.Resources.FlagBlue, loc);
                        }
                    }
                }
                else
                {
                    loc.Offset((int)(size / 2), (int)(size / 2));
                    loc.Offset(-3, -17);
                    g.DrawImage(TribalWars.Properties.Resources.PinSmall, loc);
                }
            }
        }
        #endregion
    }
}
