#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using TribalWars.Controls;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Villages;
using TribalWars.Villages.Units;
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
        public override string WriteXml()
        {
            return _attacker.WriteXml();
        }

        public override void ReadXml(XDocument doc)
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
