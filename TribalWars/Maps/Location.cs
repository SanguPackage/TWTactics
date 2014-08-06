#region Imports
using System;
using System.Drawing;
using TribalWars.Maps.Drawing.Displays;

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

        /// <summary>
        /// Shape or Icon display
        /// </summary>
        public DisplayTypes Display { get; private set; }
        #endregion

        #region Constructors
        public Location(DisplayTypes display, int x, int y, int zoom)
            : this(display, new Point(x, y), zoom)
        {
        }

        public Location(DisplayTypes display, Point loc, int zoom)
        {
            Display = display;
            _point = loc;
            _zoom = zoom;
        }
        #endregion

        #region Public Methods
        public Location ChangeShapeAndZoom(DisplayTypes displayType, int zoom)
        {
            return new Location(displayType, _point, zoom);
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}|{2} (X{3})", Display, X, Y, Zoom);
        }
        #endregion

        #region IEquatable<MapLocation> Members
        public override int GetHashCode()
        {
            return ((int)Display + Point.X + Point.Y + _zoom).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Location);
        }

        public bool Equals(Location obj)
        {
            if (obj == null) return false;
            return Display == obj.Display && _point == obj.Point && Zoom == obj.Zoom;
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
