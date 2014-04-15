#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Maps.Manipulators;
#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// Provides data for the ManipulatorChanged event
    /// </summary>
    public class ManipulatorEventArgs : EventArgs
    {
        #region Fields
        private ManipulatorManagerBase _manipulator;
        private ManipulatorManagerTypes _type;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the active manipulator
        /// </summary>
        public ManipulatorManagerBase Manipulator
        {
            get { return _manipulator; }
        }

        /// <summary>
        /// Gets the active manipulator type
        /// </summary>
        public ManipulatorManagerTypes ManipulatorType
        {
            get { return _type; }
        }
        #endregion

        #region Constructors
        public ManipulatorEventArgs(ManipulatorManagerBase manipulator, ManipulatorManagerTypes type)
        {
            _manipulator = manipulator;
            _type = type;
        }
        #endregion
    }
}
