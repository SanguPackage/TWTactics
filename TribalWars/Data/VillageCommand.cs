#region Using

#endregion

namespace TribalWars.Data
{
    /// <summary>
    /// Encapsulates an action to be performed
    /// on the forms
    /// </summary>
    public class VillageCommand
    {
        #region Fields
        #endregion

        #region Properties
        public VillageTools Tool { get; set; }
        #endregion

        #region Constructors
        public VillageCommand(VillageTools tool)
        {
            Tool = tool;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
