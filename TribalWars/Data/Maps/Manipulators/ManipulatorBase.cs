#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Data.Villages;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Controls.Maps;
using System.Xml;
using TribalWars.Data.Maps.Manipulators.Helpers;
#endregion

namespace TribalWars.Data.Maps.Manipulators
{
    /// <summary>
    /// Base class for a simple manipulator implementation (MapMover, ...)
    /// but also for the manipulator managers (DefaultManipulator, ...)
    /// </summary>
    public class ManipulatorBase
    {
        #region Fields
        protected Map _map;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ManipulatorBase(Map map)
        {
            _map = map;
        }
        #endregion

        #region IManipulator Members
        internal protected virtual bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            return false;
        }

        internal protected virtual bool OnVillageClickCore(MapVillageEventArgs e)
        {
            return false;
        }

        internal protected virtual bool OnVillageDoubleClickCore(MapVillageEventArgs e)
        {
            return false;
        }

        internal protected virtual bool OnKeyDownCore(MapKeyEventArgs e)
        {
            return false;
        }

        internal protected virtual bool OnKeyUpCore(MapKeyEventArgs e)
        {
            return false;
        }

        internal protected virtual bool MouseDownCore(MapMouseEventArgs e)
        {
            return false;
        }

        internal protected virtual bool MouseUpCore(MapMouseEventArgs e)
        {
            return false;
        }

        public virtual string VillageTooltip(Village village)
        {
            return village.Tooltip;
        }

        /// <summary>
        /// Triggered when this Manipulator gains full control
        /// </summary>
        internal protected virtual void SetFullControlManipulatorCore()
        {

        }

        /// <summary>
        /// Triggered when this Manipulator loses full control
        /// </summary>
        internal protected virtual void RemoveFullControlManipulatorCore()
        {
            
        }

        /// <summary>
        /// Saves state to stream
        /// </summary>
        internal protected virtual void WriteXmlCore(XmlWriter w)
        {

        }

        /// <summary>
        /// Loads state from stream
        /// </summary>
        internal protected virtual void ReadXmlCore(XmlReader r)
        {

        }
        #endregion

        #region IDrawer Members
        public virtual void TimerPaint(MapTimerPaintEventArgs e)
        {
            
        }

        public virtual void Paint(MapPaintEventArgs e)
        {

        }
        #endregion

        #region IDisposable Members
        public virtual void Dispose()
        {
            
        }
        #endregion
    }
}
