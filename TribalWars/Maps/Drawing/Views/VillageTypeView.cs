#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using TribalWars.Maps.Displays;
using TribalWars.Maps.Drawers;
using TribalWars.Maps.Drawers.VillageDrawers;
using TribalWars.Maps.Markers;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Views
{
    /// <summary>
    /// Village decorator to add an image indicating the village type (Def, Off, ...)
    /// </summary>
    public class VillageTypeView : ViewBase, IDecoratorView
    {
        #region Fields
        // TODO: High zoom: verschillende IconOrientation
        // Low zoom: just one with FillMiddle

        private readonly Dictionary<VillageType, DecoratorDrawerData> _cache;
        #endregion

        #region Constructors
        public VillageTypeView(string name)
            : base(name)
        {
            _cache = new Dictionary<VillageType, DecoratorDrawerData>();
        }
        #endregion

        #region Public Methods
        public DrawerBase GetDecoratorDrawer(DrawerFactoryBase drawerFactory, Village village, BackgroundDrawerData mainData)
        {
            DecoratorDrawerData data = _cache
                .Where(x => village.Type.HasFlag(x.Key))
                .Select(x => x.Value)
                .FirstOrDefault();

            if (data == null)
            {
                return null;
            }

            return drawerFactory.CreateVillageDecoratorDrawer(data, mainData);


            //DecoratorDrawerData data;
            //if (!_cache.TryGetValue(village.Type, out data))
            //{
            //    // When there are combined village types, we take the most important one
            //    data = _importance.Where(t => village.Type.HasFlag(t)).Select(t => _cache[t]).FirstOrDefault();
            //    if (data != null)
            //    {
            //        data = new DecoratorDrawerData(data.ShapeDrawer, data.IconDrawer, data.BonusIconDrawer, data.ExtraDrawerInfo, village.Type);
            //    }
            //    else
            //    {
            //        if (village.Type.HasFlag(VillageType.Noble) || village.Type.HasFlag(VillageType.Comments))
            //            data = new DecoratorDrawerData(null, null, null, null, village.Type);
            //    }
            //    _cache.Add(village.Type, data);
            //}
            //return data;
        }

        //public override void AddDrawer(WorldTemplate.WorldConfigurationViewsViewDrawersDrawer drawer)
        //{
        //    // TODO:
        //    //drawer.IconOrientation
        //    //drawer.IconBackground

        //    var villageType = (VillageType) Enum.Parse(typeof (VillageType), drawer.Value);

        //    Color color = XmlHelper.GetColor(drawer.ShapeDrawerColor);
        //    _cache.Add(villageType, new DrawerData(drawer.ShapeDrawer, drawer.IconDrawer, drawer.BonusIconDrawer, color, villageType));
        //}

        

        public override void ReadDrawerXml(XElement drawer)
        {
            DecoratorDrawerData.IconData icon = null;
            DecoratorDrawerData.ShapeData shape = null;

            var villageType = (VillageType)Enum.Parse(typeof(VillageType), drawer.Attribute("VillageType").Value);

            var xShape = drawer.Element("Shape");
            if (xShape != null)
            {
                shape = new DecoratorDrawerData.ShapeData(
                    xShape.Attribute("Drawer").Value,
                    XmlHelper.GetColor(xShape.Attribute("Color").Value));
            }

            var xIcon = drawer.Element("Icon");
            if (xIcon != null)
            {
                Color backgroundColor = Color.Transparent;
                var xBackgroundColor = xIcon.Attribute("Background");
                if (xBackgroundColor != null)
                {
                    backgroundColor = XmlHelper.GetColor(xBackgroundColor.Value);
                }

                icon = new DecoratorDrawerData.IconData(
                    xIcon.Attribute("Icon").Value,
                    (IconOrientation)Enum.Parse(typeof(IconOrientation), xIcon.Attribute("Orientation").Value),
                    backgroundColor);
            }

            var data = new DecoratorDrawerData(shape, icon);
            _cache.Add(villageType, data);
        }
        #endregion
    }
}