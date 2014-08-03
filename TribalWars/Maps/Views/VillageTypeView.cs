#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TribalWars.Maps.Drawers;
using TribalWars.Maps.Drawers.VillageDrawers;
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
        // High zoom: verschillende IconOrientation
        // Low zoom: just one with FillMiddle
        // Each orientation has sortedenumerable of villageType & DrawerData
        // --> first defined in xml = first come
        // 
        // SupportDecorators depends on VillageSize
        //   
        private Dictionary<IconOrientation, IEnumerable<DrawerData>> _drawers;

        private readonly Dictionary<VillageType, DrawerData> _cache;
        private readonly VillageType[] _importance;
        #endregion

        #region Constructors
        public VillageTypeView(string name)
            : base(name, true)
        {
            _cache = new Dictionary<VillageType, DrawerData>();
            _importance = new[] {VillageType.Attack, VillageType.Catapult, VillageType.Defense, VillageType.Scout, VillageType.Farm };
        }
        #endregion

        #region Public Methods
        // TODO: Pass the village dimensions here?
        public override DrawerData GetDrawerData(Village village)
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

        public override void AddDrawer(WorldTemplate.WorldConfigurationViewsViewDrawersDrawer drawer)
        {
            var villageType = (VillageType) Enum.Parse(typeof (VillageType), drawer.Value);

            Color color = XmlHelper.GetColor(drawer.ExtraValue);
            _cache.Add(villageType, new DrawerData(drawer.Type, drawer.Icon, drawer.BonusIcon, color, villageType));
        }
        #endregion
    }
}