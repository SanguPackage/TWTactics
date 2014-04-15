#region Using
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace TribalWars
{
    /// <summary>
    /// Encapsulates an action to be performed
    /// on the forms
    /// </summary>
    public class VillageCommand
    {
        #region Fields
        private VillageTools _tool;
        #endregion

        #region Properties
        public VillageTools Tool
        {
            get { return _tool; }
            set { _tool = value; }
        }
        #endregion

        #region Constructors
        public VillageCommand(VillageTools tool)
        {
            _tool = tool;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
