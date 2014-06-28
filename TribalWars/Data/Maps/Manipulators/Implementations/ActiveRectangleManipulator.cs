#region Using
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;

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

        #region Constructors
        public ActiveRectangleManipulator(Map map)
            : base(map)
        {
            _rectanglePen = new Pen(Color.Yellow, 3);

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

        #region Public Methods
        public override void Paint(MapPaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_rectanglePen, _activeRectangle);

            // Tooltip :)
        }

        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            // Make the ActiveRectangle move with the mouse
            CalculateActiveRectanglePosition(e.Location.X, e.Location.Y);
            return true;
        }

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
            }
            return false;
        }

        private void InflateActiveRectangle(int amount)
        {
            _activeRectangleSize.Width += amount * 2;
            _activeRectangleSize.Height += amount * 2;
            _activeRectangle.Inflate(amount, amount);
        }

        /// <summary>
        /// Moves the center of the map
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


                    //Rectangle mapGameRectangle = _map.Display.GetGameRectangle(_map.Control.ClientRectangle);
                    //Point leftTop = _map.Display.GetMapLocation(mapGameRectangle.X, mapGameRectangle.Y);
                    //Point rightBottom = _map.Display.GetMapLocation(mapGameRectangle.Right, mapGameRectangle.Bottom);
                    //_mainMapRectangle = new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);


                    World.Default.Monitor.ActiveRectangle = worldRectangle;
                    World.Default.SaveSettings();
                }
            }
            _map.Manipulators.CurrentManipulator.RemoveFullControlManipulator();
            return true;
        }
        #endregion

        #region Private Methods
        private void CalculateActiveRectanglePosition(int x, int y)
        {
            x -= _activeRectangleSize.Width / 2;
            y -= _activeRectangleSize.Height / 2;
            _activeRectangle = new Rectangle(x, y, _activeRectangleSize.Width, _activeRectangleSize.Height);
        }
        #endregion
    }
}