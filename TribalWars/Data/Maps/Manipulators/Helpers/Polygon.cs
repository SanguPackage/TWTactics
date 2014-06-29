#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using TribalWars.Data.Maps.Displays;
using TribalWars.Data.Villages;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Helpers
{
    /// <summary>
    /// Encapsulates the Polygon defined by userdrawing
    /// </summary>
    public class Polygon
    {
        #region Fields
        private readonly int _minOffset;

        private readonly bool _differentVillage;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether we are currently drawing
        /// the polygon
        /// </summary>
        public bool Drawing { get; private set; }

        /// <summary>
        /// Gets the list with points on the polygon border
        /// </summary>
        public LinkedList<Point> List { get; private set; }

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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// polygon should be drawn to the map
        /// </summary>
        public bool Visible { get; set; }
        #endregion

        #region Constructors
        public Polygon(string name, int x, int y, int minOffset, bool differentVillage)
        {
            _minOffset = minOffset;
            Name = name;
            Visible = true;
            _differentVillage = differentVillage;
            Start(x, y);
        }

        public Polygon(string name, bool visible, List<Point> points, int minOffset)
        {
            _minOffset = minOffset;
            Name = name;
            Visible = visible;
            List = new LinkedList<Point>(points);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the villages inside the polygon
        /// </summary>
        public IEnumerable<Village> GetVillages()
        {
            var villages = new List<Village>();
            if (Defined)
            {
                Region areaRegion = GetRegion();
                villages.AddRange(World.Default.Villages.Values.Where(vil => areaRegion.IsVisible(vil.Location)));
            }
            return villages;
        }

        /// <summary>
        /// Starts a new polygon
        /// </summary>
        public void Start(int x, int y)
        {
            Drawing = true;
            List = new LinkedList<Point>();
            List.AddFirst(GetPoint(x, y));
        }

        /// <summary>
        /// Adds a point to the polygon
        /// </summary>
        public bool Add(int mapCurrentX, int mapCurrentY)
        {
            Point gameLast = List.Last.Value;

            bool addCurrentLocation = false;
            if (World.Default.Map.Display.DisplayManager.CurrentDisplayType == DisplayTypes.Icon)
            {
                var mapLast = World.Default.Map.Display.GetMapLocation(gameLast);
                double distance = Math.Sqrt(Math.Pow(mapCurrentX - mapLast.X, 2) + Math.Pow(mapCurrentY - mapLast.Y, 2));
                addCurrentLocation = distance > 50;
            }
            else
            {
                Debug.Assert(World.Default.Map.Display.DisplayManager.CurrentDisplayType == DisplayTypes.Shape);
                var mapLast = World.Default.Map.Display.GetMapLocation(gameLast);
                double distance = Math.Sqrt(Math.Pow(mapCurrentX - mapLast.X, 2) + Math.Pow(mapCurrentY - mapLast.Y, 2));
                addCurrentLocation = distance > 50;
            }
            

            
            if (addCurrentLocation)
            {
                Point gameCurrent = GetPoint(mapCurrentX, mapCurrentY);
                if (gameCurrent != gameLast || !_differentVillage)
                {
                    List.AddLast(gameCurrent);
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
            Drawing = false;
            List.AddLast(GetPoint(x, y));
        }

        /// <summary>
        /// Returns a value indicating whether a point is inside the polygon
        /// </summary>
        public bool IsHitIn(int x, int y)
        {
            Region areaRegion = GetRegion();
            return areaRegion.IsVisible(GetPoint(x, y));
        }

        /// <summary>
        /// Gets the region defined by the polygon
        /// </summary>
        public Region GetRegion()
        {
            var areaPath = new System.Drawing.Drawing2D.GraphicsPath();
            int x = -1, y = -1;
            foreach (Point p in List)
            {
                if (x > -1)
                {
                    areaPath.AddLine(x, y, p.X, p.Y);
                }
                x = p.X;
                y = p.Y;
            }
            areaPath.CloseFigure();
            return new Region(areaPath);
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