using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using TribalWars.Data.Descriptors;

namespace TribalWars.Data.Villages
{
    /// <summary>
    /// Village descriptor for a PropertyGrid
    /// </summary>
    [TypeConverter(typeof(Tools.PropertySorter))]
    public class ExtendedVillageDescriptor
    {
        #region Constants
        protected const string PROPERTY_CATEGORY = "Village";
        #endregion

        #region Fields
        private Village _village;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the underlying village
        /// </summary>
        [Browsable(false)]
        public Village Village
        {
            get { return _village; }
        }
	
        #endregion

        #region Constructors
        public ExtendedVillageDescriptor(Village vil)
        {
            _village = vil;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return Village.Points.ToString("#,0") + " points";
        }
        #endregion

        #region Properties
        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(10)]
        public string Name
        {
            get { return Village.Name; }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(12)]
        public string Points
        {
            get { return Village._Points.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(15), Editor(typeof(VillagePointerUIEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Location
        {
            get { return Village.LocationString; }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(20)]
        public VillagePlayerDescriptor Player
        {
            get
            {
                if (Village.HasPlayer)
                    return new VillagePlayerDescriptor(Village.Player);
                else
                    return null;
            }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(25)]
        public TribeDescriptor Tribe
        {
            get
            {
                if (Village.HasTribe)
                    return new TribeDescriptor(Village.Player.Tribe);
                else
                    return null;
            }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(50), Editor(typeof(ClipboardCopierUIEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BBCode
        {
            get { return Village.BBCode(); }
            set { }
        }
        #endregion
    }
}
