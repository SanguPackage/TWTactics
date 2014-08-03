#region Using
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TribalWars.Maps.Drawing.Drawers;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Drawing.Views
{
    /// <summary>
    /// Display villages based on the amount of points
    /// </summary>
    public sealed class PointsView : ViewBase, IBackgroundView
    {
        #region Fields
        private readonly Dictionary<int, BackgroundDrawerData> _drawers;
        #endregion

        #region Constructors
        public PointsView(string name)
            : base(name, "Points")
        {
            _drawers = new Dictionary<int, BackgroundDrawerData>();
        }
        #endregion

        #region Public Methods
        public BackgroundDrawerData GetBackgroundDrawer(Village village)
        {
            foreach (KeyValuePair<int, BackgroundDrawerData> pair in _drawers)
            {
                if (village.Points < pair.Key)
                {
                    return pair.Value;
                }
            }
            return null;
        }

        public override void ReadDrawerXml(XElement drawer)
        {
            int pointsTreshold = int.Parse(drawer.Attribute("VillagePoints").Value);
            var data = new BackgroundDrawerData(
                drawer.Attribute("ShapeDrawer").Value,
                drawer.Attribute("IconDrawer").Value,
                drawer.Attribute("BonusIconDrawer").Value);

            _drawers.Add(pointsTreshold, data);
        }

        public override object[] WriteDrawerXml()
        {
            var drawers = _drawers.Select(x => 
                new XElement("Drawer",
                    new XAttribute("VillagePoints", x.Key),
                    new XAttribute("ShapeDrawer", x.Value.ShapeDrawer),
                    new XAttribute("IconDrawer", x.Value.IconDrawer),
                    new XAttribute("BonusIconDrawer", x.Value.BonusIconDrawer)));

            return drawers.Cast<object>().ToArray();
        }
        #endregion
    }
}
