using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
        Comments = 32,
        /// <summary>
        /// Offense village with catapults
        /// </summary>
        Catapult = 64
    }

    public static class VillageTypeHelper
    {
        /// <summary>
        /// Convert the imageList index back to a VillageType
        /// </summary>
        public static VillageType GetVillageType(int imageListIndex)
        {
            var enumValues = ((VillageType[])Enum.GetValues(typeof(VillageType)));
            return enumValues[imageListIndex]; // This breaks if GetImageList changes!
        }

        /// <summary>
        /// VillageType imagelist for an ImageCombobox
        /// </summary>
        public static ImageList GetImageList()
        {
            var enumValues = ((VillageType[])Enum.GetValues(typeof (VillageType)));
            IEnumerable<Image> images = new Image[] { new Bitmap(18, 18) };
            images = images.Concat(enumValues.Select(x => x.GetImage(true)).Where(x => x != null));

            var list = new ImageList();
            list.Images.AddRange(images.ToArray());
            return list;
        }

        /// <summary>
        /// Gets the most important image
        /// (in case multiple flags are active)
        /// </summary>
        public static Image GetImage(this VillageType type, bool displayComments)
        {
            if (type.HasFlag(VillageType.Noble))
                return UnitImages.Noble;

            if (type.HasFlag(VillageType.Attack))
                return UnitImages.Axe;

            if (type.HasFlag(VillageType.Catapult))
                return UnitImages.Catapult;

            if (type.HasFlag(VillageType.Defense))
                return Properties.Resources.Defense;

            if (type.HasFlag(VillageType.Scout))
                return UnitImages.Scout;

            if (type.HasFlag(VillageType.Farm))
                return BuildingImages.Farm;

            if (displayComments && type.HasFlag(VillageType.Comments))
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

            if (type.HasFlag(VillageType.Catapult))
                return "Catapults";

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
