using System.Windows.Forms;

namespace TribalWars.Controls.Distance
{
    /// <summary>
    /// Wraps a DistanceCollectionControl to be used in a ToolStrip
    /// </summary>
    public class DistanceControlHost : ToolStripControlHost
    {
        #region Properties
        /// <summary>
        /// Gets the underlying DistanceCollectionControl
        /// </summary>
        public DistanceCollectionControl DistanceCollection
        {
            get { return Control as DistanceCollectionControl; }
        }
        #endregion

        #region Constructors
        public DistanceControlHost()
            : base(new DistanceCollectionControl())
        {
            AutoSize = true;
            //Size = new System.Drawing.Size(75, 21);
            Text = string.Empty;
            ToolTipText = string.Empty;
            Size = DistanceCollection.Size;
        }
        #endregion
    }
}
