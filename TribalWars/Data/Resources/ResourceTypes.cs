using System;
using System.Collections.Generic;
using System.Text;

namespace TribalWars.Data.Resources
{
    /// <summary>
    /// The list of all resources available in Tribal Wars
    /// </summary>
    public enum ResourceTypes
    {
        NotSet = -1,
        Wood,
        /// <summary>
        /// This used to be 'stone'
        /// </summary>
        Clay,
        Iron,
        /// <summary>
        /// People
        /// </summary>
        Face,
        All
    }
}
