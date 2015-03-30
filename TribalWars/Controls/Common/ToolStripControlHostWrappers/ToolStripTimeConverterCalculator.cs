using System.Windows.Forms;
using System.Windows.Forms.Design;
using TribalWars.Controls.TimeConverter;

namespace TribalWars.Controls.Common.ToolStripControlHostWrappers
{
    /// <summary>
    /// Wrapper for a TimeConverterCalculatorControl for use in a ToolStrip
    /// </summary>
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripTimeConverterCalculator : ToolStripControlHost
    {
        #region Properties
        /// <summary>
        /// Gets the underlying TimeConverterCalculatorControl
        /// </summary>
        public TimeConverterCalculatorControl TimeConverterCalculator
        {
            get { return Control as TimeConverterCalculatorControl; }
        }
        #endregion

        #region Constructors
        public ToolStripTimeConverterCalculator()
            : base(new TimeConverterCalculatorControl())
        {
            AutoSize = false;
            Text = string.Empty;
            ToolTipText = string.Empty;
        }
        #endregion
    }
}
