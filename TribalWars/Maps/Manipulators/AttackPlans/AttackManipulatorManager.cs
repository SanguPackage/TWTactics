#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using TribalWars.Controls;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// The managing attackmanipulator
    /// </summary>
    public class AttackManipulatorManager : DefaultManipulatorManager
    {
        #region Fields
        private readonly AttackManipulator _attacker;
        #endregion

        #region Constructors
        public AttackManipulatorManager(Map map)
            : base(map)
        {
            UseLegacyXmlWriter = false;

            // Active manipulators
            _attacker = new AttackManipulator(map, this);
            _manipulators.Add(_attacker);

            MapMover.RightClickToMove = false;
        }
        #endregion

        #region Methods
        public override IContextMenu GetContextMenu(Point location, Village village)
        {
            if (village.Player == World.Default.You)
            {
                return null;
            }
            return base.GetContextMenu(location, village);
        }
        #endregion

        private Dictionary<ToolStripMenuItem, MapDistanceControl> _plans;
        private Func<MapDistanceControl> _activePlanGetter;

        public List<AttackPlanInfo> HackTogether(Dictionary<ToolStripMenuItem, MapDistanceControl> plans, Func<MapDistanceControl> activePlanGetter)
        {
            _activePlanGetter = activePlanGetter;
            _plans = plans;

            if (_savedPlans == null)
            {
                _savedPlans = new List<AttackPlanInfo>();
            }

            return _savedPlans;
        }

        public void Draw(Graphics g)
        {
            foreach (MapDistanceControl plan in _plans.Values)
            {
                Point loc = World.Default.Map.Display.GetMapLocation(plan.Target.Location);
                Size size = World.Default.Map.Display.Dimensions.Size;
                if (plan == _activePlanGetter())
                {
                    // The active plan attacked village
                    loc.Offset(size.Width / 2, size.Height / 2);
                    loc.Offset(-3, -40);
                    g.DrawImage(Properties.Resources.pin, loc);

                    List<MapDistanceVillageComparor> list = plan.GetVillageList();
                    if (list != null)
                    {
                        foreach (MapDistanceVillageComparor itm in list)
                        {
                            // Villages attacking the active target village
                            loc = World.Default.Map.Display.GetMapLocation(itm.Village.Location);
                            loc.Offset(size.Width / 2, size.Height / 2);
                            loc.Offset(-10, -17);
                            g.DrawImage(Properties.Resources.FlagBlue, loc);
                        }
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

        #region Persistence
        public override string WriteXml()
        {
            var plans = new List<AttackPlanInfo>();
            foreach (MapDistanceControl planControl in _plans.Values)
            {
                AttackPlanInfo plan = planControl.GetPlanInfo();
                plans.Add(plan);
            }

            var output = new XDocument(
                new XElement("Plans",
                    plans.Select(plan => 
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

        private List<AttackPlanInfo> _savedPlans;

        public override void ReadXml(XDocument doc)
        {
            _savedPlans = new List<AttackPlanInfo>();

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

                _savedPlans.Add(plan);
            }
        }
        #endregion
    }
}
