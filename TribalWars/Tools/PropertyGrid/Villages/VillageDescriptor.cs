using System.Collections.Generic;
using System.ComponentModel;
using TribalWars.Villages;

namespace TribalWars.Tools.PropertyGrid.Villages
{
    /// <summary>
    /// Village descriptor for a PropertyGrid
    /// </summary>
    [TypeConverter(typeof(PropertySorter)), Editor(typeof(VillagePointerUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class VillageDescriptor : IEnumerable<Village>
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
        public VillageDescriptor(Village vil)
        {
            _village = vil;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return Village.PointsWithDiff;
        }
        #endregion

        #region Properties
        [Category(PROPERTY_CATEGORY), PropertyOrder(30)]
        public string Location
        {
            get { return Village.LocationString; }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(40)]
        public string Kingdom
        {
            get { return "K" + Village.Kingdom; }
            set { }
        }

        [Category(PROPERTY_CATEGORY), PropertyOrder(50), Editor(typeof(ClipboardCopierUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BBCode
        {
            get { return Village.BbCode(); }
            set { }
        }

        [Browsable(false)]
        public string Points
        {
            // Used by the VillagePropertyDescriptor
            get { return Village.PointsWithDiff; }
            set { }
        }
        #endregion

        #region IEnumerable<Village> Members
        public IEnumerator<Village> GetEnumerator()
        {
            yield return Village;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
