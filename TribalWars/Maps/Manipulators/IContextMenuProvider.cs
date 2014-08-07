using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TribalWars.Controls;
using TribalWars.Villages;
using TribalWars.Villages.ContextMenu;
using TribalWars.Worlds;

namespace TribalWars.Maps.Manipulators
{
    /// <summary>
    /// The ContextMenu for Manipulators
    /// </summary>
    public interface IContextMenuProvider
    {
        IContextMenu GetContextMenu(Point location, Village village);
    }

    /// <summary>
    /// The default implementation of the <see cref="IContextMenuProvider"/>
    /// </summary>
    public static class ContextMenuProvider
    {
        public static IContextMenu DefaultProvider(Map map, Point location, Village village)
        {
            if (village != null)
            {
                return new VillageContextMenu(map, village);
            }
            Point gameLocation = World.Default.Map.Display.GetGameLocation(location);
            return new NoVillageContextMenu(gameLocation);
        }
    }
}
