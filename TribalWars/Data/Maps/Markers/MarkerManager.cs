#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
        private Dictionary<int, MarkerGroup> _markTribe;
        private Dictionary<int, MarkerGroup> _markPlayer;

        private readonly List<MarkerGroup> _markers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets all specific markers 
        /// </summary>
        public IEnumerable<MarkerGroup> Markers
        {
            get { return _markers; }
        }

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
            _markers = new List<MarkerGroup>();
            _markPlayer = new Dictionary<int, MarkerGroup>();
            _markTribe = new Dictionary<int, MarkerGroup>();
        }
        #endregion

        #region Public Methods
        public MarkerGroup GetMarker(Player player)
        {
            Debug.Assert(player != null);
            MarkerGroup found;
            if (_markPlayer.TryGetValue(player.Id, out found))
            {
                return found;
            }
            Debug.Assert(!_markers.Any(x => x.Players.Contains(player)));
            return MarkerGroup.CreateEmpty();
        }

        public MarkerGroup GetMarker(Tribe tribe)
        {
            Debug.Assert(tribe != null);
            MarkerGroup found;
            if (_markTribe.TryGetValue(tribe.Id, out found))
            {
                return found;
            }
            Debug.Assert(!_markers.Any(x => x.Tribes.Contains(tribe)));
            return MarkerGroup.CreateEmpty();
        }

        /// <summary>
        /// Adds MarkerGroups to the Manager
        /// </summary>
        public void AddMarkers(IEnumerable<MarkerGroup> groups)
        {
            foreach (MarkerGroup group in groups)
            {
                if (!group.Empty)
                {
                    _markers.Add(group);
                }
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
            _markPlayer = new Dictionary<int, MarkerGroup>();
            _markTribe = new Dictionary<int, MarkerGroup>();

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
