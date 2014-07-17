using System;
using System.Collections.Generic;
using TribalWars.Villages;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    public class AttackPlanInfo
    {
        public Village Target { get; set; }

        public DateTime ArrivalTime { get; set; }

        public List<AttackPlanFrom> Attacks { get; private set; }

        public AttackPlanInfo()
        {
            Attacks = new List<AttackPlanFrom>();
        }

        public override string ToString()
        {
            return string.Format("Target={0}, ArrivalTime={1}, Attacks={2}", Target.LocationString, ArrivalTime.ToString("dd/MM hh:mm:ss"), Attacks.Count);
        }
    }
}