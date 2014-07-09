#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Markers
{
    /// <summary>
    /// Contains all markers on a map and keeps a
    /// cache that combines the default (You, Your
    /// Tribe, Enemy and Abandoned) and user
    /// defined markers
    /// </summary>
    public sealed class MarkerManager
    {
        #region Fields
        // Cache: _markers + YourMarker + YourTribeMarker
        private Dictionary<int, Marker> _markTribe;
        private Dictionary<int, Marker> _markPlayer;

        private readonly List<Marker> _markers;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the marker for your own villages
        /// </summary>
        private Marker YourMarker { get; set; }

        /// <summary>
        /// Gets the marker for all other villages
        /// </summary>
        private Marker EnemyMarker { get; set; }

        /// <summary>
        /// Gets the marker settinggs for all other villages
        /// </summary>
        public MarkerSettings EnemyMarkerSettings
        {
            get { return EnemyMarker.Settings; }
        }

        /// <summary>
        /// Gets the marker for villages within your tribe
        /// </summary>
        private Marker YourTribeMarker { get; set; }

        /// <summary>
        /// Gets the marker for abandoned villages
        /// </summary>
        private Marker AbandonedMarker { get; set; }

        /// <summary>
        /// Gets the marker settings for abandoned villages
        /// </summary>
        public MarkerSettings AbandonedMarkerSettings
        {
            get { return AbandonedMarker.Settings; }
        }
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
        /// Update Enemy or Abandoned markers
        /// </summary>
        public void UpdateDefaultMarker(Map map, MarkerSettings settings)
        {
            if (settings.Name == Marker.DefaultNames.Abandoned)
            {
                AbandonedMarker = new Marker(settings);
            }
            else if (settings.Name == Marker.DefaultNames.Enemy)
            {
                EnemyMarker = new Marker(settings);
            }
            else
            {
                Debug.Assert(false, "'You' and 'Your Tribe' markers are updated through the regular UpdateMarker methods");
            }

            InvalidateMarkers();
            map.Invalidate();
        }

        /// <summary>
        /// Update a player marker and refresh the map
        /// </summary>
        public void UpdateMarker(Map map, Player player, MarkerSettings settings)
        {
            if (player == World.Default.You)
            {
                YourMarker = new Marker(settings);
            }
            else
            {
                _markers.RemoveAll(x => x.Player == player);
                _markers.Add(new Marker(player, settings));
            }

            InvalidateMarkers();
            map.Invalidate();
        }

        /// <summary>
        /// Update a tribe marker and refresh the map
        /// </summary>
        public void UpdateMarker(Map map, Tribe tribe, MarkerSettings settings)
        {
            if (World.Default.You.HasTribe && tribe == World.Default.You.Tribe)
            {
                YourTribeMarker = new Marker(settings);
            }
            else
            {
                _markers.RemoveAll(x => x.Tribe == tribe);
                _markers.Add(new Marker(tribe, settings));
            }

            InvalidateMarkers();
            map.Invalidate();
        }

        /// <summary>
        /// Gets all user defined markers
        /// </summary>
        public IEnumerable<MarkerGridRow> GetMarkers()
        {
            return _markers.Select(x => new MarkerGridRow(x));
        }

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

            MarkerSettings defaultSettings = EnemyMarker.Settings;
            if (player.HasTribe)
            {
                Marker marker;
                if (_markTribe.TryGetValue(player.Tribe.Id, out marker))
                {
                    defaultSettings = marker.Settings;
                }
            }
            return new Marker(MarkerSettings.Create(defaultSettings.Color, defaultSettings.View));
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

            return new Marker(MarkerSettings.Create(EnemyMarker.Settings.Color, EnemyMarker.Settings.View));
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
                if (_markPlayer.TryGetValue(ply.Id, out marker) && marker.Settings.Enabled)
                {
                    return marker;
                }

                if (ply.HasTribe && _markTribe.TryGetValue(ply.Tribe.Id, out marker) && marker.Settings.Enabled)
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
        /// Rebuild marker cache
        /// </summary>
        public void InvalidateMarkers()
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
                if (marker.Tribe != null && !_markTribe.ContainsKey(marker.Tribe.Id))
                {
                    _markTribe.Add(marker.Tribe.Id, marker);
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

        #region Persistence
        #region Save
        public void WriteUserDefinedMarkers(XmlWriter w)
        {
            foreach (Marker marker in _markers)
            {
                WriteMarker(w, marker);
            }
        }

        public void WriteDefaultMarkers(XmlWriter w)
        {
            WriteMarker(w, YourMarker);
            WriteMarker(w, YourTribeMarker);
            WriteMarker(w, EnemyMarker);
            WriteMarker(w, AbandonedMarker);
        }

        /// <summary>
        /// Writes a marker to the XML node
        /// </summary>
        private static void WriteMarker(XmlWriter w, Marker marker)
        {
            w.WriteStartElement("Marker");
            w.WriteAttributeString("Name", marker.Settings.Name);
            w.WriteAttributeString("Enabled", marker.Settings.Enabled.ToString());
            w.WriteAttributeString("Color", XmlHelper.SetColor(marker.Settings.Color));
            w.WriteAttributeString("ExtraColor", XmlHelper.SetColor(marker.Settings.ExtraColor));
            w.WriteAttributeString("View", marker.Settings.View);

            marker.WriteXml(w);

            w.WriteEndElement();
        }
        #endregion

        #region Read
        public void ReadUserDefinedMarkers(XmlReader r)
        {
            var markers = new List<Marker>();
            while (r.IsStartElement("Marker"))
            {
                markers.Add(ReadMarker(r));
            }
            SetUserDefinedMarkers(markers);
        }

        public void ReadDefaultMarkers(XmlReader r)
        {
            YourMarker = ReadMarker(r);
            Debug.Assert(YourMarker.Settings.Name == Marker.DefaultNames.You);
            YourTribeMarker = ReadMarker(r);
            Debug.Assert(YourTribeMarker.Settings.Name == Marker.DefaultNames.YourTribe);
            EnemyMarker = ReadMarker(r);
            Debug.Assert(EnemyMarker.Settings.Name == Marker.DefaultNames.Enemy);
            AbandonedMarker = ReadMarker(r);
            Debug.Assert(AbandonedMarker.Settings.Name == Marker.DefaultNames.Abandoned);
        }

        /// <summary>
        /// Reads a Marker from the XML node
        /// </summary>
        private static Marker ReadMarker(XmlReader r)
        {
            string name = r.GetAttribute("Name");
            bool enabled = Convert.ToBoolean(r.GetAttribute("Enabled").ToLower());
            Color color = XmlHelper.GetColor(r.GetAttribute("Color"));
            Color extraColor = XmlHelper.GetColor(r.GetAttribute("ExtraColor"));
            string view = r.GetAttribute("View");
            var settings = new MarkerSettings(name, enabled, color, extraColor, view);
            Marker marker = null;

            if (!r.IsEmptyElement)
            {
                r.ReadStartElement();
                while (r.IsStartElement("Marker"))
                {
                    string markerType = r.GetAttribute("Type");
                    string markerName = r.GetAttribute("Name");

                    if (markerType == "Player")
                    {
                        Player ply = World.Default.GetPlayer(markerName);
                        if (ply != null)
                        {
                            marker = new Marker(ply, settings);
                        }
                    }
                    else
                    {
                        Debug.Assert(markerType == "Tribe");
                        Tribe tribe = World.Default.GetTribe(markerName);
                        if (tribe != null)
                        {
                            marker = new Marker(tribe, settings);
                        }
                    }

                    r.Read();
                }
                r.ReadEndElement();
            }
            else
            {
                r.Read();
            }

            return marker ?? new Marker(settings);
        }

        /// <summary>
        /// Adds Markers to the Manager
        /// </summary>
        private void SetUserDefinedMarkers(IEnumerable<Marker> markers)
        {
            _markers.Clear();
            foreach (Marker marker in markers)
            {
                if (!marker.Empty)
                {
                    _markers.Add(marker);
                }
            }
        }
        #endregion
        #endregion
    }
}
