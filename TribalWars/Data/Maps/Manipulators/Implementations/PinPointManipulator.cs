#region Using
using TribalWars.Data.Maps.Manipulators.Helpers.EventArgs;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Implementations
{
    /// <summary>
    /// Manages the markers on players and tribes
    /// </summary>
    internal class PinPointManipulator : ManipulatorBase
    {
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
    }
}
