using TribalWars.Villages;
using TribalWars.Villages.Units;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    public class AttackPlanFrom
    {
        public Village Attacker { get; set; }

        public Unit SlowestUnit { get; set; }

        public override string ToString()
        {
            return string.Format("Attacker={0}, SlowestUnit={1}", Attacker.LocationString, SlowestUnit);
        }
    }
}