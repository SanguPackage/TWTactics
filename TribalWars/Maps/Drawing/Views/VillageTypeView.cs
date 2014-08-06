#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using TribalWars.Maps.Drawing.Displays;
using TribalWars.Maps.Drawing.Drawers;
using TribalWars.Maps.Drawing.Drawers.VillageDrawers;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Drawing.Views
{
    /// <summary>
    /// Village decorator to add an image indicating the village type (Def, Off, ...)
    /// </summary>
    public class VillageTypeView : ViewBase, IDecoratorView
    {
        #region Fields
        private readonly Dictionary<VillageType, DecoratorDrawerData> _cache;
        #endregion

        #region Constructors
        public VillageTypeView(string name)
            : base(name, "VillageType")
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
        }
        #endregion

        #region Persistence
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
                Color? backgroundColor = null;
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

        public override object[] WriteDrawerXml()
        {
            var list = new List<object>();
            foreach (KeyValuePair<VillageType, DecoratorDrawerData> cache in _cache)
            {
                var drawer = new XElement("Drawer", new XAttribute("VillageType", cache.Key.ToString()));
                if (cache.Value.Shape != null)
                {
                    var shape = cache.Value.Shape;
                    drawer.Add(new XElement("Shape",
                        new XAttribute("Drawer", shape.Drawer),
                        new XAttribute("Color", XmlHelper.SetColor(shape.Color))));
                }

                if (cache.Value.Icon != null)
                {
                    var icon = cache.Value.Icon;
                    var xIcon = new XElement("Icon",
                        new XAttribute("Icon", icon.IconName),
                        new XAttribute("Orientation", icon.Orientation.ToString()));

                    if (icon.Background.HasValue)
                    {
                        xIcon.Add(new XAttribute("Background", XmlHelper.SetColor(icon.Background.Value)));
                    }
                    drawer.Add(xIcon);
                }

                list.Add(drawer);
            }

            return list.ToArray();
        }
        #endregion
    }
}