#region Using
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TribalWars.Data.Villages;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Helpers
{
    /// <summary>
    /// Encapsulates the Polygon defined by userdrawing
    /// </summary>
    public class Polygon : IEquatable<Polygon>
    {
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
        /// Gets or sets an string id identifying the polygon
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Group polygons together with this string.
        /// Grouped polygons can be manipulated together.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the color of the polygon lines / rectangles
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// polygon should be drawn to the map
        /// </summary>
        public bool Visible { get; set; }
        #endregion

        #region Constructors
        public Polygon(string name, int x, int y)
        {
            Name = name;
            Visible = true;
            Drawing = true;
            List = new LinkedList<Point>();
            List.AddFirst(GetPoint(x, y));
            LineColor = Color.White;
            Group = "";
        }

        public Polygon(string name, bool visible, Color color, string group, IEnumerable<Point> points)
        {
            Name = name;
            Visible = visible;
            List = new LinkedList<Point>(points);
            LineColor = color;
            Group = group;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the villages inside the polygon
        /// </summary>
        public IEnumerable<Village> GetVillages()
        {
            var villages = new List<Village>();
            Region areaRegion = GetRegion();
            villages.AddRange(World.Default.Villages.Values.Where(vil => areaRegion.IsVisible(vil.Location)));
            return villages;
        }

        /// <summary>
        /// Adds a point to the polygon
        /// </summary>
        public void Add(int mapCurrentX, int mapCurrentY)
        {
            Point gameCurrent = GetPoint(mapCurrentX, mapCurrentY);
            List.AddLast(gameCurrent);
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
        /// Move the entire polygon with the parameter
        /// </summary>
        public void Move(Point amount)
        {
            var newList = new LinkedList<Point>();
            foreach (Point point in List)
            {
                // Create a new list because Point is a struct
                // (the 'point' variable is not contained in List)
                point.Offset(amount);
                newList.AddLast(point);
            }
            List = newList;
        }

        /// <summary>
        /// Gets the region defined by the polygon
        /// </summary>
        private Region GetRegion()
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

        public bool Equals(Polygon other)
        {
            if (ReferenceEquals(other, null)) return false;
            return ReferenceEquals(List, other.List);
        }

        public override int GetHashCode()
        {
            return List.GetHashCode();
        }

        public override string ToString()
        {
            var str = string.Format("Name={0}, Points={1}, IsDrawing={2}", Name, List.Count, Drawing);
            if (!Visible)
            {
                str += " (Not Visible!)";
            }
            return str;
        }
        #endregion
    }
}