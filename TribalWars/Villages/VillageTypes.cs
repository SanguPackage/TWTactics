using System;
using System.Drawing;
using TribalWars.Maps.Icons;
using TribalWars.Villages.Buildings;
using TribalWars.Villages.Units;

namespace TribalWars.Villages
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
                return UnitImages.Noble;

            if (type.HasFlag(VillageType.Attack))
                return UnitImages.Axe;

            if (type.HasFlag(VillageType.Defense))
                return Properties.Resources.Defense;

            if (type.HasFlag(VillageType.Scout))
                return UnitImages.Scout;

            if (type.HasFlag(VillageType.Farm))
                return BuildingImages.Farm;

            if (type.HasFlag(VillageType.Comments))
                return Other.Note;

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
