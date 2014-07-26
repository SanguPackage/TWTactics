using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TribalWars.Maps.Displays
{
    /// <summary>
    /// Encapsulates the current zoom level
    /// and the zoom boundries
    /// </summary>
    public class ZoomInfo
    {
        #region Properties
        /// <summary>
        /// Gets the minimum zoom level
        /// </summary>
        public int Minimum { get; private set; }

        /// <summary>
        /// Gets the maximum zoom level
        /// </summary>
        public int Maximum { get; private set; }

        /// <summary>
        /// Gets or sets the zoom level that will be used
        /// when the user switches back to this displaytype
        /// </summary>
        public int Current { get; private set; }
        #endregion

        #region Constructors
        public ZoomInfo(int min, int max, int current)
        {
            Minimum = min;
            Maximum = max;
            Current = current;
        }
        #endregion

        /// <summary>
        /// Returns the <see cref="Location"/> parameter or an updated
        /// one if the zoom level is invalid for the <see cref="Display"/>
        /// </summary>
        public Location Validate(Location location)
        {
            if (location.Zoom < Minimum)
            {
                return new Location(location.Display, location.Point, Minimum);
            }
            if (location.Zoom > Maximum)
            {
                return new Location(location.Display, location.Point, Maximum);
            }
            return location;
        }

        public override string ToString()
        {
            return string.Format("Min={0}, Max={1}, Current={2}", Minimum, Maximum, Current);
        }
    }
}
