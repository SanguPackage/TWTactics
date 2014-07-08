#region Using
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TribalWars.Maps.Drawers;
using TribalWars.Tools;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Views
{
    /// <summary>
    /// Village decorator to add an image indicating the village type (Def, Off, ...)
    /// </summary>
    public class VillageTypeView : ViewBase
    {
        #region Fields
        private readonly Dictionary<VillageType, DrawerData> _cache;
        private readonly VillageType[] _importance;
        #endregion

        #region Constructors
        public VillageTypeView(string name)
            : base(name, true)
        {
            _cache = new Dictionary<VillageType, DrawerData>();
            _importance = new[] {VillageType.Attack, VillageType.Defense, VillageType.Scout, VillageType.Farm };
        }
        #endregion

        #region Public Methods
        public override DrawerData GetDrawer(Village village)
        {
            DrawerData data;
            if (!_cache.TryGetValue(village.Type, out data))
            {
                // When there are combined village types, we take the most important one
                data = _importance.Where(t => village.Type.HasFlag(t)).Select(t => _cache[t]).FirstOrDefault();
                if (data != null)
                {
                    data = new DrawerData(data.ShapeDrawer, data.IconDrawer, data.BonusIconDrawer, data.ExtraDrawerInfo, village.Type);
                }
                else
                {
                    if (village.Type.HasFlag(VillageType.Noble) || village.Type.HasFlag(VillageType.Comments))
                        data = new DrawerData(null, null, null, null, village.Type);
                }
                _cache.Add(village.Type, data);
            }
            return data;
        }

        public override void AddDrawer(string drawerType, string drawerIcon, string drawerBonusIcon, int value, object extraValues)
        {
            // Value=VillageType, extraValues=Color for BorderDrawer
            var type = (VillageType)value;
            Color color = XmlHelper.GetColor(extraValues.ToString());
            _cache.Add(type, new DrawerData(drawerType, drawerIcon, drawerBonusIcon, color, value));
        }
        #endregion
    }
}