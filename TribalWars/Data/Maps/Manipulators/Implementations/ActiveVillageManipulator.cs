#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TribalWars.Data.Villages;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators.Helpers;
#endregion

namespace TribalWars.Data.Maps.Manipulators
{
    /// <summary>
    /// Draws circles around the villages of the owner
    /// of the village the user last hovered over
    /// </summary>
    internal class ActiveVillageManipulator : ManipulatorBase
    {
        #region Fields
        private Pen _activeVillagePen;
        private Pen _newVillagesPen;
        private Pen _lostVillagesPen;
        private Pen _otherVillagesPen;

        private Village _selectedVillage;
        private Village _pinpointedVillage;
        private Village _unpinpointedVillage;
        #endregion

        #region Properties
        public Village SelectedVillage
        {
            get { return _selectedVillage; }
            set { _selectedVillage = value; }
        }

        public Village PinPointedVillage
        {
            get { return _pinpointedVillage; }
            set { _pinpointedVillage = value; }
        }

        public Pen ActiveVillagePen
        {
            get { return _activeVillagePen; }
            set { _activeVillagePen = value; }
        }

        public Pen NewVillagesPen
        {
            get { return _newVillagesPen; }
            set { _newVillagesPen = value; }
        }

        public Pen LostVillagesPen
        {
            get { return _lostVillagesPen; }
            set { _lostVillagesPen = value; }
        }

        public Pen OtherVillagesPen
        {
            get { return _otherVillagesPen; }
            set { _otherVillagesPen = value; }
        }
        #endregion

        #region Constructors
        public ActiveVillageManipulator(Map map, Color activeVillage, Color newVillages, Color lostVillages, Color otherVillages)
            : base(map)
        {
            _activeVillagePen = new Pen(activeVillage, 2);
            _newVillagesPen = new Pen(newVillages);
            _lostVillagesPen = new Pen(lostVillages);
            _otherVillagesPen = new Pen(otherVillages);

            _activeVillagePen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            _newVillagesPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            _lostVillagesPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            _otherVillagesPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

            _map.EventPublisher.VillagesSelected += new EventHandler<TribalWars.Data.Events.VillagesEventArgs>(EventPublisher_VillagesSelected);
        }
        #endregion

        #region Public Methods
        internal protected override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            if (e.Village != null && _pinpointedVillage == null && _unpinpointedVillage != e.Village)
            {
                if (_selectedVillage != e.Village)
                    _map.EventPublisher.SelectVillages(this, e.Village, VillageTools.SelectVillage);

                _selectedVillage = e.Village;
                _unpinpointedVillage = null;
                return true;
            }
            return false;
        }

        public override void Paint(MapPaintEventArgs e)
        {
            if (e.IsActiveManipulator)
            {
                // Todo: adhv gameSize + villageWidth/Height uitrekenen ipv GetMapLocation
                Village village = _pinpointedVillage == null ? _selectedVillage : _pinpointedVillage;
                if (village != null)
                {
                    Rectangle gameSize = _map.Display.GetGameRectangle(e.FullMapRectangle);

                    int villageWidth = _map.Display.DisplayManager.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);
                    int villageHeight = _map.Display.DisplayManager.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);

                    // active village -- now in the TimerPaint
                    /*if (gameSize.Contains(village.Location))
                    {
                        Point mapLocation = _map.Display.GetMapLocation(village.Location);
                        g.DrawEllipse(_activeVillagePen, mapLocation.X - 5, mapLocation.Y - 5, villageWidth + 10, villageHeight + 10);
                    }*/

                    if (village.HasPlayer)
                    {
                        // other villages
                        // TODO: code for creating a map with big blocks
                        //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath(); 
                        //path.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
                        foreach (Village draw in village.Player)
                        {
                            if (gameSize.Contains(draw.Location))
                            {
                                Point villageLocation = _map.Display.GetMapLocation(draw.Location);
                                e.Graphics.DrawEllipse(_otherVillagesPen, villageLocation.X, villageLocation.Y, villageWidth, villageHeight);
                                //path.AddEllipse(villageLocation.X - 20, villageLocation.Y - 20, villageWidth + 40, villageHeight + 40);
                            }
                        }
                        //Region region = new Region(path);
                        //g.FillRegion(new SolidBrush(Color.White), region);
                        //path.Dispose();

                        // Gained & lost villages:
                        if (village.Player.GainedVillages != null)
                            foreach (Village draw in village.Player.GainedVillages)
                            {
                                if (gameSize.Contains(draw.Location))
                                {
                                    Point villageLocation = _map.Display.GetMapLocation(draw.Location);
                                    e.Graphics.DrawEllipse(_newVillagesPen, villageLocation.X, villageLocation.Y, villageWidth, villageHeight);
                                }
                            }

                        if (village.Player.LostVillages != null)
                            foreach (Village draw in village.Player.LostVillages)
                            {
                                if (gameSize.Contains(draw.Location))
                                {
                                    Point villageLocation = _map.Display.GetMapLocation(draw.Location);
                                    e.Graphics.DrawEllipse(_lostVillagesPen, villageLocation.X, villageLocation.Y, villageWidth, villageHeight);
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
                                e.Graphics.DrawEllipse(_otherVillagesPen, villageLocation.X, villageLocation.Y, villageWidth, villageHeight);
                            }
                        }
                    }
                }

