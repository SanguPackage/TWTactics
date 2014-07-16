#region Using
using System.Drawing;
using TribalWars.Controls;
using System.Xml;
using TribalWars.Maps.Controls;
using TribalWars.Maps.Manipulators.Helpers.EventArgs;
using TribalWars.Villages;
using TribalWars.Villages.ContextMenu;
using TribalWars.Worlds;

#endregion

namespace TribalWars.Maps.Manipulators
{
    /// <summary>
    /// Base class for a simple manipulator implementation (MapMover, ...)
    /// but also for the manipulator managers (DefaultManipulator, ...)
    /// </summary>
    public abstract class ManipulatorBase
    {
        #region Fields
        protected readonly Map _map;
        #endregion

        #region Constructors
        protected ManipulatorBase(Map map)
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

        internal protected virtual bool MouseLeave()
        {
            return false;
        }

        public virtual IContextMenu GetContextMenu(Point location, Village village)
        {
            if (village != null)
            {
                return new VillageContextMenu(_map, village);
            }
            Point gameLocation = World.Default.Map.Display.GetGameLocation(location);
            return new NoVillageContextMenu(gameLocation);
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

        /// <summary>
        /// Cleanup anything when switching worlds or settings
        /// </summary>
        protected internal abstract void CleanUp();
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

        //--> TODO This stuff needs to be in the ManipulatorBase
        //--> Adding a control to the map
        //public void AddControl()
        //{
        //    RemoveControl();
        //    _control = new MapPolygonControl(ActivePolygon, this, new Point(100, 100));
        //    _map.Control.Controls.Add(_control);
        //    _control.Focus();
        //}

        //public void RemoveControl()
        //{
        //    if (_control != null)
        //    {
        //        _map.Control.Controls.Remove(_control);
        //        _control.Dispose();
        //    }
        //}
    }
}
