#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// Plan attacks on the map
    /// </summary>
    public class AttackManipulator : ManipulatorBase
    {
        #region Constants
        #endregion

        #region Fields
        private readonly AttackManipulatorManager _parent;

        private readonly List<AttackPlanInfo> _plans;
        private AttackPlanInfo _activePlan;
        #endregion

        

        //private Dictionary<ToolStripMenuItem, MapDistanceControl> _plans;
        //private Func<MapDistanceControl> _activePlanGetter;

        //public List<AttackPlanInfo> HackTogether(Dictionary<ToolStripMenuItem, MapDistanceControl> plans, Func<MapDistanceControl> activePlanGetter)
        //{
        //    _activePlanGetter = activePlanGetter;
        //    _plans = plans;

        //    if (_savedPlans == null)
        //    {
        //        _savedPlans = new List<AttackPlanInfo>();
        //    }

        //    return _savedPlans;
        //}


        #region Properties
        #endregion

        #region Constructors
        public AttackManipulator(Map map, AttackManipulatorManager parent)
            : base(map)
        {
            _parent = parent;
            _plans = new List<AttackPlanInfo>();

            //map.EventPublisher.VillagesSelected += EventPublisherOnVillagesSelected;
        }

        //private void EventPublisherOnVillagesSelected(object sender, VillagesEventArgs e)
        //{
        //    if (e.Tool == VillageTools.DistanceCalculationTarget)
        //    {
        //        _plans.Add(e.FirstVillage);
        //    }
        //    else if (e.Tool == VillageTools.DistanceCalculation)
        //    {
                
        //    }
        //}
        #endregion

        #region Events
        public override void Paint(MapPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (var plan in _plans)
            {
                Point loc = World.Default.Map.Display.GetMapLocation(plan.Target.Location);
                Size size = World.Default.Map.Display.Dimensions.Size;
                if (plan == _activePlan)
                {
                    // The active plan attacked village
                    loc.Offset(size.Width / 2, size.Height / 2);
                    loc.Offset(-3, -40);
                    g.DrawImage(Properties.Resources.pin, loc);

                    foreach (AttackPlanFrom attacker in _activePlan.Attacks)
                    {
                        // Villages attacking the active target village
                        loc = World.Default.Map.Display.GetMapLocation(attacker.Attacker.Location);
                        loc.Offset(size.Width / 2, size.Height / 2);
                        loc.Offset(-10, -17);
                        g.DrawImage(Properties.Resources.FlagBlue, loc);
                    }
                }
                else
                {
                    // Other villages attacked but not the active plan
                    loc.Offset(size.Width / 2, size.Height / 2);
                    loc.Offset(-3, -17);
                    g.DrawImage(Properties.Resources.PinSmall, loc);
                }
            }
        }

        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.Village != null)
            {
                if (e.MouseEventArgs.Button == MouseButtons.Left)
                {
                    World.Default.Map.EventPublisher.SelectVillages(this, e.Village, VillageTools.DistanceCalculationTarget);
                    return true;
                }
                else if (e.MouseEventArgs.Button == MouseButtons.Right && (e.Village.Player == World.Default.You || World.Default.You.Empty))
                {
                    World.Default.Map.EventPublisher.SelectVillages(this, e.Village, VillageTools.DistanceCalculation);
                    return true;
                }
            }
            return false;
        }

        protected internal override bool MouseUpCore(MapMouseEventArgs e)
        {
            
            return false;
        }

        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            return false;
        }

        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            return false;
        }
        #endregion

        #region Public Methods
        public IEnumerable<AttackPlanInfo> GetPlans()
        {
            return _plans;
        }

        public override void Dispose()
        {
        }
        #endregion

        #region Persistence
        public string WriteXml()
        {
            var output = new XDocument(
                new XElement("Plans",
                    _plans.Select(plan =>
                        new XElement("Plan",
                            new XAttribute("Target", plan.Target.LocationString),
                            new XAttribute("ArrivalTime", plan.ArrivalTime.ToFileTimeUtc()),
                            new XElement("Attackers",
                                plan.Attacks.Select(attacker =>
                                    new XElement("Attacker",
                                        new XAttribute("Attacker", attacker.Attacker.LocationString),
                                        new XAttribute("SlowestUnit", attacker.SlowestUnit.Type))))))));

            return output.ToString();
        }

        public void ReadXml(XDocument doc)
        {
            var plans = doc.Descendants("Manipulator")
                           .Where(manipulator => manipulator.Attribute("Type").Value == "Attack")
                           .SelectMany(manipulator => manipulator.Descendants("Plan"));

            foreach (XElement xmlPlan in plans)
            {
                var plan = new AttackPlanInfo
                {
                    Target = World.Default.GetVillage(xmlPlan.Attribute("Target").Value),
                    ArrivalTime = DateTime.FromFileTimeUtc(long.Parse(xmlPlan.Attribute("ArrivalTime").Value))
                };

                foreach (var attackerXml in xmlPlan.Descendants("Attacker"))
                {
                    var slowestUnit = (UnitTypes)Enum.Parse(typeof(UnitTypes), attackerXml.Attribute("SlowestUnit").Value);
                    var attacker = new AttackPlanFrom
                    {
                        Attacker = World.Default.GetVillage(attackerXml.Attribute("Attacker").Value),
                        SlowestUnit = WorldUnits.Default[slowestUnit]
                    };

                    plan.Attacks.Add(attacker);
                }

                _plans.Add(plan);
            }
        }

        /// <summary>
        /// Cleanup anything when switching worlds or settings
        /// </summary>
        protected internal override void CleanUp()
        {
            _plans.Clear();
            _activePlan = null;
        }
        #endregion

        #region Private Implementation
        #endregion
    }
}