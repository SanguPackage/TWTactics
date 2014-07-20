using System;
using System.Text;
using TribalWars.Villages;
using TribalWars.Villages.Units;
using TribalWars.Worlds;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// An attacking/defending village 
    /// </summary>
    public class AttackPlanFrom
    {
        /// <summary>
        /// The plan this attack is part of
        /// </summary>
        public AttackPlan Plan { get; private set; }

        /// <summary>
        /// The attacking village
        /// </summary>
        public Village Attacker { get; set; }

        /// <summary>
        /// The slowest unit in the attack
        /// </summary>
        public Unit SlowestUnit { get; set; }

        /// <summary>
        /// Time from Target to Attacking villages
        /// </summary>
        public TimeSpan TravelTime
        {
            get { return Village.TravelTime(Plan.Target, Attacker, SlowestUnit); }
        }

        public AttackPlanFrom(AttackPlan plan, Village attacker, Unit slowestUnit)
        {
            Plan = plan;
            Attacker = attacker;
            SlowestUnit = slowestUnit;
        }

        /// <summary>
        /// The date the attack needs to be sent to arrive at the specified time
        /// </summary>
        public string FormattedSendDate()
        {
            return Tools.Common.GetPrettyDate(Plan.ArrivalTime - TravelTime);
        }

        /// <summary>
        /// Time left before this attack needs to be sent to arrive at specified time
        /// </summary>
        public TimeSpan GetTimeLeftBeforeSendDate()
        {
            return Plan.ArrivalTime - World.Default.Settings.ServerTime - TravelTime;
        }

        public override string ToString()
        {
            return string.Format("Attacker={0}, SlowestUnit={1}", Attacker.LocationString, SlowestUnit);
        }
    }
}