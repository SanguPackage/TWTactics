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
        private Dictionary<int, Marker> _markTribe;
        private Dictionary<int, Marker> _markPlayer;

        private readonly List<Marker> _markers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets all specific markers 
        /// </summary>
        public IEnumerable<Marker> Markers
        {
            get { return _markers; }
        }

        /// <summary>
        /// Gets the marker for your own villages
        /// </summary>
        public Marker YourMarker { get; set; }

        /// <summary>
        /// Gets the marker for all other villages
        /// </summary>
        public Marker EnemyMarker { get; set; }

        /// <summary>
        /// Gets the marker for villages within your tribe
        /// </summary>
        public Marker YourTribeMarker { get; set; }

        /// <summary>
        /// Gets the marker for abandoned villages
        /// </summary>
        public Marker AbandonedMarker { get; set; }
        #endregion

        #region Constructors
        public MarkerManager()
        {
            _markers = new List<Marker>();
            _markPlayer = new Dictionary<int, Marker>();
            _markTribe = new Dictionary<int, Marker>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the marker for the player or
        /// return a new one
        /// </summary>
        public Marker GetMarker(Player player)
        {
            Debug.Assert(player != null);
            Marker found;
            if (_markPlayer.TryGetValue(player.Id, out found))
            {
                return found;
            }
            return Marker.CreateEmpty();
        }

        /// <summary>
        /// Gets the marker for the tribe or
        /// return a new one
        /// </summary>
        public Marker GetMarker(Tribe tribe)
        {
            Debug.Assert(tribe != null);
            Marker found;
            if (_markTribe.TryGetValue(tribe.Id, out found))
            {
                return found;
            }
            return Marker.CreateEmpty();
        }

        /// <summary>
        /// Find out which marker to use for the village
        /// </summary>
        public Marker GetMarker(DisplaySettings settings, Village village)
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
                Marker marker;
                Player ply = village.Player;
                if (_markPlayer.TryGetValue(ply.Id, out marker))
                {
                    return marker;
                }

                if (ply.HasTribe && _markTribe.TryGetValue(ply.Tribe.Id, out marker))
                {
                    return marker;
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
            _markPlayer = new Dictionary<int, Marker>();
            _markTribe = new Dictionary<int, Marker>();

            CacheYouMarkers();

            foreach (Marker marker in _markers)
            {
                if (marker.Player != null && !_markPlayer.ContainsKey(marker.Player.Id))
                {
                    _markPlayer.Add(marker.Player.Id, marker);
                }
                if (marker.Tribe != null && _markTribe.ContainsKey(marker.Tribe.Id))
                {
                    _markTribe.Add(marker.Tribe.Id, marker);
                }
            }
        }

        /// <summary>
        /// Adds Marker to the Manager
        /// </summary>
        public void AddMarkers(IEnumerable<Marker> markers)
        {
            foreach (Marker marker in markers)
            {
                if (!marker.Empty)
                {
                    _markers.Add(marker);
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
