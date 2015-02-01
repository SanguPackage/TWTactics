using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using TribalWars.Maps.Drawing.Displays;
using TribalWars.Maps.Manipulators;

namespace TribalWars.Maps.Drawing
{
    /// <summary>
    /// Get which villages need to be displayed
    /// </summary>
    public class DisplayVillageCalculator
    {
        #region Fields
        private readonly Rectangle _toPaint;
        private readonly Rectangle _visibleGameRectangle;

        private readonly int _villageWidthSpacing;
        private readonly int _villageHeightSpacing;
        #endregion

        #region Constructor
        public DisplayVillageCalculator(VillageDimensions dimensions, Rectangle gameRectangle, Rectangle mapSize)
        {
            _visibleGameRectangle = gameRectangle;
            _toPaint = mapSize;
            
            _villageWidthSpacing = dimensions.SizeWithSpacing.Width;
            _villageHeightSpacing = dimensions.SizeWithSpacing.Height;

        }
        #endregion

        #region Villages
        public IEnumerable<VillageDrawingLocation> GetVillages()
        {
            int mapX = _toPaint.Left;
            int mapY = _toPaint.Top;

            int gameY = _visibleGameRectangle.Y;

            for (int yMap = mapY; yMap <= _toPaint.Height; yMap += _villageHeightSpacing)
            {
                int gameX = _visibleGameRectangle.X;
                for (int xMap = mapX; xMap <= _toPaint.Width; xMap += _villageWidthSpacing)
                {
                    yield return new VillageDrawingLocation(new Point(gameX, gameY), new Point(xMap, yMap));
                    gameX += 1;
                }
                gameY += 1;
            }
        }
        #endregion
    }
}
