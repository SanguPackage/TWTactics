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
        #endregion

        #region Properties
        public Village SelectedVillage { get; set; }

        public Village PinPointedVillage { get; set; }

        public Pen ActiveVillagePen { get; set; }

        public Pen NewVillagesPen { get; set; }

        public Pen LostVillagesPen { get; set; }

        public Pen OtherVillagesPen { get; set; }
        #endregion

        #region Constructors
        public ActiveVillageManipulator(Map map, Color activeVillage, Color newVillages, Color lostVillages,
                                        Color otherVillages)
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
                    _map.EventPublisher.SelectVillages(this, e.Village, VillageTools.SelectVillage);

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
                // Todo: adhv gameSize + villageWidth/Height uitrekenen ipv GetMapLocation
                Village village = PinPointedVillage == null ? SelectedVillage : PinPointedVillage;
                if (village != null)
                {
                    Rectangle gameSize = _map.Display.GetGameRectangle(e.FullMapRectangle);

                    int villageWidth =
                        _map.Display.DisplayManager.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);
                    int villageHeight =
                        _map.Display.DisplayManager.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);

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
                                e.Graphics.DrawEllipse(OtherVillagesPen, villageLocation.X, villageLocation.Y,
                                                       villageWidth, villageHeight);
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
                                    e.Graphics.DrawEllipse(NewVillagesPen, villageLocation.X, villageLocation.Y,
                                                           villageWidth, villageHeight);
                                }
                            }

                        if (village.Player.LostVillages != null)
                            foreach (Village draw in village.Player.LostVillages)
                            {
                                if (gameSize.Contains(draw.Location))
                                {
                                    Point villageLocation = _map.Display.GetMapLocation(draw.Location);
                                    e.Graphics.DrawEllipse(LostVillagesPen, villageLocation.X, villageLocation.Y,
                                                           villageWidth, villageHeight);
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
                                e.Graphics.DrawEllipse(OtherVillagesPen, villageLocation.X, villageLocation.Y,
                                                       villageWidth, villageHeight);
                            }
                        }
                    }
                }

                //timerCount -= 10;
                //TimerPaint(g, fullMap, isActiveManipulator);
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
        }

        private int timerCount;

        public override void TimerPaint(MapTimerPaintEventArgs e)
        {
            if (e.IsActiveManipulator)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Village village = PinPointedVillage == null ? SelectedVillage : PinPointedVillage;
                if (village != null)
                {
                    int villageHeight =
                        _map.Display.DisplayManager.CurrentDisplay.GetVillageHeightSpacing(_map.Location.Zoom);
                    int villageWidth =
                        _map.Display.DisplayManager.CurrentDisplay.GetVillageWidthSpacing(_map.Location.Zoom);

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
                    using (
                        Brush grad1 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(x1, y1),
                                                                                       new Point(x2, y2), Color.Black,
                                                                                       Color.White))
                        //using (Brush grad2 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(mapLocation.X - 5, mapLocation.Y - 5), new Point(mapLocation.X - 5 + villageWidth, mapLocation.Y - 5 + villageHeight), Color.White, Color.Black))
                    {
                        using (Pen pen1 = new Pen(grad1, 3))
                            //using (Pen pen2 = new Pen(grad2, 2))
                        {
                            //Pen p = new Pen(gradF, 2);

                            //g.DrawLine(new Pen(Color.White, 2), x1, y1, x2, y2);

                            //g.DrawEllipse(new Pen(Color.Black, 3), mapLocation.X - xOff - 1, mapLocation.Y - xOff - 1, villageWidth + yOff + 1, villageHeight + yOff + 1);
                            e.Graphics.DrawEllipse(new Pen(Color.White, 2), mapLocation.X - xOff, mapLocation.Y - xOff,
                                                   villageWidth + yOff, villageHeight + yOff);
                            e.Graphics.DrawArc(new Pen(Color.Black, 2), mapLocation.X - xOff, mapLocation.Y - xOff,
                                               villageWidth + yOff, villageHeight + yOff, timerCount, 30);
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
                PinPointedVillage = e.FirstVillage;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}