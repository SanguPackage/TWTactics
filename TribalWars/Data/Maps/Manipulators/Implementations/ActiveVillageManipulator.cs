#region Using
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Data.Tribes;
using TribalWars.Data.Villages;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Draws circles around the villages of the owner
    /// of the village the user last hovered over
    /// </summary>
    internal class ActiveVillageManipulator : ManipulatorBase
    {
        #region Fields
        private Village _selectedVillage;
        private Village _pinPointedVillage;
        private Village _unpinpointedVillage;
        private bool _lockPinpointedVillage;

        private readonly Pen _pinpointedPen;
        private readonly Pen _pinpointedAnimationPen;
        private int _pinpointedAnimationCounter;
        private readonly Pen _newVillagesPen;
        private readonly Pen _lostVillagesPen;
        private readonly Pen _otherVillagesPen;
        #endregion

        #region Constructors
        public ActiveVillageManipulator(Map map)
            : base(map)
        {
            _newVillagesPen = new Pen(Color.Chartreuse, 2);
            _lostVillagesPen = new Pen(Color.Violet, 2);
            _otherVillagesPen = new Pen(Color.White, 2);
            _pinpointedPen = new Pen(Color.CornflowerBlue, 2);
            _pinpointedAnimationPen = new Pen(Color.Black, 2);

            _map.EventPublisher.VillagesSelected += EventPublisher_VillagesSelected;
        }
        #endregion

        #region Methods
        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            if (e.Village != null && _pinPointedVillage == null && _unpinpointedVillage != e.Village)
            {
                if (_selectedVillage != e.Village)
                {
                    _map.EventPublisher.SelectVillages(this, e.Village, VillageTools.SelectVillage);
                }

                _selectedVillage = e.Village;
                _unpinpointedVillage = null;
                return true;
            }
            if (e.Village == null && _pinPointedVillage == null && _selectedVillage != null)
            {
                _selectedVillage = null;
                return true;
            }
            return false;
        }

        public override void Paint(MapPaintEventArgs e)
        {
            if (e.IsActiveManipulator)
            {
                Village village = _pinPointedVillage ?? _selectedVillage;
                if (village != null)
                {
                    Rectangle gameSize = _map.Display.GetGameRectangle(e.FullMapRectangle);

                    int villageWidth = _map.Display.DisplayManager.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);
                    int villageHeight = _map.Display.DisplayManager.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);

                    if (village.HasPlayer)
                    {
                        // other villages
                        var uneventfulVillages = village.Player.Where(x => !village.Player.GainedVillages.Contains(x) && !village.Player.LostVillages.Contains(x));
                        DrawVillageMarkers(e.Graphics, uneventfulVillages, gameSize, _otherVillagesPen, villageWidth, villageHeight);

                        // Gained & lost villages:
                        DrawVillageMarkers(e.Graphics, village.Player.GainedVillages, gameSize, _newVillagesPen, villageWidth, villageHeight);
                        DrawVillageMarkers(e.Graphics, village.Player.LostVillages, gameSize, _lostVillagesPen, villageWidth, villageHeight);
                    }
                    else if (village.PreviousVillageDetails != null && village.PreviousVillageDetails.HasPlayer)
                    {
                        // Newly barbarian
                        DrawVillageMarkers(e.Graphics, village.PreviousVillageDetails.Player.Villages, gameSize, _otherVillagesPen, villageWidth, villageHeight);
                    }
                }
            }
        }

        private void DrawVillageMarkers(Graphics g, IEnumerable<Village> villages, Rectangle gameSize, Pen pen, int villageWidth, int villageHeight)
        {
            foreach (Village draw in villages)
            {
                if (gameSize.Contains(draw.Location))
                {
                    Point villageLocation = _map.Display.GetMapLocation(draw.Location);
                    g.DrawEllipse(
                        pen, 
                        villageLocation.X, 
                        villageLocation.Y,
                        villageWidth, 
                        villageHeight);
                }
            }
        }

        protected internal override bool OnVillageClickCore(MapVillageEventArgs e)
        {
            if (_pinPointedVillage != null)
            {
                if (_pinPointedVillage == e.Village)
                {
                    _unpinpointedVillage = e.Village;
                    _pinPointedVillage = null;
                    _selectedVillage = null;
                    _lockPinpointedVillage = false;
                    return true;
                }
                else if (_lockPinpointedVillage)
                {
                    return false;
                }
                else
                {
                    _pinPointedVillage = e.Village;
                    _map.EventPublisher.SelectVillages(this, e.Village, VillageTools.PinPoint);
                    return true;
                }
            }

            _pinPointedVillage = e.Village;
            return _selectedVillage != e.Village;
        }

        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            if (e.KeyEventArgs.KeyData == Keys.L && _pinPointedVillage != null)
            {
                _lockPinpointedVillage = !_lockPinpointedVillage;
            }
            return false;
        }

        public override void TimerPaint(MapTimerPaintEventArgs e)
        {
            if (e.IsActiveManipulator)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Village village = _pinPointedVillage ?? _selectedVillage;
                if (village != null)
                {
                    int villageHeight = _map.Display.DisplayManager.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);
                    int villageWidth = _map.Display.DisplayManager.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);

                    Point mapLocation = _map.Display.GetMapLocation(village.Location);

                    const int xOff = 5;
                    const int yOff = 10;

                    _pinpointedAnimationCounter += 5;
                    if (_pinpointedAnimationCounter > 360) _pinpointedAnimationCounter = 0;

                    e.Graphics.DrawEllipse(
                        _lockPinpointedVillage ? _pinpointedAnimationPen : _pinpointedPen, 
                        mapLocation.X - xOff, 
                        mapLocation.Y - xOff,
                        villageWidth + yOff, 
                        villageHeight + yOff);

                    e.Graphics.DrawArc(
                        _lockPinpointedVillage ? _pinpointedPen : _pinpointedAnimationPen, 
                        mapLocation.X - xOff, 
                        mapLocation.Y - xOff,
                        villageWidth + yOff, 
                        villageHeight + yOff,
                        _pinpointedAnimationCounter, 
                        30);
                }
            }
        }

        private void EventPublisher_VillagesSelected(object sender, Events.VillagesEventArgs e)
        {
            if (e.Tool == VillageTools.PinPoint)
            {
                var tribe = e.Villages as Tribe;
                if (tribe != null)
                {
                    
                }
                else
                {
                    _pinPointedVillage = e.FirstVillage;
                }
            }
        }

        public override void Dispose()
        {
            _newVillagesPen.Dispose();
            _lostVillagesPen.Dispose();
            _otherVillagesPen.Dispose();
            _pinpointedPen.Dispose();
            _pinpointedAnimationPen.Dispose();
        }
        #endregion
    }
}