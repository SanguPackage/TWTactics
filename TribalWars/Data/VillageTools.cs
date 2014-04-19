#region Using

#endregion

namespace TribalWars.Data
{
    /// <summary>
    /// The actions that can be performed on the world
    /// </summary>
    public enum VillageTools
    {
        ///// <summary>
        ///// Adds a village as a new target for the attack planner
        ///// </summary>
        //DistanceCalculationTarget,
        ///// <summary>
        ///// Add a village to attack from for the attack planner
        ///// </summary>
        //DistanceCalculation,
        //BBCodePolygonDraw,
        //BBCodePolygonSelect,
        //Notes,

        /// <summary>
        /// Changes the center of the map
        /// </summary>
        ChangeCenter,
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
        /// Plans an attack
        /// </summary>
        Attack,
        /// <summary>
        /// Add notes to a TW element
        /// </summary>
        Notes
    }
}
