using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using TribalWars.Maps.Manipulators;

namespace TribalWars.Maps.Drawing
{
    /// <summary>
    /// Paints the villages and continent/province lines on a canvas
    /// </summary>
    public class Painter
    {
        #region Fields
        private readonly Bitmap _canvas;
        private readonly Graphics _g;
        private readonly Rectangle _toPaint;
        private readonly Rectangle _visibleGameRectangle;

        private readonly int _villageWidth;
        private readonly int _villageHeight;
        private readonly int _villageWidthSpacing;
        private readonly int _villageHeightSpacing;

        private readonly Display _display;
        #endregion

        #region Constructor
        public Painter(Display display, Rectangle mapSize, IMapPainter painter)
        {
            _display = display;
            _canvas = new Bitmap(mapSize.Width, mapSize.Height);
            _g = Graphics.FromImage(_canvas);
               
            _visibleGameRectangle = display.GetGameRectangle();
            // _visibleGameRectangle is sometimes negative!!

            // Also draw villages that are only partially visible at left/top
            Point mapOffset = _display.GetMapLocation(_visibleGameRectangle.Location);
            mapSize.Offset(mapOffset);
            _toPaint = mapSize;

            using (var backgroundBrush = new SolidBrush(_display.Settings.BackgroundColor))
            {
                _g.FillRectangle(backgroundBrush, _toPaint);
            }

            var dimensions = display.Dimensions;
            _villageWidthSpacing = dimensions.SizeWithSpacing.Width;
            _villageHeightSpacing = dimensions.SizeWithSpacing.Height;

            _villageWidth = dimensions.Size.Width;
            _villageHeight = dimensions.Size.Height;

            DrawVillages();
            DrawContinentLines();

            //painter.Paint(_g, );
        }
        #endregion

        #region Villages
        private void DrawVillages()
        {
            int mapX = _toPaint.Left;
            int mapY = _toPaint.Top;

            int gameY = _visibleGameRectangle.Y;

            for (int yMap = mapY; yMap <= _toPaint.Height; yMap += _villageHeightSpacing)
            {
                int gameX = _visibleGameRectangle.X;
                for (int xMap = mapX; xMap <= _toPaint.Width; xMap += _villageWidthSpacing)
                {
                    _display.Paint(_g, new Point(gameX, gameY), new Rectangle(xMap, yMap, _villageWidth, _villageHeight));
                    gameX += 1;
                }
                gameY += 1;
            }
        }
        #endregion

        #region Continent Lines
        private void DrawContinentLines()
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

            if (_villageWidthSpacing > 4 && _display.Settings.ProvinceLines)
            {
                using (var provincePen = _display.Settings.CreateProvincePen())
                {
                    provincePen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    DrawContinentLines(provincePen, mapMin, mapMax, gameMin, gameMax, villageSize, isHorizontal, provinceWidth, otherMapMin, otherMapMax, otherGameMin, otherGameMax, otherVillageSize);
                }
            }

            if (_display.Settings.ContinentLines)
            {
                using (var continentPen = _display.Settings.CreateContinentPen())
                {
                    continentPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
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

        #region Public Methods
        /// <summary>
        /// Gets the game rectangle representing what is drawn on the map
        /// </summary>
        public Rectangle GetVisibleGameRectangle()
        {
            return _visibleGameRectangle;
        }

        /// <summary>
        /// Gets the canvas with all villages drawn to it
        /// </summary>
        public Bitmap GetBitmap()
        {
            return _canvas;
        }
        #endregion
    }
}
