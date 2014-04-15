using System;
using System.Collections.Generic;
using System.Text;

namespace TribalWars.Controls.Main.Browser
{
    /// <summary>
    /// The destination for a browse event
    /// </summary>
    public enum DestinationEnum
    {
        /// <summary>
        /// Opens TW stats for the selected village
        /// </summary>
        TWStatsVillage,
        /// <summary>
        /// Opens TW stats for the selected player
        /// </summary>
        TWStatsPlayer,
        /// <summary>
        /// Opens TW stats for the selected tribe
        /// </summary>
        TWStatsTribe,
        /// <summary>
        /// Navigates to the global village overview page
        /// </summary>
        Overview,
        /// <summary>
        /// Navigates to a village overview page
        /// </summary>
        Info_Village
    }
}
