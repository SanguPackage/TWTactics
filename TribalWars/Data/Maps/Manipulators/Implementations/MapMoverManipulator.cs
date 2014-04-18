#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data.Villages;
using TribalWars.Data.Events;
using System.Drawing;
using TribalWars.Data.Maps.Manipulators.Helpers;
#endregion

namespace TribalWars.Data.Maps.Manipulators
{
    /// <summary>
    /// Allows the user to move the map by left/right clicking
    /// and by using the arrow keys
    /// </summary>
    internal class MapMoverManipulator : ManipulatorBase
    {
        #region Fields
        private bool _rightClickToMove;
        private bool _leftClickToMove;
        private bool _arrowsToMove;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value whether the map can be moved
        /// by right clicking somewhere on the 'ground'
        /// </summary>
        public bool RightClickToMove
        {
            get { return _rightClickToMove; }
            set { _rightClickToMove = value; }
        }

        /// <summary>
        /// Gets or sets a value whether the map can be moved
        /// by left clicking somewhere on the 'ground'
        /// </summary>
        public bool LeftClickToMove
        {
            get { return _leftClickToMove; }
            set { _leftClickToMove = value; }
        }

        /// <summary>
        /// Gets or sets a value whether the map can be moved
        /// by using the arrow keys
        /// </summary>
        public bool ArrowsToMove
        {
            get { return _arrowsToMove; }
            set { _arrowsToMove = value; }
        }
        #endregion

        #region Constructors
        public MapMoverManipulator(Map map)
            : this(map, false, false, true)
        {
            
        }

        public MapMoverManipulator(Map map, bool rightClick, bool leftClick, bool arrows)
            : base(map)
        {
            _rightClickToMove = rightClick;
            _leftClickToMove = leftClick;
            _arrowsToMove = arrows;
        }
        #endregion

        #region Public Methods
        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            if (_arrowsToMove)
            {
                int step = 5;
                int largeStep = 20;
                switch (e.KeyEventArgs.KeyData)
                {
                    case Keys.Left:
                    case Keys.NumPad4:
                        return MoveMap(-step, 0);

                    case Keys.Right:
                    case Keys.NumPad6:
                        return MoveMap(step, 0);

                    case Keys.Up:
                    case Keys.NumPad8:
                        return MoveMap(0, -step);

                    case Keys.Down:
                    case Keys.NumPad2:
                        return MoveMap(0, step);

                    case Keys.Left | Keys.Control:
                    case Keys.NumPad4 | Keys.Control:
                        return MoveMap(-largeStep, 0);

                    case Keys.Right | Keys.Control:
                    case Keys.NumPad6 | Keys.Control:
                        return MoveMap(largeStep, 0);

                    case Keys.Up | Keys.Control:
                    case Keys.NumPad8 | Keys.Control:
                        return MoveMap(0, -largeStep);

                    case Keys.Down | Keys.Control:
                    case Keys.NumPad2 | Keys.Control:
                        return MoveMap(0, largeStep);

                    case Keys.Home:
                        _map.SetCenter(_map.HomeLocation);
                        return true;

                    case Keys.Add:
                        _map.SetCenter(_map.Location.Zoom + 1);
                        return true;

                    case Keys.Subtract:
                        _map.SetCenter(_map.Location.Zoom - 1);
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Moves the center of the map
        /// </summary>
        internal protected override bool MouseDownCore(MapMouseEventArgs e)
        {
            if ((e.Village == null && e.MouseEventArgs.Button == MouseButtons.Right && _rightClickToMove) || (e.MouseEventArgs.Button == MouseButtons.Left && _leftClickToMove))
            {
                Point game = _map.Display.GetGameLocation(e.MouseEventArgs.X, e.MouseEventArgs.Y);
                _map.SetCenter(game.X, game.Y);
                return true;
            }
            return false;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Moves the map by x and y
        /// </summary>
        private bool MoveMap(int x, int y)
        {
            _map.SetCenter(_map.Location.X + x, _map.Location.Y + y);
            return true;
        }
        #endregion
    }
}
