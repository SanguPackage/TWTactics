#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Villages;
using TribalWars.Tools;
#endregion

namespace TribalWars.Data.Maps.Views
{
    /// <summary>
    /// Village decorator to add an image indicating the village type (Def, Off, ...)
    /// </summary>
    public class VillageTypeView : ViewBase
    {
        #region Fields
        private Dictionary<VillageType, DrawerData> _cache;
        private VillageType[] _importance;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public VillageTypeView(string name)
            : base(name, Types.Points, Categories.Background)
        {
            _cache = new Dictionary<VillageType, DrawerData>();
            _importance = new VillageType[] {VillageType.Attack, VillageType.Defense, VillageType.Scout, VillageType.Farm };
        }
        #endregion

        #region Public Methods
        public override DrawerData GetDrawer(Village village)
        {
            DrawerData data = null;
            if (!_cache.TryGetValue(village.Type, out data))
            {
                // When there are combined village types, we take the most important one
                for (int i = 0; i < _importance.Length; i++)
                {
                    if ((village.Type & _importance[i]) == _importance[i])
                    {
                        data = _cache[_importance[i]];
                        i = _importance.Length + 1;
                    }
                }

                if (data == null)
                {
                    if ((village.Type & VillageType.Noble) == VillageType.Noble || (village.Type & VillageType.Comments) == VillageType.Comments)
                        data = new DrawerData(null, null, null, village.Type);
                }
                else data = new DrawerData(data.ShapeDrawer, data.IconDrawer, data.ExtraDrawerInfo, village.Type);
                _cache.Add(village.Type, data);
            }
            return data;
        }

        public override void AddDrawer(string drawerType, string drawerIcon, int value, object extraValues)
        {
            // Value=VillageType, extraValues=Color for BorderDrawer
            VillageType type = (VillageType)value;
            Color color = XmlHelper.GetColor(extraValues.ToString());
            _cache.Add(type, new DrawerData(drawerType, drawerIcon, color, value));
        }

        public override void AddDrawer(DrawerData drawer, int value)
        {
            throw new NotImplementedException();
        }

        public override void AddDrawer(DrawerData drawer, int value, object extraValues)
        {
            throw new NotImplementedException();
        }
        #endregion

        /*#region TypedViewData
        /// <summary>
        /// View data with typed VillageType value
        /// </summary>
        private class TypedViewData : ViewData
        {
            #region Fields
            private VillageType _value;
            #endregion

            #region Properties
            /// <summary>
            /// Gets the value that decides which DrawerBase to use
            /// for a certain village
            /// </summary>
            public VillageType VillageType
            {
                get { return _value; }
            }
            #endregion

            #region Constructors
            public TypedViewData(VillageType villageType)
                : base((int)villageType)
            {
                _value = villageType;
            }
            #endregion

            #region Public Methods
            public override int GetHashCode()
            {
                return _value.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                return Equals(obj as TypedViewData);
            }

            public bool Equals(TypedViewData other)
            {
                if (other == null) return false;
                return _value == other.VillageType;
            }

            public override string ToString()
            {
                return string.Format("TypedViewData: {0}", _value.ToString());
            }
            #endregion
        }
        #endregion*/
    }
}