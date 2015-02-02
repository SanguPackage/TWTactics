using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TribalWars.Maps.Drawing.Helpers
{
    /// <summary>
    /// Given the old and new portion of the game map that needs to be displayed,
    /// calculate which Rectangles need to be drawn
    /// </summary>
    public class RegionsToDrawCalculator
    {
        private Rectangle _old;
        private Rectangle _new;

        public RegionsToDrawCalculator(Rectangle oldVisibleGameArea, Rectangle newVisibleGameArea)
        {
            _old = oldVisibleGameArea;
            _new = newVisibleGameArea;
        }

        /// <summary>
        /// Check if there is any overlap at at all
        /// </summary>
        public bool HasIntersection
        {
            get
            {
                var intersection = _old;
                intersection.Intersect(_new);
                return !intersection.IsEmpty;
            }
        }

        /// <summary>
        /// Get the game rectangles that need to be redrawn
        /// </summary>
        public IEnumerable<Rectangle> GetNonOverlappingGameRectangles(out Rectangle backgroundMove)
        {
            var nonOverlapping = new List<Rectangle>();
            backgroundMove = new Rectangle();
            if (_old.Left < _new.Left)
            {

                // Movement to the right
                var leftMove = _new.Left - _old.Left;

                var rec = new Rectangle(_old.Right, _old.Top, leftMove, _old.Height);
                nonOverlapping.Add(rec);

                
                backgroundMove.X = leftMove;
            }



            return nonOverlapping;
        }
    }
}
