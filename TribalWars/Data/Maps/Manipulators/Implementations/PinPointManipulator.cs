#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TribalWars.Data.Villages;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators.Helpers;
#endregion

namespace TribalWars.Data.Maps.Manipulators
{
    /// <summary>
    /// Manages the markers on players and tribes
    /// </summary>
    internal class PinPointManipulator : ManipulatorBase
    {
        #region Fields
        
        #endregion

        #region Properties
        
        #endregion

        #region Constructors
        public PinPointManipulator(Map map)
            : base(map)
        {

        }
        #endregion

        #region Public Methods
        internal protected override bool MouseMoveCore(MapMouseMoveEventArgs e)
        {
            return false;
        }

        public override void Paint(MapPaintEventArgs e)
        {
            if (e.IsActiveManipulator)
            {
                

            }
        }

        internal protected override bool OnVillageClickCore(MapVillageEventArgs e)
        {
            return false;
        }

        public override void Dispose()
        {

        }

        #endregion

        #region Event Handlers
        #endregion

        #region Private Methods
        #endregion
    }
}
