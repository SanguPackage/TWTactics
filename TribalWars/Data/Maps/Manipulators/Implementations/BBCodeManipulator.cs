#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data.Villages;
using TribalWars.Controls;
using System.Xml;
using TribalWars.Controls.Maps;
using TribalWars.Data.Maps.Manipulators.Helpers;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls.TWContextMenu;
#endregion

namespace TribalWars.Data.Maps.Manipulators
{
    /// <summary>
    /// Allows the user to draw polygons on the map
    /// </summary>
    internal class BBCodeManipulator : MouseMoveManipulatorBase
    {
        #region Fields
        private Pen _pen;
        private SolidBrush _brush;
        private Font _font;

        private MapPolygonControl _control;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public BBCodeManipulator(Map map, DefaultManipulatorManager parentManipulatorHandler, int polygonOffset)
            : base(map, parentManipulatorHandler, polygonOffset)
        {
            _pen = new Pen(Color.White);
            _brush = new SolidBrush(_pen.Color);
            _font = new Font("Verdana", 12, FontStyle.Bold);
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Paints the polygons
        /// </summary>
        public override void Paint(MapPaintEventArgs e)
        {
            if (Polygons != null)
            {
                foreach (Polygon poly in Polygons)
                {
                    bool active = poly == ActivePolygon && e.IsActiveManipulator;
                    if (poly.Defined && poly.List.Count > 1 && poly.Visible)
                    {
                        Point firstP = new Point();
                        Point p = new Point();
                        int x = -1, y = -1;
                        foreach (Point gameP in poly.List)
                        {
                            p = World.Default.Map.Display.GetMapLocation(gameP.X, gameP.Y);
                            if (x != -1 || y != -1)
                            {
                                e.Graphics.DrawLine(_pen, x, y, p.X, p.Y);
                            }
                            else firstP = p;
                            if (active)
                            {
                                e.Graphics.FillRectangle(_brush, new Rectangle(p.X - 3, p.Y - 3, 7, 7));
                            }

                            x = p.X;
                            y = p.Y;
                        }
                        if (!poly.Drawing)
                        {
                            e.Graphics.DrawLine(_pen, firstP, p);

                            if (_map.Display.DisplayManager.CurrentDisplay.Zoom.Maximum > 5 && _map.Location.Zoom >= 5)
                            {
                                SizeF size = e.Graphics.MeasureString(poly.Name, _font);
                                p.Offset(5, 5);
                                e.Graphics.DrawEllipse(_pen, p.X, p.Y, size.Width, size.Height);
                                e.Graphics.DrawString(poly.Name, _font, _brush, new Point(p.X, p.Y));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles keypresses on the map
        /// </summary>
        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            if (e.KeyEventArgs.KeyCode == Keys.Delete && ActivePolygon != null)
            {
                Delete(ActivePolygon);
                return true;
            }
            return false;
        }

        /// <summary>
        /// When the visibility of the polygons has changed
        /// </summary>
        protected override void OnVisibilityChanged(bool activeOnly, bool visible)
        {
            if (!visible && _control != null)
                _map.Control.Controls.Remove(_control);
        }

        /// <summary>
        /// Saves state to stream
        /// </summary>
        protected internal override void WriteXmlCore(XmlWriter w)
        {
            if (Polygons.Count > 0)
            {
                w.WriteStartElement("BBCodeManipulator");
                foreach (Polygon poly in Polygons)
                {
                    if (poly.Defined)
                    {
                        w.WriteStartElement("Polygon");
                        w.WriteAttributeString("ID", poly.Name);
                        w.WriteAttributeString("Visible", poly.Visible.ToString());
                        foreach (Point p in poly.List)
                        {
                            w.WriteStartElement("Point");
                            w.WriteAttributeString("X", p.X.ToString());
                            w.WriteAttributeString("Y", p.Y.ToString());
                            w.WriteEndElement();
                        }
                        w.WriteEndElement();
                    }
                }
                w.WriteEndElement();
            }
        }

        /// <summary>
        /// Loads state from stream
        /// </summary>
        protected internal override void ReadXmlCore(XmlReader r)
        {
            this.Clear();
            if (r.IsStartElement("BBCodeManipulator") && !r.IsEmptyElement)
            {
                r.ReadStartElement();
                while (r.IsStartElement("Polygon"))
                {
                    string id = r.GetAttribute("ID");
                    bool visible = System.Convert.ToBoolean(r.GetAttribute("Visible"));
                    List<Point> points = new List<Point>();
                    r.ReadStartElement();
                    while (r.IsStartElement("Point"))
                    {
                        int x = System.Convert.ToInt32(r.GetAttribute("X"));
                        int y = System.Convert.ToInt32(r.GetAttribute("Y"));
                        points.Add(new Point(x, y));
                        r.Read();
                    }
                    Polygon poly = new Polygon(id, visible, points);
                    Polygons.Add(poly);
                    r.ReadEndElement();
                }
                r.ReadEndElement();
            }
        }

        /// <summary>
        /// If the polygon has no surface, we will not add it to the collection
        /// </summary>
        protected override void Stop(Polygon polygon)
        {
            int leftX = 1000;
            int rightX = 0;
            int topY = 1000;
            int bottomY = 0;

            foreach (Point p in polygon.List)
            {
                if (p.X > rightX) rightX = p.X;
                if (p.X < leftX) leftX = p.X;
                if (p.Y > bottomY) bottomY = p.Y;
                if (p.Y < topY) topY = p.Y;
            }

            if (leftX >= rightX || topY >= bottomY)
                Delete(polygon);
        }

        /// <summary>
        /// Dispose resources
        /// </summary>
        public override void Dispose()
        {
            _pen.Dispose();
            _brush.Dispose();
            _font.Dispose();
        }
        #endregion

        #region Private Implementation
        //--> This stuff needs to be in the ManipulatorBase
        //--> Adding a control to the map
        public void AddControl()
        {
            RemoveControl();
            _control = new MapPolygonControl(ActivePolygon, this, new Point(100, 100));
            _map.Control.Controls.Add(_control);
            _control.Focus();
        }

        public void RemoveControl()
        {
            if (_control != null)
            {
                _map.Control.Controls.Remove(_control);
                _control.Dispose();
            }

        }
        #endregion
    }
}
