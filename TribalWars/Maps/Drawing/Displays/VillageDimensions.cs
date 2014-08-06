using System.Diagnostics;
using System.Drawing;

namespace TribalWars.Maps.Drawing.Displays
{
    /// <summary>
    /// The dimensions of the village
    /// </summary>
    public class VillageDimensions
    {
        public Size Size { get; private set; }

        /// <summary>
        /// Gets the width &amp; height of a village WITH the spacing to the next village
        /// </summary>
        public Size SizeWithSpacing { get; private set; }

        public VillageDimensions(int size)
            : this(new Size(size, size))
        {

        }

        public VillageDimensions(Size size)
            : this(size, size)
        {
            
        }

        public VillageDimensions(Size size, Size sizeWithSpacing)
        {
            Debug.Assert(size.Width <= sizeWithSpacing.Width && size.Height <= sizeWithSpacing.Height);
            Size = size;
            SizeWithSpacing = sizeWithSpacing;
        }

        public override string ToString()
        {
            return string.Format("Size={0}, WithSpacing={1}", Size, SizeWithSpacing);
        }
    }
}
