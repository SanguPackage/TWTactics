using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// Export AttackPlan(s) as text or BBCode
    /// </summary>
    public class AttackPlanExporter
    {
        #region Constants
        /// <summary>
        /// Amount of attacks to trigger the _warnFor
        /// </summary>
        private const int AmountOfAttacksInQuickSuccessionForWarn = 3;

        /// <summary>
        /// When <see cref="AmountOfAttacksInQuickSuccessionForWarn"/> attacks
        /// need to be sent in this timespan, add a warning
        /// </summary>
        private readonly TimeSpan _warnFor = new TimeSpan(0, 1, 0);

        /// <summary>
        /// Add a critical warning when the next attack needs to be sent
        /// in under less then this amount of time
        /// </summary>
        private readonly TimeSpan _warnForCritical = new TimeSpan(0, 0, 30);
        #endregion

        #region Fields
        private readonly IEnumerable<AttackPlanFrom> _attacks;
        private readonly StringBuilder _str;
        #endregion

        #region Constructors
        private AttackPlanExporter(IEnumerable<AttackPlanFrom> attacks)
        {
            _attacks = attacks;
            _str = new StringBuilder();
        }

        public static string GetMultiPlanTextExport(IEnumerable<AttackPlanFrom> attacks)
        {
            var exporter = new AttackPlanExporter(attacks);
            exporter.AddMultiHeader(false);
            return exporter.GetTextExport(false);
        }

        public static string GetMultiPlanBbCodeExport(IEnumerable<AttackPlanFrom> attacks)
        {
            var exporter = new AttackPlanExporter(attacks);
            exporter.AddMultiHeader(true);
            return exporter.GetBbCodeExport(false);
        }

        public static string GetSinglePlanTextExport(AttackPlan plan)
        {
            var exporter = new AttackPlanExporter(plan.Attacks);
            exporter.AddHeader(plan.Target.ToString(), plan.ArrivalTime);
            return exporter.GetTextExport(true);
        }

        public static string GetSinglePlanBbCodeExport(AttackPlan plan)
        {
            var exporter = new AttackPlanExporter(plan.Attacks);
            exporter.AddHeader(plan.Target.BbCode(), plan.ArrivalTime);
            return exporter.GetBbCodeExport(true);
        }
        #endregion

        #region Implementation
        private string PrintDate(DateTime date)
        {
            return date.ToLongDateString() + " " + date.ToLongTimeString();
        }

        private void AddHeader(string target, DateTime arrivalTime)
        {
            _str.AppendLine("*** Attack Plan ***");
            _str.AppendLine(target);
            _str.AppendLine();

            _str.AppendLine("Arrival time: " + PrintDate(arrivalTime));
            _str.AppendLine("Export time : " + PrintDate(World.Default.Settings.ServerTime));
            _str.AppendLineFormat("Total attacks: {0}", _attacks.Count());
            _str.AppendLine();
        }

        private void AddMultiHeader(bool bbCode)
        {
            _str.AppendLine("*** Attack Plan ***");
            _str.AppendLine("Export time: " + PrintDate(World.Default.Settings.ServerTime));
            _str.AppendLine();

            var plans = _attacks.GroupBy(x => new { x.Plan.Target, x.Plan.ArrivalTime });
            foreach (var plan in plans)
            {
                _str.AppendLine(bbCode ? plan.Key.Target.BbCode() : plan.Key.Target.ToString());
                _str.AppendLine("Arrival time: " + PrintDate(plan.Key.ArrivalTime));
                _str.AppendLineFormat("Total attacks: {0}", plan.Count());
                _str.AppendLine();
            }

            _str.AppendLine("-----");
            _str.AppendLine();
        }

        private AttackPlanFrom[] GetAttackers()
        {
            return _attacks.OrderByDescending(x => x.TravelTime).ToArray();
        }

        private string GetBbCodeExport(bool singleAttackPlan)
        {
            AttackPlanFrom[] attackers = GetAttackers();
            for (int i = 0; i < attackers.Length; i++)
            {
                var attacker = attackers[i];

                if (!singleAttackPlan)
                {
                    _str.AppendLine("Attack " + attacker.Plan.Target.BbCode());
                }

                _str.AppendLine(string.Format("{0} from {1}", attacker.SlowestUnit.BbCodeImage, attacker.Attacker.BbCode()));
                _str.AppendLine("Send on: [b]" + attacker.FormattedSendDate() + "[/b]");
                AddWarning(attackers, attacker.TravelTime, i);
                _str.AppendLine();
            }
            return _str.ToString().Trim();
        }

        private string GetTextExport(bool singleAttackPlan)
        {
            AttackPlanFrom[] attackers = GetAttackers();
            for (int i = 0; i < attackers.Length; i++)
            {
                var attacker = attackers[i];

                if (!singleAttackPlan)
                {
                    _str.AppendLine("Attack " + attacker.Plan.Target);
                }

                _str.AppendLine(string.Format("{0} from {1}", attacker.SlowestUnit.Name, attacker.Attacker));
                _str.AppendLine("Send on: " + attacker.FormattedSendDate());
                AddWarning(attackers, attacker.TravelTime, i);
                _str.AppendLine();
            }
            return _str.ToString().Trim();
        }

        private void AddWarning(IEnumerable<AttackPlanFrom> attackers, TimeSpan currentTravelTime, int index)
        {
            var attacksInQuickSuccession = attackers
                .Skip(index + 1)
                .Select(x => new
                {
                    AttackPlanFrom = x,
                    TimeBetween = currentTravelTime - x.TravelTime
                })
                .Where(x => x.TimeBetween < _warnFor)
                .ToArray();

            if (attacksInQuickSuccession.Any())
            {
                if (attacksInQuickSuccession.Length >= AmountOfAttacksInQuickSuccessionForWarn)
                {
                    _str.AppendLineFormat("ATTN: {0} more attacks coming up soon!", attacksInQuickSuccession.Count());
                }
                else if (attacksInQuickSuccession.First().TimeBetween < _warnForCritical)
                {
                    _str.AppendLineFormat("ATTN: Another attack coming up {0} seconds later!", attacksInQuickSuccession.First().TimeBetween.TotalSeconds);
                }
            }
        }
        #endregion
    }
}
