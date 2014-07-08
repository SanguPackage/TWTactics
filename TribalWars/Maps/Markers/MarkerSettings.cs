using System;
using System.Drawing;

namespace TribalWars.Maps.Markers
{
    public class MarkerSettings : IEquatable<MarkerSettings>
    {
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

        public MarkerSettings(string name, bool enabled, Color color, Color extraColor, string view)
        {
            Name = name;
            Enabled = enabled;
            Color = color;
            ExtraColor = extraColor;
            View = view;
        }

        private MarkerSettings(MarkerSettings settings)
        {
            Enabled = true;

            Name = settings.Name;
            Color = settings.Color;
            ExtraColor = settings.ExtraColor;
            View = settings.View;
        }

        public static MarkerSettings ChangeName(MarkerSettings settings, string name)
        {
            var newSettings = new MarkerSettings(settings);
            newSettings.Name = name;
            return newSettings;
        }

        public static MarkerSettings ChangeColor(MarkerSettings settings, Color color)
        {
            var newSettings = new MarkerSettings(settings);
            newSettings.Color = color;
            return newSettings;
        }

        public static MarkerSettings ChangeExtraColor(MarkerSettings settings, Color extraColor)
        {
            var newSettings = new MarkerSettings(settings);
            newSettings.ExtraColor = extraColor;
            return newSettings;
        }

        public static MarkerSettings ChangeEnabled(MarkerSettings settings, bool enabled)
        {
            var newSettings = new MarkerSettings(settings);
            newSettings.Enabled = enabled;
            return newSettings;
        }

        public static MarkerSettings ChangeView(MarkerSettings settings, string view)
        {
            var newSettings = new MarkerSettings(settings);
            newSettings.View = view;
            return newSettings;
        }
        
        public static MarkerSettings Create(Color color, string view)
        {
            return new MarkerSettings("", true, color, Color.Transparent, view);
        }

        public bool Equals(MarkerSettings other)
        {
            if (ReferenceEquals(other, null)) return false;
            return View == other.View
                   && Color == other.Color
                   && ExtraColor == other.ExtraColor;
        }

        public override string ToString()
        {
            string views = string.Empty;
            if (View != null) views = View;
            return string.Format("{0} ({1} - {2} / {3})", Name, views, Color, ExtraColor); 
        }
    }
}
