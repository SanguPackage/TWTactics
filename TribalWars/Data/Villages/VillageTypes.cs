using System;
using System.Collections.Generic;
using System.Text;

namespace TribalWars.Data.Villages
{
    /// <summary>
    /// Values indicating the troop content
    /// </summary>
    [Flags]
    public enum VillageType
    {
        /// <summary>
        /// No types assigned yet
        /// </summary>
        None = 0,
        /// <summary>
        /// An offensive village
        /// </summary>
        Attack = 1,
        /// <summary>
        /// A defensive village
        /// </summary>
        Defense = 2,
        /// <summary>
        /// A village with nobles
        /// </summary>
        Noble = 4,
        /// <summary>
        /// A village with scouts
        /// </summary>
        Scout = 8,
        /// <summary>
        /// A village you will be farming
        /// </summary>
        Farm = 16,
        /// <summary>
        /// A village with user-defined comments
        /// </summary>
        Comments = 32
    }
}
