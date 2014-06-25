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
        #region Fields
        private readonly Map _mainMap;
        private Rectangle _mainMapRectangle;
        private Village _mainMapSelectedVillage;

        private readonly Pen _mainMapActiveBorderPen;
        private readonly Pen _mainMapSelectedVillagesPen;
        private readonly Font _continentFont;
        private readonly Pen _activeVillagePen;
        #endregion

        #region Constructors
        public MiniMapActiveVillageManipulator(Map map, Map mainMap)
            : base(map)
        {
            _mainMap = mainMap;
            mainMap.EventPublisher.VillagesSelected += EventPublisher_MainMapVillagesSelected;
            mainMap.EventPublisher.LocationChanged += EventPublisher_MainMapLocationChanged;
            mainMap.EventPublisher.DisplayTypeChanged += EventPublisher_MainMapDisplayTypeChanged;
            map.EventPublisher.LocationChanged += EventPublisher_OwnLocationChanged;

            _mainMapActiveBorderPen = new Pen(Color.Yellow);
            _mainMapSelectedVillagesPen = new Pen(Color.Black);
            _continentFont = new Font("Verdana", 18);
            _activeVillagePen = new Pen(Color.Black, 3);
        }
        #endregion

        #region Event Handlers
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
                    int villageWidth = _map.Display.DisplayManager.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);
                    int villageHeight = _map.Display.DisplayManager.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);
                    const int offset = 1;

                    foreach (Village village in player)
                    {
                        // Draw a cross for each village
                        Point villageLocation = _map.Display.GetMapLocation(village.X, village.Y);
                        // left top to bottom right
                        Pen pen = _mainMapSelectedVillagesPen;
                        if (village == _mainMapSelectedVillage)
                        {
                            pen = _activeVillagePen;
                        }


                        e.Graphics.DrawLine(pen,
                                            villageLocation.X - offset,
                                            villageLocation.Y - offset,
                                            villageLocation.X + villageWidth + offset,
                                            villageLocation.Y + villageHeight + offset);

                        // top right to left bottom
                        e.Graphics.DrawLine(pen,
                                            villageLocation.X + villageWidth + offset,
                                            villageLocation.Y - offset,
                                            villageLocation.X - offset,
                                            villageLocation.Y + villageHeight + offset);
                    }
                }
            }

            // Draws the rectangle active on the mainmap
            Rectangle mainMapGameRectangle = _mainMap.Display.GetGameRectangle(_mainMap.Control.ClientRectangle);
            Point leftTop = _map.Display.GetMapLocation(mainMapGameRectangle.X, mainMapGameRectangle.Y);
            Point rightBottom = _map.Display.GetMapLocation(mainMapGameRectangle.Right, mainMapGameRectangle.Bottom);
            _mainMapRectangle = new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);
            e.Graphics.DrawRectangle(_mainMapActiveBorderPen, _mainMapRectangle);

            const int width = 40;
            const int height = 35;
            const int cOff = -5;

            // Draw the continent in the top right corner
            Point cPos = _map.Display.GetGameLocation(e.FullMapRectangle.Right, e.FullMapRectangle.Top);
            if (cPos.IsValidGameCoordinate())
            {
                string continentNumber = cPos.Kingdom().ToString(CultureInfo.InvariantCulture);
                e.Graphics.FillRectangle(Brushes.Black, e.FullMapRectangle.Right - width - cOff, -1, width, height + cOff);
                e.Graphics.DrawString(continentNumber, _continentFont, SystemBrushes.GradientInactiveCaption, e.FullMapRectangle.Right - width + 3, e.FullMapRectangle.Top - 2);
            }
            
            // Left top
            cPos = _map.Display.GetGameLocation(e.FullMapRectangle.Left, e.FullMapRectangle.Top);
            if (cPos.IsValidGameCoordinate())
            {
                string continentNumber = cPos.Kingdom().ToString(CultureInfo.InvariantCulture);
                e.Graphics.FillRectangle(Brushes.Black, cOff, -1, width, height + cOff);
                e.Graphics.DrawString(continentNumber, _continentFont, SystemBrushes.GradientInactiveCaption, cOff + 1, cOff + 2);
            }

            // Left bottom
            cPos = _map.Display.GetGameLocation(e.FullMapRectangle.Left, e.FullMapRectangle.Bottom);
            if (cPos.IsValidGameCoordinate())
            {
                string continentNumber = cPos.Kingdom().ToString(CultureInfo.InvariantCulture);
                e.Graphics.FillRectangle(Brushes.Black, cOff, e.FullMapRectangle.Bottom - height - cOff, width, height + cOff);
                e.Graphics.DrawString(continentNumber, _continentFont, SystemBrushes.GradientInactiveCaption, cOff + 1, e.FullMapRectangle.Bottom - height - cOff);
            }
        }

        /// <summary>
        /// Moves the center of the map
        /// </summary>
        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                Point game = _map.Display.GetGameLocation(e.MouseEventArgs.X, e.MouseEventArgs.Y);
                _mainMap.SetCenter(game.X, game.Y);
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
            // TODO: we might have some bubbling of events here
            // ie MainMap.SetCenter -> MiniMap.SetCenter -> MainMap.SetCenter
            // check this sometime
            if (sender != this)
            {
                if (_map.Location == null)
                {
                    _map.SetCenter(e.NewLocation.X, e.NewLocation.Y, 1);
                }
                else if (e.NewLocation.Zoom != e.OldLocation.Zoom || GetDistance(e.NewLocation, _map.Location) > 100)
                {
                    _map.SetCenter(e.NewLocation.X, e.NewLocation.Y, 1);
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
            _map.Display.ResetCache();
            _map.Control.Invalidate();
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
        }
        #endregion
    }
}