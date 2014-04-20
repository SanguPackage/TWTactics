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
            var type = (VillageType)value;
            Color color = XmlHelper.GetColor(extraValues.ToString());
            _cache.Add(type, new DrawerData(drawerType, drawerIcon, color, value));
        }

        public override void AddDrawer(DrawerData drawer, int value)
        {
            throw new NotSupportedException();
        }

        public override void AddDrawer(DrawerData drawer, int value, object extraValues)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}