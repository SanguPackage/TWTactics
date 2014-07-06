#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Data.Maps.Displays;
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
    public class BbCodeManipulator : ManipulatorBase
    {
        #region Constants
        private const int MinDistanceBetweenPoints = 50;
        #endregion

        #region Fields
        private readonly Dictionary<Color, Tuple<Pen, Brush>> _pens;
        private readonly Font _font;

        private readonly DefaultManipulatorManager _parent;
        private int _nextId = 1;
        private Polygon _activePolygon;
        private List<Polygon> _collection = new List<Polygon>();
        private Point _lastAddedMapLocation;

        private Polygon _currentSelectedPolygon;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the currently selected polygon
        /// </summary>
        public Polygon ActivePolygon
        {
            get { return _activePolygon; }
            private set
            {
                _activePolygon = value;
                if (value == null)
                {
                    _parent.RemoveFullControlManipulator();
                }
            }
        }

        /// <summary>
        /// Gets all defined polygons
        /// </summary>
        public List<Polygon> Polygons
        {
            get { return _collection; }
        }
        #endregion

        #region Constructors
        public BbCodeManipulator(Map map, DefaultManipulatorManager parent)
            : base(map)
        {
            _parent = parent;
            _pens = new Dictionary<Color, Tuple<Pen, Brush>>();
            _font = new Font("Verdana", 12, FontStyle.Bold);

        }
        #endregion

        #region Events
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
                            p = World.Default.Map.Display.GetMapLocation(gameP);
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

                            if (_map.Display.CurrentDisplay.AllowText)
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

        /// <summary>
        /// Start creation of a new polygon
        /// </summary>
        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                if (_activePolygon == null || !_activePolygon.Drawing)
                {
                    StartNewPolygon(e.MouseEventArgs.Location);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Stop polygon creation.
        /// </summary>
        protected internal override bool MouseUpCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                if (_activePolygon != null && _activePolygon.Drawing)
                {
                    if (_activePolygon.List.Count > 2)
                    {
                        // Polygon completed
                        _activePolygon.Stop(e.MouseEventArgs.Location);
                        _nextId++;
                        DeleteIfEmpty(_activePolygon);
                    }
                    else
                    {
                        // Too small area to be a polygon
                        // try to select an existing one instead
                        Delete(_activePolygon);
                        Select(e.MouseEventArgs.Location);
                    }
                }
                return true;
            }
            else if (e.MouseEventArgs.Button == MouseButtons.Right)
            {
                Select(e.MouseEventArgs.Location);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add points to the polygon
        /// </summary>
        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                Point currentMap = e.MouseEventArgs.Location;
                if (Control.ModifierKeys.HasFlag(Keys.Control))
                {
                    currentMap.X = _lastAddedMapLocation.X;
                }
                if (Control.ModifierKeys.HasFlag(Keys.Shift))
                {
                    currentMap.Y = _lastAddedMapLocation.Y;
                }

                if (_activePolygon != null && _activePolygon.Drawing && CanAddPointToPolygon(_lastAddedMapLocation, currentMap))
                {
                    // Add extra point to the polygon
                    _activePolygon.Add(currentMap);
                    _lastAddedMapLocation = currentMap;
                    return true;
                }
            }
            return false;
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
        #endregion

        #region Public Methods
        public override IContextMenu GetContextMenu(Point location, Villages.Village village)
        {
            Debug.Assert(ActivePolygon != null);
            return new SelectedPolygonContextMenu(this);
        }

        /// <summary>
        /// Deletes all polygons
        /// </summary>
        public void Clear()
        {
            _collection = new List<Polygon>();
            ActivePolygon = null;
            _nextId = 1;
            _map.Invalidate(false);
        }

        /// <summary>
        /// Hides/Shows the polygons
        /// </summary>
        public void ToggleVisibility(bool visible)
        {
            if (_collection.Count > 0)
            {
                foreach (Polygon poly in _collection)
                {
                    poly.Visible = visible;
                }
                _map.Control.Invalidate();
            }
        }

        /// <summary>
        /// Toggles the visibility of the active polygon
        /// </summary>
        public void ToggleVisibility()
        {
            if (ActivePolygon != null)
            {
                ActivePolygon.Visible = !ActivePolygon.Visible;
                _map.Control.Invalidate();
            }
        }

        /// <summary>
        /// Deletes the active polygon
        /// </summary>
        public void Delete()
        {
            Delete(ActivePolygon);
        }

        /// <summary>
        /// Deletes a polygon
        /// </summary>
        public void Delete(Polygon poly)
        {
            if (poly != null)
            {
                Polygons.Remove(poly);
                if (poly == ActivePolygon)
                {
                    ActivePolygon = null;
                }
                _map.Control.Invalidate();
            }
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

        #region Persistence
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
                    w.WriteAttributeString("Group", poly.Group);
                    foreach (Point p in poly.List)
                    {
                        w.WriteStartElement("Point");
                        w.WriteAttributeString("X", p.X.ToString(CultureInfo.InvariantCulture));
                        w.WriteAttributeString("Y", p.Y.ToString(CultureInfo.InvariantCulture));
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
            if (r.IsStartElement("BBCodeManipulator") && !r.IsEmptyElement)
            {
                r.ReadStartElement();
                while (r.IsStartElement("Polygon"))
                {
                    string id = r.GetAttribute("ID");
                    bool visible = Convert.ToBoolean(r.GetAttribute("Visible"));
                    Color color = XmlHelper.GetColor(r.GetAttribute("Color"), Color.White);
                    string group = r.GetAttribute("Group");

                    var points = new List<Point>();
                    r.ReadStartElement();
                    while (r.IsStartElement("Point"))
                    {
                        int x = Convert.ToInt32(r.GetAttribute("X"));
                        int y = Convert.ToInt32(r.GetAttribute("Y"));
                        points.Add(new Point(x, y));
                        r.Read();
                    }
                    var poly = new Polygon(id, visible, color, group, points);
                    Polygons.Add(poly);
                    r.ReadEndElement();
                }
                r.ReadEndElement();
            }
        }

        /// <summary>
        /// Cleanup anything when switching worlds or settings
        /// </summary>
        protected internal override void CleanUp()
        {
            Clear();
        }
        #endregion

        #region Private Implementation
        /// <summary>
        /// Polygon started
        /// </summary>
        private void StartNewPolygon(Point loc)
        {
            _lastAddedMapLocation = loc;

            _activePolygon = new Polygon("poly" + _nextId.ToString(CultureInfo.InvariantCulture), _lastAddedMapLocation);
            _collection.Add(_activePolygon);
            _parent.SetFullControlManipulator(this);
        }

        /// <summary>
        /// If the polygon has no surface, we will not add it to the collection
        /// </summary>
        private void DeleteIfEmpty(Polygon polygon)
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
            {
                Delete(polygon);
            }
        }

        /// <summary>
        /// Returns the first polygon that contains the point.
        /// Cycle when there are multiple in the location.
        /// </summary>
        private Polygon GetSelectedPolygon(Point loc)
        {
            var polys = (from poly in _collection
                         where poly.IsHitIn(loc) && poly.Visible
                         select poly).ToArray();

            if (!polys.Any())
            {
                _currentSelectedPolygon = null;
            }
            else if (polys.Count() == 1)
            {
                _currentSelectedPolygon = polys.Single();
            }
            else
            {
                if (_currentSelectedPolygon == null || !polys.Contains(_currentSelectedPolygon) || polys.Last() == _currentSelectedPolygon)
                {
                    _currentSelectedPolygon = polys.First();
                }
                else
                {
                    _currentSelectedPolygon = polys.SkipWhile((Polygon poly) => !poly.Equals(_currentSelectedPolygon)).Take(2).Last();
                }
            }

            return _currentSelectedPolygon;
        }

        /// <summary>
        /// Set the active polygon
        /// </summary>
        private void Select(Point loc)
        {
            _activePolygon = GetSelectedPolygon(loc);
            if (_activePolygon == null)
            {
                _parent.RemoveFullControlManipulator();
            }
            else
            {
                _parent.SetFullControlManipulator(this);
            }
        }

        /// <summary>
        /// Returns false if the point is not to be added to the collection
        /// </summary>
        private bool CanAddPointToPolygon(Point lastMap, Point currentMap)
        {
            if (World.Default.Map.Display.CurrentDisplay.Type == DisplayTypes.Icon)
            {
                Point lastGame = World.Default.Map.Display.GetGameLocation(lastMap);
                Point currentGame = World.Default.Map.Display.GetGameLocation(currentMap);
                return lastGame != currentGame;
            }
            else
            {
                Debug.Assert(World.Default.Map.Display.CurrentDisplay.Type == DisplayTypes.Shape);

                double distance = Math.Sqrt(Math.Pow(currentMap.X - lastMap.X, 2) + Math.Pow(currentMap.Y - lastMap.Y, 2));
                return distance > MinDistanceBetweenPoints;
            }
        }
        #endregion
       
        #region Pens & Brushes
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
        #endregion
    }
}