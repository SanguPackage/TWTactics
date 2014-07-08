using System.Windows.Forms;
using TribalWars.Browsers.Reporting;

namespace TribalWars.Controls.AccordeonDetails
{
    public partial class VillageControl : UserControl
    {
        private Report _report;
        private CurrentSituation _currentSituation;

        #region Constructors
        public VillageControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Methods
        public void SetReport(Report report)
        {
            _report = report;
            _currentSituation = null;
            ReportCanvas.Invalidate();
        }

        public void SetReport(CurrentSituation current)
        {
            _report = null;
            _currentSituation = current;
            ReportCanvas.Invalidate();
        }
        #endregion

        private void ReportCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (_report != null)
            {
                _report.Paint(e.Graphics);
            }
            else if (_currentSituation != null)
            {
                _currentSituation.Paint(e.Graphics);
            }
        }
    }
}
