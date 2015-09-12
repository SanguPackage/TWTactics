using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls;
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
                var cmd = menu.AddCommand(string.Format(ControlsRes.AttackersPoolContextMenu_AddXToPool, _villages.Count), OnAddAttackers, Resources.FlagGreen);
                cmd.ToolTipText = ControlsRes.AttackersPoolContextMenu_AddXVillagesTooltip;
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
