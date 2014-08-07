#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using TribalWars.Maps.Manipulators.EventArg;
using TribalWars.Villages;

#endregion

namespace TribalWars.Maps.Manipulators.Implementations.Church
{
    /// <summary>
    /// Display the radius of the churches
    /// </summary>
    public class ChurchManipulator : ManipulatorBase
    {
        #region Fields
        private readonly List<ChurchInfo> _churches;
        #endregion

        #region Constructors
        public ChurchManipulator(Map map)
            : base(map)
        {
            _churches = new List<ChurchInfo>();
            _map.EventPublisher.ChurchChanged += EventPublisherOnChurchChanged;
        }

        private void EventPublisherOnChurchChanged(object sender, ChurchEventArgs e)
        {
            if (e.Church.ChurchLevel == 0)
            {
                if (_churches.Contains(e.Church))
                {
                    _churches.Remove(e.Church);
                }
            }
            else
            {
                if (!_churches.Contains(e.Church))
                {
                    _churches.Add(e.Church);
                }
            }
        }
        #endregion

        #region Methods
        public ChurchInfo GetChurch(Village village)
        {
            return _churches.FirstOrDefault(x => x.Village == village);
        }

        public override void Paint(MapPaintEventArgs e)
        {

        }

        protected internal override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            
            return false;
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