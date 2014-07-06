#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Reporting;
#endregion

namespace TribalWars.Data.Events
{
    /// <summary>
    /// EventArgs for an event
    /// </summary>
    public class ReportEventArgs : EventArgs
    {
        #region Properties
        /// <summary>
        /// Gets the report
        /// </summary>
        public Report SelectedTribe { get; private set; }
        #endregion

        #region Constructors
        public ReportEventArgs(Report report)
        {
            SelectedTribe = report;
        }
        #endregion
    }
}
