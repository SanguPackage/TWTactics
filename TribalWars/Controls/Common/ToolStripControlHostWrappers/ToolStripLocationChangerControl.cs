using System.Windows.Forms;
using System.Windows.Forms.Design;
using TribalWars.Controls.Finders;

namespace TribalWars.Controls.Common.ToolStripControlHostWrappers
{
    /// <summary>
    /// Wrapper for a LocationChangerControl for use in a ToolStrip.
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripLocationChangerControl : ToolStripControlHost
    {
        #region Properties
        /// <summary>
        /// Gets the underlying LocationChangerControl
        /// </summary>
        public LocationChangerControl LocationChanger
        {
            get { return Control as LocationChangerControl; }
        }
        #endregion

        #region Constructors
        public ToolStripLocationChangerControl()
            : base(new LocationChangerControl())
        {
            AutoSize = false;
            ToolTipText = string.Empty;
            LocationChanger.Width = 200;
        }
        #endregion
    }
}