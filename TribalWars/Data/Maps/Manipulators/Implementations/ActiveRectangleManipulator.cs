#region Using
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Tools;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Sets the ActiveRectangle for the Monitoring tab
    /// </summary>
    internal class ActiveRectangleManipulator : ManipulatorBase
    {
        #region Fields
        private readonly Pen _rectanglePen;
        private Size _activeRectangleSize;
        private Rectangle _activeRectangle;
        #endregion

        #region Tooltip Fields
        private bool _wasShowingTooltip;

        private bool _showHelpTooltip;
        private Point _lastTooltipLocation;
        private readonly ToolTip _helpTooltip;
        private const string HelpTitle = "Monitoring ActiveRectangle";
        private const string HelpBody = 
                @"Press + to grow the area
Press - to shrink the area
Left click to save the area
Right click to cancel
Press 's' to remove this tooltip";
        #endregion

        #region Constructors
        public ActiveRectangleManipulator(Map map)
            : base(map)
        {
            _rectanglePen = new Pen(Color.Yellow, 3);
            _helpTooltip = WinForms.CreateTooltip();
            _showHelpTooltip = true;

            SetInitialActiveRectangle();
        }

        private void SetInitialActiveRectangle()
        {
            Size mapSize = _map.Control.ClientRectangle.Size;
            int width = mapSize.Width / 3;
            int height = mapSize.Height / 3;
            _activeRectangleSize = new Size(width, height);
        }
        #endregion

        #region Methods
        protected internal override void SetFullControlManipulatorCore()
        {
            _wasShowingTooltip = _map.Manipulators.CurrentManipulator.ShowTooltip;
            _map.Manipulators.CurrentManipulator.ShowTooltip = false;
        }

        protected internal override void RemoveFullControlManipulatorCore()
        {
            _map.Manipulators.CurrentManipulator.ShowTooltip = _wasShowingTooltip;
        }

        public override void Paint(MapPaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_rectanglePen, _activeRectangle);
        }

        /// <summary>
        /// Make the ActiveRectangle move with the mouse.
        /// Display informative tooltip.
        /// </summary>
        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            if (_showHelpTooltip && _lastTooltipLocation != e.Location)
            {
                _lastTooltipLocation = e.Location;
                _helpTooltip.ToolTipTitle = HelpTitle;
                _helpTooltip.SetToolTip(_map.Control, HelpBody);
            }
            
            CalculateActiveRectanglePosition(e.Location.X, e.Location.Y);
            return true;
        }

        /// <summary>
        /// Grow or shrink the ActiveRectangle.
        /// Stop the Tooltip.
        /// </summary>
        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            const int step = 5;
            const int largeStep = 20;
            switch (e.KeyEventArgs.KeyData)
            {
                case Keys.Subtract | Keys.Control:
                    InflateActiveRectangle(-largeStep);
                    return true;

                case Keys.Add | Keys.Control:
                    InflateActiveRectangle(largeStep);
                    return true;

                case Keys.Add:
                    InflateActiveRectangle(step);
                    return true;

                case Keys.Subtract:
                    InflateActiveRectangle(-step);
                    return true;

                case Keys.S:
                    _showHelpTooltip = false;
                    _helpTooltip.Active = false;
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Save new Monitoring ActiveRectangle or remove FullControl
        /// </summary>
        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                string text = string.Format("Set this as the new area you want to keep updated about? (In Monitoring tab)");
                var saveActiveRectangle = MessageBox.Show(text, "Set Monitoring ActiveRectangle", MessageBoxButtons.YesNo);
                if (saveActiveRectangle == DialogResult.Yes)
                {
                    Point gameLocation = _map.Display.GetGameLocation(_activeRectangle.Location);
                    Point gameSize = _map.Display.GetGameLocation(_activeRectangle.Right, _activeRectangle.Bottom);

                    var worldRectangle = new Rectangle(gameLocation, new Size(gameSize.X - gameLocation.X, gameSize.Y - gameLocation.Y));
                    World.Default.Monitor.ActiveRectangle = worldRectangle;
                    World.Default.SaveSettings();
                }
            }
            _map.Manipulators.CurrentManipulator.RemoveFullControlManipulator();
            return true;
        }
        
        public override void Dispose()
        {
            _rectanglePen.Dispose();
            _helpTooltip.Dispose();
        }
        #endregion

        #region Private Implementation
        private void InflateActiveRectangle(int amount)
        {
            _activeRectangleSize.Width += amount * 2;
            _activeRectangleSize.Height += amount * 2;
            _activeRectangle.Inflate(amount, amount);
        }

        private void CalculateActiveRectanglePosition(int x, int y)
        {
            x -= _activeRectangleSize.Width / 2;
            y -= _activeRectangleSize.Height / 2;
            _activeRectangle = new Rectangle(x, y, _activeRectangleSize.Width, _activeRectangleSize.Height);
        }
        #endregion
    }
}