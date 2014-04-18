#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Villages;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
#endregion

namespace TribalWars.Data.Maps.Markers
{
    /// <summary>
    /// Contains all markers on a map
    /// </summary>
    public class MarkerManager
    {
        #region Properties
        /// <summary>
        /// Gets the map the markers are displayed on
        /// </summary>
        public Map Map { get; private set; }

        /// <summary>
        /// Gets all specific markers 
        /// </summary>
        public List<MarkerGroup> Markers { get; set; }

        /// <summary>
        /// Gets the markergroup for your own villages
        /// </summary>
        public MarkerGroup YourMarker { get; set; }

        /// <summary>
        /// Gets the markergroup for bonus villages
        /// </summary>
        public MarkerGroup BonusMarker { get; set; }

        /// <summary>
        /// Gets the markergroup for all other villages
        /// </summary>
        public MarkerGroup EnemyMarker { get; set; }

        /// <summary>
        /// Gets the markergroup for villages within your tribe
        /// </summary>
        public MarkerGroup YourTribeMarker { get; set; }

        /// <summary>
        /// Gets the markergroup for abandoned villages
        /// </summary>
        public MarkerGroup AbandonedMarker { get; set; }
        #endregion

        #region Constructors
        public MarkerManager(Map map)
        {
            Map = map;
            Markers = new List<MarkerGroup>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds MarkerGroups to the Manager
        /// </summary>
        public void AddMarker(MarkerGroup[] groups)
        {
            var tribes = new List<Tribe>();
            var players = new List<Player>();
            foreach (MarkerGroup group in groups)
            {
                bool add = true;
                foreach (Tribe p in group.Tribes)
                {
                    if (tribes.Contains(p)) 
                        add = false;
                    else tribes.Add(p);
                }
                foreach (Player p in group.Players)
                {
                    if (players.Contains(p)) 
                        add = false;
                    else players.Add(p);
                }
                if (group.Players.Count == 0 && group.Tribes.Count == 0) add = false;

                if (add) Markers.Add(group);
            }
        }
        #endregion
    }
}
