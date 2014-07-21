using System;
using System.Diagnostics;

namespace TribalWars.Maps.Manipulators.AttackPlans.EventArg
{
    /// <summary>
    /// Add, Remove or Select an attackplan
    /// </summary>
    public class AttackEventArgs : EventArgs
    {
        /// <summary>
        /// The selected plan
        /// </summary>
        public AttackPlan Plan { get; private set; }

        /// <summary>
        /// The selected attack within the plan
        /// </summary>
        public AttackPlanFrom Attacker { get; private set; }

        public AttackEventArgs(AttackPlan plan, AttackPlanFrom attacker)
        {
            Debug.Assert(attacker == null || attacker.Plan == plan);
            Attacker = attacker;
            Plan = plan;
        }
    }
}
