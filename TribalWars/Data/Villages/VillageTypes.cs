using System;
using System.Collections.Generic;
using System.Drawing;
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

    public static class VillageTypeHelper
    {
        /// <summary>
        /// Gets the most important image
        /// (in case multiple flags are active)
        /// </summary>
        public static Image GetImage(this VillageType type)
        {
            if (type.HasFlag(VillageType.Noble))
                return Units.Images.Noble;

            if (type.HasFlag(VillageType.Attack))
                return Units.Images.Axe;

            if (type.HasFlag(VillageType.Defense))
                return Properties.Resources.Defense;

            if (type.HasFlag(VillageType.Scout))
                return Units.Images.Scout;

            if (type.HasFlag(VillageType.Farm))
                return Buildings.Images.Farm;

            if (type.HasFlag(VillageType.Comments))
                return Maps.Icons.Other.Note;

            return null;
        }

        /// <summary>
        /// Gets the description of the most important type
        /// </summary>
        public static string GetDescription(this VillageType type)
        {
            if (type.HasFlag(VillageType.Noble))
                return "Nobles";

            if (type.HasFlag(VillageType.Attack))
                return "Offensive";

            if (type.HasFlag(VillageType.Defense))
                return "Defensive";

            if (type.HasFlag(VillageType.Scout))
                return "Scouts";

            if (type.HasFlag(VillageType.Farm))
                return "Farm";

            if (type.HasFlag(VillageType.Comments))
                return "Comments";

            return null;
        }
    }
}
