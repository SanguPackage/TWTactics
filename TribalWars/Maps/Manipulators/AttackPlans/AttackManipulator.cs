#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Villages.Units;
using TribalWars.Worlds;
#endregion

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// Plan attacks on the map
    /// </summary>
    public class AttackManipulator : ManipulatorBase
    {
        #region Fields
        private readonly List<AttackPlan> _plans;
        private AttackPlan _activePlan;
        #endregion

        #region Constructors
        public AttackManipulator(Map map)
            : base(map)
        {
            _plans = new List<AttackPlan>();

            map.EventPublisher.TargetAdded += EventPublisherOnTargetAdded;
            map.EventPublisher.TargetUpdated += EventPublisherOnTargetUpdated;
            map.EventPublisher.TargetSelected += EventPublisherOnTargetSelected;
            map.EventPublisher.TargetRemoved += EventPublisherOnTargetRemoved;
        }
        #endregion

        #region AttackPlan Events
        private void EventPublisherOnTargetRemoved(object sender, AttackEventArgs e)
        {
            _plans.Remove(e.Plan);

            if (_activePlan == e.Plan)
            {
                _map.EventPublisher.AttackSelect(sender, _plans.FirstOrDefault());
            }

            _map.Invalidate(false);
        }

        private void EventPublisherOnTargetSelected(object sender, AttackEventArgs e)
        {
            _activePlan = e.Plan;
            _map.Invalidate(false);
        }

        private void EventPublisherOnTargetAdded(object sender, AttackEventArgs e)
        {
            Debug.Assert(!_plans.Contains(e.Plan));
            _plans.Add(e.Plan);
        }

        private void EventPublisherOnTargetUpdated(object sender, AttackUpdateEventArgs e)
        {
            foreach (AttackPlanFrom attacker in e.AttackFrom.ToArray())
            {
                Debug.Assert(_plans.Contains(attacker.Plan));
                AttackPlan plan = _plans.Single(x => x == attacker.Plan);
                switch (e.Action)
                {
                    case AttackUpdateEventArgs.ActionKind.Add:
                        Debug.Assert(!plan.Attacks.Contains(attacker));
                        plan.Attacks.Add(attacker);
                        break;
                        
                   case AttackUpdateEventArgs.ActionKind.Delete:
                        Debug.Assert(plan.Attacks.Contains(attacker));
                        plan.Attacks.Remove(attacker);
                        break;

                   case AttackUpdateEventArgs.ActionKind.Update:
                        break;

                    default:
                        Debug.Assert(false);
                        break;
                }
            }
            World.Default.Map.Invalidate(false);
        }
        #endregion

        #region Map Events
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
                    var existingPlan = _plans.FirstOrDefault(x => x.Target == e.Village);
                    if (existingPlan == null)
                    {
                        _map.EventPublisher.AttackAddTarget(this, e.Village);
                    }
                    else
                    {
                        _map.EventPublisher.AttackSelect(this, existingPlan);
                    }
                    return true;
                }
                else if (e.MouseEventArgs.Button == MouseButtons.Right && (e.Village.Player == World.Default.You || World.Default.You.Empty))
                {
                    var attackEventArgs = AttackUpdateEventArgs.AddAttackFrom(new AttackPlanFrom(_activePlan, e.Village, WorldUnits.Default[UnitTypes.Ram]));
                    _map.EventPublisher.AttackUpdateTarget(this, attackEventArgs);
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
        public IEnumerable<AttackPlan> GetPlans()
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
                var plan = new AttackPlan(
                    World.Default.GetVillage(xmlPlan.Attribute("Target").Value),
                    DateTime.FromFileTimeUtc(long.Parse(xmlPlan.Attribute("ArrivalTime").Value)));

                foreach (var attackerXml in xmlPlan.Descendants("Attacker"))
                {
                    var slowestUnit = (UnitTypes)Enum.Parse(typeof(UnitTypes), attackerXml.Attribute("SlowestUnit").Value);
                    var attacker = new AttackPlanFrom(
                        plan,
                        World.Default.GetVillage(attackerXml.Attribute("Attacker").Value),
                        WorldUnits.Default[slowestUnit]);

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
    }
}