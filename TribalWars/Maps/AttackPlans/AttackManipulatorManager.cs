#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using Janus.Windows.Common;
using TribalWars.Controls;
using TribalWars.Maps.AttackPlans.Controls;
using TribalWars.Maps.Icons;
using TribalWars.Maps.Manipulators.Implementations;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.AttackPlans
{
    /// <summary>
    /// The managing attackmanipulator
    /// </summary>
    public class AttackManipulatorManager : ManipulatorManagerBase
    {
        private const int DefaultArrivalTimeServerOffset = 8;

        #region Fields
        private readonly AttackManipulator _attacker;
        #endregion

        #region Properties
        /// <summary>
        /// Global attack planner configuration
        /// </summary>
        public AttackManipulator.SettingsInfo Settings
        {
            get { return _attacker.Settings; }
        }
        #endregion

        #region Constructors
        public AttackManipulatorManager(Map map)
            : base(map, true)
        {
            UseLegacyXmlWriter = false;

            // Active manipulators
            var mover = new MapMoverManipulator(map, false, false, true);
            var dragger = new MapDraggerManipulator(map, this);
            _attacker = new AttackManipulator(map);

            _manipulators.Add(_attacker);
            _manipulators.Add(mover);
            _manipulators.Add(dragger);
        }
        #endregion

        #region Methods
        public DateTime GetDefaultArrivalTime()
        {
            if (_attacker.ActivePlan == null)
            {
                return World.Default.Settings.ServerTime.AddHours(DefaultArrivalTimeServerOffset);
            }
            else
            {
                return _attacker.ActivePlan.ArrivalTime;
            }
        }

        public IEnumerable<AttackPlan> GetPlans()
        {
            return _attacker.GetPlans();
        }

        /// <summary>
        /// Gets the first plan where the village is either the target
        /// or one of the attackers
        /// </summary>
        public AttackPlan GetPlan(Village village, out bool isActivePlan, out AttackPlanFrom attacker)
        {
            var plan = _attacker.GetPlan(village, out attacker);
            isActivePlan = plan == _attacker.ActivePlan;
            return plan;
        }

        protected override SuperTipSettings BuildTooltip(Village village)
        {
            AttackPlanFrom attacker;
            var plan = _attacker.GetPlan(village, out attacker);
            if (plan == null)
            {
                return base.BuildTooltip(village);
            }

            var settings = new SuperTipSettings();
            settings.ToolTipStyle = ToolTipStyle.Standard;
            settings.HeaderText = plan.Target.Tooltip.Title;
            settings.HeaderImage = Properties.Resources.FlagGreen;
            var str = new System.Text.StringBuilder();
            str.AppendFormat("Points: {0}", Common.GetPrettyNumber(plan.Target.Points));
            str.Append(Environment.NewLine);
            str.AppendFormat("Arrival date: {0}", plan.ArrivalTime.GetPrettyDate());

            if (attacker != null)
            {
                settings.Image = attacker.SlowestUnit.Image;
                str.Append(Environment.NewLine);
                str.AppendFormat("Travel time: {0}", attacker.TravelTime);
                str.Append(Environment.NewLine);
                str.AppendFormat("Send on: {0}", attacker.FormattedSendDate());
            }
            else
            {
                str.Append(Environment.NewLine);
                str.Append(Environment.NewLine);
                if (!plan.Attacks.Any())
                {
                    str.AppendFormat("Right click on your own villages to add them as attacking (or defending) villages.");
                    str.AppendLine();
                    str.AppendFormat("Or easier: use the search function (binoculars icon) in the 'Plan Attacks' panel on the left!");

                    if (World.Default.You.Empty)
                    {
                        str.AppendLine();
                        str.AppendLine();
                        str.Append("You have not yet set who you are so right click wont work! You can do so using the menu 'World' -> 'Set Active Player'");
                    }
                }
                else
                {
                    IOrderedEnumerable<AttackPlanFrom> attacks = plan.Attacks.OrderByDescending(x => x.TravelTime);

                    IEnumerable<IGrouping<Unit, AttackPlanFrom>> unitsSent;
                    if (plan.Attacks.Any(x => x.SlowestUnit.Type == UnitTypes.Snob))
                    {
                        settings.Image = WorldUnits.Default[UnitTypes.Snob].Image;
                        unitsSent = attacks.GroupBy(x => x.SlowestUnit);
                    }
                    else
                    {
                        unitsSent = attacks.GroupBy(x => x.SlowestUnit);
                    }

                    foreach (var unitSent in unitsSent)
                    {
                        str.AppendFormat("{0}: {1}", unitSent.Key.Name, unitSent.Count());
                        str.AppendLine();
                    }
                }
            }

            settings.Text = str.ToString();

            if (!string.IsNullOrEmpty(village.Tooltip.Footer))
            {
                settings.FooterText = village.Tooltip.Footer;
                settings.FooterImage = Other.Note;
            }
            return settings;
        }

        public override IContextMenu GetContextMenu(Point location, Village village)
        {
            if (village == null)
            {
                return new NoVillageAttackContextMenu(this);
            }

            if (village.Player == World.Default.You)
            {
                // Right click on a village you own = add new attacker
                return null;
            }

            return base.GetContextMenu(location, village);
        }
        #endregion

        #region Persistence
        protected override string WriteXmlCore()
        {
            return _attacker.WriteXml();
        }

        protected override void ReadXmlCore(XDocument doc)
        {
            _attacker.ReadXml(doc);
        }
        #endregion
    }
}
