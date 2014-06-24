#region Using
using System;
using System.Diagnostics;
using System.Drawing;
using TribalWars.Data.Maps.Manipulators.Helpers;
using TribalWars.Data.Maps.Manipulators.Managers;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Allows the user to move the map around by dragging
    /// </summary>
    internal class MapDraggerManipulator : MouseMoveManipulatorBase
    {
        #region Constructors
        public MapDraggerManipulator(Map map, DefaultManipulatorManager parentManipulatorHandler, int polygonOffset)
            : base(map, parentManipulatorHandler, polygonOffset, false)
        {
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Polygon started
        /// </summary>
        protected override void Start(Polygon polygon)
        {
        }

        /// <summary>
        /// Point added to the polygon
        /// </summary>
        protected override void Continue(Polygon polygon)
        {
            Location current = _map.Location;
            Point first = polygon.List.First.Value;
            Point last = polygon.List.Last.Value;

            int x = first.X - last.X;
            int y = first.Y - last.Y;

            var game = new Location(current.X + x, current.Y + y, current.Zoom);
            var dist = Math.Pow(game.X - _map.Location.X, 2) + Math.Pow(game.Y - _map.Location.Y, 2);
            if (dist > 1)
            {
                _map.SetCenter(new Location(current.X + x, current.Y + y, current.Zoom));
            }
        }

        /// <summary>
        /// Polygon finished
        /// </summary>
        protected override void Stop(Polygon polygon)
        {
            Clear();
        }

        protected internal override void SetFullControlManipulatorCore()
        {
            _map.SetCursor(System.Windows.Forms.Cursors.Hand);
        }

        protected internal override void RemoveFullControlManipulatorCore()
        {
            _map.SetCursor();
        }
        #endregion
    }
}