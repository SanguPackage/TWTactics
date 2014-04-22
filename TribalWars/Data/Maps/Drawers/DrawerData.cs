#region Using

#endregion

namespace TribalWars.Data.Maps.Drawers
{
    /// <summary>
    /// Holds the data for creating a DrawerBase
    /// for any DisplayType
    /// </summary>
    public sealed class DrawerData
    {
        #region Properties
        /// <summary>
        /// Gets or sets which shapedrawer to use
        /// </summary>
        public string ShapeDrawer { get; set; }

        /// <summary>
        /// Gets or sets which icondrawer to use
        /// </summary>
        public string IconDrawer { get; set; }

        /// <summary>
        /// Extra info for creating the DrawerBase that
        /// is not present in the MarkerGroup.
        /// When it is the same colors for every MarkerGroup.
        /// </summary>
        /// <remarks>
        /// For example a color for a BorderDrawer based on the
        /// village type, ...
        /// </remarks>
        public object ExtraDrawerInfo { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public object Value { get; set; }
        #endregion

        #region Constructors
        public DrawerData(string shape, string icon)
        {
            ShapeDrawer = shape;
            IconDrawer = icon;
        }

        public DrawerData(string shape, string icon, object extra)
        {
            ShapeDrawer = shape;
            IconDrawer = icon;
            ExtraDrawerInfo = extra;
        }

        public DrawerData(string shape, string icon, object extra, object value)
        {
            ShapeDrawer = shape;
            IconDrawer = icon;
            ExtraDrawerInfo = extra;
            Value = value;
        }

        public DrawerData(DrawerData data)
        {
            ShapeDrawer = data.ShapeDrawer;
            IconDrawer = data.IconDrawer;
            ExtraDrawerInfo = data.ExtraDrawerInfo;
            Value = data.Value;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return string.Format("Shape:{0},Icon:{1}", ShapeDrawer, IconDrawer);
        }
        #endregion
    }
}