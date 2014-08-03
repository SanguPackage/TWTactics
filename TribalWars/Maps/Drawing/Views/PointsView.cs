#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using TribalWars.Maps.Displays;
using TribalWars.Maps.Drawers;
using TribalWars.Maps.Markers;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Views
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
            : base(name)
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
        #endregion
    }
}
