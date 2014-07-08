using System.Collections.Generic;
using System.ComponentModel;
using TribalWars.Data.Tribes;
using TribalWars.Data.Villages;
using TribalWars.Tools.PropertyGrid.Villages;

namespace TribalWars.Tools.PropertyGrid.Tribes
{
    /// <summary>
    /// Descriptor used in ExtendedPlayerDescriptor and ExtendedVillageDescriptor
    /// </summary>
    [TypeConverter(typeof(PropertySorter)), Editor(typeof(VillagePointerUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
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
        [Category(PROPERTY_CATEGORY), PropertyOrder(10)]
        public string Name
        {
            get { return tribe.Name; }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(40)]
        public string Points
        {
            get { return tribe.AllPoints.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(50)]
        public string Rank
        {
            get { return tribe.Rank.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(55)]
        public string Players
        {
            get { return tribe.Players.Count.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(60), Editor(typeof(ClipboardCopierUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BBCode
        {
            get { return tribe.BbCode(); }
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
