#region Using
using System;
using System.Diagnostics;
using System.Windows.Forms;
using TribalWars.Villages;
using TribalWars.Worlds;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Maps.Controls
{
    /// <summary>
    /// The control on which a TW map is painted
    /// </summary>
    public class ScrollableMapControl : ScrollableControl
    {
        #region Fields
        protected Map Map;
        private readonly Timer _timer;
        #endregion

        #region Properties
        /// <summary>
        /// Do we need to call the manipulators on ControlEvents
        /// or has no world been selected yet?
        /// </summary>
        private bool IsManipulatable
        {
            get { return Visible && Map.HasPainted; }
        }
        #endregion

        #region Constructors
        public ScrollableMapControl()
        {
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            _timer = new Timer();
            _timer.Enabled = false;
            _timer.Interval = 10;
            _timer.Tick += Timer_Tick;
            
        }
        #endregion

        #region Event Handlers
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (IsManipulatable && Map.Manipulators.KeyDown(e))
            {
                Invalidate();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (IsManipulatable && Map.Manipulators.KeyUp(e))
            {
                Invalidate();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (IsManipulatable && Map.Manipulators.MouseWheel(e))
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (IsManipulatable)
            {
                Village village = World.Default.Map.Display.GetGameVillage(e.Location);
                if (Map.Manipulators.MouseDown(e, village))
                {
                    Invalidate();
                }
            }
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
            Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsManipulatable && Map.Manipulators.MouseMove(e, this))
            {
                Invalidate();
            }
            Focus();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (IsManipulatable)
            {
                Village village = World.Default.Map.Display.GetGameVillage(e.Location);
                if (Map.Manipulators.MouseUp(e, village))
                {
                    Invalidate();
                }

                if (e.Button == MouseButtons.Right)
                {
                    Map.Manipulators.CurrentManipulator.ShowContextMenu(e.Location, village);
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (IsManipulatable)
            {
                Village village = World.Default.Map.Display.GetGameVillage(e.Location);
                if (Visible && Map != null && Map.Manipulators.OnVillageDoubleClick(e, village))
                {
                    Invalidate();
                }
            }
        }
       
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!Visible || DesignMode || !World.Default.HasLoaded || Map == null)
            {

            }
            else
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Map.Display.Paint(e.Graphics, ClientRectangle);

                Map.Manipulators.Paint(e.Graphics, ClientRectangle);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Map != null && World.Default.HasLoaded)
            {
                Map.Invalidate();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!Visible || DesignMode || !World.Default.HasLoaded || Map == null)
            {

            }
            else
            {
                Map.Manipulators.TimerPaint(this, ClientRectangle);
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
            Debug.Assert(Map == null);

            Map = map;
            Map.EventPublisher.LocationChanged += EventPublisher_LocationChanged;
            Map.EventPublisher.VillagesSelected += EventPublisher_VillagesSelected;
            _timer.Enabled = true;
        }
        #endregion
    }
}
