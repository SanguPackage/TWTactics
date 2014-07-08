namespace TribalWars.Browsers.Control
{
    /// <summary>
    /// The destination for a browse event
    /// </summary>
    public enum DestinationEnum
    {
        /// <summary>
        /// Opens TW stats for the selected village
        /// </summary>
        TwStatsVillage,
        /// <summary>
        /// Opens TW stats for the selected player
        /// </summary>
        TwStatsPlayer,
        /// <summary>
        /// Opens TW stats for the selected tribe
        /// </summary>
        TwStatsTribe,
        /// <summary>
        /// Navigates to the global village overview page
        /// </summary>
        Overview,
        /// <summary>
        /// Navigates to a village overview page
        /// </summary>
        InfoVillage,
        /// <summary>
        /// Official TW guest page for player
        /// </summary>
        GuestPlayer,
        /// <summary>
        /// Official TW guest page for tribe
        /// </summary>
        GuestTribe
    }
}
