#region Using
using System;
using TribalWars.Maps.Manipulators.Managers;

#endregion

namespace TribalWars.Worlds.Events.Impls
{
    /// <summary>
    /// Provides data for the ManipulatorChanged event
    /// </summary>
    public class ManipulatorEventArgs : EventArgs
    {
        #region Fields
        private readonly ManipulatorManagerTypes _type;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the active manipulator type
        /// </summary>
        public ManipulatorManagerTypes ManipulatorType
        {
            get { return _type; }
        }
        #endregion

        #region Constructors
        public ManipulatorEventArgs(ManipulatorManagerTypes type)
        {
            _type = type;
        }
        #endregion
    }
}
