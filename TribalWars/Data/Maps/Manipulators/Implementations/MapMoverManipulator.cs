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
    /// Allows the user to move the map by left/right clicking
    /// and by using the arrow keys
    /// </summary>
    internal class MapMoverManipulator : ManipulatorBase
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value whether the map can be moved
        /// by right clicking somewhere on the 'ground'
        /// </summary>
        public bool RightClickToMove { get; set; }

        /// <summary>
        /// Gets or sets a value whether the map can be moved
        /// by left clicking somewhere on the 'ground'
        /// </summary>
        public bool LeftClickToMove { get; set; }

        /// <summary>
        /// Gets or sets a value whether the map can be moved
        /// by using the arrow keys
        /// </summary>
        public bool ArrowsToMove { get; set; }
        #endregion

        #region Constructors
        public MapMoverManipulator(Map map)
            : this(map, false, false, true)
        {
        }

        public MapMoverManipulator(Map map, bool rightClick, bool leftClick, bool arrows)
            : base(map)
        {
            RightClickToMove = rightClick;
            LeftClickToMove = leftClick;
            ArrowsToMove = arrows;
        }
        #endregion

        #region Public Methods
        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            if (ArrowsToMove)
            {
                const int step = 5;
                const int largeStep = 20;
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
        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            // TODO: looks like this if is never true. And that this is implemented by MapDraggerManipulator
            if ((e.Village == null && e.MouseEventArgs.Button == MouseButtons.Right && RightClickToMove) ||
                (e.MouseEventArgs.Button == MouseButtons.Left && LeftClickToMove))
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