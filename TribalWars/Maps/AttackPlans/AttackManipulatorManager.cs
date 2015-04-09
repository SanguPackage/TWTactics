#region Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
        #region Constants
        /// <summary>
        /// When searching for the fastest villages that can still make it for
        /// the given travel time, add this many possible attackers per click
        /// </summary>
        private const int AutoFindAmountOfAttackers = 10;

        /// <summary>
        /// When searching for the fastest villages that can still make it for
        /// the given travel time, add this many possible attackers per click
        /// when none are found that can still reach in time
        /// </summary>
        private const int AutoFindAmountOfAttackersWhenNone = 3;

        private const int DefaultArrivalTimeServerOffset = 8;

        /// <summary>
        /// Auto find functionality: only add attackers that have
        /// more then this amount in seconds left before 'send time'
        /// </summary>
        private const int AutoFindMinimumAmountOfSecondsLeft = 0;
        #endregion

        #region Fields
        private readonly AttackManipulator _attacker;
        private readonly List<Village> _attackersPool;
        #endregion

        #region Properties
        public UnitTypes DefaultSpeed { get; set; }

        /// <summary>
        /// Global attack planner configuration
        /// </summary>
        public AttackManipulator.SettingsInfo Settings
        {
            get { return _attacker.Settings; }
        }

        public bool IsAttackersPoolEmpty
        {
            get { return !_attackersPool.Any(); }
        }
        #endregion

        #region Constructors
        public AttackManipulatorManager(Map map)
            : base(map, true)
        {
            _attackersPool = new List<Village>();

            UseLegacyXmlWriter = false;

            // Active manipulators
            var mover = new MapMoverManipulator(map, false, false, true);
            var dragger = new MapDraggerManipulator(map, this);
            _attacker = new AttackManipulator(map);

            AddManipulator(_attacker);
            AddManipulator(mover);
            AddManipulator(dragger);
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
        public AttackPlan GetPlan(Village village, out bool isActivePlan, out AttackPlanFrom attacker, bool cycleVillage)
        {
            var plan = _attacker.GetPlan(village, out attacker, cycleVillage);
            isActivePlan = plan == _attacker.ActivePlan;
            return plan;
        }

        protected override SuperTipSettings BuildTooltip(Village village)
        {
            AttackPlanFrom attacker;
            AttackPlan plan = _attacker.GetPlan(village, out attacker, false);
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
                str.Append(Environment.NewLine);
                str.AppendFormat("Attacker: {0}", attacker.Attacker);
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

                    if (!string.IsNullOrWhiteSpace(plan.Comments))
                    {
                        settings.FooterText = plan.Comments;
                        settings.FooterImage = Other.Note;
                    }
                }
            }

            settings.Text = str.ToString();

            if (!string.IsNullOrEmpty(village.Tooltip.Footer))
            {
                if (!string.IsNullOrWhiteSpace(settings.FooterText))
                {
                    settings.FooterText += "\n\n";
                    settings.FooterText += village.Tooltip.Footer;
                }
                else
                {
                    settings.FooterText = village.Tooltip.Footer;
                }
                
                settings.FooterImage = village.Tooltip.FooterImage;
            }
            return settings;
        }

        public override IContextMenu GetContextMenu(Point location, Village village)
        {
            if (village == null)
            {
                return new NoVillageAttackContextMenu(this);
            }

            if (village.Player == World.Default.You && (_attacker.ActivePlan == null || _attacker.ActivePlan.Target != village))
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

        #region Finding Attackers
        public void AddToAttackersPool(IEnumerable<Village> villages)
        {
            villages = villages.Where(x => !_attackersPool.Contains(x));
            _attackersPool.AddRange(villages.Distinct());
        }

        public IEnumerable<Village> GetAttackersFromYou(AttackPlan plan, Unit slowestUnit)
        {
            return GetAttackers(World.Default.You, plan, slowestUnit);
        }

        public IEnumerable<Village> GetAttackersFromPool(AttackPlan plan, Unit slowestUnit, out bool depleted)
        {
            Village[] attackers = GetAttackers(_attackersPool, plan, slowestUnit).ToArray();
            _attackersPool.RemoveAll(attackers.Contains);

            depleted = !_attackersPool.Any();
            return attackers;
        }

        private IEnumerable<Village> GetAttackers(IEnumerable<Village> searchIn, AttackPlan plan, Unit slowestUnit)
        {
            Village[] villagesAlreadyUsed = GetPlans().SelectMany(x => x.Attacks)
                                      .Select(x => x.Attacker)
                                      .ToArray();

            var villagesWithTimeLeft =
               (from village in searchIn
                where !villagesAlreadyUsed.Contains(village)
                let travelTime = Village.TravelTime(plan.Target, village, slowestUnit)
                let timeBeforeNeedToSend = plan.ArrivalTime - World.Default.Settings.ServerTime.Add(travelTime)
                select new
                {
                    Village = village,
                    TimeBeforeNeedToSend = timeBeforeNeedToSend
                })
                .OrderBy(x => x.TimeBeforeNeedToSend)
                .ToArray();

            if (!villagesWithTimeLeft.Any(x => x.TimeBeforeNeedToSend.TotalSeconds > AutoFindMinimumAmountOfSecondsLeft))
            {
                return villagesWithTimeLeft.OrderByDescending(x => x.TimeBeforeNeedToSend).Take(AutoFindAmountOfAttackersWhenNone).Select(x => x.Village);
            }
            else
            {
                return villagesWithTimeLeft.Where(x => x.TimeBeforeNeedToSend.TotalSeconds > AutoFindMinimumAmountOfSecondsLeft).Take(AutoFindAmountOfAttackers).Select(x => x.Village);
            }
        }
        #endregion
    }
}
