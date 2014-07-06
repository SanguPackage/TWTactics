using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TribalWars.Data.Maps
{
    /// <summary>
    /// Settings for Display
    /// </summary>
    public class DisplaySettings
    {
        /// <summary>
        /// Gets or sets the canvas background color
        /// </summary>
        public Color BackgroundColor { get; private set; }

        /// <summary>
        /// Gets a value indicating whether continent lines should be drawn
        /// </summary>
        public bool ContinentLines { get; private set; }

        /// <summary>
        /// Gets a value indicating whether province lines should be drawn
        /// </summary>
        public bool ProvinceLines { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating wether abandoned 
        /// villages should be shown on the map
        /// </summary>
        public bool HideAbandoned { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating wether unmarked
        /// villages should be shown on the map
        /// </summary>
        public bool MarkedOnly { get; private set; }

        public DisplaySettings(Color backgroundColor, bool continentLines, bool provinceLines, bool hideAbandoned, bool markedOnly)
        {
            BackgroundColor = backgroundColor;
            ContinentLines = continentLines;
            ProvinceLines = provinceLines;
            HideAbandoned = hideAbandoned;
            MarkedOnly = markedOnly;
        }
    }
}
