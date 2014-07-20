using System;
using System.Collections.Generic;
using System.Linq;

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
            Delete,
            Update
        }

        public IEnumerable<AttackPlanFrom> AttackFrom { get; private set; }

        public ActionKind Action { get; private set; }

        public static AttackUpdateEventArgs UpdateAttackFrom(AttackPlanFrom attackFrom)
        {
            return new AttackUpdateEventArgs(new[] { attackFrom }, ActionKind.Update);
        }

        public static AttackUpdateEventArgs AddAttackFrom(AttackPlanFrom attackFrom)
        {
            return new AttackUpdateEventArgs(new[] {attackFrom}, ActionKind.Add);
        }

        public static AttackUpdateEventArgs DeleteAttacksFrom(IEnumerable<AttackPlanFrom> attackFrom)
        {
            return new AttackUpdateEventArgs(attackFrom, ActionKind.Delete);
        }

        public static AttackUpdateEventArgs DeleteAttackFrom(AttackPlanFrom attackFrom)
        {
            return new AttackUpdateEventArgs(new[] { attackFrom }, ActionKind.Delete);
        }

        public static AttackUpdateEventArgs Update()
        {
            return new AttackUpdateEventArgs(new AttackPlanFrom[0], ActionKind.Update);
        }

        private AttackUpdateEventArgs(IEnumerable<AttackPlanFrom> attackFrom, ActionKind action)
        {
            AttackFrom = attackFrom.ToArray();
            Action = action;
        }

        public override string ToString()
        {
            return string.Format("{0}, AttackFromCount={1}", Action, AttackFrom.Count());
        }

        
    }
}