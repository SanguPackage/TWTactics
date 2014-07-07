#region Imports
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml;
using TribalWars.Data.Tribes;
using TribalWars.Data.Players;
#endregion

namespace TribalWars.Data.Maps.Markers
{
    /// <summary>
    /// Represents a named collection of Player and Tribe Markers
    /// </summary>
    public sealed class Marker : IEquatable<Marker>
    {
        #region Properties
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
            get { return Player == null && Player == null; }
        }

        /// <summary>
        /// Gets the tribes marked by the group
        /// </summary>
        public Tribe Tribe { get; set; }

        /// <summary>
        /// Gets the players marked by the group
        /// </summary>
        public Player Player { get; set; }
        #endregion

        #region Constructor
        public Marker(Player player, string name, bool enabled, Color color, Color extraColor, string view)
            : this(name, enabled, color, extraColor, view)
        {
            Player = player;
        }

        public Marker(Tribe tribe, string name, bool enabled, Color color, Color extraColor, string view)
            : this(name, enabled, color, extraColor, view)
        {
            Tribe = tribe;
        }

        public Marker(string name, bool enabled, Color color, Color extraColor, string view)
        {
            Name = name;
            Enabled = enabled;
            Color = color;
            ExtraColor = extraColor;
            View = view;
        }

        public static Marker CreateEmpty()
        {
            return new Marker("", false, Color.Transparent, Color.Transparent, "Points");
        }
        #endregion

        #region Persistence
        public void WriteXml(XmlWriter w)
        {
            if (Player != null)
            {
                w.WriteStartElement("Marker");
                w.WriteAttributeString("Type", "Player");
                w.WriteAttributeString("Name", Player.Name);
                w.WriteEndElement();
            }

            if (Tribe != null)
            {
                Debug.Assert(Player == null);
                w.WriteStartElement("Marker");
                w.WriteAttributeString("Type", "Tribe");
                w.WriteAttributeString("Name", Tribe.Tag);
                w.WriteEndElement();
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

        #region IEquatable<Marker> Members
        public bool Equals(Marker other)
        {
            if (other == null) return false;
            return View == other.View
                && Color == other.Color 
                && ExtraColor == other.ExtraColor;
        }
        #endregion
    }
}