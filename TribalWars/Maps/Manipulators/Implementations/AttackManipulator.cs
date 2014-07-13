#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using TribalWars.Controls;
using TribalWars.Controls.Polygons;
using TribalWars.Maps.Displays;
using TribalWars.Maps.Manipulators.Helpers;
using TribalWars.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Plan attacks on the map
    /// </summary>
    public class AttackManipulator : ManipulatorBase
    {
        #region Constants
        #endregion

        #region Fields
        private readonly AttackManipulatorManager _parent;
        private List<Village> _plans;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public AttackManipulator(Map map, AttackManipulatorManager parent)
            : base(map)
        {
            _parent = parent;
            _plans = new List<Village>();

            map.EventPublisher.VillagesSelected += EventPublisherOnVillagesSelected;
        }

        private void EventPublisherOnVillagesSelected(object sender, VillagesEventArgs e)
        {
            if (e.Tool == VillageTools.DistanceCalculationTarget)
            {
                _plans.Add(e.FirstVillage);
            }
            else if (e.Tool == VillageTools.DistanceCalculation)
            {
                
            }
        }
        #endregion

        #region Events
        public override void Paint(MapPaintEventArgs e)
        {
            _parent.Draw(e.Graphics);
        }

        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                
            }
            return false;
        }

        protected internal override bool MouseUpCore(MapMouseEventArgs e)
        {
            if (e.Village != null)
            {
                if (e.MouseEventArgs.Button == MouseButtons.Left)
                {
                    World.Default.Map.EventPublisher.SelectVillages(this, e.Village, VillageTools.DistanceCalculationTarget);
                    return true;
                }
                else if (e.MouseEventArgs.Button == MouseButtons.Right)
                {
                    World.Default.Map.EventPublisher.SelectVillages(this, e.Village, VillageTools.DistanceCalculation);
                    return true;
                }
            }
            return false;
        }

        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            return false;
        }

        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            return false;
        }
        #endregion

        #region Public Methods
        public override IContextMenu GetContextMenu(Point location, Village village)
        {
            return null;
        }

        public override void Dispose()
        {
        }
        #endregion

        #region Persistence
        protected internal override void WriteXmlCore(XmlWriter w)
        {
            //if (Polygons.Count > 0)
            //{
            //    w.WriteStartElement("BBCodeManipulator");
            //    foreach (Polygon poly in Polygons)
            //    {
            //        w.WriteStartElement("Polygon");
            //        w.WriteAttributeString("ID", poly.Name);
            //        w.WriteAttributeString("Visible", poly.Visible.ToString());
            //        w.WriteAttributeString("Color", XmlHelper.SetColor(poly.LineColor));
            //        w.WriteAttributeString("Group", poly.Group);
            //        foreach (Point p in poly.List)
            //        {
            //            w.WriteStartElement("Point");
            //            w.WriteAttributeString("X", p.X.ToString(CultureInfo.InvariantCulture));
            //            w.WriteAttributeString("Y", p.Y.ToString(CultureInfo.InvariantCulture));
            //            w.WriteEndElement();
            //        }
            //        w.WriteEndElement();
            //    }
            //    w.WriteEndElement();
            //}
        }

        protected internal override void ReadXmlCore(XmlReader r)
        {
            //if (r.IsStartElement("BBCodeManipulator") && !r.IsEmptyElement)
            //{
            //    r.ReadStartElement();
            //    while (r.IsStartElement("Polygon"))
            //    {
            //        string id = r.GetAttribute("ID");
            //        bool visible = Convert.ToBoolean(r.GetAttribute("Visible"));
            //        Color color = XmlHelper.GetColor(r.GetAttribute("Color"), Color.White);
            //        string group = r.GetAttribute("Group");

            //        var points = new List<Point>();
            //        r.ReadStartElement();
            //        while (r.IsStartElement("Point"))
            //        {
            //            int x = Convert.ToInt32(r.GetAttribute("X"));
            //            int y = Convert.ToInt32(r.GetAttribute("Y"));
            //            points.Add(new Point(x, y));
            //            r.Read();
            //        }
            //        var poly = new Polygon(id, visible, color, group, points);
            //        Polygons.Add(poly);
            //        r.ReadEndElement();
            //    }
            //    r.ReadEndElement();
            //}
        }

        /// <summary>
        /// Cleanup anything when switching worlds or settings
        /// </summary>
        protected internal override void CleanUp()
        {

        }
        #endregion

        #region Private Implementation
        #endregion
    }
}