#region Imports
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TribalWars.Data.Events;
#endregion

namespace TribalWars.Data.Maps
{
    /// <summary>
    /// Represents the view on a map
    /// </summary>
    public class Location : IEquatable<Location>
    {
        #region Events
        #endregion

        #region Fields
        public int _x;
        public int _y;
        public int _zoom;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the X coordinate
        /// </summary>
        public int X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets or sets the Y coordinate
        /// </summary>
        public int Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets or sets the zoom level
        /// </summary>
        public int Zoom
        {
            get { return _zoom; }
        }
        #endregion

        #region Constructors
        public Location()
            : this(500, 500)
        {

        }

        public Location(int x, int y)
        {
            _x = x;
            _y = y;
            _zoom = 1;
        }

        public Location(int x, int y, int zoom)
        {
            _x = x;
            _y = y;
            _zoom = zoom;
        }

        public Location(Location location)
        {
            _x = location.X;
            _y = location.Y;
            _zoom = location.Zoom;
        }

        /// <summary>
        /// Creates a new Location with a different zoom level
        /// </summary>
        public Location(Location location, int zoom)
        {
            _x = location.X;
            _y = location.Y;
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
            return (_x + _y + _zoom).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Location);
        }

        public bool Equals(Location obj)
        {
            if (obj == null) return false;
            return X == obj.X && Y == obj.Y && Zoom == obj.Zoom;
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