                //timerCount -= 10;
                //TimerPaint(g, fullMap, isActiveManipulator);
            }
        }

        internal protected override bool OnVillageClickCore(MapVillageEventArgs e)
        {
            if (_pinpointedVillage != null)
            {
                if (_pinpointedVillage == e.Village)
                {
                    _unpinpointedVillage = e.Village;
                    _pinpointedVillage = null;
                    _selectedVillage = null;
                }
                else
                {
                    _pinpointedVillage = e.Village;
                    _map.EventPublisher.SelectVillages(this, e.Village, VillageTools.PinPoint);
                }

                return true;
            }
            _pinpointedVillage = e.Village;
            return _selectedVillage != e.Village;
        }

        public override void Dispose()
        {
            _activeVillagePen.Dispose();
            _newVillagesPen.Dispose();
            _lostVillagesPen.Dispose();
            _otherVillagesPen.Dispose();
        }

        private int timerCount = 0;
        public override void TimerPaint(MapTimerPaintEventArgs e)
        {
            if (e.IsActiveManipulator)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Village village = _pinpointedVillage == null ? _selectedVillage : _pinpointedVillage;
                if (village != null)
                {
                    int villageHeight = _map.Display.DisplayManager.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);
                    int villageWidth = _map.Display.DisplayManager.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);

                    timerCount += 5;
                    //if (timerCount > villageHeight + villageWidth + 10 + 5) timerCount = 0;
                    if (timerCount > 360) timerCount = 0;
                    else if (timerCount < 0)
                    {
                        timerCount = 0;
                    }
                    

                    Point mapLocation = _map.Display.GetMapLocation(village.Location);
                    //g.DrawArc(_activeVillagePen, mapLocation.X - 5, mapLocation.Y - 5, villageWidth + 10, villageHeight + 10, timerCount, 320);


                    //Pen pen = new Pen(SystemColors.GradientActiveCaption, 2);
                    //g.DrawArc(pen, mapLocation.X - 5, mapLocation.Y - 5, villageWidth + 10, villageHeight + 10, timerCount, 360);

                    //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath(null, 

                    int xOff = 5;
                    int yOff = 10;

                    int x1 = mapLocation.X - 5;
                    int y1 = mapLocation.Y - 5;
                    int x2 = mapLocation.X + yOff + villageWidth;
                    int y2 = mapLocation.Y + yOff + villageHeight;

                    /*if (timerCount < villageHeight + 10 + 5)
                    {
                        x1 += 0;
                        y1 += timerCount;

                        x2 += 0;
                        y2 += 0 - timerCount;
                        
                    }
                    else
                    {
                        //timerCount = 0;
                        x1 += timerCount - villageHeight;
                        int temp = y2;
                        y2 = y1;
                        y1 = temp;

                        x2 += 0 - timerCount + villageHeight;
                        //y2 = 0;
                    }*/

                    //g.DrawLine(new Pen(Color.White, 2), x1, y1, x2, y2);

                    //using (Brush gradF = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, timerCount), new Point(villageWidth, timerCount + 10 + villageHeight), Color.Black, Color.White))
                    using (Brush grad1 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(x1, y1), new Point(x2, y2), Color.Black, Color.White))
                    //using (Brush grad2 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(mapLocation.X - 5, mapLocation.Y - 5), new Point(mapLocation.X - 5 + villageWidth, mapLocation.Y - 5 + villageHeight), Color.White, Color.Black))
                    {
                        using (Pen pen1 = new Pen(grad1, 3))
                        //using (Pen pen2 = new Pen(grad2, 2))
                        {
                            //Pen p = new Pen(gradF, 2);

                            //g.DrawLine(new Pen(Color.White, 2), x1, y1, x2, y2);

                            //g.DrawEllipse(new Pen(Color.Black, 3), mapLocation.X - xOff - 1, mapLocation.Y - xOff - 1, villageWidth + yOff + 1, villageHeight + yOff + 1);
                            e.Graphics.DrawEllipse(new Pen(Color.White, 2), mapLocation.X - xOff, mapLocation.Y - xOff, villageWidth + yOff, villageHeight + yOff);
                            e.Graphics.DrawArc(new Pen(Color.Black, 2), mapLocation.X - xOff, mapLocation.Y - xOff, villageWidth + yOff, villageHeight + yOff, timerCount, 30);
                            //g.FillRectangle(grad1, mapLocation.X - xOff, mapLocation.Y - xOff, villageWidth + yOff, villageHeight + yOff);

                            //g.DrawArc(pen1, mapLocation.X - 5, mapLocation.Y - 5, villageWidth + 10, villageHeight + 10, 0, 30);
                            //g.DrawArc(pen2, mapLocation.X - 5, mapLocation.Y - 5, villageWidth + 10, villageHeight + 10, 0 + 30, 30);

                            //g.FillRectangle(Brushes.Black, 50, 50, 50, 50);
                            //g.DrawString(timerCount.ToString(), new Font("Verdana", 16), Brushes.White, new Point(50, 50));
                        }
                    }
                    

                    //g.DrawEllipse(_activeVillagePen, mapLocation.X - 5, mapLocation.Y - 5, villageWidth + 10, villageHeight + 10);
                }
            }
        }
        #endregion

        #region Event Handlers
        private void EventPublisher_VillagesSelected(object sender, TribalWars.Data.Events.VillagesEventArgs e)
        {
            if (e.Tool == VillageTools.PinPoint)
                _pinpointedVillage = e.FirstVillage;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
