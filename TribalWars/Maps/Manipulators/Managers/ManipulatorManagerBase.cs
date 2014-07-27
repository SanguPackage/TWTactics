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
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Manipulators.Managers
{
    /// <summary>
    /// The base class for a manipulator manager
    /// </summary>
    public abstract class ManipulatorManagerBase : ManipulatorBase
    {
        #region Fields
        protected readonly List<ManipulatorBase> _manipulators;
        protected ManipulatorBase _fullControllManipulator;
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
            : base(map)
        {
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
                _map.ShowTooltip(village);
            }
        }
        #endregion

        #region Event Handlers
        public void Initialize()
        {
            _map.SetCursor();
            _map.Invalidate();
        }

        /// <summary>
        /// Cleanup before reinitializing
        /// </summary>
        protected internal override void CleanUp()
        {
            foreach (var manipulator in _manipulators)
            {
                manipulator.CleanUp();
            }
        }
        #endregion

        #region IMapManipulator Members
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

        /// <summary>
        /// Informs all the manipulators the user
        /// has clicked the map
        /// </summary>
        protected internal override bool MouseDownCore(MapMouseEventArgs e)
        {
            if (e.MouseEventArgs.Button == MouseButtons.XButton1)
            {
                // TODO: back
            }
            if (e.MouseEventArgs.Button == MouseButtons.XButton2)
            {
                // TODO: forward
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

        protected internal override bool MouseLeave()
        {
            return _manipulators.Aggregate(false, (redraw, m) => redraw | m.MouseLeave());
        }

        protected internal override bool MouseUpCore(MapMouseEventArgs e)
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

        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
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

        protected internal override bool OnVillageDoubleClickCore(MapVillageEventArgs e)
        {
            if (_fullControllManipulator != null)
            {
                // TODO: this isHandled is actually "mustRedraw"
                //       these should return void and have EventArgs.Redraw and .Handled props
                var isHandled = _fullControllManipulator.OnVillageDoubleClickCore(e);
                if (isHandled)
                    return true;
            }

            bool redraw = false;
            foreach (ManipulatorBase m in _manipulators) redraw |= m.OnVillageDoubleClickCore(e);
            return redraw;
        }

        protected internal override bool OnVillageClickCore(MapVillageEventArgs e)
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

        public bool MouseWheel(MouseEventArgs e)
        {
            // TODO we zaten hier
            //from d in mouseDowns.Timestamp()
            //from p in pointChanges
            //    .TakeUntil(mouseUps)
            //    .SkipUntil(Observable.Timer(d.Timestamp + TimeSpan.FromSeconds(1.0)))
            //select p;


            _map.IncreaseZoomLevel(e.Delta > 0 ? 1 : -1);
            return true;
        }

        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
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
                    // TODO: return to previous location
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

        protected internal override bool OnKeyUpCore(MapKeyEventArgs e)
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
        public override void Paint(MapPaintEventArgs e)
        {
            if (_fullControllManipulator != null && !_manipulators.Contains(_fullControllManipulator))
            {
                _fullControllManipulator.Paint(e);
            }

            foreach (ManipulatorBase manipulator in _manipulators)
                manipulator.Paint(e);
        }

        public override void TimerPaint(MapTimerPaintEventArgs e)
        {
            if (_fullControllManipulator != null && !_manipulators.Contains(_fullControllManipulator))
            {
                _fullControllManipulator.TimerPaint(e);
            }

            foreach (ManipulatorBase manipulator in _manipulators)
                manipulator.TimerPaint(e);
        }

        public override void Dispose()
        {
            _manipulators.ForEach(m => m.Dispose());
            _manipulators.Clear();
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
        #endregion
    }
}