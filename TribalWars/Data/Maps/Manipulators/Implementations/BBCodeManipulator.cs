#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Data.Maps.Displays;
using TribalWars.Data.Maps.Manipulators.Controls;
using TribalWars.Data.Maps.Manipulators.Helpers;
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Data.Maps.Manipulators.Managers;
using TribalWars.Tools;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Allows the user to draw polygons on the map
    /// </summary>
    public class BbCodeManipulator : MouseMoveManipulatorBase
    {
        #region Constants
        private const int MinDistanceBetweenPoints = 50;
        #endregion

        #region Fields
        private readonly Dictionary<Color, Tuple<Pen, Brush>> _pens;
        private readonly Font _font;

        private MapPolygonControl _control;
        #endregion

        #region Constructors
        public BbCodeManipulator(Map map, DefaultManipulatorManager parentManipulatorHandler)
            : base(map, parentManipulatorHandler)
        {
            _pens = new Dictionary<Color, Tuple<Pen, Brush>>();
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
                    if (poly.List.Count > 1 && poly.Visible)
                    {
                        Pen pen = GetPen(poly.LineColor);
                        Brush brush = GetBrush(poly.LineColor);

                        var firstP = new Point();
                        var p = new Point();
                        int x = -1, y = -1;
                        foreach (Point gameP in poly.List)
                        {
                            p = World.Default.Map.Display.GetMapLocation(gameP.X, gameP.Y);
                            if (x != -1 || y != -1)
                            {
                                e.Graphics.DrawLine(pen, x, y, p.X, p.Y);
                            }
                            else
                            {
                                firstP = p;
                            }
                            if (active)
                            {
                                e.Graphics.FillRectangle(brush, new Rectangle(p.X - 3, p.Y - 3, 7, 7));
                            }

                            x = p.X;
                            y = p.Y;
                        }
                        if (!poly.Drawing)
                        {
                            e.Graphics.DrawLine(pen, firstP, p);

                            if (_map.Display.DisplayManager.CurrentDisplayType == DisplayTypes.Icon || _map.Location.Zoom >= 5)
                            {
                                SizeF size = e.Graphics.MeasureString(poly.Name, _font);
                                p.Offset(5, 5);
                                e.Graphics.DrawEllipse(pen, p.X, p.Y, size.Width, size.Height);
                                e.Graphics.DrawString(poly.Name, _font, brush, new Point(p.X, p.Y));
                            }
                        }
                    }
                }
            }
        }

        protected override bool CanAddPointToPolygon(Point lastMap, Point currentMap)
        {
            if (World.Default.Map.Display.DisplayManager.CurrentDisplayType == DisplayTypes.Icon)
            {
                Point lastGame = World.Default.Map.Display.GetGameLocation(lastMap);
                Point currentGame = World.Default.Map.Display.GetGameLocation(currentMap);
                return lastGame != currentGame;
            }
            else
            {
                Debug.Assert(World.Default.Map.Display.DisplayManager.CurrentDisplayType == DisplayTypes.Shape);

                double distance = Math.Sqrt(Math.Pow(currentMap.X - lastMap.X, 2) + Math.Pow(currentMap.Y - lastMap.Y, 2));
                return distance > MinDistanceBetweenPoints;
            }
        }

        /// <summary>
        /// Handles keypresses on the map
        /// </summary>
        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            if (ActivePolygon != null)
            {
                if (e.KeyEventArgs.KeyCode == Keys.Delete)
                {
                    Delete(ActivePolygon);
                    return true;
                }

                if (!ActivePolygon.Drawing)
                {
                    var polygonMove = new KeyboardInputToMovementConverter(e.KeyEventArgs.KeyData, 1, 5).GetKeyMove();
                    if (polygonMove.HasValue)
                    {
                        ActivePolygon.Move(polygonMove.Value);
                        return true;
                    }
                }
            }
            return false;
        }

        public override IContextMenu GetContextMenu(Point location, Villages.Village village)
        {
            Debug.Assert(ActivePolygon != null);
            return new SelectedPolygonContextMenu(this);
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
                    w.WriteStartElement("Polygon");
                    w.WriteAttributeString("ID", poly.Name);
                    w.WriteAttributeString("Visible", poly.Visible.ToString());
                    w.WriteAttributeString("Color", XmlHelper.SetColor(poly.LineColor));
                    foreach (Point p in poly.List)
                    {
                        w.WriteStartElement("Point");
                        w.WriteAttributeString("X", p.X.ToString());
                        w.WriteAttributeString("Y", p.Y.ToString());
                        w.WriteEndElement();
                    }
                    w.WriteEndElement();
                }
                w.WriteEndElement();
            }
        }

        /// <summary>
        /// Loads state from stream
        /// </summary>
        protected internal override void ReadXmlCore(XmlReader r)
        {
            Clear();
            if (r.IsStartElement("BBCodeManipulator") && !r.IsEmptyElement)
            {
                r.ReadStartElement();
                while (r.IsStartElement("Polygon"))
                {
                    string id = r.GetAttribute("ID");
                    bool visible = System.Convert.ToBoolean(r.GetAttribute("Visible"));
                    Color color = XmlHelper.GetColor(r.GetAttribute("Color"), Color.White);

                    var points = new List<Point>();
                    r.ReadStartElement();
                    while (r.IsStartElement("Point"))
                    {
                        int x = System.Convert.ToInt32(r.GetAttribute("X"));
                        int y = System.Convert.ToInt32(r.GetAttribute("Y"));
                        points.Add(new Point(x, y));
                        r.Read();
                    }
                    var poly = new Polygon(id, visible, color, points);
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
            foreach (KeyValuePair<Color, Tuple<Pen, Brush>> penAndBrush in _pens)
            {
                penAndBrush.Value.Item1.Dispose();
                penAndBrush.Value.Item2.Dispose();
            }
            _font.Dispose();
        }
        #endregion

        #region Private Implementation
        private Pen GetPen(Color color)
        {
            CacheIfNotExists(color);
            return _pens[color].Item1;
        }

        private Brush GetBrush(Color color)
        {
            CacheIfNotExists(color);
            return _pens[color].Item2;
        }

        private void CacheIfNotExists(Color color)
        {
            if (!_pens.ContainsKey(color))
            {
                var pen = new Pen(color);
                var brush = new SolidBrush(color);
                _pens.Add(color, new Tuple<Pen, Brush>(pen, brush));
            }
        }

        //--> TODO This stuff needs to be in the ManipulatorBase
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