using System;
using System.ComponentModel;
using TribalWars.Data.Players;
using TribalWars.Data.Villages;

namespace TribalWars.Tools.PropertyGrid.Villages
{
    [TypeConverter(typeof(PropertySorter))]
    public class VillagePropertyDescriptor : PropertyDescriptor
    {
        #region Fields
        private readonly VillageDescriptor village;
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
            village = new VillageDescriptor(vil);
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
