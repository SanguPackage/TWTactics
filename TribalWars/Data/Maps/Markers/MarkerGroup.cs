#region Imports
using System;
using System.Collections.Generic;
using System.Drawing;
using TribalWars.Data.Tribes;
using TribalWars.Data.Players;
#endregion

namespace TribalWars.Data.Maps.Markers
{
    /// <summary>
    /// Represents a named collection of Player, Tribe and VillageMarkers
    /// </summary>
    public sealed class MarkerGroup : IEquatable<MarkerGroup>
    {
        #region Properties
        /// <summary>
        /// Gets the tribes marked by the group
        /// </summary>
        public List<Tribe> Tribes { get; private set; }

        /// <summary>
        /// Gets the players marked by the group
        /// </summary>
        public List<Player> Players { get; private set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating the markers are to be drawn
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the secundary color for the marker
        /// </summary>
        public Color ExtraColor { get; set; }

        /// <summary>
        /// Gets or sets the primary color for the marker
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets how to represent the marked
        /// villages on the map
        /// </summary>
        public string View { get; set; }

        /// <summary>
        /// Gets or sets how to extra decorate the marked
        /// villages on the map
        /// </summary>
        public string Decorator { get; set; }

        /// <summary>
        /// Gets a value indicating whether 
        /// the markergroup has a decorator
        /// </summary>
        public bool HasDecorator
        {
            get { return Decorator != null; }
        }

        /// <summary>
        /// Gets the marker manager this group belongs to
        /// </summary>
        public MarkerManager MarkerManager { get; private set; }
        #endregion

        #region Constructor
        public MarkerGroup(Map map, string name, bool enabled, Color color, Color extraColor, string view, string decorator)
        {
            MarkerManager = map.MarkerManager;
            Name = name;
            Enabled = enabled;
            Color = color;
            ExtraColor = extraColor;
            View = view;
            Decorator = decorator;

            Players = new List<Player>();
            Tribes = new List<Tribe>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a PlayerMarker to the collection
        /// </summary>
        public void Add(PlayerMarker itm)
        {
            if (itm != null && itm.Player != null)
            {
                if (!Players.Contains(itm.Player))
                    Players.Add(itm.Player);
            }
        }

        /// <summary>
        /// Adds a TribeMarker to the collection
        /// </summary>
        public void Add(TribeMarker itm)
        {
            if (itm != null && itm.Tribe != null)
            {
                if (!Tribes.Contains(itm.Tribe))
                    Tribes.Add(itm.Tribe);
            }
        }

        /// <summary>
        /// Removes a PlayerMarker from the collection
        /// </summary>
        public void Remove(PlayerMarker itm)
        {
            if (itm != null && itm.Player != null)
            {
                if (Players.Contains(itm.Player))
                    Players.Remove(itm.Player);
            }
        }

        /// <summary>
        /// Removes a TribeMarker from the collection
        /// </summary>
        public void Remove(TribeMarker itm)
        {
            if (itm != null && itm.Tribe != null)
            {
                if (Tribes.Contains(itm.Tribe))
                    Tribes.Remove(itm.Tribe);
            }
        }
        #endregion

        #region Overriden Methods
        public override string ToString()
        {
            string views = string.Empty;
            if (View != null) views = View;
            if (Decorator != null) views += (View == null ? "" : ", ") + Decorator;
            return string.Format("{0} ({1} - {2})", Name, views, Color.ToKnownColor()); 
        }
        #endregion

        #region IEquatable<MarkerGroup> Members
        public bool Equals(MarkerGroup other)
        {
            if (other == null) return false;
            return View == other.View && Decorator == other.Decorator
                && Color == other.Color && ExtraColor == other.ExtraColor;
        }
        #endregion
    }
}