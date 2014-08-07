#region Using

#endregion

namespace TribalWars.Maps.Manipulators.Implementations.Church
{
    public class ChurchEventArgs : System.EventArgs
    {
        #region Properties
        /// <summary>
        /// Village with the church change
        /// </summary>
        public ChurchInfo Church { get; private set; }
        #endregion

        #region Constructors
        public ChurchEventArgs(ChurchInfo church)
        {
            Church = church;
        }
        #endregion
    }
}