#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Xml.Linq;
using TribalWars.Maps.Drawing.Displays;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Manipulators.Implementations.Church
{
    /// <summary>
    /// Display the radius of the churches
    /// </summary>
    public class ChurchManipulator : ManipulatorBase
    {
        #region Fields
        private readonly List<ChurchInfo> _churches;
        #endregion

        #region Constructors
        public ChurchManipulator(Map map)
            : base(map)
        {
            _churches = new List<ChurchInfo>();
            _map.EventPublisher.ChurchChanged += EventPublisherOnChurchChanged;
        }

        private void EventPublisherOnChurchChanged(object sender, ChurchEventArgs e)
        {
            _churches.RemoveAll(x => x.Village == e.Church.Village);
            if (e.Church.ChurchLevel != 0)
            {
                _churches.Add(e.Church);
            }
        }
        #endregion

        #region Persistence
        public override void ReadXml(XDocument doc)
        {
            var churchesContainer = doc.Descendants("ChurchesManipulator").FirstOrDefault();
            if (churchesContainer != null)
            {
                var churches = churchesContainer.Elements().Select(x => new ChurchInfo(
                    World.Default.GetVillage(x.Attribute("Village").Value),
                    Convert.ToInt32(x.Attribute("Level").Value),
                    XmlHelper.GetColor(x.Attribute("Color").Value)));

                _churches.AddRange(churches);
            }
        }

        public override string WriteXml()
        {
            if (World.Default.Settings.Church)
            {
                var churches =
                    new XElement("ChurchesManipulator",
                        _churches.Select(x =>
                            new XElement("Church",
                                new XAttribute("Village", x.Village.LocationString),
                                new XAttribute("Level", x.ChurchLevel),
                                new XAttribute("Color", XmlHelper.SetColor(x.Color)))));

                return churches.ToString();
            }
            return "";
        }
        #endregion

        #region Methods
        public ChurchInfo GetChurch(Village village)
        {
            return _churches.FirstOrDefault(x => x.Village == village);
        }

        public override void Paint(MapPaintEventArgs e, bool isActiveManipulator)
        {
            foreach (ChurchInfo church in _churches)
            {
                Point mapLocation = _map.Display.GetMapLocation(church.Village.Location);

                var villageSize = _map.Display.Dimensions.SizeWithSpacing;

                // Paint curch level
                if (_map.Display.Type == DisplayTypes.Icon)
                {
                    if (villageSize.Width > 30)
                    {
                        using (var levelBackground = new SolidBrush(Color.Red))
                        using (var levelTextBrush = new SolidBrush(Color.Yellow))
                        using (var levelFont = new Font("Verdana", 10, FontStyle.Bold))
                        {
                            e.Graphics.FillEllipse(
                                levelBackground,
                                mapLocation.X + villageSize.Width / 2 - 6,
                                mapLocation.Y + villageSize.Height / 3 - 3,
                                12, 
                                15);

                            e.Graphics.DrawString(
                                church.ChurchLevel.ToString(), 
                                levelFont, 
                                levelTextBrush,
                                mapLocation.X + villageSize.Width / 2 - 6,
                                mapLocation.Y + villageSize.Height / 3 - 3);
                        }
                    }
                    else if (villageSize.Width > 20)
                    {
                        using (var levelBackground = new SolidBrush(Color.Red))
                        {
                            e.Graphics.FillRectangle(
                                levelBackground,
                                mapLocation.X + villageSize.Width / 2 - villageSize.Width / 4 + 2,
                                mapLocation.Y + villageSize.Height / 2 - villageSize.Width / 4 + 2,
                                villageSize.Width / 4,
                                villageSize.Width / 4);
                        }
                    }
                }
                else if (villageSize.Width > 15)
                {
                    // Shape Display
                    using (var levelBackground = new SolidBrush(Color.Red))
                    {
                        e.Graphics.FillRectangle(
                            levelBackground,
                            mapLocation.X + villageSize.Width / 2 - villageSize.Width / 8,
                            mapLocation.Y + villageSize.Height / 2 - villageSize.Width / 8,
                            villageSize.Width / 4,
                            villageSize.Width / 4);
                    }
                }

                // Paint church radius
                if (villageSize.Width > 0)
                {
                    GraphicsPath churchInfluence = church.GetRadius(mapLocation, villageSize);

                    Color color = Color.FromArgb(church.Transparancy, church.Color);
                    using (var brush = new SolidBrush(color))
                    {
                        e.Graphics.FillPath(brush, churchInfluence);
                    }
                    using (var pen = new Pen(church.Color) { DashStyle = DashStyle.DashDotDot })
                    {
                        e.Graphics.DrawPath(pen, churchInfluence);
                    }
                }
            }
        }

        protected internal override void CleanUp()
        {
            _churches.Clear();
        }
        #endregion
    }
}