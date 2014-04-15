#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TribalWars.Data.Villages;
#endregion

namespace TribalWars.Data.Maps.Manipulators
{
    /// <summary>
    /// Encapsulates the Polygon defined by userdrawing
    /// </summary>
    public class Polygon
    {
        #region Fields
        private int _minOffset;

        private bool _drawing;
        private string _name;
        private LinkedList<Point> _points;
        private bool _visible;
        private bool _differentVillage;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether we are currently drawing
        /// the polygon
        /// </summary>
        public bool Drawing
        {
            get { return _drawing; }
        }

        /// <summary>
        /// Gets the list with points on the polygon border
        /// </summary>
        public LinkedList<Point> List
        {
            get { return _points; }
        }

        /// <summary>
        /// Gets a value indicating whether the polygon is defined
        /// </summary>
        public bool Defined
        {
            get { return List != null; }
        }

        /// <summary>
        /// Gets or sets an string id identifying the polygon
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// polygon should be drawn to the map
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        #endregion

        #region Constructors
        public Polygon(string name, int x, int y, int minOffset, bool differentVillage)
        {
            _minOffset = minOffset;
            _name = name;
            _visible = true;
            _differentVillage = differentVillage;
            Start(x, y);
        }

        public Polygon(string name, bool visible, List<Point> points)
        {
            _minOffset = 15 * 15;
            _name = name;
            _visible = visible;
            _points = new LinkedList<Point>(points);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the villages inside the polygon
        /// </summary>
        public IEnumerable<Village> GetVillages()
        {
            List<Village> villages = new List<Village>();
            if (Defined)
            {
                Region areaRegion = GetRegion();
                foreach (Village vil in World.Default.Villages.Values)
                {
                    if (areaRegion.IsVisible(vil.Location))
                    {
                        villages.Add(vil);
                    }
                }
            }
            return villages;
        }

        /// <summary>
        /// Starts a new polygon
        /// </summary>
        public void Start(int x, int y)
        {
            _drawing = true;
            _points = new LinkedList<Point>();
            _points.AddFirst(GetPoint(x, y));
        }

        /// <summary>
        /// Adds a point to the polygon
        /// </summary>
        public bool Add(int x, int y)
        {
            Point last = _points.Last.Value;
            int distance = (x - last.X) * (x - last.X) + (y - last.Y) * (y - last.Y);
            if (distance > _minOffset)
            {
                Point game = GetPoint(x, y);
                if (game != last || !_differentVillage)
                {
                    _points.AddLast(game);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Stops drawing the polygon
        /// </summary>
        public void Stop(int x, int y)
        {
            _drawing = false;
            _points.AddLast(GetPoint(x, y));
        }

        /// <summary>
        /// Returns a value indicating whether a point is inside the polygon
        /// </summary>
        public bool IsHitIn(int x, int y)
        {
            Region AreaRegion = GetRegion();
            return AreaRegion.IsVisible(GetPoint(x, y));
        }

        /// <summary>
        /// Gets the region defined by the polygon
        /// </summary>
        public Region GetRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath AreaPath = new System.Drawing.Drawing2D.GraphicsPath();
            int x = -1, y = -1;
            foreach (Point p in List)
            {
                if (x > -1)
                {
                    AreaPath.AddLine(x, y, p.X, p.Y);
                }
                x = p.X;
                y = p.Y;
            }
            AreaPath.CloseFigure();
            return new Region(AreaPath);
        }
        #endregion

        #region Private Implementation
        /// <summary>
        /// Gets the game location of the point
        /// </summary>
        private Point GetPoint(int x, int y)
        {
            return World.Default.Map.Display.GetGameLocation(x, y);
        }
        #endregion
    }
}
