#region Imports
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TribalWars.Data.Tribes;
using TribalWars.Data.Players;
#endregion

namespace TribalWars.Data.Maps.Markers
{
    /// <summary>
    /// Represents a named collection of Player and Tribe Markers
    /// </summary>
    public sealed class MarkerGroup : IEquatable<MarkerGroup>
    {
        #region Fields
        private readonly List<Tribe> _tribes;
        private readonly List<Player> _players;
        //private bool _active;
        #endregion

        #region Properties
        
        //public class MarkerSettings
        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating the markers are to be drawn
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// Gets or sets the secundary color for the marker
        /// </summary>
        public Color ExtraColor { get; private set; }

        /// <summary>
        /// Gets or sets the primary color for the marker
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// Gets or sets how to represent the marked
        /// villages on the map
        /// </summary>
        public string View { get; private set; }

        /// <summary>
        /// Returns true when there are no tribes
        /// or players actually being marked
        /// </summary>
        public bool Empty
        {
            get { return !_players.Any() && !_tribes.Any(); }
        }

        /// <summary>
        /// Gets the tribes marked by the group
        /// </summary>
        public IEnumerable<Tribe> Tribes
        {
            get { return _tribes; }
        }

        /// <summary>
        /// Gets the players marked by the group
        /// </summary>
        public IEnumerable<Player> Players
        {
            get { return _players; }
        }
        #endregion

        #region Constructor
        public MarkerGroup(string name, bool enabled, Color color, Color extraColor, string view)
        {
            Name = name;
            Enabled = enabled;
            Color = color;
            ExtraColor = extraColor;
            View = view;

            _players = new List<Player>();
            _tribes = new List<Tribe>();
        }
        
        public static MarkerGroup CreateEmpty()
        {
            return new MarkerGroup("", false, Color.Transparent, Color.Transparent, "Points");
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a PlayerMarker to the collection
        /// </summary>
        public void Add(Player player)
        {
            if (player != null)
            {
                if (!_players.Contains(player))
                    _players.Add(player);
            }
        }

        /// <summary>
        /// Adds a TribeMarker to the collection
        /// </summary>
        public void Add(Tribe tribe)
        {
            if (tribe != null)
            {
                if (!_tribes.Contains(tribe))
                    _tribes.Add(tribe);
            }
        }

        /// <summary>
        /// Removes a PlayerMarker from the collection
        /// </summary>
        public void Remove(Player player)
        {
            if (player != null)
            {
                if (_players.Contains(player))
                    _players.Remove(player);
            }
        }

        /// <summary>
        /// Removes a TribeMarker from the collection
        /// </summary>
        public void Remove(Tribe tribe)
        {
            if (tribe != null)
            {
                if (_tribes.Contains(tribe))
                    _tribes.Remove(tribe);
            }
        }
        #endregion

        #region Overriden Methods
        public override string ToString()
        {
            string views = string.Empty;
            if (View != null) views = View;
            return string.Format("{0} ({1} - {2})", Name, views, Color.ToKnownColor()); 
        }
        #endregion

        #region IEquatable<MarkerGroup> Members
        public bool Equals(MarkerGroup other)
        {
            if (other == null) return false;
            return View == other.View
                && Color == other.Color 
                && ExtraColor == other.ExtraColor;
        }
        #endregion
    }
}