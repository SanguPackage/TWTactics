#region Using
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using TribalWars.Controls;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Manipulators.AttackPlans
{
    /// <summary>
    /// The managing attackmanipulator
    /// </summary>
    public class AttackManipulatorManager : DefaultManipulatorManager
    {
        #region Fields
        private readonly AttackManipulator _attacker;
        #endregion

        #region Constructors
        public AttackManipulatorManager(Map map)
            : base(map)
        {
            UseLegacyXmlWriter = false;

            // Active manipulators
            _attacker = new AttackManipulator(map);
            _manipulators.Add(_attacker);

            MapMover.RightClickToMove = false;
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
