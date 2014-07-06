using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using TribalWars.Data.Descriptors;

namespace TribalWars.Data.Villages
{
    /// <summary>
    /// Village descriptor for a PropertyGrid
    /// </summary>
    [TypeConverter(typeof(Tools.PropertySorter)), Editor(typeof(VillagePointerUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
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
            return Village.Points.ToString("#,0") + " points";
        }
        #endregion

        #region Properties
        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(30)]
        public string Location
        {
            get { return Village.LocationString; }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(40)]
        public string Kingdom
        {
            get { return "K" + Village.Kingdom; }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(50), Editor(typeof(ClipboardCopierUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BBCode
        {
            get { return Village.BbCode(); }
            set { }
        }

        [Browsable(false)]
        public string Points
        {
            // Used by the VillagePropertyDescriptor
            get { return Village.Points.ToString("#,0"); }
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
