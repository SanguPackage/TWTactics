namespace TribalWars.Maps.Drawing.Drawers
{
    /// <summary>
    /// Holds the data for creating a DrawerBase
    /// </summary>
    public sealed class BackgroundDrawerData
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
        #endregion

        #region Constructors
        public BackgroundDrawerData(string shape, string icon, string bonusIcon)
        {
            ShapeDrawer = shape;
            IconDrawer = icon;
            BonusIconDrawer = bonusIcon;
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