#region Using

#endregion

namespace TribalWars.Worlds.Events
{
    /// <summary>
    /// The actions that can be performed on the world
    /// </summary>
    public enum VillageTools
    {
        /// <summary>
        /// Puts the pinpoint on village(s)
        /// </summary>
        PinPoint,
        /// <summary>
        /// Sets the statusbar details for the village
        /// </summary>
        Default,
        /// <summary>
        /// Shows the details for a village, player or tribe
        /// </summary>
        SelectVillage,
        /// <summary>
        /// Sets a village as the source for the DistanceToolStrip
        /// </summary>
        Distance,
        /// <summary>
        /// Add notes to a TW element
        /// </summary>
        Notes
    }
}
