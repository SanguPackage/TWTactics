#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
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

        public override void Paint(MapPaintEventArgs e)
        {
            foreach (ChurchInfo church in _churches)
            {
                Point mapLocation = _map.Display.GetMapLocation(church.Village.Location);
                Color color = Color.FromArgb(church.Transparancy, church.Color);
                using (var brush = new SolidBrush(color))
                {
                    e.Graphics.FillRectangle(brush, mapLocation.X, mapLocation.Y, 100, 100);
                }
            }
        }

        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            
            return false;
        }

        protected internal override bool OnVillageClickCore(MapVillageEventArgs e)
        {
            return false;
        }

        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            
            return false;
        }

        protected internal override void CleanUp()
        {
            _churches.Clear();
        }

        public override void TimerPaint(MapTimerPaintEventArgs e)
        {
            
        }

        public override void Dispose()
        {

        }
        #endregion

        #region Privates
        
        #endregion

        
    }
}