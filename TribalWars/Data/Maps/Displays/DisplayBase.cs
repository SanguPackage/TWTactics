#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps.Markers;
#endregion

namespace TribalWars.Data.Maps.Displays
{
    /// <summary>
    /// Base class for the Shape and IconDisplays
    /// </summary>
    public abstract class DisplayBase
    {
        #region Fields
        protected Dictionary<Data, DrawerBase> _cache;
        private ZoomInfo _zoom;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the zoom possibilities of the display
        /// </summary>
        public ZoomInfo Zoom
        {
            get { return _zoom; }
        }

        /// <summary>
        /// Returns a value indicating whether the display supports decorating villages
        /// </summary>
        public virtual bool SupportDecorators
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Constructors
        public DisplayBase(ZoomInfo zoom)
        {
            _cache = new Dictionary<Data, DrawerBase>();
            _zoom = zoom;
        }
        #endregion

        #region Public Methods
        public DrawerBase CreateDrawer(DrawerData data, MarkerGroup colors)
        {
            return CreateDrawer(data, colors, null);
        }

        /// <summary>
        /// Create a new drawer
        /// </summary>
        /// <param name="data">The shape of the drawer</param>
        /// <param name="colors">The colors for the drawer</param>
        /// <param name="mainData">The data for the main drawer (used for BorderDrawer)</param>
        public DrawerBase CreateDrawer(DrawerData data, MarkerGroup colors, DrawerData mainData)
        {
            Data dataHolder = CreateData(data, colors, mainData);
            if (_cache.ContainsKey(dataHolder)) return _cache[dataHolder];

            DrawerBase drawer = CreateDrawerCore(data, colors, mainData);
            _cache.Add(dataHolder, drawer);
            return drawer;

            // TODO: find out if the current implementation of flyweight for the drawers
            // is actually useful because it slows the map drawing down a little...
            // simply deleting _cache field to stop it being a flyweight
            //return CreateDrawerCore(data, colors, mainData);
        }

        /// <summary>
        /// Gets drawer for a location on the map where there is no village present
        /// </summary>
        public DrawerBase CreateNonVillageDrawer(Point game)
        {
            return CreateNonVillageDrawerCore(game);
        }

        /// <summary>
        /// Gets drawer for a location on the map where there is no village present
        /// </summary>
        protected virtual DrawerBase CreateNonVillageDrawerCore(Point game)
        {
            return null;
        }

        /// <summary>
        /// Gets the width of a village WITH the spacing to the next village
        /// </summary>
        public abstract int GetVillageWidthSpacing(int zoom);

        /// <summary>
        /// Gets the height of a village WITH the spacing to the next village
        /// </summary>
        public abstract int GetVillageHeightSpacing(int zoom);

        /// <summary>
        /// Gets the width of a village
        /// </summary>
        public abstract int GetVillageWidth(int zoom);

        /// <summary>
        /// Gets the height of a village
        /// </summary>
        public abstract int GetVillageHeight(int zoom);

        protected abstract Data CreateData(DrawerData data, MarkerGroup colors, DrawerData mainData);

        protected abstract DrawerBase CreateDrawerCore(DrawerData data, MarkerGroup colors, DrawerData mainData);

        /// <summary>
        /// Resets the cache
        /// </summary>
        public void Reset()
        {

        }
        #endregion

        #region Data Structure
        /// <summary>
        /// If two Data objects are equal, a cached DrawerBase can be used
        /// to draw the location
        /// </summary>
        protected struct Data : IEquatable<Data>
        {
            #region Fields
            public string Type;
            public Color Color;
            public Color ExtraColor;
            public object Value;
            #endregion

            #region Constructors
            public Data(string type, Color color, Color extraColor)
            {
                Type = type;
                Color = color;
                ExtraColor = extraColor;
                Value = null;
            }

            public Data(string type, Color color, Color extraColor, object value)
            {
                Type = type;
                Color = color;
                ExtraColor = extraColor;
                Value = value;
            }
            #endregion

            #region Public Methods
            public bool Equals(Data other)
            {
                return Type == other.Type && Color == other.Color && ExtraColor == other.ExtraColor && Value == other.Value;
            }
            
            public override string ToString()
            {
                return string.Format("Type:{0},Color:{1},{2} (Value: {3}", Type, Color.ToString(), ExtraColor.ToString(), Value);
            }
            #endregion
        }
        #endregion

        #region ZoomInfo
        /// <summary>
        /// Encapsulates the current zoom level
        /// and the zoom boundries
        /// </summary>
        public class ZoomInfo
        {
            #region Fields
            private int _min;
            private int _max;
            private int _current;
            #endregion

            #region Properties
            /// <summary>
            /// Gets the minimum zoom level
            /// </summary>
            public int Minimum
            {
                get { return _min; }
            }

            /// <summary>
            /// Gets the maximum zoom level
            /// </summary>
            public int Maximum
            {
                get { return _max; }
            }

            /// <summary>
            /// Gets or sets the zoom level that will be used
            /// when the user switches back to this displaytype
            /// </summary>
            public int Current
            {
                get { return _current; }
                set { _current = value; }
            }
            #endregion

            #region Constructors
            public ZoomInfo(int min, int max, int current)
            {
                _min = min;
                _max = max;
                _current = current;
            }
            #endregion
        }
        #endregion
    }
}
