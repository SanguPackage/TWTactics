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
        #region Action Enum
        public enum ActionKind
        {
            /// <summary>
            /// Add an attacker
            /// </summary>
            Add,
            /// <summary>
            /// Delete an attacker
            /// </summary>
            Delete,
            /// <summary>
            /// Update an attacker or the AttackPlan itself
            /// </summary>
            Update
        }
        #endregion

        #region Properties
        public IEnumerable<AttackPlanFrom> AttackFrom { get; private set; }

        public ActionKind Action { get; private set; }
        #endregion

        #region Constructors
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
        #endregion

        public override string ToString()
        {
            return string.Format("{0}, AttackFromCount={1}", Action, AttackFrom.Count());
        }
    }
}