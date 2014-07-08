#region Using
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Allows the user to move the map around by dragging
    /// </summary>
    public class MapDraggerManipulator : ManipulatorBase
    {
        #region Fields
        private readonly DefaultManipulatorManager _parent;

        private bool _isDragging;
        private Point _startPosition;
        private Point _lastPosition;
        #endregion

        #region Constructors
        public MapDraggerManipulator(Map map, DefaultManipulatorManager parent)
            : base(map)
        {
            _parent = parent;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Start dragging
        /// </summary>
        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _startPosition = CreateGamePoint(e.MouseEventArgs.Location);
                _lastPosition = _startPosition;
                _parent.SetFullControlManipulator(this);
            }
            return false;
        }

        /// <summary>
        /// Stop dragging
        /// </summary>
        protected internal override bool MouseUpCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left && _isDragging)
            {
                _isDragging = false;
                _parent.RemoveFullControlManipulator();
            }
            return false;
        }

        /// <summary>
        /// Add points to the polygon
        /// </summary>
        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left && _isDragging)
            {
                Point currentMap = CreateGamePoint(e.MouseEventArgs.Location);
                if (_lastPosition != currentMap)
                {
                    _lastPosition = currentMap;
                    SetMapCenter();
                }
            }
            return false;
        }

        private void SetMapCenter()
        {
            Location current = _map.Location;

            int x = _startPosition.X - _lastPosition.X;
            int y = _startPosition.Y - _lastPosition.Y;

            _map.SetCenter(new Location(current.X + x, current.Y + y, current.Zoom));
        }

        protected internal override void SetFullControlManipulatorCore()
        {
            _map.SetCursor(Cursors.Hand);
        }

        protected internal override void RemoveFullControlManipulatorCore()
        {
            _map.SetCursor();
        }

        /// <summary>
        /// Cleanup anything when switching worlds or settings
        /// </summary>
        protected internal override void CleanUp()
        {
        }

        private static Point CreateGamePoint(Point loc)
        {
            return World.Default.Map.Display.GetGameLocation(loc);
        }
        #endregion
    }
}