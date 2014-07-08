namespace TribalWars.Maps.Displays
{
    #region Enums
    /// <summary>
    /// Enumeration of the different ways
    /// a TW map can be displayed
    /// </summary>
    public enum DisplayTypes
    {
        /// <summary>
        /// Wait for the DisplayType to be read
        /// from the settings file
        /// </summary>
        None,
        /// <summary>
        /// Display rectangles etc
        /// </summary>
        Shape,
        /// <summary>
        /// Display the TW images
        /// </summary>
        Icon,
        /// <summary>
        /// Displays all rectangles
        /// </summary>
        MiniMap
    }
    #endregion
}
