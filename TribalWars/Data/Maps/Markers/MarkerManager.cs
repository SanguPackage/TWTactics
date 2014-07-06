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
    public sealed class MarkerManager
    {
        #region Fields
        private SortedDictionary<int, MarkerGroup> _markTribe;
        private SortedDictionary<int, MarkerGroup> _markPlayer;
        #endregion

        #region Properties
        /// <summary>
        /// Gets all specific markers 
        /// </summary>
        public List<MarkerGroup> Markers { get; private set; }

        /// <summary>
        /// Gets the markergroup for your own villages
        /// </summary>
        public MarkerGroup YourMarker { get; set; }

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
        public MarkerManager()
        {
            Markers = new List<MarkerGroup>();
            _markPlayer = new SortedDictionary<int, MarkerGroup>();
            _markTribe = new SortedDictionary<int, MarkerGroup>();
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

        /// <summary>
        /// Find out which marker to use for the village
        /// </summary>
        public MarkerGroup GetMarkerGroup(DisplaySettings settings, Village village)
        {
            if (!village.HasPlayer)
            {
                if (settings.HideAbandoned)
                {
                    return null;
                }
                return AbandonedMarker;
            }
            else
            {
                MarkerGroup markerGroup;
                Player ply = village.Player;
                if (_markPlayer.TryGetValue(ply.Id, out markerGroup))
                {
                    return markerGroup;
                }

                if (ply.HasTribe && _markTribe.TryGetValue(ply.Tribe.Id, out markerGroup))
                {
                    return markerGroup;
                }

                if (settings.MarkedOnly)
                {
                    return null;
                }

                return EnemyMarker;
            }
        }

        /// <summary>
        /// Cache all special markers
        /// </summary>
        public void CacheSpecialMarkers()
        {
            _markPlayer = new SortedDictionary<int, MarkerGroup>();
            _markTribe = new SortedDictionary<int, MarkerGroup>();

            CacheYouMarkers();

            foreach (MarkerGroup markerGroup in Markers)
            {
                foreach (Player player in markerGroup.Players)
                {
                    if (!_markPlayer.ContainsKey(player.Id))
                    {
                        _markPlayer.Add(player.Id, markerGroup);
                    }
                }

                foreach (Tribe tribe in markerGroup.Tribes)
                {
                    if (!_markTribe.ContainsKey(tribe.Id))
                    {
                        _markTribe.Add(tribe.Id, markerGroup);
                    }
                }
            }
        }

        /// <summary>
        /// Cache you and your tribe markers
        /// </summary>
        private void CacheYouMarkers()
        {
            Player you = World.Default.You;
            if (you != null)
            {
                _markPlayer.Add(you.Id, YourMarker);
                Tribe youTribe = World.Default.You.Tribe;
                if (youTribe != null)
                {
                    _markTribe.Add(youTribe.Id, YourTribeMarker);
                }
            }
        }
        #endregion
    }
}
