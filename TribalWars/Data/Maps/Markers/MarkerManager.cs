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
        #region Fields
        private Map _map;

        private List<MarkerGroup> _markers;
        private MarkerGroup _yourMarker;
        private MarkerGroup _yourTribeMarker;
        private MarkerGroup _enemyMarker;
        private MarkerGroup _bonusMarker;
        private MarkerGroup _abandonedMarker;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the map the markers are displayed on
        /// </summary>
        public Map Map
        {
            get { return _map; }
        }

        /// <summary>
        /// Gets all specific markers 
        /// </summary>
        public List<MarkerGroup> Markers
        {
            get { return _markers; }
            set { _markers = value; }
        }

        /// <summary>
        /// Gets the markergroup for your own villages
        /// </summary>
        public MarkerGroup YourMarker
        {
            get { return _yourMarker; }
            set { _yourMarker = value; }
        }

        /// <summary>
        /// Gets the markergroup for bonus villages
        /// </summary>
        public MarkerGroup BonusMarker
        {
            get { return _bonusMarker; }
            set { _bonusMarker = value; }
        }

        /// <summary>
        /// Gets the markergroup for all other villages
        /// </summary>
        public MarkerGroup EnemyMarker
        {
            get { return _enemyMarker; }
            set { _enemyMarker = value; }
        }

        /// <summary>
        /// Gets the markergroup for villages within your tribe
        /// </summary>
        public MarkerGroup YourTribeMarker
        {
            get { return _yourTribeMarker; }
            set { _yourTribeMarker = value; }
        }

        /// <summary>
        /// Gets the markergroup for abandoned villages
        /// </summary>
        public MarkerGroup AbandonedMarker
        {
            get { return _abandonedMarker; }
            set { _abandonedMarker = value; }
        }
        #endregion

        #region Constructors
        public MarkerManager(Map map)
        {
            _map = map;
            _markers = new List<MarkerGroup>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds MarkerGroups to the Manager
        /// </summary>
        public void AddMarker(MarkerGroup[] groups)
        {
            List<Tribe> tribes = new List<Tribe>();
            List<Player> players = new List<Player>();
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

                if (add) _markers.Add(group);
            }
        }
        #endregion

        #region Event Handlers
        #endregion
    }
}
