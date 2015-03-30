using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using TribalWars.Villages;

namespace TribalWars.Tools.PropertyGrid.Villages
{
    /// <summary>
    /// Represents a list of villages in a PropertyGrid
    /// </summary>
    [Editor(typeof(VillagePointerUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class VillageCollection : ICustomTypeDescriptor, IEnumerable<Village>
    {
        #region Fields
        private List<Village> list;
        private Player ply;
        #endregion

        #region Properties
        /// <summary>
        /// Gets an enumeration of villages
        /// </summary>
        public IEnumerable<Village> Villages
        {
            get { return list; }
        }
        #endregion

        #region Constructors
        public VillageCollection(Player player)
        {
            list = player.Villages;
            ply = player;

        }
        #endregion

        #region BBCode
        public override string ToString()
        {
            if (ply != null && ply.PreviousPlayerDetails != null)
            {
                var conquer = ply.ConquerString;
                if (!string.IsNullOrWhiteSpace(conquer))
                {
                    return string.Format("{0} ({1})", list.Count, conquer);
                }
            }
            return string.Format("{0}", list.Count);
        }
        #endregion

        #region Implemented ICustomTypeDescriptor Members
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            // This sort does not work by itself:
            // Needs: PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            list.Sort();
            PropertyDescriptorCollection desc = new PropertyDescriptorCollection(null);
            for (int i = 0; i < list.Count; i++)
            {
                VillagePropertyDescriptor vil = new VillagePropertyDescriptor(list[i]);
                desc.Add(vil);
            }
            return desc;
        }
        #endregion

        #region Other ICustomTypeDescriptor Members
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
        #endregion

        #region IEnumerable<Village> Members
        public IEnumerator<Village> GetEnumerator()
        {
            return Villages.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
