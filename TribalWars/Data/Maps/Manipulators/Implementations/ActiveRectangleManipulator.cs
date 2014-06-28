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
            //Rectangle mapGameRectangle = _map.Display.GetGameRectangle(_map.Control.ClientRectangle);
            //Point leftTop = _map.Display.GetMapLocation(mapGameRectangle.X, mapGameRectangle.Y);
            //Point rightBottom = _map.Display.GetMapLocation(mapGameRectangle.Right, mapGameRectangle.Bottom);
            //_mainMapRectangle = new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);

            e.Graphics.DrawRectangle(_rectanglePen, _activeRectangle);

            
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
            //// TODO: looks like this if is never true. And that this is implemented by MapDraggerManipulator
            //if ((e.Village == null && e.MouseEventArgs.Button == MouseButtons.Right && RightClickToMove) ||
            //    (e.MouseEventArgs.Button == MouseButtons.Left && LeftClickToMove))
            //{
            //    Point game = _map.Display.GetGameLocation(e.MouseEventArgs.X, e.MouseEventArgs.Y);
            //    _map.SetCenter(game.X, game.Y);
            //    return true;
            //}
            return false;
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