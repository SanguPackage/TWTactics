#region Using
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Maps.Manipulators.EventArg;
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
        private readonly ManipulatorManagerBase _parent;

        private bool _isDragging;
        private Point _startPosition;
        private Point _lastPosition;

        /// <summary>
        /// Showing a tooltip reverted the cursor.
        /// This is not a complete fix: if the user doesn't move 
        /// before starting to drag, the default pointer is
        /// shown during drag due the tooltip still popping up.
        /// </summary>
        private bool _wasTooltipActive;
        #endregion

        #region Constructors
        public MapDraggerManipulator(Map map, ManipulatorManagerBase parent)
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

        private void SetMapCenter()
        {
            Location current = _map.Location;

            int x = _startPosition.X - _lastPosition.X;
            int y = _startPosition.Y - _lastPosition.Y;

            _map.SetCenter(new Location(current.Display, current.X + x, current.Y + y, current.Zoom));
        }

        protected internal override void SetFullControlManipulatorCore()
        {
            _wasTooltipActive = _map.Manipulators.CurrentManipulator.TooltipActive;
            _map.Manipulators.CurrentManipulator.TooltipActive = false;
            _map.SetCursor(Cursors.SizeAll);
        }

        protected internal override void RemoveFullControlManipulatorCore()
        {
            _map.Manipulators.CurrentManipulator.TooltipActive = _wasTooltipActive;
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