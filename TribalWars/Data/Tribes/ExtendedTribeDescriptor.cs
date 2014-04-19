using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using TribalWars.Data.Villages;
using TribalWars.Data.Players;
using TribalWars.Data.Descriptors;

namespace TribalWars.Data.Tribes
{
    /// <summary>
    /// Stand alone descriptor
    /// </summary>
    [TypeConverter(typeof(Tools.PropertySorter))]
    public class ExtendedTribeDescriptor : IEnumerable<Village>
    {
        #region Constants
        private const string PROPERTY_CATEGORY = "Tribe";
        #endregion

        #region Fields
        private Tribe tribe;

        [Browsable(false)]
        public Tribe Tribe
        {
            get { return tribe; }
        }
        #endregion

        #region Constructors
        public ExtendedTribeDescriptor(Tribe tribe)
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
        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(5)]
        public string Tag
        {
            get { return tribe.Tag; }
            set { }
        }

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

        [Category(PROPERTY_CATEGORY), TypeConverter(typeof(ExpandableObjectConverter)), Tools.PropertyOrder(45)]
        public PlayerCollection Players
        {
            get { return new PlayerCollection(tribe.Players, false); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(50)]
        public string Rank
        {
            get { return tribe.Rank.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(60), Editor(typeof(ClipboardCopierUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
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
