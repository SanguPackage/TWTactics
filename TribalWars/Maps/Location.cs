#region Imports
using System;
using System.Drawing;

#endregion

namespace TribalWars.Maps
{
    /// <summary>
    /// Represents the view on a map. All coordinates are GAME coordinates.
    /// </summary>
    public sealed class Location : IEquatable<Location>
    {
        #region Fields
        private readonly Point _point;
        private readonly int _zoom;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the X coordinate
        /// </summary>
        public int X
        {
            get { return _point.X; }
        }

        /// <summary>
        /// Gets the Y coordinate
        /// </summary>
        public int Y
        {
            get { return _point.Y; }
        }

        /// <summary>
        /// Gets the X &amp; Y coordinates
        /// </summary>
        public Point Point
        {
            get { return _point; }
        }

        /// <summary>
        /// Gets the zoom level
        /// </summary>
        public int Zoom
        {
            get { return _zoom; }
        }
        #endregion

        #region Constructors
        public Location(int x, int y, int zoom)
        {
            _point = new Point(x, y);
            _zoom = zoom;
        }

        public Location(Point loc, int zoom)
        {
            _point = loc;
            _zoom = zoom;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return string.Format("{0}|{1} (X{2})", X, Y, Zoom);
        }
        #endregion

        #region IEquatable<MapLocation> Members
        public override int GetHashCode()
        {
            return (Point.X + Point.Y + _zoom).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Location);
        }

        public bool Equals(Location obj)
        {
            if (obj == null) return false;
            return _point == obj.Point && Zoom == obj.Zoom;
        }

        public static bool operator ==(Location left, Location right)
        {
            if (ReferenceEquals(left, right)) return true;
            if ((object)left == null || (object)right == null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Location left, Location right)
        {
            return !(left == right);
        }
        #endregion
    }
}
