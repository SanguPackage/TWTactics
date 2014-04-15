using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using TribalWars;
using TribalWars.Data.Villages;
using TribalWars.Data.Descriptors;

namespace TribalWars.Data.Tribes
{
    /// <summary>
    /// Descriptor used in ExtendedPlayerDescriptor and ExtendedVillageDescriptor
    /// </summary>
    [TypeConverter(typeof(Tools.PropertySorter)), Editor(typeof(VillagePointerUIEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class TribeDescriptor : IEnumerable<Village>
    {
        #region Constants
        private const string PROPERTY_CATEGORY = "Tribe";
        #endregion

        #region Fields
        private Tribe tribe;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the underlying Tribe
        /// </summary>
        [Browsable(false)]
        public Tribe Tribe
        {
            get { return tribe; }
        }
        #endregion

        #region Constructors
        public TribeDescriptor(Tribe tribe)
        {
            this.tribe = tribe;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return tribe.Tag;
        }
        #endregion

        #region Properties
        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(10)]
        public string Name
        {
            get { return tribe.Name; }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(40)]
        public string Points
        {
            get { return tribe.AllPoints.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(50)]
        public string Rank
        {
            get { return tribe.Rank.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(55)]
        public string Players
        {
            get { return tribe.Players.Count.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(60), Editor(typeof(ClipboardCopierUIEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BBCode
        {
            get { return tribe.BBCode(); }
            set { }
        }
        #endregion

        #region IEnumerable<Village> Members
        public IEnumerator<Village> GetEnumerator()
        {
            return Tribe.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
