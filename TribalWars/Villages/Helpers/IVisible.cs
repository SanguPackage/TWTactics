using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TribalWars.Maps;

namespace TribalWars.Villages.Helpers
{
    /// <summary>
    /// Is at least one village of the entity
    /// visible on a TW Map
    /// </summary>
    public interface IVisible
    {
        bool IsVisible(Map map);
    }
}
