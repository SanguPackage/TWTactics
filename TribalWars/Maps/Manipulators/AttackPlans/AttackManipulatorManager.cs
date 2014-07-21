#region Using
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using TribalWars.Controls;
using TribalWars.Maps.Manipulators.Implementations;
using TribalWars.Maps.Manipulators.Managers;
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
        public override IContextMenu GetContextMenu(Point location, Village village)
        {
            if (village != null && village.Player == World.Default.You)
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

        public IEnumerable<AttackPlan> GetPlans()
        {
            return _attacker.GetPlans();
        }
    }
}
