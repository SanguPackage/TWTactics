#region Using
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Villages;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Draws circles around the villages of the owner
    /// of the village the user last hovered over
    /// </summary>
    internal class ActiveVillageManipulator : ManipulatorBase
    {
        #region Fields
        private Tribe _pinPointedTribe;
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
            _map.EventPublisher.PlayerSelected += EventPublisher_VillagesSelected;
            _map.EventPublisher.TribeSelected += EventPublisher_VillagesSelected;
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

                    _selectedVillage = e.Village;
                    _unpinpointedVillage = null;
                    return true;
                }
            }
            if (e.Village == null && _pinPointedVillage == null && _selectedVillage != null)
            {
                _selectedVillage = null;
                _map.EventPublisher.Deselect(this);
                return true;
            }
            return false;
        }

        public override void Paint(MapPaintEventArgs e, bool isActiveManipulator)
        {
            if (isActiveManipulator)
            {
                Rectangle gameSize = _map.Display.GetGameRectangle();
                Debug.Assert(new Rectangle(new Point(0, 0), _map.CanvasSize) == e.FullMapRectangle);

                var villageDimension = _map.Display.Dimensions.SizeWithSpacing;

                if (_pinPointedTribe != null)
                {
                    foreach (Player player in _pinPointedTribe.Players)
                    {
                        IEnumerable<Village> uneventfulVillages = GetUneventfulVillages(player);
                        DrawVillageMarkers(e.Graphics, uneventfulVillages, gameSize, _otherVillagesPen, villageDimension);
                    }

                    // Gained & lost villages:
                    foreach (Player player in _pinPointedTribe.Players)
                    {
                        DrawVillageMarkers(e.Graphics, player.GainedVillages, gameSize, _newVillagesPen, villageDimension);
                        DrawVillageMarkers(e.Graphics, player.LostVillages, gameSize, _lostVillagesPen, villageDimension);
                    }
                }
                else
                {
                    Village village = _pinPointedVillage ?? _selectedVillage;
                    if (village != null)
                    {
                        if (village.HasPlayer)
                        {
                            IEnumerable<Village> uneventfulVillages = GetUneventfulVillages(village.Player);
                            DrawVillageMarkers(e.Graphics, uneventfulVillages, gameSize, _otherVillagesPen, villageDimension);

                            // Gained & lost villages:
                            DrawVillageMarkers(e.Graphics, village.Player.GainedVillages, gameSize, _newVillagesPen, villageDimension);
                            DrawVillageMarkers(e.Graphics, village.Player.LostVillages, gameSize, _lostVillagesPen, villageDimension);
                        }
                        else if (village.PreviousVillageDetails != null && village.PreviousVillageDetails.HasPlayer)
                        {
                            // Newly barbarian
                            DrawVillageMarkers(e.Graphics, village.PreviousVillageDetails.Player.Villages, gameSize, _otherVillagesPen, villageDimension);
                        }
                    }
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

                    _map.EventPublisher.Deselect(this);

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

            _map.EventPublisher.SelectVillages(this, e.Village, VillageTools.PinPoint);
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

        /// <summary>
        /// Cleanup anything when switching worlds or settings
        /// </summary>
        protected internal override void CleanUp()
        {
            _selectedVillage = null;
            _pinPointedTribe = null;
            _lockPinpointedVillage = false;
            _unpinpointedVillage = null;
        }

        public override void TimerPaint(MapTimerPaintEventArgs e)
        {
            if (e.IsActiveManipulator)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Village village = _pinPointedVillage ?? _selectedVillage;
                if (village != null)
                {
                    var villageSize = _map.Display.Dimensions.SizeWithSpacing;

                    Point mapLocation = _map.Display.GetMapLocation(village.Location);

                    const int xOff = 5;
                    const int yOff = 10;

                    _pinpointedAnimationCounter += 5;
                    if (_pinpointedAnimationCounter > 360) _pinpointedAnimationCounter = 0;

                    e.Graphics.DrawEllipse(
                        _lockPinpointedVillage ? _pinpointedAnimationPen : _pinpointedPen, 
                        mapLocation.X - xOff, 
                        mapLocation.Y - xOff,
                        villageSize.Width + yOff,
                        villageSize.Height + yOff);

                    e.Graphics.DrawArc(
                        _lockPinpointedVillage ? _pinpointedPen : _pinpointedAnimationPen, 
                        mapLocation.X - xOff, 
                        mapLocation.Y - xOff,
                        villageSize.Width + yOff,
                        villageSize.Height + yOff,
                        _pinpointedAnimationCounter, 
                        30);
                }
            }
        }

        private void EventPublisher_VillagesSelected(object sender, VillagesEventArgs e)
        {
            if (e.Tool == VillageTools.PinPoint)
            {
                var tribe = e.Villages as Tribe;
                if (tribe != null)
                {
                    _pinPointedTribe = tribe;
                    _pinPointedVillage = null;
                    _selectedVillage = null;
                    _lockPinpointedVillage = false;
                }
                else
                {                    
                    _pinPointedTribe = null;
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

        #region Privates
        private IEnumerable<Village> GetUneventfulVillages(Player player)
        {
            return player.Where(x => !player.GainedVillages.Contains(x) && !player.LostVillages.Contains(x));
        }

        private void DrawVillageMarkers(Graphics g, IEnumerable<Village> villages, Rectangle gameSize, Pen pen, Size villageDimension)
        {
            foreach (Village village in villages)
            {
                if (gameSize.Contains(village.Location))
                {
                    Point villageLocation = _map.Display.GetMapLocation(village.Location);
                    g.DrawEllipse(
                        pen,
                        villageLocation.X,
                        villageLocation.Y,
                        villageDimension.Width,
                        villageDimension.Height);
                }
            }
        }
        #endregion
    }
}