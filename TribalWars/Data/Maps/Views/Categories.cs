#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TribalWars.Data.Maps.Views
{
    /// <summary>
    /// The categories of views
    /// </summary>
    public enum Categories
    {
        /// <summary>
        /// Each village has one background drawer
        /// </summary>
        Background = 0,
        /// <summary>
        /// A village can have additional decorators
        /// </summary>
        Decorator = 1,
    }
}
