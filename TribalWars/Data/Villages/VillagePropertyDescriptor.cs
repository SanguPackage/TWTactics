using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using TribalWars.Data.Players;

namespace TribalWars.Data.Villages
{
    [TypeConverter(typeof(Tools.PropertySorter))]
    public class VillagePropertyDescriptor : PropertyDescriptor
    {
        #region Fields
        private VillageDescriptor village = null;
        #endregion

        #region Properties
        public Village Village
        {
            get { return village.Village; }
        }
        #endregion

        #region Constructors
        public VillagePropertyDescriptor(Village vil)
            : base(vil.Name, null)
        {
            this.village = new VillageDescriptor(vil);
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return village.Points;
        }
        #endregion

        #region PropertyDescriptor Methods
        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return typeof(Player); }
        }

        public override object GetValue(object component)
        {
            return village;
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get { return village.GetType(); }
        }

        public override void ResetValue(object component)
        {

        }

        public override void SetValue(object component, object value)
        {

        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
        #endregion
    }
}
