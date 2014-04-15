#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using TribalWars.Data.Maps;
using TribalWars.Data.Villages;
using System.Diagnostics;
using TribalWars.Data.Events;
#endregion

namespace TribalWars.Controls.Maps
{
    /// <summary>
    /// The control on which a TW map is painted
    /// </summary>
    public class ScrollableMapControl : ScrollableControl
    {
        #region Fields
        protected Map _map;
        protected ToolTip _villageToolTipControl;
        private Timer _timer;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the timercontrol for animated painting
        /// </summary>
        public Timer Timer
        {
            get { return _timer; }
        }

        /// <summary>
        /// Gets the width of the canvas
        /// </summary>
        public int PictureWidth
        {
            get { return ClientRectangle.Width; }
        }

        /// <summary>
        /// Gets the height of the canvas
        /// </summary>
        public int PictureHeight
        {
            get { return ClientRectangle.Height; }
        }

        /// <summary>
        /// Gets the size of the canvas
        /// </summary>
        public Rectangle PictureRectangle
        {
            get { return ClientRectangle; }
        }
        #endregion

        #region Constructors
        public ScrollableMapControl()
            : base()
        {
            BackColor = Color.Green;
            _villageToolTipControl = new ToolTip();
            _villageToolTipControl.Active = true;
            _villageToolTipControl.IsBalloon = true;
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            _timer = new Timer();
            _timer.Enabled = false;
            _timer.Interval = 10;
            _timer.Tick += new EventHandler(Timer_Tick);
            
        }
        #endregion

        #region Event Handlers
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (this.Visible && _map != null && _map.Manipulators.KeyDown(e, this))
            {
                Invalidate();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (this.Visible && _map != null && _map.Manipulators.KeyUp(e, this))
            {
                Invalidate();
            }
        }

        /// <summary>
        /// We want our map to also react to keys that are not normally
        /// triggered by the KeyDown event of the control
        /// </summary>
        /// <remarks>For example the arrow keys for moving the map etc</remarks>
        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down) return true;
            return base.IsInputKey(keyData);
        }

        //protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        //{
        //    bool blnProcess = false;

        //    if (keyData == Keys.Right || Keys.Down)
        //    {
        //        // Process the keystroke
        //        blnProcess = true;
        //    }
        //    else if (keyData == Keys.Left || Keys.Up)
        //    {
        //        // Process the keystroke
        //        blnProcess = true;
        //    }

        //    if (blnProcess == true)
        //        return true;
        //    else
        //        return base.ProcessCmdKey(ref m, keyData);
        //}

        /*protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Debug.Print("OnGotFocus");
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Debug.Print("OnLostFocus");
        }*/

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Village village = World.Default.Map.Display.GetGameVillage(e.X, e.Y);
            if (this.Visible && _map != null && _map.Manipulators.MouseDown(e, village, this))
            {
                Invalidate();
            }
            GiveFocus();
        }

        /// <summary>
        /// A usercontrol cannot gain focus. That is why
        /// we call this.Focus() in the OnMouseDown method.
        /// Without focus the map would not respond to the
        /// KeyDown events.
        /// </summary>
        /// <remarks>
        /// The MiniMap gives back the focus to the main map
        /// </remarks>
        public virtual void GiveFocus()
        {
            this.Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.Visible && _map != null && _map.Manipulators.MouseMove(e, this, _villageToolTipControl))
            {
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Village village = World.Default.Map.Display.GetGameVillage(e.X, e.Y);
            if (this.Visible && _map != null && _map.Manipulators.MouseUp(e, village, this))
            {
                Invalidate();
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            Village village = World.Default.Map.Display.GetGameVillage(e.X, e.Y);
            if (this.Visible && _map != null && _map.Manipulators.OnVillageDoubleClick(e, village, this))
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.Visible || DesignMode || !World.Default.HasLoaded || _map == null)
            {

            }
            else
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                _map.Display.Paint(e.Graphics, e.ClipRectangle, ClientRectangle);

                _map.Manipulators.Paint(e.Graphics, e.ClipRectangle, ClientRectangle);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_map != null && World.Default.HasLoaded)
            {
                _map.Display.ResetCache();
                Invalidate();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!this.Visible || DesignMode || !World.Default.HasLoaded || _map == null)
            {

            }
            else
            {
                _map.Manipulators.TimerPaint(this, ClientRectangle);
                //this.Invalidate();
            }
        }

        private void EventPublisher_VillagesSelected(object sender, VillagesEventArgs e)
        {
            Invalidate();
        }

        private void EventPublisher_LocationChanged(object sender, MapLocationEventArgs e)
        {
            Invalidate();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the map parent
        /// </summary>
        public void SetMap(Map map)
        {
            _map = map;
            _map.EventPublisher.LocationChanged += new EventHandler<MapLocationEventArgs>(EventPublisher_LocationChanged);
            _map.EventPublisher.VillagesSelected += new EventHandler<VillagesEventArgs>(EventPublisher_VillagesSelected);
            _timer.Enabled = true;
        }

        /// <summary>
        /// Creates a screenshot of the map
        /// </summary>
        public void Screenshot(string fileName)
        {
            Bitmap shot = null;
            try
            {
                shot = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                DrawToBitmap(shot, ClientRectangle);
                shot.Save(fileName);
            }
            finally
            {
                if (shot != null) shot.Dispose();
            }
        }
        #endregion
    }
}
