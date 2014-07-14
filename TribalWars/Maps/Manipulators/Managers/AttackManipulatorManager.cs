#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using TribalWars.Controls;
using TribalWars.Controls.AttackPlan;
using TribalWars.Controls.Polygons;
using TribalWars.Maps.Manipulators.Helpers;
using TribalWars.Maps.Manipulators.Implementations;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Manipulators.Managers
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
        protected internal override void ReadXmlCore(XmlReader r)
        {
            
        }

        protected internal override void WriteXmlCore(XmlWriter w)
        {
            
        }

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

        public void HackTogether(Dictionary<ToolStripMenuItem, MapDistanceControl> plans, Func<MapDistanceControl> activePlanGetter)
        {
            _activePlanGetter = activePlanGetter;
            _plans = plans;
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
                plans.Select(plan => 
                    new XElement("Plan", 
                        new XAttribute("Target", plan.Target.LocationString), 
                        new XAttribute("ArrivalTime", plan.ArrivalTime.ToFileTimeUtc()),
                        new XElement("Attackers",
                            plan.Attacks.Select(attacker => 
                                new XElement("Attacker",
                                    new XAttribute("Attacker", attacker.Attacker.LocationString),
                                    new XAttribute("SlowestUnit", attacker.SlowestUnit.Type)))))));
                            
            return output.ToString();
        }
        #endregion
    }
}
