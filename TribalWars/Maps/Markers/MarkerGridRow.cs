using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TribalWars.Properties;
using TribalWars.Tools;
using TribalWars.Villages;

namespace TribalWars.Maps.Markers
{
    /// <summary>
    /// A marker as displayed in the MarkersControl.GridEX
    /// </summary>
    public class MarkerGridRow
    {
        #region Properties
        public bool Enabled { get; set; }
        public Color Color { get; set; }
        public Color ExtraColor { get; set; }
        public Tribe Tribe { get; set; }
        public Player Player { get; set; }
        public string View { get; set; }

        public string Name
        {
            get
            {
                if (Tribe != null)
                    return Tribe.Tag;

                if (Player != null)
                    return Player.Name;

                return "";
            }
            set { }
        }

        public object SelectedPlayerOrTribe
        {
            get
            {
                if (Tribe != null)
                {
                    return Tribe;
                }
                if (Player != null)
                {
                    return Player;
                }
                return "";
            }
            set
            {
                Player = null;
                Tribe = null;

                var tribe = value as Tribe;
                if (tribe != null)
                {
                    Tribe = tribe;
                }
                else
                {
                    var player = value as Player;
                    if (player != null)
                    {
                        Player = player;
                    }
                }
            }
        }
        #endregion

        #region Constructors
        public MarkerGridRow(Marker marker)
        {
            Enabled = marker.Settings.Enabled;
            Color = marker.Settings.Color;
            ExtraColor = marker.Settings.ExtraColor;
            View = marker.Settings.View;
            Tribe = marker.Tribe;
            Player = marker.Player;
        }

        public MarkerGridRow()
        {

        }
        #endregion

        #region Public
        public MarkerSettings GetMarkerSettings()
        {
            return new MarkerSettings("", Enabled, Color, ExtraColor, View);
        }

        public Image GetTypeImage()
        {
            if (Tribe != null)
            {
                return Resources.Tribe;
            }
            if (Player != null)
            {
                return Resources.Player;
            }
            return null;
        }

        public string GetTooltip()
        {
            if (Tribe != null)
            {
                return Tribe.Tooltip;
            }
            if (Player != null)
            {
                return Player.Tooltip;
            }
            return null;
        }

        public override string ToString()
        {
            return string.Format(
                "{0}, Colors=({1}+{2}), View={3}, Enabled={4}",
                Player == null ? Tribe.Tag : Player.Name,
                Color.Description().Replace("=", ":"),
                ExtraColor.Description().Replace("=", ":"),
                View,
                Enabled);
        }
        #endregion
    }
}
