#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using TribalWars.Maps.Manipulators.AttackPlans.Controls;
using TribalWars.Maps.Manipulators.AttackPlans.EventArg;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Properties;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

#endregion

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// Plan attacks on the map
    /// </summary>
    public class AttackManipulator : ManipulatorBase
    {
        #region Constants
        /// <summary>
        /// Village width below this: don't show any attack markers
        /// </summary>
        private const int MinVillageWidthToShowMarkers = 5;

        /// <summary>
        /// Bigger village width: show Pins, otherwise show small flags
        /// </summary>
        private const int VillageWidthToSwitchToSmallerFlags = 30;
        #endregion

        #region Fields
        private readonly List<AttackPlan> _plans;
        private Village _hoverVillage;

        public AttackPlan ActivePlan { get; private set; }

        public AttackPlanFrom ActiveAttacker { get; private set; }
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

            if (ActivePlan == e.Plan)
            {
                _map.EventPublisher.AttackSelect(sender, _plans.FirstOrDefault());
            }

            _map.Invalidate(false);
        }

        private void EventPublisherOnTargetSelected(object sender, AttackEventArgs e)
        {
            ActiveAttacker = e.Attacker;
            ActivePlan = e.Plan;
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
                        ActiveAttacker = attacker;
                        plan.Attacks.Add(attacker);
                        break;
                        
                   case AttackUpdateEventArgs.ActionKind.Delete:
                        Debug.Assert(plan.Attacks.Contains(attacker));
                        ActiveAttacker = null;
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
            Size villageSize = _map.Display.Dimensions.Size;
            if (villageSize.Width < MinVillageWidthToShowMarkers)
            {
                return;
            }

            Rectangle gameSize = _map.Display.GetGameRectangle();
            Graphics g = e.Graphics;
            
            if (e.IsActiveManipulator && ActivePlan != null)
            {
                // Paint circles around the active plan
                using (var activeTargetPen = new Pen(Color.Yellow, 2))
                {
                    if (gameSize.Contains(ActivePlan.Target.Location))
                    {
                        Point villageLocation = _map.Display.GetMapLocation(ActivePlan.Target.Location);
                        g.DrawEllipse(
                            activeTargetPen,
                            villageLocation.X,
                            villageLocation.Y,
                            villageSize.Width,
                            villageSize.Height);

                        g.DrawEllipse(
                            activeTargetPen,
                            villageLocation.X - 4,
                            villageLocation.Y - 4,
                            villageSize.Width + 8,
                            villageSize.Height + 8);
                    }
                }

                using (var activeAttackersPen = new Pen(Color.Yellow, 1))
                using (var selectedActiveAttackersPen = new Pen(Color.Yellow, 3))
                {
                    // cirkels for the active plan attackers
                    foreach (AttackPlanFrom attacker in ActivePlan.Attacks)
                    {
                        if (gameSize.Contains(attacker.Attacker.Location))
                        {
                            Point villageLocation = _map.Display.GetMapLocation(attacker.Attacker.Location);
                            g.DrawEllipse(
                                ActiveAttacker == attacker ? selectedActiveAttackersPen : activeAttackersPen,
                                villageLocation.X,
                                villageLocation.Y,
                                villageSize.Width,
                                villageSize.Height);
                        }
                    }
                }
            }

            foreach (var plan in _plans)
            {
                Point loc = World.Default.Map.Display.GetMapLocation(plan.Target.Location);
                
                if (plan != ActivePlan)
                {
                    // Other villages attacked but not the active plan
                    loc.Offset(villageSize.Width / 2, villageSize.Height / 2);
                    loc.Offset(-5, -27); // more - means to the top or the left
                    g.DrawImage(AttackIcons.FlagBlue25, loc);

                    foreach (AttackPlanFrom attacker in plan.Attacks)
                    {
                        // Villages attacking other target villages
                        loc = World.Default.Map.Display.GetMapLocation(attacker.Attacker.Location);
                        loc.Offset(villageSize.Width / 2, villageSize.Height / 2);

                        if (villageSize.Width < VillageWidthToSwitchToSmallerFlags)
                        {
                            loc.Offset(-10, -17);
                            g.DrawImage(Resources.FlagBlue, loc);
                        }
                        else
                        {
                            loc.Offset(-6, -25);
                            g.DrawImage(AttackIcons.PinBlue20, loc);
                        }
                    }
                }
            }

            if (ActivePlan != null)
            {
                Point activePlanTargetLocation = World.Default.Map.Display.GetMapLocation(ActivePlan.Target.Location);

                // The active plan attacked village
                activePlanTargetLocation.Offset(villageSize.Width / 2, villageSize.Height / 2);
                activePlanTargetLocation.Offset(-8, -48); // more - means to the top or the left
                g.DrawImage(AttackIcons.FlagGreen, activePlanTargetLocation);

                foreach (AttackPlanFrom attacker in ActivePlan.Attacks)
                {
                    // Villages attacking the active target village
                    activePlanTargetLocation = World.Default.Map.Display.GetMapLocation(attacker.Attacker.Location);
                    activePlanTargetLocation.Offset(villageSize.Width / 2, villageSize.Height / 2);

                    if (villageSize.Width < VillageWidthToSwitchToSmallerFlags)
                    {
                        activePlanTargetLocation.Offset(-10, -17);
                        g.DrawImage(Resources.FlagGreen, activePlanTargetLocation);
                    }
                    else
                    {
                        activePlanTargetLocation.Offset(-6, -25);
                        g.DrawImage(AttackIcons.PinGreen20, activePlanTargetLocation);
                    }
                }
            }
        }

        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.Village != null)
            {
                AttackPlan existingPlan = _plans.FirstOrDefault(x => x.Target == e.Village);
                AttackPlanFrom existingAttack = _plans.SelectMany(plan => plan.Attacks).FirstOrDefault(attack => attack.Attacker == e.Village);

                if (e.MouseEventArgs.Button == MouseButtons.Left)
                {
                    if (existingPlan == null)
                    {
                        if (existingAttack == null)
                        {
                            _map.EventPublisher.AttackAddTarget(this, e.Village);
                        }
                        else
                        {
                            if (existingAttack != ActiveAttacker)
                            {
                                _map.EventPublisher.AttackSelect(this, existingAttack);
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (existingPlan == ActivePlan && ActivePlan != null)
                        {
                            if (existingAttack != ActiveAttacker)
                            {
                                if (existingAttack == null)
                                {
                                    _map.EventPublisher.AttackSelect(this, ActivePlan);
                                }
                                else
                                {
                                    _map.EventPublisher.AttackSelect(this, existingAttack);
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            _map.EventPublisher.AttackSelect(this, existingPlan);
                        }
                    }
                    return true;
                }
                else if (e.MouseEventArgs.Button == MouseButtons.Right)
                {
                    if (e.Village.Player == World.Default.You || World.Default.You.Empty)
                    {
                        var attackEventArgs = AttackUpdateEventArgs.AddAttackFrom(new AttackPlanFrom(ActivePlan, e.Village, WorldUnits.Default[UnitTypes.Ram]));
                        _map.EventPublisher.AttackUpdateTarget(this, attackEventArgs);
                        return true;
                    }
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
            // This is for the MiniMap:
            // Inform the MiniMap that the selected village changed so that
            // the minimap still pinpoints what we hover while the main map
            // keeps track of just the AttackPlans
            if (e.Village != null)
            {
                if (_hoverVillage != e.Village)
                {
                    _map.EventPublisher.SelectVillages(this, e.Village, VillageTools.SelectVillage);

                    _hoverVillage = e.Village;
                    return true;
                }
            }
            if (e.Village == null && _hoverVillage != null)
            {
                _map.EventPublisher.Deselect(this);

                _hoverVillage = null;
                return true;
            }
            return false;
        }

        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            switch (e.KeyEventArgs.KeyCode)
            {
                case Keys.Delete:
                    if (ActiveAttacker != null)
                    {
                        _map.EventPublisher.AttackUpdateTarget(this, AttackUpdateEventArgs.DeleteAttackFrom(ActiveAttacker));
                    }
                    else if (ActivePlan != null)
                    {
                        _map.EventPublisher.AttackRemoveTarget(this, ActivePlan);
                    }
                    break;
            }

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

                if (plan.Target != null)
                {
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
        }

        /// <summary>
        /// Cleanup anything when switching worlds or settings
        /// </summary>
        protected internal override void CleanUp()
        {
            _plans.Clear();
            ActivePlan = null;
            ActiveAttacker = null;
        }
        #endregion
    }
}