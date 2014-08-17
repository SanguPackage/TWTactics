using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using TribalWars.Tools;
using TribalWars.Villages;

namespace TribalWars.Maps.Manipulators.Implementations.Church
{
    /// <summary>
    /// TW church
    /// </summary>
    public class ChurchInfo
    {
        #region Fields
        private int _churchLevel;
        #endregion

        #region Properties
        /// <summary>
        /// Village with the church change
        /// </summary>
        public Village Village { get; private set; }

        /// <summary>
        /// Level of the church in the village
        /// </summary>
        public int ChurchLevel
        {
            get { return _churchLevel; }
            set
            {
                _churchLevel = Math.Max(0, Math.Min(3, value));
            }
        }

        /// <summary>
        /// The church influence radius color
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The church influence radius transparacy %
        /// </summary>
        public int Transparancy { get; set; }

        /// <summary>
        /// The default Color for church radius
        /// </summary>
        public static Color DefaultColor
        {
            get { return Color.Blue; }
        }
        #endregion

        #region Constructors
        public ChurchInfo(Village village, int churchLevel, Color color)
        {
            Village = village;
            ChurchLevel = churchLevel;
            Color = color;
            Transparancy = 50;
        }

        public ChurchInfo(Village village, int churchLevel)
            : this(village, churchLevel, DefaultColor)
        {
            
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format("Village={0}, Level={1}, Color={2}", Village.LocationString, ChurchLevel, Color.Description());
        }
        #endregion

        #region Church Influence Radius
        /// <summary>
        /// Get the radius of the church on a map Control
        /// </summary>
        public GraphicsPath GetRadius(Point mapLocation, Size villageSize)
        {
            var influenceRadius = new ChurchRadiusCalculator(this, mapLocation, villageSize);
            return influenceRadius.GetPath();
        }

        /// <summary>
        /// Figures out the church radius path around the village
        /// </summary>
        private class ChurchRadiusCalculator
        {
            private readonly ChurchInfo _church;
            private Point _mapLoc;
            private readonly Size _villageSize;
            private GraphicsPath _churchInfluence;

            public ChurchRadiusCalculator(ChurchInfo church, Point mapLoc, Size villageSize)
            {
                _church = church;
                _mapLoc = mapLoc;
                _villageSize = villageSize;
            }

            public GraphicsPath GetPath()
            {
                _churchInfluence = new GraphicsPath();
                _churchInfluence.FillMode = FillMode.Winding;

                int subSize;
                int churchSize;
                int toMove;
                switch (_church.ChurchLevel)
                {
                    case 1:
                        churchSize = 2;
                        toMove = 4;
                        subSize = 1;
                        break;

                    case 2:
                        churchSize = 3;
                        toMove = 6;
                        subSize = 1;
                        break;

                    case 3:
                        churchSize = 3;
                        toMove = 8;
                        subSize = 2;
                        break;

                    default:
                        throw new Exception("Unexisting church level");
                }

                MoveLeft(toMove);

                // most left - going down
                DrawDown();
                DrawRight();
                DrawDown(churchSize);
                DrawRight();
                DrawDown(subSize);

                if (_church.ChurchLevel > 1)
                {
                    DrawRight();
                    DrawDown();

                    if (_church.ChurchLevel == 3)
                    {
                        DrawRight(subSize);
                        DrawDown();
                    }
                }

                DrawRight(churchSize);
                DrawDown();
                DrawRight();

                // going back up now
                DrawUp();
                DrawRight(churchSize);
                DrawUp();
                DrawRight(subSize);

                if (_church.ChurchLevel > 1)
                {
                    DrawUp();
                    DrawRight();

                    if (_church.ChurchLevel == 3)
                    {
                        DrawUp(subSize);
                        DrawRight();
                    }
                }

                DrawUp(churchSize);
                DrawRight();
                DrawUp();

                // going back left now
                DrawLeft();
                DrawUp(churchSize);
                DrawLeft();
                DrawUp(subSize);

                if (_church.ChurchLevel > 1)
                {
                    DrawLeft();
                    DrawUp();

                    if (_church.ChurchLevel == 3)
                    {
                        DrawLeft(subSize);
                        DrawUp();
                    }
                }

                DrawLeft(churchSize);
                DrawUp();
                DrawLeft();

                // and going back down
                DrawDown();
                DrawLeft(churchSize);
                DrawDown();
                DrawLeft(subSize);

                if (_church.ChurchLevel > 1)
                {
                    DrawDown();
                    DrawLeft();

                    if (_church.ChurchLevel == 3)
                    {
                        DrawDown(subSize);
                        DrawLeft();
                    }
                }

                DrawDown(churchSize);
                DrawLeft();

                return _churchInfluence;
            }

            private void DrawLeft(int villages = 1)
            {
                Draw(new Point(_mapLoc.X - _villageSize.Width * villages, _mapLoc.Y));
            }

            private void DrawUp(int villages = 1)
            {
                Draw(new Point(_mapLoc.X, _mapLoc.Y - _villageSize.Height * villages));
            }

            private void DrawRight(int villages = 1)
            {
                Draw(new Point(_mapLoc.X + _villageSize.Width * villages, _mapLoc.Y));
            }

            private void DrawDown(int villages = 1)
            {
                Draw(new Point(_mapLoc.X, _mapLoc.Y + _villageSize.Height * villages));
            }

            private void Draw(Point newLoc)
            {
                _churchInfluence.AddLine(_mapLoc, newLoc);
                _mapLoc = newLoc;
            }

            private void MoveLeft(int villages)
            {
                _mapLoc.Offset(-villages * _villageSize.Width, 0);
            }
        }
        #endregion
    }
}
