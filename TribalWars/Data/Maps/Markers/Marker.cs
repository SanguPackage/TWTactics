#region Imports
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml;
using TribalWars.Villages;

#endregion

namespace TribalWars.Data.Maps.Markers
{
    /// <summary>
    /// Represents a named collection of Player and Tribe Markers
    /// </summary>
    /// <remarks>
    /// Either Player or Tribe is null.
    /// </remarks>
    public sealed class Marker : IEquatable<Marker>
    {
        #region Constants
        /// <summary>
        /// Names of the You, YourTribe, ...
        /// standard markers
        /// </summary>
        public static class DefaultNames
        {
            public const string You = "You";
            public const string YourTribe = "Your Tribe";
            public const string Enemy = "Enemy";
            public const string Abandoned = "Abandoned";
        }

        private static readonly string[] AllDefaultNames = new[]
            {
                DefaultNames.You,
                DefaultNames.YourTribe,
                DefaultNames.Enemy,
                DefaultNames.Abandoned
            };
        #endregion

        #region Properties
        public MarkerSettings Settings { get; private set; }

        /// <summary>
        /// Returns true when there are no tribes
        /// or players actually being marked
        /// </summary>
        public bool Empty
        {
            get { return Player == null && Tribe == null && !IsDefaultMarker; }
        }

        /// <summary>
        /// Returns true if this is one of the (non deletable)
        /// default markers
        /// </summary>
        private bool IsDefaultMarker
        {
            get { return AllDefaultNames.Contains(Settings.Name); }
        }

        /// <summary>
        /// Gets the tribe
        /// </summary>
        public Tribe Tribe { get; private set; }

        /// <summary>
        /// Gets the player
        /// </summary>
        public Player Player { get; private set; }
        #endregion

        #region Constructor
        public Marker(Player player, MarkerSettings settings)
            : this(settings)
        {
            Debug.Assert(player != null);
            Player = player;
        }

        public Marker(Tribe tribe, MarkerSettings settings)
            : this(settings)
        {
            Debug.Assert(tribe != null);
            Tribe = tribe;
        }

        public Marker(MarkerSettings settings)
        {
            Debug.Assert(settings != null);
            Settings = settings;
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
            var str = "";
            if (Player != null)
            {
                str = string.Format("Player {0}", Player.Name);
            }
            else if (Tribe != null)
            {
                str = string.Format("Tribe {0}", Tribe.Tag);
            }
            else
            {
                str = "NOTHING!!";
            }
            return string.Format("{0} -- {1}", str, Settings);
        }
        #endregion

        #region IEquatable<Marker> Members
        public bool Equals(Marker other)
        {
            if (other == null) return false;
            return Tribe == other.Tribe && Player == other.Player;
        }
        #endregion
    }
}