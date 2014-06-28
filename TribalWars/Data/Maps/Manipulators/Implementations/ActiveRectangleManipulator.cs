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
    /// 
    /// </summary>
    internal class ActiveRectangleManipulator : ManipulatorBase
    {
        #region Fields
        private readonly Pen _rectanglePen;
        private Size _activeRectangleSize;
        private Rectangle _activeRectangle;
        private bool _inflating;
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

            Debug.WriteLine("Paint: " + _activeRectangle + " Size: " + _activeRectangleSize);
            e.Graphics.DrawRectangle(_rectanglePen, _activeRectangle);
        }

        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            if (!_inflating)
            {
                // Make the ActiveRectangle move with the mouse
                CalculateActiveRectanglePosition(e.Location.X, e.Location.Y);
                Debug.WriteLine("MouseMove: " + _activeRectangle + " Size: " + _activeRectangleSize);
                return true;
            }
            return false;
        }

        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            // Need to check some boundaries for inflates...

            const int step = 5;
            const int largeStep = 20;
            switch (e.KeyEventArgs.KeyData)
            {
                case Keys.Subtract | Keys.Control:
                    Inflate(-largeStep);
                    return true;

                case Keys.Add | Keys.Control:
                    Inflate(largeStep);
                    return true;

                case Keys.Add:
                    Inflate(step);
                    return true;

                case Keys.Subtract:
                    Inflate(-step);
                    return true;
            }
            return false;
        }

        private void Inflate(int amount)
        {
            _inflating = true;
            _activeRectangleSize.Width += amount;
            _activeRectangleSize.Height += amount;
            _activeRectangle.Inflate(amount, amount);
            Debug.WriteLine("Inflate: " + _activeRectangle + " Size: " + _activeRectangleSize);
            _inflating = false;
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