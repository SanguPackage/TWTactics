#region Using
using System.Drawing;
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;
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
        private Village _unpinpointedVillage;

        private readonly Pen _pinpointedPen = new Pen(Color.White, 2);
        private readonly Pen _pinpointedAnimationPen = new Pen(Color.Black, 2);
        private int _pinpointedAnimationCounter;
        #endregion

        #region Properties
        private Village SelectedVillage { get; set; }

        private Village PinPointedVillage { get; set; }

        private Pen ActiveVillagePen { get; set; }

        private Pen NewVillagesPen { get; set; }

        private Pen LostVillagesPen { get; set; }

        private Pen OtherVillagesPen { get; set; }
        #endregion

        #region Constructors
        public ActiveVillageManipulator(Map map, Color activeVillage, Color newVillages, Color lostVillages, Color otherVillages)
            : base(map)
        {
            ActiveVillagePen = new Pen(activeVillage, 2);
            NewVillagesPen = new Pen(newVillages);
            LostVillagesPen = new Pen(lostVillages);
            OtherVillagesPen = new Pen(otherVillages);

            ActiveVillagePen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            NewVillagesPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            LostVillagesPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            OtherVillagesPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

            _map.EventPublisher.VillagesSelected += EventPublisher_VillagesSelected;
        }
        #endregion

        #region Public Methods
        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            if (e.Village != null && PinPointedVillage == null && _unpinpointedVillage != e.Village)
            {
                if (SelectedVillage != e.Village)
                {
                    _map.EventPublisher.SelectVillages(this, e.Village, VillageTools.SelectVillage);
                }

                SelectedVillage = e.Village;
                _unpinpointedVillage = null;
                return true;
            }
            if (e.Village == null && PinPointedVillage == null && SelectedVillage != null)
            {
                SelectedVillage = null;
                return true;
            }
            return false;
        }

        public override void Paint(MapPaintEventArgs e)
        {
            if (e.IsActiveManipulator)
            {
                Village village = PinPointedVillage == null ? SelectedVillage : PinPointedVillage;
                if (village != null)
                {
                    Rectangle gameSize = _map.Display.GetGameRectangle(e.FullMapRectangle);

                    int villageWidth = _map.Display.DisplayManager.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);
                    int villageHeight = _map.Display.DisplayManager.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);

                    if (village.HasPlayer)
                    {
                        // other villages
                        var path = new System.Drawing.Drawing2D.GraphicsPath();
                        path.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
                        foreach (Village draw in village.Player)
                        {
                            if (gameSize.Contains(draw.Location))
                            {
                                Point villageLocation = _map.Display.GetMapLocation(draw.Location);
                                e.Graphics.DrawEllipse(
                                    OtherVillagesPen, 
                                    villageLocation.X, 
                                    villageLocation.Y,
                                    villageWidth, 
                                    villageHeight);

                                //path.AddEllipse(villageLocation.X - 20, villageLocation.Y - 20, villageWidth + 40, villageHeight + 40);
                            }
                        }

                        // Gained & lost villages:
                        if (village.Player.GainedVillages != null)
                            foreach (Village draw in village.Player.GainedVillages)
                            {
                                if (gameSize.Contains(draw.Location))
                                {
                                    Point villageLocation = _map.Display.GetMapLocation(draw.Location);
                                    e.Graphics.DrawEllipse(
                                        NewVillagesPen, 
                                        villageLocation.X, 
                                        villageLocation.Y,
                                        villageWidth, 
                                        villageHeight);
                                }
                            }

                        if (village.Player.LostVillages != null)
                            foreach (Village draw in village.Player.LostVillages)
                            {
                                if (gameSize.Contains(draw.Location))
                                {
                                    Point villageLocation = _map.Display.GetMapLocation(draw.Location);
                                    e.Graphics.DrawEllipse(
                                        LostVillagesPen, 
                                        villageLocation.X, 
                                        villageLocation.Y,
                                        villageWidth, 
                                        villageHeight);
                                }
                            }
                    }
                    else if (village.PreviousVillageDetails != null && village.PreviousVillageDetails.HasPlayer)
                    {
                        foreach (Village draw in village.PreviousVillageDetails.Player.Villages)
                        {
                            if (gameSize.Contains(draw.Location))
                            {
                                Point villageLocation = _map.Display.GetMapLocation(draw.Location);
                                e.Graphics.DrawEllipse(
                                    OtherVillagesPen, 
                                    villageLocation.X, 
                                    villageLocation.Y,
                                    villageWidth, 
                                    villageHeight);
                            }
                        }
                    }
                }
            }
        }

        protected internal override bool OnVillageClickCore(MapVillageEventArgs e)
        {
            if (PinPointedVillage != null)
            {
                if (PinPointedVillage == e.Village)
                {
                    _unpinpointedVillage = e.Village;
                    PinPointedVillage = null;
                    SelectedVillage = null;
                }
                else
                {
                    PinPointedVillage = e.Village;
                    _map.EventPublisher.SelectVillages(this, e.Village, VillageTools.PinPoint);
                }

                return true;
            }
            PinPointedVillage = e.Village;
            return SelectedVillage != e.Village;
        }

        public override void Dispose()
        {
            ActiveVillagePen.Dispose();
            NewVillagesPen.Dispose();
            LostVillagesPen.Dispose();
            OtherVillagesPen.Dispose();
            _pinpointedPen.Dispose();
            _pinpointedAnimationPen.Dispose();
        }

        public override void TimerPaint(MapTimerPaintEventArgs e)
        {
            if (e.IsActiveManipulator)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Village village = PinPointedVillage ?? SelectedVillage;
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
                        _pinpointedPen, 
                        mapLocation.X - xOff, 
                        mapLocation.Y - xOff,
                        villageWidth + yOff, 
                        villageHeight + yOff);

                    e.Graphics.DrawArc(
                        _pinpointedAnimationPen, 
                        mapLocation.X - xOff, 
                        mapLocation.Y - xOff,
                        villageWidth + yOff, 
                        villageHeight + yOff,
                        _pinpointedAnimationCounter, 
                        30);
                }
            }
        }
        #endregion

        #region Event Handlers
        private void EventPublisher_VillagesSelected(object sender, Events.VillagesEventArgs e)
        {
            if (e.Tool == VillageTools.PinPoint)
                PinPointedVillage = e.FirstVillage;
        }
        #endregion
    }
}