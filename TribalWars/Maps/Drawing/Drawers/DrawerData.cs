#region Using

#endregion

namespace TribalWars.Maps.Drawers
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
        public string ShapeDrawer { get; private set; }

        /// <summary>
        /// Gets or sets which icondrawer to use
        /// </summary>
        public string IconDrawer { get; private set; }

        /// <summary>
        /// Gets or sets which icondrawer to use when it is a bonus village
        /// </summary>
        public string BonusIconDrawer { get; private set; }

        /// <summary>
        /// Extra info for creating the DrawerBase that
        /// is not present in the Marker.
        /// When it is the same colors for every Marker.
        /// </summary>
        /// <remarks>
        /// For example a color for a BorderDrawer based on the
        /// village type, ...
        /// </remarks>
        public object ExtraDrawerInfo { get; private set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public object Value { get; private set; }
        #endregion

        #region Constructors
        public DrawerData(string shape, string icon, string bonusIcon, object extra)
        {
            ShapeDrawer = shape;
            IconDrawer = icon;
            BonusIconDrawer = bonusIcon;
            ExtraDrawerInfo = extra;
        }

        public DrawerData(string shape, string icon, string bonusIcon, object extra, object value)
        {
            ShapeDrawer = shape;
            IconDrawer = icon;
            BonusIconDrawer = bonusIcon;
            ExtraDrawerInfo = extra;
            Value = value;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return string.Format("Shape:{0}, Icon:{1}", ShapeDrawer, IconDrawer);
        }
        #endregion
    }
}