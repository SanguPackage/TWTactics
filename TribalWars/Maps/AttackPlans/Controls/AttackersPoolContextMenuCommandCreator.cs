using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Janus.Windows.UI.CommandBars;
using TribalWars.Properties;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Villages;
using TribalWars.Worlds;

namespace TribalWars.Maps.AttackPlans.Controls
{
    /// <summary>
    /// Add villages to the AttackersPool
    /// </summary>
    public class AttackersPoolContextMenuCommandCreator
    {
        private readonly ICollection<Village> _villages;

        private AttackersPoolContextMenuCommandCreator(UIContextMenu menu, IEnumerable<Village> villages)
        {
            _villages = villages.ToArray();

            if (_villages.Any())
            {
                var cmd = menu.AddCommand(string.Format("Add {0} villages to attackers pool", _villages.Count), OnAddAttackers, Resources.FlagGreen);
                cmd.ToolTipText = "When using the attack plan search function, don't search through all your villages but only select villages from those added to the 'attackers pool'.";
            }
        }

        public static void Add(UIContextMenu menu, IEnumerable<Village> villages)
        {
            new AttackersPoolContextMenuCommandCreator(menu, villages);
        }

        private void OnAddAttackers(object sender, EventArgs e)
        {
            World.Default.Map.Manipulators.AttackManipulator.AddToAttackersPool(_villages);
        }
    }
}
