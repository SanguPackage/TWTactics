using System;
using System.Collections.Generic;
using System.Drawing;
using TribalWars.Data;
using TribalWars.Data.Villages;
using TribalWars.Data.Reporting;
using XPTable.Models;

namespace TribalWars.Controls.Display
{
    #region Enums
    /// <summary>
    /// Lists the different columns in the ReportTableRow columns
    /// </summary>
    [Flags]
    public enum ReportFields
    {
        None = 0,
        Type = 1,
        Status = 2,
        Village = 4,
        Player = 8,
        Date = 16,
        Flag = 32,
        All = 63,
        Default = 63
    }
    #endregion

    /// <summary>
    /// Represents an XPTable row for a report
    /// </summary>
    public class ReportTableRow : Row, TWContextMenu.ITwContextMenu
    {
        #region Fields
        private readonly Report _report;
        private readonly Village _village;
        private Village _villageOther;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the report the row represents
        /// </summary>
        public Report Report
        {
            get { return _report; }
        }
        #endregion

        #region Constructors
        public ReportTableRow(Village village, Report report)
        {
            _report = report;
            if (village == report.Defender.Village)
            {
                _village = report.Defender.Village;
                _villageOther = report.Attacker.Village;
            }
            else
            {
                _village = report.Attacker.Village;
                _villageOther = report.Defender.Village;
            }

            Cells.Add(new Cell(string.Empty, Report.GetCircleImage(report)));
            Cells.Add(new Cell(string.Empty, Report.GetInfoImage(report)));
            Cells.Add(new Cell(_village.LocationString));
            if (_village.HasPlayer)
            {
                Cells.Add(new Cell(_village.Player.Name));
            }
            else
            {
                Cells.Add(new Cell());
            }
            Cells.Add(new Cell(report.Date));
        }
        #endregion

        #region Private Implementation
        #endregion

        #region ITWContextMenu Members
        public void ShowContext(Point p)
        {
            //if (this.TableModel != null)
            //    World.Default.Map.Manipulators.CurrentManipulator.VillageContextMenu.Show(this.TableModel.Table, p, _village);
        }

        public IEnumerable<Village> GetVillages()
        {
            return _village;
        }

        public void DisplayDetails()
        {
            World.Default.Map.EventPublisher.SelectReport(null, _report);
        }
        #endregion
    }
}
