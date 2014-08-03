using System.Diagnostics;
using System.Drawing;
using TribalWars.Maps.Displays;
using TribalWars.Tools;

namespace TribalWars.Maps
{
    /// <summary>
    /// Settings for Display
    /// </summary>
    public class DisplaySettings
    {
        /// <summary>
        /// Gets or sets the canvas background color
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets a value indicating whether continent lines should be drawn
        /// </summary>
        public bool ContinentLines { get; set; }

        /// <summary>
        /// Gets a value indicating whether province lines should be drawn
        /// </summary>
        public bool ProvinceLines { get; set; }

        /// <summary>
        /// Gets or sets a value indicating wether abandoned 
        /// villages should be shown on the map
        /// </summary>
        public bool HideAbandoned { get; set; }

        /// <summary>
        /// Gets or sets a value indicating wether unmarked
        /// villages should be shown on the map
        /// </summary>
        public bool MarkedOnly { get; set; }

        /// <summary>
        /// Which world.dat to use for displaying mountains etc.
        /// Is specific per world and not a user setting.
        /// </summary>
        public IconDrawerFactory.Scenery Scenery { get; set; }

        public DisplaySettings(Color backgroundColor, bool continentLines, bool provinceLines, bool hideAbandoned, bool markedOnly)
        {
            BackgroundColor = backgroundColor;
            ContinentLines = continentLines;
            ProvinceLines = provinceLines;
            HideAbandoned = hideAbandoned;
            MarkedOnly = markedOnly;
        }

        public Pen CreateProvincePen()
        {
            Debug.Assert(ProvinceLines);
            return new Pen(Color.FromArgb(42, 94, 31), 1f);
        }

        public Pen CreateContinentPen()
        {
            Debug.Assert(ContinentLines);
            return new Pen(Color.Black, 1);
        }

        public override string ToString()
        {
            return string.Format(
                "BackColor={0}, Continent={1}, Province={2}, HideAbandoned={3}, MarkedOnly={4}", 
                BackgroundColor.Description(), 
                ContinentLines, 
                ProvinceLines, 
                HideAbandoned, 
                MarkedOnly);
        }
    }
}
