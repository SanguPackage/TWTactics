#region Using
using System;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Maps.Manipulators;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Maps.Manipulators.Implementations;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Monitoring
{
    /// <summary>
    /// Sets the ActiveRectangle for the Monitoring tab
    /// </summary>
    internal class ActiveRectangleManipulator : ManipulatorBase
    {
        #region Fields
        private Size _activeRectangleSize;
        private Rectangle _activeRectangle;
        private readonly Action<bool> _releaseAction;
        private bool _hasSetNew;
        #endregion

        #region Tooltip Fields
        private bool _wasShowingTooltip;

        private bool _showHelpTooltip;
        private Point _lastTooltipLocation;
        private const string HelpTitle = "Monitoring ActiveRectangle";
        private const string HelpBody = 
                @"Press +, - and the arrow keys to 
grow and shrink the area.

Left click to save the area.
Right click to cancel.

Press 's' to remove this tooltip.";
        #endregion

        #region Constructors
        public ActiveRectangleManipulator(Map map, Action<bool> releaseAction)
            : base(map)
        {
            _showHelpTooltip = true;
            _releaseAction = releaseAction;

            SetInitialActiveRectangle();
        }

        private void SetInitialActiveRectangle()
        {
            Size mapSize = _map.CanvasSize;
            int width = mapSize.Width / 3;
            int height = mapSize.Height / 3;
            _activeRectangleSize = new Size(width, height);
        }
        #endregion

        #region Methods
        protected internal override void SetFullControlManipulatorCore()
        {
            _wasShowingTooltip = _map.Manipulators.CurrentManipulator.TooltipActive;
            _map.Manipulators.CurrentManipulator.TooltipActive = false;
        }

        protected internal override void RemoveFullControlManipulatorCore()
        {
            _map.Manipulators.CurrentManipulator.TooltipActive = _wasShowingTooltip;

            if (_releaseAction != null)
                _releaseAction(_hasSetNew);
        }

        protected internal override void CleanUp()
        {
        }

        public override void Paint(MapPaintEventArgs e, bool isActiveManipulator)
        {
            using (var rectanglePen = new Pen(Color.FromArgb(118, 143, 219), 3))
            {
                e.Graphics.DrawRectangle(rectanglePen, _activeRectangle);
            }
        }

        /// <summary>
        /// Save new Monitoring ActiveRectangle or remove FullControl
        /// </summary>
        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                string text = string.Format("Set this as the new area you want to keep updated about? (In Monitoring tab)");
                var saveActiveRectangle = MessageBox.Show(text, "Set Monitoring ActiveRectangle", MessageBoxButtons.YesNoCancel);
                if (saveActiveRectangle == DialogResult.Cancel)
                {
                    return false;
                }

                if (saveActiveRectangle == DialogResult.Yes)
                {
                    var game = _map.Display.GetGameRectangle(_activeRectangle);
                    World.Default.Monitor.ActiveRectangle = game;
                    World.Default.SaveSettings();

                    _hasSetNew = true;
                }
            }
            _map.Manipulators.CurrentManipulator.RemoveFullControlManipulator();
            return true;
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
                _map.ShowTooltip(HelpTitle, HelpBody);
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
            Point? keyMove = new KeyboardInputToMovementConverter(e.KeyEventArgs.KeyData).GetKeyMove();
            if (keyMove.HasValue)
            {
                _activeRectangle.Width += keyMove.Value.X;
                _activeRectangleSize.Width += keyMove.Value.X;
                _activeRectangleSize.Height -= keyMove.Value.Y;
                _activeRectangle.Height -= keyMove.Value.Y;
                return true;
            }
            else
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
                        return true;
                }
            }
            return false;
        }
        
        public override void Dispose()
        {
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