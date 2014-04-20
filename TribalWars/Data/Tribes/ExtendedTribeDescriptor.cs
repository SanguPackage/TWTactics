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
        private const string PropertyCategory = "Tribe";
        #endregion

        #region Fields
        [Browsable(false)]
        public Tribe Tribe { get; private set; }
        #endregion

        #region Constructors
        public ExtendedTribeDescriptor(Tribe tribe)
        {
            Tribe = tribe;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return Tribe.Tag;
        }
        #endregion

        #region Properties
        [Category(PropertyCategory), Tools.PropertyOrder(5)]
        public string Tag
        {
            get { return Tribe.Tag; }
            set { }
        }

        [Category(PropertyCategory), Tools.PropertyOrder(10)]
        public string Name
        {
            get { return Tribe.Name; }
            set { }
        }

        [Category(PropertyCategory), Tools.PropertyOrder(40)]
        public string Points
        {
            get { return Tribe.AllPoints.ToString("#,0"); }
            set { }
        }

        [Category(PropertyCategory), TypeConverter(typeof(ExpandableObjectConverter)), Tools.PropertyOrder(45)]
        public PlayerCollection Players
        {
            get { return new PlayerCollection(Tribe.Players, false); }
            set { }
        }

        [Category(PropertyCategory), Tools.PropertyOrder(50)]
        public string Rank
        {
            get { return Tribe.Rank.ToString("#,0"); }
            set { }
        }

        [Category(PropertyCategory), Tools.PropertyOrder(60), Editor(typeof(ClipboardCopierUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BBCode
        {
            get { return Tribe.BbCode(); }
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
