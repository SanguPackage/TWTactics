using System;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// Add, Remove or Select an attackplan
    /// </summary>
    public class AttackEventArgs : EventArgs
    {
        public AttackPlan Plan { get; private set; }

        public AttackEventArgs(AttackPlan plan)
        {
            Plan = plan;
        }
    }
}
