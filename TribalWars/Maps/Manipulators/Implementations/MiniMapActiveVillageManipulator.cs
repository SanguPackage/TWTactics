#region Using
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using TribalWars.Maps.Drawing.Displays;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Tools;
using TribalWars.Villages;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Adds some extra stuff to the minimap:
    /// - Active village(s)are X crossed
    /// - Show rectangle visible on main map
    /// - Draw the continent indicators
    /// </summary>
    public class MiniMapActiveVillageManipulator : ManipulatorBase
    {
        #region Constants
        /// <summary>
        /// Move the mini map whenever the middle point of the current 
        /// main map Location is more then this distance (in game coords) 
        /// away from the currently displayed mini map.
        /// 
        /// TODO: Once map drawing performance is better, this number can be
        ///       set to 0 for having the mini map always follow the mainmap
        /// </summary>
        private const int MoveVisibleRectangleDistance = 100;
        #endregion


        #region Constants
        private const int CrossPaintOffset = 1;
        #endregion

        #region Fields
        private readonly Map _mainMap;
        private Rectangle _mainMapRectangle;
        private Village _mainMapSelectedVillage;

        private readonly Pen _mainMapSelectedVillagesPen;
        private readonly Font _continentFont;

        private int _activeVillagePaintsCounter;
        private Pen _activeVillageAnimationPen;
        private readonly Pen _activeVillagePen;
        private readonly Pen _activeVillagePen2;

        private Rectangle? _currentLocationMainMapRectangle;
        #endregion

        #region Constructors
        public MiniMapActiveVillageManipulator(Map map, Map mainMap)
            : base(map)
        {
            _mainMap = mainMap;
            mainMap.EventPublisher.VillagesDeselected += EventPublisher_MainMapVillagesDeselected;
            mainMap.EventPublisher.VillagesSelected += EventPublisher_MainMapVillagesSelected;
            mainMap.EventPublisher.PlayerSelected += EventPublisher_MainMapVillagesSelected;
            mainMap.EventPublisher.TribeSelected += EventPublisher_MainMapVillagesSelected;

            mainMap.EventPublisher.LocationChanged += EventPublisher_MainMapLocationChanged;
            map.EventPublisher.LocationChanged += EventPublisher_OwnLocationChanged;

            _mainMapSelectedVillagesPen = new Pen(Color.White);
            _continentFont = new Font("Verdana", 18);
            _activeVillagePen = new Pen(Color.Black, 3);
            _activeVillagePen2 = new Pen(Color.White, 3);
            _activeVillageAnimationPen = _activeVillagePen;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Cleanup anything when switching worlds or settings
        /// </summary>
        protected internal override void CleanUp()
        {
        }

        public override void TimerPaint(MapTimerPaintEventArgs e)
        {
            // Toggle marker of selected village
            _activeVillagePaintsCounter++;
            if (_mainMapSelectedVillage != null && _activeVillagePaintsCounter % 50 == 0)
            {
                _activeVillageAnimationPen = ReferenceEquals(_activeVillageAnimationPen, _activeVillagePen2) ? _activeVillagePen : _activeVillagePen2;
                Point villageLocation = _map.Display.GetMapLocation(_mainMapSelectedVillage.Location);
                VillageDimensions village = _map.Display.Dimensions;

                PaintCross(e.Graphics, _activeVillageAnimationPen, villageLocation, village.SizeWithSpacing);
            }
        }

        public override void Paint(MapPaintEventArgs e, bool isActiveManipulator)
        {
            if (_mainMapSelectedVillage != null)
            {
                Player player = _mainMapSelectedVillage.Player;
                if (!_mainMapSelectedVillage.HasPlayer && _mainMapSelectedVillage.PreviousVillageDetails != null && _mainMapSelectedVillage.PreviousVillageDetails.HasPlayer)
                {
                    // Abandoned village with previous owner
                    // -> show the previous owner villages
                    player = _mainMapSelectedVillage.PreviousVillageDetails.Player;
                }

                if (player != null)
                {
                    var villageDimension = _map.Display.Dimensions;

                    foreach (Village village in player)
                    {
                        Point villageLocation = _map.Display.GetMapLocation(village.Location);
                        
                        Pen pen = _mainMapSelectedVillagesPen;
                        if (village == _mainMapSelectedVillage)
                        {
                            pen = _activeVillagePen2;
                        }

                        PaintCross(e.Graphics, pen, villageLocation, villageDimension.SizeWithSpacing);
                    }
                }
            }

            // Draws the rectangle active on the mainmap
            Rectangle mainMapGameRectangle = _mainMap.Display.GetGameRectangle();
            _mainMapRectangle = _map.Display.GetMapRectangle(mainMapGameRectangle);
            using (var mainMapActiveBorderPen = new Pen(Color.Yellow))
            {
                e.Graphics.DrawRectangle(mainMapActiveBorderPen, _mainMapRectangle);
            }

            const int width = 40;
            const int height = 35;
            const int cOff = -5;

            // Draw the continents
            // Right Top
            Point cPos = _map.Display.GetGameLocation(new Point(e.FullMapRectangle.Right, e.FullMapRectangle.Top));
            if (cPos.IsValidGameCoordinate())
            {
                string continentNumber = cPos.Kingdom().ToString(CultureInfo.InvariantCulture);
                e.Graphics.FillRectangle(Brushes.Black, e.FullMapRectangle.Right - width - cOff, -1, width, height + cOff);
                e.Graphics.DrawString(continentNumber, _continentFont, SystemBrushes.GradientInactiveCaption, e.FullMapRectangle.Right - width + 3, e.FullMapRectangle.Top - 2);
            }
            
            // Left top
            cPos = _map.Display.GetGameLocation(new Point(e.FullMapRectangle.Left, e.FullMapRectangle.Top));
            if (cPos.IsValidGameCoordinate())
            {
                string continentNumber = cPos.Kingdom().ToString(CultureInfo.InvariantCulture);
                e.Graphics.FillRectangle(Brushes.Black, cOff, -1, width, height + cOff);
                e.Graphics.DrawString(continentNumber, _continentFont, SystemBrushes.GradientInactiveCaption, cOff + 1, cOff + 2);
            }

            // Left bottom
            cPos = _map.Display.GetGameLocation(new Point(e.FullMapRectangle.Left, e.FullMapRectangle.Bottom));
            if (cPos.IsValidGameCoordinate())
            {
                string continentNumber = cPos.Kingdom().ToString(CultureInfo.InvariantCulture);
                e.Graphics.FillRectangle(Brushes.Black, cOff, e.FullMapRectangle.Bottom - height - cOff, width, height + cOff);
                e.Graphics.DrawString(continentNumber, _continentFont, SystemBrushes.GradientInactiveCaption, cOff + 1, e.FullMapRectangle.Bottom - height - cOff);
            }

            // Active location indicator
            if (_currentLocationMainMapRectangle.HasValue)
            {
                using (var miniPen = new Pen(Color.Blue, 2))
                {
                    miniPen.DashStyle = DashStyle.Dot;
                    e.Graphics.DrawRectangle(miniPen, _currentLocationMainMapRectangle.Value);
                }
            }
        }

        private static void PaintCross(Graphics g, Pen pen, Point villageLocation, Size villageSize)
        {
            // Draw a cross for each village
            // left top to bottom right
            g.DrawLine(
                pen,
                villageLocation.X - CrossPaintOffset,
                villageLocation.Y - CrossPaintOffset,
                villageLocation.X + villageSize.Width + CrossPaintOffset,
                villageLocation.Y + villageSize.Height + CrossPaintOffset);

            // top right to left bottom
            g.DrawLine(
                pen,
                villageLocation.X + villageSize.Width + CrossPaintOffset,
                villageLocation.Y - CrossPaintOffset,
                villageLocation.X - CrossPaintOffset,
                villageLocation.Y + villageSize.Height + CrossPaintOffset);
        }

        /// <summary>
        /// Moves the center of the map
        /// </summary>
        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                Point game = _map.Display.GetGameLocation(e.MouseEventArgs.Location);
                _mainMap.SetCenter(game);
                return true;
            }
            return false;
        }

        protected internal override bool MouseLeave()
        {
            _currentLocationMainMapRectangle = null;
            return true;
        }

        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            // Draw a rectangle around what would be seen on MouseDown on the mainmap
            Rectangle mainMapGameRectangle = _mainMap.Display.GetGameRectangle();
            _currentLocationMainMapRectangle = _map.Display.GetMapRectangle(mainMapGameRectangle);

            _currentLocationMainMapRectangle = new Rectangle(
                e.Location.X - _currentLocationMainMapRectangle.Value.Width / 2,
                e.Location.Y - _currentLocationMainMapRectangle.Value.Height / 2,
                _currentLocationMainMapRectangle.Value.Width,
                _currentLocationMainMapRectangle.Value.Height);

            return true;
        }

        private void EventPublisher_OwnLocationChanged(object sender, MapLocationEventArgs e)
        {
            if (sender != _map)
            {
                _mainMap.SetCenter(new Location(e.NewLocation.Display, e.NewLocation.Point, _mainMap.Location.Zoom));
                _map.Invalidate(false);
            }
            else
            {
                _map.Invalidate(false);
            }
        }

        private void EventPublisher_MainMapLocationChanged(object sender, MapLocationEventArgs e)
        {
            if (sender != this)
            {
                if (e.OldLocation == null)
                {
                    // First time the main map has been set
                    var miniMapStart = new Location(DisplayTypes.Shape, e.NewLocation.Point, MiniMapDrawerFactory.MaxZoomLevel);
                    _map.SetCenter(miniMapStart);
                }
                else if (e.NewLocation.Zoom != e.OldLocation.Zoom || GetDistance(e.NewLocation, _map.Location) > MoveVisibleRectangleDistance)
                {
                    _map.SetCenter(e.NewLocation.Point, GetZoomLevel());
                    _map.Invalidate(false);
                }
                else
                {
                    _map.Invalidate(false);
                }
            }
            else
            {
                _map.Invalidate(false);
            }
        }

        private int GetZoomLevel()
        {
            var mapGameRectangle = _mainMap.Display.GetGameRectangle();
            var loc = _map.GetSpan(mapGameRectangle, true, 50);
            return loc.Zoom;
        }

        private void EventPublisher_MainMapVillagesDeselected(object sender, EventArgs e)
        {
            _mainMapSelectedVillage = null;
            _map.Invalidate(false);
        }

        private void EventPublisher_MainMapVillagesSelected(object sender, VillagesEventArgs e)
        {
            if (e.Tool == VillageTools.SelectVillage || e.Tool == VillageTools.PinPoint)
            {
                _mainMapSelectedVillage = e.FirstVillage;
                _map.Invalidate(false);
            }
        }
        #endregion

        #region Private Methods
        private int GetDistance(Location first, Location last)
        {
            return (first.X - last.X)*(first.X - last.X) + (first.Y - last.Y)*(first.Y - last.Y);
        }

        public override void Dispose()
        {
            _continentFont.Dispose();
            _mainMapSelectedVillagesPen.Dispose();
            _activeVillagePen.Dispose();
            _activeVillagePen2.Dispose();
        }
        #endregion
    }
}