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
        InfoVillage
    }
}
