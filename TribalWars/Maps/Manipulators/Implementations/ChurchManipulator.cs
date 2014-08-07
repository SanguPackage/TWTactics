#region Using
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Villages;
using TribalWars.Worlds.Events;
using TribalWars.Worlds.Events.Impls;

#endregion

namespace TribalWars.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Display the radius of the churches
    /// </summary>
    internal class ChurchManipulator : ManipulatorBase
    {
        #region Fields
        #endregion

        #region Constructors
        public ChurchManipulator(Map map)
            : base(map)
        {

            //_map.EventPublisher.VillagesSelected += EventPublisher_VillagesSelected;
            //_map.EventPublisher.PlayerSelected += EventPublisher_VillagesSelected;
            //_map.EventPublisher.TribeSelected += EventPublisher_VillagesSelected;
        }
        #endregion

        #region Methods
        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            
            return false;
        }

        public override void Paint(MapPaintEventArgs e)
        {
            
        }

        protected internal override bool OnVillageClickCore(MapVillageEventArgs e)
        {
            return false;
        }

        protected internal override bool OnKeyDownCore(MapKeyEventArgs e)
        {
            
            return false;
        }

        protected internal override void CleanUp()
        {
        }

        public override void TimerPaint(MapTimerPaintEventArgs e)
        {
            
        }

        public override void Dispose()
        {

        }
        #endregion

        #region Privates
        
        #endregion
    }
}