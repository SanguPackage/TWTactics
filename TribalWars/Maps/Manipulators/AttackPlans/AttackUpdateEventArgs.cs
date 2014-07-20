using System;
using System.Collections.Generic;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// Update an existing <see cref="AttackPlan" />
    /// </summary>
    public class AttackUpdateEventArgs : EventArgs
    {
        public enum ActionKind
        {
            Add,
            Delete
        }

        public IEnumerable<AttackPlanFrom> AttackFrom { get; private set; }

        public ActionKind Action { get; private set; }

        public static AttackUpdateEventArgs AddAttackFrom(AttackPlanFrom attackFrom)
        {
            return new AttackUpdateEventArgs(new[] {attackFrom}, ActionKind.Add);
        }

        public static AttackUpdateEventArgs AddAttacksFrom(IEnumerable<AttackPlanFrom> attackFrom)
        {
            return new AttackUpdateEventArgs(attackFrom, ActionKind.Add);
        }

        public static AttackUpdateEventArgs DeleteAttacksFrom(IEnumerable<AttackPlanFrom> attackFrom)
        {
            return new AttackUpdateEventArgs(attackFrom, ActionKind.Delete);
        }

        public static AttackUpdateEventArgs DeleteAttackFrom(AttackPlanFrom attackFrom)
        {
            return new AttackUpdateEventArgs(new[] { attackFrom }, ActionKind.Delete);
        }

        private AttackUpdateEventArgs(IEnumerable<AttackPlanFrom> attackFrom, ActionKind action)
        {
            AttackFrom = attackFrom;
            Action = action;
        }
    }
}