#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Janus.Windows.Common;
using TribalWars.Controls;
using TribalWars.Maps.Icons;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Villages;
using TribalWars.Villages.ContextMenu;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Manipulators.Managers
{
    /// <summary>
    /// The base class for a manipulator manager
    /// </summary>
    public abstract class ManipulatorManagerBase : IContextMenuProvider, IDisposable
    {
        #region Fields
        private readonly Map _map;
        private readonly List<ManipulatorBase> _manipulators;
        private ManipulatorBase _fullControllManipulator;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether a tooltip should
        /// show up when hovering over a village
        /// </summary>
        public bool TooltipActive { get; set; }

        /// <summary>
        /// Old manipulators use XmlWriter/Reader which has its perks.
        /// For new development, it will be easier to turn this property
        /// off and use XDocument/Linq for persistence
        /// </summary>
        public bool UseLegacyXmlWriter { get; protected set; }
        #endregion

        #region Constructors
        protected ManipulatorManagerBase(Map map, bool showTooltip)
        {
            _map = map;
            UseLegacyXmlWriter = true;
            _manipulators = new List<ManipulatorBase>();
            TooltipActive = showTooltip;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gives this manipulator full control of the map, the other manipulators
        /// are the completely ignored
        /// </summary>
        public void SetFullControlManipulator(ManipulatorBase manipulator)
        {
            if (!ReferenceEquals(_fullControllManipulator, manipulator))
            {
                RemoveFullControlManipulator();
                _fullControllManipulator = manipulator;
                _fullControllManipulator.SetFullControlManipulatorCore();
                _map.GiveFocus();
            }
        }

        /// <summary>
        /// Gives each manipulator the ability to respond to map events again
        /// </summary>
        public void RemoveFullControlManipulator()
        {
            if (_fullControllManipulator != null)
            {
                _fullControllManipulator.RemoveFullControlManipulatorCore();
                _fullControllManipulator = null;
            }
        }

        public void ShowTooltip(Village village)
        {
            if (TooltipActive)
            {
                _map.ShowTooltip(BuildTooltip(village));
            }
        }

        protected virtual SuperTipSettings BuildTooltip(Village village)
        {
            var settings = new SuperTipSettings();
            settings.ToolTipStyle = ToolTipStyle.Standard;
            settings.HeaderText = village.Tooltip.Title;
            settings.Text = village.Tooltip.Text;
            settings.Image = village.Type.GetImage(false);

            if (!string.IsNullOrEmpty(village.Tooltip.Footer))
            {
                settings.FooterText = village.Tooltip.Footer;
                settings.FooterImage = village.Tooltip.FooterImage;
            }
            return settings;
        }

        public void AddManipulator(ManipulatorBase manipulator)
        {
            _manipulators.Add(manipulator);
        }

        public void RemoveManipulator(ManipulatorBase manipulator)
        {
            _manipulators.Remove(manipulator);
        }

        public void Initialize()
        {
            _map.SetCursor();
            _map.Invalidate();
        }

        /// <summary>
        /// Cleanup before reinitializing
        /// </summary>
        public void CleanUp()
        {
            foreach (var manipulator in _manipulators)
            {
                manipulator.CleanUp();
            }
        }

        public void Dispose()
        {
            _manipulators.ForEach(m => m.Dispose());
            _manipulators.Clear();
        }
        #endregion

        #region Context Menu
        public virtual IContextMenu GetContextMenu(Point location, Village village)
        {
            return ContextMenuProvider.DefaultProvider(_map, location, village);
        }

        public void ShowContextMenu(Point location, Village village)
        {
            IContextMenu contextMenu;
            if (_fullControllManipulator != null)
            {
                contextMenu = _fullControllManipulator.GetContextMenu(location, village);
            }
            else
            {
                contextMenu = GetContextMenu(location, village);
            }
            
            if (contextMenu != null)
            {
                _map.ShowContextMenu(contextMenu, location);
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Informs all the manipulators the user
        /// has clicked the map
        /// </summary>
        public bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.XButton1)
            {
                
            }
            if (e.MouseEventArgs.Button == MouseButtons.XButton2)
            {
                
            }

            if (e.MouseEventArgs.Button == MouseButtons.Middle)
            {
                _map.Manipulators.SwitchManipulator();
                return true;
            }

            if (_fullControllManipulator != null)
            {
                return _fullControllManipulator.MouseDownCore(e);
            }
            else
            {
                bool redraw = false;
                foreach (ManipulatorBase m in _manipulators) redraw |= m.MouseDownCore(e);
                return redraw;
            }
        }

        public bool MouseLeave()
        {
            return _manipulators.Aggregate(false, (redraw, m) => redraw | m.MouseLeave());
        }

        public bool MouseUpCore(MapMouseEventArgs e)
        {
            if (_fullControllManipulator != null)
            {
                return _fullControllManipulator.MouseUpCore(e);
            }
            else
            {
                bool redraw = false;
                foreach (ManipulatorBase m in _manipulators) redraw |= m.MouseUpCore(e);
                return redraw;
            }
        }

        public bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            if (_fullControllManipulator != null)
            {
                return _fullControllManipulator.MouseMoveCore(e);
            }
            else
            {
                bool redraw = false;
                foreach (ManipulatorBase m in _manipulators) redraw |= m.MouseMoveCore(e);
                return redraw;
            }
        }

        public bool OnVillageDoubleClickCore(MapVillageEventArgs e)
        {
            if (_fullControllManipulator != null)
            {
                var mustRedraw = _fullControllManipulator.OnVillageDoubleClickCore(e);
                if (mustRedraw)
                    return true;
            }

            bool redraw = false;
            foreach (ManipulatorBase m in _manipulators) redraw |= m.OnVillageDoubleClickCore(e);
            return redraw;
        }

        public bool OnVillageClickCore(MapVillageEventArgs e)
        {
            if (_fullControllManipulator != null)
            {
                return _fullControllManipulator.OnVillageClickCore(e);
            }
            else
            {
                bool redraw = false;
                foreach (ManipulatorBase m in _manipulators) redraw |= m.OnVillageClickCore(e);
                return redraw;
            }
        }

        #region MouseWheel
        private MouseWheelInfo _lastMouseWheel;

        /// <summary>
        /// Zoom in fast with mousewheel: 
        /// remember at what game location
        /// zooming started
        /// </summary>
        private class MouseWheelInfo
        {
            private readonly DateTime _occuredAt;

            public Point GameLocation { get; private set; }

            public MouseWheelInfo(Point gameLocation)
            {
                GameLocation = gameLocation;
                _occuredAt = DateTime.Now;
            }

            /// <summary>
            /// If it's too long ago that the previous scroll happened
            /// don't take its gameLocation into account anymore
            /// </summary>
            public bool IsRelevant()
            {
                return DateTime.Now - _occuredAt < new TimeSpan(0, 0, 1);
            }

            /// <summary>
            /// Convert MouseWheel delta to a 1 increase/decrease of zoom level
            /// </summary>
            public static int ToTilt(int delta)
            {
                return delta > 0 ? 1 : -1;
            }

            public override string ToString()
            {
                return string.Format("At={0}, Loc={1}, StillRelevant={2}", _occuredAt, GameLocation, IsRelevant());
            }
        }

        public bool MouseWheel(MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                // Zoom out (don't move map center)
                _map.IncreaseZoomLevel(MouseWheelInfo.ToTilt(e.Delta));
            }
            else
            {
                // Zoom in
                Point location;
                if (_lastMouseWheel != null && _lastMouseWheel.IsRelevant())
                {
                    location = _lastMouseWheel.GameLocation;
                }
                else
                {
                    location = _map.Display.GetGameLocation(e.Location);
                }
                _lastMouseWheel = new MouseWheelInfo(location);

                _map.IncreaseZoomLevel(location, MouseWheelInfo.ToTilt(e.Delta));
            }
            
            return true;
        }
        #endregion

        public bool OnKeyDownCore(MapKeyEventArgs e)
        {
            switch (e.KeyEventArgs.KeyCode)
            {
                case Keys.T:
                    TooltipActive = !TooltipActive;
                    return false;

                case Keys.D:
                    if (e.KeyEventArgs.Modifiers.HasFlag(Keys.Alt))
                    {
                        _map.SwitchDisplay();
                    }
                    return false;

                case Keys.Back:
                    break;
            }

            if (_fullControllManipulator != null)
            {
                return _fullControllManipulator.OnKeyDownCore(e);
            }
            else
            {
                bool redraw = false;
                foreach (ManipulatorBase m in _manipulators) redraw |= m.OnKeyDownCore(e);
                return redraw;
            }
        }

        public bool OnKeyUpCore(MapKeyEventArgs e)
        {
            if (_fullControllManipulator != null)
            {
                return _fullControllManipulator.OnKeyUpCore(e);
            }
            else
            {
                bool redraw = false;
                foreach (ManipulatorBase m in _manipulators) redraw |= m.OnKeyUpCore(e);
                return redraw;
            }
        }
        #endregion

        #region IMapDrawer Members
        public void Paint(MapPaintEventArgs e, bool isActiveManipulator)
        {
            if (_fullControllManipulator != null && !_manipulators.Contains(_fullControllManipulator))
            {
                _fullControllManipulator.Paint(e, isActiveManipulator);
            }

            foreach (ManipulatorBase manipulator in _manipulators)
                manipulator.Paint(e, isActiveManipulator);
        }

        public void TimerPaint(MapTimerPaintEventArgs e)
        {
            if (_fullControllManipulator != null && !_manipulators.Contains(_fullControllManipulator))
            {
                _fullControllManipulator.TimerPaint(e);
            }

            foreach (ManipulatorBase manipulator in _manipulators)
                manipulator.TimerPaint(e);
        }
        #endregion

        #region Persistence
        /// <summary>
        /// The result will be Raw written to the XmlWriter
        /// </summary>
        public string WriteXml()
        {
            Debug.Assert(!UseLegacyXmlWriter);
            return WriteXmlCore();
        }

        /// <summary>
        /// The result will be Raw written to the XmlWriter
        /// </summary>
        protected virtual string WriteXmlCore()
        {
            return "";
        }

        /// <summary>
        /// The doc is the ENTIRE settings file
        /// </summary>
        public void ReadXml(XDocument doc)
        {
            Debug.Assert(!UseLegacyXmlWriter);
            ReadXmlCore(doc);
        }

        /// <summary>
        /// The doc is the ENTIRE settings file
        /// </summary>
        protected virtual void ReadXmlCore(XDocument doc)
        {
        }

        /// <summary>
        /// LEGACY: Saves state to stream
        /// </summary>
        public void WriteXml(XmlWriter w)
        {
            Debug.Assert(UseLegacyXmlWriter);
            WriteXmlCore(w);
        }

        /// <summary>
        /// LEGACY: Loads state from stream
        /// </summary>
        public void ReadXml(XmlReader r)
        {
            Debug.Assert(UseLegacyXmlWriter);
            if (r.IsEmptyElement)
            {
                r.Read();
            }
            else
            {
                r.ReadStartElement();
                ReadXmlCore(r);
                r.ReadEndElement();
            }
        }

        protected virtual void ReadXmlCore(XmlReader r)
        {
            
        }

        protected virtual void WriteXmlCore(XmlWriter w)
        {
            
        }
        #endregion
    }
}