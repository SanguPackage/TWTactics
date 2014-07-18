using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    public class AttackEventArgs : EventArgs
    {
        public AttackPlanInfo Plan { get; private set; }

        public AttackEventArgs(AttackPlanInfo plan)
        {
            Plan = plan;
        }
    }
}
