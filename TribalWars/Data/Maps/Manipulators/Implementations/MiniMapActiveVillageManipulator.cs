#region Using
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using TribalWars.Data.Events;
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Data.Villages;
using TribalWars.Tools;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Implementations
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
        private const int CrossPaintOffset = 1;
        #endregion

        #region Fields
        private readonly Map _mainMap;
        private Rectangle _mainMapRectangle;
        private Village _mainMapSelectedVillage;

        private readonly Pen _mainMapActiveBorderPen;
        private readonly Pen _mainMapSelectedVillagesPen;
        private readonly Font _continentFont;

        private int _activeVillagePaintsCounter;
        private Pen _activeVillageAnimationPen;
        private readonly Pen _activeVillagePen;
        private readonly Pen _activeVillagePen2;
        #endregion

        #region Constructors
        public MiniMapActiveVillageManipulator(Map map, Map mainMap)
            : base(map)
        {
            _mainMap = mainMap;
            mainMap.EventPublisher.VillagesSelected += EventPublisher_MainMapVillagesSelected;
            mainMap.EventPublisher.PlayerSelected += EventPublisher_MainMapVillagesSelected;
            mainMap.EventPublisher.TribeSelected += EventPublisher_MainMapVillagesSelected;

            mainMap.EventPublisher.LocationChanged += EventPublisher_MainMapLocationChanged;
            mainMap.EventPublisher.DisplayTypeChanged += EventPublisher_MainMapDisplayTypeChanged;
            map.EventPublisher.LocationChanged += EventPublisher_OwnLocationChanged;

            _mainMapActiveBorderPen = new Pen(Color.Yellow);
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
                int villageWidth = _map.Display.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);
                int villageHeight = _map.Display.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);

                Point villageLocation = _map.Display.GetMapLocation(_mainMapSelectedVillage.Location);

                _activeVillageAnimationPen = ReferenceEquals(_activeVillageAnimationPen, _activeVillagePen2) ? _activeVillagePen : _activeVillagePen2;

                PaintCross(e.Graphics, _activeVillageAnimationPen, villageLocation, villageWidth, villageHeight);
            }
        }

        public override void Paint(MapPaintEventArgs e)
        {
            if (_mainMapSelectedVillage != null)
            {
                Players.Player player = _mainMapSelectedVillage.Player;
                if (!_mainMapSelectedVillage.HasPlayer && _mainMapSelectedVillage.PreviousVillageDetails != null && _mainMapSelectedVillage.PreviousVillageDetails.HasPlayer)
                {
                    player = _mainMapSelectedVillage.PreviousVillageDetails.Player;
                }

                if (player != null)
                {
                    int villageWidth = _map.Display.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);
                    int villageHeight = _map.Display.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);

                    foreach (Village village in player)
                    {
                        Point villageLocation = _map.Display.GetMapLocation(village.Location);
                        
                        Pen pen = _mainMapSelectedVillagesPen;
                        if (village == _mainMapSelectedVillage)
                        {
                            pen = _activeVillagePen2;
                        }

                        PaintCross(e.Graphics, pen, villageLocation, villageWidth, villageHeight);
                    }
                }
            }

            // Draws the rectangle active on the mainmap
            Rectangle mainMapGameRectangle = _mainMap.Display.GetGameRectangle();
            _mainMapRectangle = _map.Display.GetMapRectangle(mainMapGameRectangle);
            e.Graphics.DrawRectangle(_mainMapActiveBorderPen, _mainMapRectangle);

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
        }

        private static void PaintCross(Graphics g, Pen pen, Point villageLocation, int villageWidth, int villageHeight)
        {
            // Draw a cross for each village
            // left top to bottom right
            g.DrawLine(
                pen,
                villageLocation.X - CrossPaintOffset,
                villageLocation.Y - CrossPaintOffset,
                villageLocation.X + villageWidth + CrossPaintOffset,
                villageLocation.Y + villageHeight + CrossPaintOffset);

            // top right to left bottom
            g.DrawLine(
                pen,
                villageLocation.X + villageWidth + CrossPaintOffset,
                villageLocation.Y - CrossPaintOffset,
                villageLocation.X - CrossPaintOffset,
                villageLocation.Y + villageHeight + CrossPaintOffset);
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

        private void EventPublisher_OwnLocationChanged(object sender, MapLocationEventArgs e)
        {
            if (sender != _map)
            {
                _mainMap.SetCenter(this, new Location(e.NewLocation, _mainMap.Location.Zoom));
                _map.Control.Invalidate();
            }
            else
            {
                _map.Control.Invalidate();
            }
        }

        private void EventPublisher_MainMapLocationChanged(object sender, MapLocationEventArgs e)
        {
            if (sender != this)
            {
                if (_map.Location == null)
                {
                    _map.SetCenter(e.NewLocation, 1);
                }
                else if (e.NewLocation.Zoom != e.OldLocation.Zoom || GetDistance(e.NewLocation, _map.Location) > 100)
                {
                    _map.SetCenter(e.NewLocation, 1);
                    _map.Control.Invalidate();
                }
                else
                {
                    _map.Control.Invalidate();
                }
            }
            else
            {
                _map.Control.Invalidate();
            }
        }

        private void EventPublisher_MainMapDisplayTypeChanged(object sender, MapDisplayTypeEventArgs e)
        {
            _map.Invalidate();
        }

        private void EventPublisher_MainMapVillagesSelected(object sender, VillagesEventArgs e)
        {
            if (e.Tool == VillageTools.SelectVillage || e.Tool == VillageTools.PinPoint)
            {
                _mainMapSelectedVillage = e.FirstVillage;
                _map.Control.Invalidate();
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
            _mainMapActiveBorderPen.Dispose();
            _mainMapSelectedVillagesPen.Dispose();
            _activeVillagePen.Dispose();
            _activeVillagePen2.Dispose();
        }
        #endregion
    }
}