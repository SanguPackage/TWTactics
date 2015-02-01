using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using TribalWars.Maps.Drawing.Displays;

namespace TribalWars.Maps.Drawing
{
    //public class Line
    //{
    //    public int X1 { get; private set; }

    //    public int X2 { get; private set; }

    //    public int Y1 { get; private set; }

    //    public int Y2 { get; private set; }

    //    public Line(int x1, int y1, int x2, int y2)
    //    {
    //        X1 = x1;
    //        Y1 = y1;
    //        X2 = x2;
    //        Y2 = y2;
    //    }
    //}

    /// <summary>
    /// Calculates positions for and draws the continent and province lines
    /// </summary>
    public class ContinentLinesPainter
    {
        #region Fields
        private readonly Graphics _g;
        private readonly Rectangle _toPaint;
        private readonly Rectangle _visibleGameRectangle;

        private readonly int _villageWidthSpacing;
        private readonly int _villageHeightSpacing;

        private readonly DisplaySettings _settings;
        #endregion

        #region Constructor
        public ContinentLinesPainter(Graphics g, DisplaySettings settings, VillageDimensions dimensions, Rectangle gameRectangle, Rectangle mapSize)
        {
            _settings = settings;
            _g = g;
            _visibleGameRectangle = gameRectangle;
            _toPaint = mapSize;
            
            _villageWidthSpacing = dimensions.SizeWithSpacing.Width;
            _villageHeightSpacing = dimensions.SizeWithSpacing.Height;

        }
        #endregion

        #region Continent Lines
        public void DrawContinentLines()
        {
            //Horizontal
            DrawContinentLines(_toPaint.Top, _toPaint.Bottom, _visibleGameRectangle.Y, _visibleGameRectangle.Bottom, _villageHeightSpacing, true, _toPaint.Left, _toPaint.Right, _visibleGameRectangle.X, _visibleGameRectangle.Right, _villageWidthSpacing);

            // Vertical
            DrawContinentLines(_toPaint.Left, _toPaint.Right, _visibleGameRectangle.X, _visibleGameRectangle.Right, _villageWidthSpacing, false, _toPaint.Top, _toPaint.Bottom, _visibleGameRectangle.Y, _visibleGameRectangle.Bottom, _villageHeightSpacing);
        }

        /// <summary>
        /// Draws all horizontal or vertical continent lines (and province lines if required).
        /// </summary>
        /// <remarks>
        /// For horizontal lines: map/gameXXX params are the VERTICAL coordinates. otherMap/gameXXX are then the horizontals.
        /// For vertical lines: map/gameXXX params are the HORIZONTAL coordinates.
        /// </remarks>
        private void DrawContinentLines(int mapMin, int mapMax, int gameMin, int gameMax, int villageSize, bool isHorizontal, int otherMapMin, int otherMapMax, int otherGameMin, int otherGameMax, int otherVillageSize)
        {
            const int provinceWidth = 5;
            const int continentWidth = 100;

            if (_villageWidthSpacing > 4 && _settings.ProvinceLines)
            {
                using (var provincePen = _settings.CreateProvincePen())
                {
                    DrawContinentLines(provincePen, mapMin, mapMax, gameMin, gameMax, villageSize, isHorizontal, provinceWidth, otherMapMin, otherMapMax, otherGameMin, otherGameMax, otherVillageSize);
                }
            }

            if (_settings.ContinentLines)
            {
                using (var continentPen = _settings.CreateContinentPen())
                {
                    DrawContinentLines(continentPen, mapMin, mapMax, gameMin, gameMax, villageSize, isHorizontal, continentWidth, otherMapMin, otherMapMax, otherGameMin, otherGameMax, otherVillageSize);
                }
            }
        }

        private void DrawContinentLines(Pen pen, int mapMin, int mapMax, int gameMin, int gameMax, int villageSize, bool isHorizontal, int sizeBetweenLines, int otherMapMin, int otherMapMax, int otherGameMin, int otherGameMax, int otherVillageSize)
        {
            // Don't start the loop before the start of the world
            int mapStart = mapMin - (gameMin % sizeBetweenLines) * villageSize;
            int gameStart = gameMin - (gameMin % sizeBetweenLines);
            if (gameStart < 0)
            {
                mapStart += villageSize * gameStart * -1;
                gameStart = 0;
            }

            // Loop only until end of the world
            if (gameMax > 1000)
            {
                gameMax = 1000;
            }

            // Don't draw before the start of the world
            int otherMapStart = otherMapMin - (otherGameMin % sizeBetweenLines) * villageSize;
            int otherGameStart = otherGameMin - (otherGameMin % sizeBetweenLines);
            if (otherGameStart < 0)
            {
                otherMapStart += villageSize * otherGameStart * -1;
            }

            // Don't draw after the end of the world
            int otherMapEnd = otherMapMax;
            if (otherGameMax > 1000)
            {
                otherMapEnd += otherVillageSize * (otherGameMax - 1000) * -1;
            }
            else if (otherMapStart < 0)
            {
                otherMapEnd -= otherMapStart;
                otherMapStart = 0;
            }

            int map = mapStart;
            for (int game = gameStart; game <= gameMax; game += sizeBetweenLines)
            {
                Debug.Assert(game >= 0);
                Debug.Assert(game <= 1000);

                if (isHorizontal)
                {
                    _g.DrawLine(pen, otherMapStart, map, otherMapEnd, map);
                }
                else
                {
                    _g.DrawLine(pen, map, otherMapStart, map, otherMapEnd);
                }

                map += villageSize * sizeBetweenLines;
            }
        }
        #endregion
    }
}
