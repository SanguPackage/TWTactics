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
        #region Properties
        public VillageTools Tool { get; private set; }
        #endregion

        #region Constructors
        public VillageCommand(VillageTools tool)
        {
            Tool = tool;
        }
        #endregion
    }
}
