using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Villages;
using TribalWars.Worlds;
using System.Linq;
using TribalWars.Worlds.Events;

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// Data holder for one attack plan (= one target)
    /// </summary>
    public class AttackPlan
    {
        #region Fields
        private readonly List<AttackPlanFrom> _attacks;
        #endregion

        #region Properties
        /// <summary>
        /// The village we will attack (or defend)
        /// </summary>
        public Village Target { get; set; }

        /// <summary>
        /// Arrival time for the troops
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Villages attacking the target village
        /// </summary>
        public IEnumerable<AttackPlanFrom> Attacks
        {
            get { return _attacks; }
        }
        #endregion

        #region Constructors
        public AttackPlan(Village target, DateTime? arrivalTime)
        {
            _attacks = new List<AttackPlanFrom>();
            Target = target;
            if (arrivalTime.HasValue)
            {
                ArrivalTime = arrivalTime.Value;
            }
            else
            {
                ArrivalTime = World.Default.Settings.ServerTime.AddHours(8);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Pinpoint the plan on the main map
        /// </summary>
        public void Pinpoint(AttackPlanFrom activeAttacker)
        {
            World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Attack);
            var villages = Attacks.SelectMany(x => x.Attacker).Union(Target).ToArray();
            if (activeAttacker != null)
            {
                World.Default.Map.EventPublisher.AttackSelect(this, activeAttacker);
            }
            else
            {
                World.Default.Map.EventPublisher.AttackSelect(this, this);
            }
            World.Default.Map.SetCenter(villages);
        }

        public void AddAttacker(AttackPlanFrom attacker)
        {
            _attacks.Add(attacker);
        }

        public void RemoveAttack(AttackPlanFrom attacker)
        {
            _attacks.Remove(attacker);
        }

        public override string ToString()
        {
            return string.Format("Target={0}, ArrivalTime={1}, Attacks={2}", Target.LocationString, ArrivalTime.ToString("dd/MM hh:mm:ss"), Attacks.Count());
        }
        #endregion
    }
}