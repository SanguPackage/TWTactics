#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TribalWars.Data.Maps.Views
{
    /// <summary>
    /// The different types of views (points, group, ...)
    /// </summary>
    public enum Types
    {
        /// <summary>
        /// View based on village points
        /// </summary>
        Points = 1,
        /// <summary>
        /// Offense, defense, nobles villages
        /// </summary>
        VillageType = 2,
        /// <summary>
        /// Defense inside the village decides border
        /// </summary>
        Defense = 3,
        /// <summary>
        /// An XMark view or the topleft village circle
        /// </summary>
        Marked = 4,
        /// <summary>
        /// User defined views
        /// </summary>
        Custom = 5
    }
}
