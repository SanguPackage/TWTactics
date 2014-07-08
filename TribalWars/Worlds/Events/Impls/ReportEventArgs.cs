#region Using
using System;
using TribalWars.Browsers.Reporting;

#endregion

namespace TribalWars.Worlds.Events.Impls
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
