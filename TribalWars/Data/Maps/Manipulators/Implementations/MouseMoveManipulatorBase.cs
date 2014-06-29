#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using TribalWars.Data.Maps.Displays;
using TribalWars.Data.Maps.Manipulators.Helpers;
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Data.Maps.Manipulators.Managers;
#endregion

namespace TribalWars.Data.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Base class for mousemove gestures implemantations (Drawing, ...)
    /// </summary>
    internal abstract class MouseMoveManipulatorBase : ManipulatorBase
    {
        #region Fields
        private readonly DefaultManipulatorManager _parent;
        private int _nextId = 1;
        private Polygon _activePolygon;
        private List<Polygon> _collection = new List<Polygon>();
        private Point _lastAddedMapLocation;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the managing manipulator
        /// </summary>
        /// <remarks>
        /// So this manipulator can be promoted as
        /// the full control manipulator
        /// </remarks>
        private DefaultManipulatorManager ManipulationManager
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the currently selected polygon
        /// </summary>
        public Polygon ActivePolygon
        {
            get { return _activePolygon; }
            private set
            {
                _activePolygon = value;
                if (value == null)
                {
                    _parent.RemoveFullControlManipulator();
                }
            }
        }

        /// <summary>
        /// Gets all defined polygons
        /// </summary>
        public List<Polygon> Polygons
        {
            get { return _collection; }
        }
        #endregion

        #region Constructors
        protected MouseMoveManipulatorBase(Map map, DefaultManipulatorManager parent)
            : base(map)
        {
            _parent = parent;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Start creation of a new polygon
        /// </summary>
        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                if (_activePolygon == null || !_activePolygon.Drawing)
                {
                    // Start a new polygon
                    _lastAddedMapLocation = new Point(e.MouseEventArgs.X, e.MouseEventArgs.Y);

                    _activePolygon = new Polygon(_nextId.ToString(CultureInfo.InvariantCulture), _lastAddedMapLocation.X, _lastAddedMapLocation.Y);
                    _collection.Add(_activePolygon);
                    _parent.SetFullControlManipulator(this);
                    Start(_activePolygon);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return false if you don't want to add a new point to the polygon
        /// </summary>
        protected abstract bool AddPointPolygon(Point lastMap, Point currentMap);

        /// <summary>
        /// Stop polygon creation. Display contextmenu.
        /// </summary>
        protected internal override bool MouseUpCore(MapMouseEventArgs e)
        {
            int x = e.MouseEventArgs.X;
            int y = e.MouseEventArgs.Y;
            MouseButtons button = e.MouseEventArgs.Button;
            if (button == MouseButtons.Left && _activePolygon != null && _activePolygon.Drawing)
            {
                if (_activePolygon.List.Count > 2)
                {
                    // Polygon completed
                    _activePolygon.Stop(x, y);
                    _nextId++;
                    Stop(_activePolygon);
                }
                else
                {
                    // Too small area to be a polygon
                    // try to select an existing one instead
                    Delete(_activePolygon);

                    _activePolygon = Select(x, y);
                    if (_activePolygon == null)
                    {
                        _parent.RemoveFullControlManipulator();
                    }
                    else
                    {
                        _parent.SetFullControlManipulator(this);
                    }

                }
                return true;
            }
            /*else
            {
                // Select polygon & show contextmenu
                _activePolygon = Select(x, y);
                if (button == MouseButtons.Right && ContextMenu != null)
                {
                    ShowContextMenu(x, y);
                    return true;
                }
            }*/
            return false;
        }

        /// <summary>
        /// Add points to the polygon
        /// </summary>
        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.Left)
            {
                var currentMap = new Point(e.MouseEventArgs.X, e.MouseEventArgs.Y);
                if (Control.ModifierKeys.HasFlag(Keys.Control))
                {
                    currentMap.X = _lastAddedMapLocation.X;
                }
                if (Control.ModifierKeys.HasFlag(Keys.Shift))
                {
                    currentMap.Y = _lastAddedMapLocation.Y;
                }

                if (_activePolygon != null && _activePolygon.Drawing && AddPointPolygon(_lastAddedMapLocation, currentMap))
                {
                    // Add extra point to the polygon
                    _activePolygon.Add(currentMap.X, currentMap.Y);
                    _lastAddedMapLocation = currentMap;
                    Continue(_activePolygon);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Polygon started
        /// </summary>
        protected virtual void Start(Polygon polygon)
        {
        }

        /// <summary>
        /// Point added to the polygon
        /// </summary>
        protected virtual void Continue(Polygon polygon)
        {
        }

        /// <summary>
        /// Polygon finished
        /// </summary>
        protected virtual void Stop(Polygon polygon)
        {
        }

        /// <summary>
        /// Deletes all polygons
        /// </summary>
        public void Clear()
        {
            _collection = new List<Polygon>();
            ActivePolygon = null;
            _nextId = 1;
            _map.Control.Invalidate();
        }

        /// <summary>
        /// Hides/Shows the polygons
        /// </summary>
        public void ToggleVisibility(bool visible)
        {
            if (_collection.Count > 0)
            {
                foreach (Polygon poly in _collection)
                {
                    poly.Visible = visible;
                }
                OnVisibilityChanged(false, visible);
                _map.Control.Invalidate();
            }
        }

        /// <summary>
        /// Toggles the visibility of the active polygon
        /// </summary>
        public void ToggleVisibility()
        {
            if (ActivePolygon != null)
            {
                ActivePolygon.Visible = !ActivePolygon.Visible;
                OnVisibilityChanged(true, ActivePolygon.Visible);
                _map.Control.Invalidate();
            }
        }

        /// <summary>
        /// Deletes the active polygon
        /// </summary>
        public void Delete()
        {
            Delete(ActivePolygon);
        }

        /// <summary>
        /// Deletes a polygon
        /// </summary>
        public void Delete(Polygon poly)
        {
            if (poly != null)
            {
                Polygons.Remove(poly);
                if (poly == ActivePolygon)
                {
                    ActivePolygon = null;
                }
                _map.Control.Invalidate();
            }
        }
        #endregion

        #region Private Implementation
        /// <summary>
        /// Returns the first polygon that contains the point
        /// </summary>
        private Polygon Select(int x, int y)
        {
            foreach (Polygon poly in _collection)
            {
                if (poly.IsHitIn(x, y))
                {
                    return poly;
                }
            }
            return null;


            /*var polys = from poly in _collection
                        where poly.IsHitIn(x, y) && poly.Visible
                        select poly;

            if (!polys.Any())
            {
                _currentSelectedPolygon = null;
                return null;
            }

            Polygon selectedPoly = null;
            if (polys.Count() == 1)
                selectedPoly = polys.Single();

            else if (_currentSelectedPolygon == null || !polys.Contains(_currentSelectedPolygon) || polys.Last() == _currentSelectedPolygon)
                selectedPoly = polys.First();

            else
            {
                polys.SkipWhile((Polygon poly) => !poly.Equals(_currentSelectedPolygon));
                selectedPoly = polys.First();
            }

            _currentSelectedPolygon = selectedPoly;
            return selectedPoly;*/
        }

        /// <summary>
        /// When the visibility of the polygons changes
        /// </summary>
        protected virtual void OnVisibilityChanged(bool activeOnly, bool visible)
        {
        }
        #endregion
    }
}