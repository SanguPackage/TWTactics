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

        public ICollection<Village> AttackersPool
        {
            get { return _attackersPool; }
        }

        public AttackPlan ActivePlan
        {
            get { return _attacker.ActivePlan; }
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
            settings.HeaderImage = Properties.Resources.FlagGreen;

            var str = new System.Text.StringBuilder();
            if (attacker == null)
            {
                settings.HeaderText = plan.Target.Tooltip.Title;
            }
            else
            {
                settings.HeaderText = attacker.Attacker.Tooltip.Title;

                str.AppendFormat("Target: {0}", plan.Target.Tooltip.Title);
                str.Append(Environment.NewLine);
            }
            str.AppendFormat("Points: {0}", Common.GetPrettyNumber(plan.Target.Points));
            str.Append(Environment.NewLine);
            str.AppendFormat("Arrival date: {0}", plan.ArrivalTime.GetPrettyDate());

            if (attacker != null)
            {
                settings.Image = attacker.SlowestUnit.Image;

                str.Append(Environment.NewLine);
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

            bool isYourVillage = village.HasPlayer && village.Player == World.Default.You;
            bool villageIsNotTheTarget = _attacker.ActivePlan != null && _attacker.ActivePlan.Target != village;

            int villageUsedInAttackCount = _attacker.VillageUsedCount(village);
            bool hasExistingAttacker = villageUsedInAttackCount != 1 || (villageUsedInAttackCount == 1 && !_attacker.IsAddingTarget);
            if (isYourVillage && villageIsNotTheTarget && !hasExistingAttacker)
            {
                // Right click on a village you own = add new attacker
                // So: show no contextmenu
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

        public IEnumerable<Travelfun> GetAttackersFromYou(AttackPlan plan, Unit slowestUnit, VillageType? villageType)
        {
            return GetAttackers(World.Default.You, plan, slowestUnit, villageType);
        }

        public IEnumerable<Travelfun> GetAttackersFromPool(AttackPlan plan, Unit slowestUnit, VillageType? villageType, out bool depleted)
        {
            var attackers = GetAttackers(_attackersPool, plan, slowestUnit, villageType).ToArray();
            _attackersPool.RemoveAll(attackers.Select(x => x.Village).Contains);

            depleted = !_attackersPool.Any();
            return attackers;
        }

        /// <summary>
        /// When looking for attacks on Ram speed, also include villages that won't reach Ram but do Sword/Axe
        /// </summary>
        private static Unit[] GetAcceptableSpeeds(Unit selectedUnit)
        {
            var acceptableSpeeds = new List<Unit>();
            if (selectedUnit == null)
            {
                acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Axe]);
                acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Sword]);
                acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Light]);
                acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Heavy]);
                acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Ram]);
            }
            else
            {
                acceptableSpeeds.Add(selectedUnit);
                switch (selectedUnit.Type)
                {
                    case UnitTypes.Axe:
                        acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Light]);
                        break;

                    case UnitTypes.Spear:
                        acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Heavy]);
                        break;

                    case UnitTypes.Sword:
                        acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Spear]);
                        acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Heavy]);
                        break;

                    case UnitTypes.Knight:
                    case UnitTypes.Snob:
                    case UnitTypes.Spy:
                    case UnitTypes.Catapult:
                        break;

                    case UnitTypes.Ram:
                        acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Axe]);
                        acceptableSpeeds.Add(WorldUnits.Default[UnitTypes.Sword]);
                        break;
                }
            }
            return acceptableSpeeds.ToArray();
        }

        public class Travelfun
        {
            public Village Village { get; set; }
            public Unit Speed { get; set; }
            public TimeSpan TravelTime { get; set; }
            public TimeSpan TimeBeforeNeedToSend { get; set; }
        }

        private IEnumerable<Travelfun> GetAttackers(IEnumerable<Village> searchIn, AttackPlan plan, Unit slowestUnit, VillageType? villageType)
        {
            Unit[] acceptableSpeeds = GetAcceptableSpeeds(slowestUnit);
            Village[] villagesAlreadyUsed = 
                GetPlans().SelectMany(x => x.Attacks)
                    .Select(x => x.Attacker)
                    .ToArray();

            var matchingVillages =
                (from village in searchIn
                 where !villagesAlreadyUsed.Contains(village)
                 select village);

            if (villageType != null)
            {
                matchingVillages = matchingVillages.Where(x => x.Type.HasFlag(villageType));
            }

            var villagesWithAllSpeeds =
                from village in matchingVillages
                from speed in acceptableSpeeds
                let travelTime = Village.TravelTime(plan.Target, village, speed)
                select new Travelfun
                {
                    Village = village,
                    Speed = speed,
                    TravelTime = travelTime,
                    TimeBeforeNeedToSend = plan.ArrivalTime - World.Default.Settings.ServerTime.Add(travelTime)
                };

            var villagesWithBestSpeed =
                villagesWithAllSpeeds
                    .GroupBy(x => x.Village)
                    .Select(x => GetBestSpeedMatch(x.ToList()));

            var villages =
                villagesWithBestSpeed
                    .OrderBy(x => x.TimeBeforeNeedToSend)
                    .ToArray();

            if (!villages.Any(x => x.TimeBeforeNeedToSend.TotalSeconds > AutoFindMinimumAmountOfSecondsLeft))
            {
                return villages.OrderByDescending(x => x.TimeBeforeNeedToSend).Take(AutoFindAmountOfAttackersWhenNone);
            }
            else
            {
                return villages.Where(x => x.TimeBeforeNeedToSend.TotalSeconds > AutoFindMinimumAmountOfSecondsLeft).Take(AutoFindAmountOfAttackers);
            }
        }

        private static Travelfun GetBestSpeedMatch(ICollection<Travelfun> speeds)
        {
            speeds = speeds.OrderBy(t => t.TimeBeforeNeedToSend).ToList();
            if (!speeds.Any(x => x.TimeBeforeNeedToSend.TotalSeconds > AutoFindMinimumAmountOfSecondsLeft))
            {
                return speeds.OrderByDescending(x => x.TimeBeforeNeedToSend).FirstOrDefault();
            }
            else
            {
                return speeds.FirstOrDefault(x => x.TimeBeforeNeedToSend.TotalSeconds > AutoFindMinimumAmountOfSecondsLeft);
            }
        }
        #endregion
    }
}