#region Using
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls;
using TribalWars.Maps.Manipulators.AttackPlans.Controls;
using TribalWars.Maps.Manipulators.Implementations;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Villages;
using TribalWars.Worlds;
#endregion

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// The managing attackmanipulator
    /// </summary>
    public class AttackManipulatorManager : ManipulatorManagerBase
    {
        #region Fields
        private readonly AttackManipulator _attacker;
        #endregion

        #region Properties
        /// <summary>
        /// Global attack planner configuration
        /// </summary>
        public AttackManipulator.SettingsInfo Settings
        {
            get { return _attacker.Settings; }
        }
        #endregion

        #region Constructors
        public AttackManipulatorManager(Map map)
            : base(map, true)
        {
            UseLegacyXmlWriter = false;

            // Active manipulators
            var mover = new MapMoverManipulator(map, false, false, true);
            var dragger = new MapDraggerManipulator(map, this);
            _attacker = new AttackManipulator(map);

            _manipulators.Add(_attacker);
            _manipulators.Add(mover);
            _manipulators.Add(dragger);
        }
        #endregion

        #region Methods
        public IEnumerable<AttackPlan> GetPlans()
        {
            return _attacker.GetPlans();
        }

        /// <summary>
        /// Gets the first plan where the village is either the target
        /// or one of the attackers
        /// </summary>
        public AttackPlan GetPlan(Village village, out bool isActivePlan, out AttackPlanFrom attacker)
        {
            var plan = _attacker.GetPlan(village, out attacker);
            isActivePlan = plan == _attacker.ActivePlan;
            return plan;
        }

        public override IContextMenu GetContextMenu(Point location, Village village)
        {
            if (village == null)
            {
                return new NoVillageAttackContextMenu(this);
            }

            if (village.Player == World.Default.You)
            {
                return null;
            }

            return base.GetContextMenu(location, village);
        }
        #endregion

        #region Persistence
        protected override string WriteXmlCore()
        {
            return _attacker.WriteXml();
        }

        protected override void ReadXmlCore(XDocument doc)
        {
            _attacker.ReadXml(doc);
        }
        #endregion
    }
}
