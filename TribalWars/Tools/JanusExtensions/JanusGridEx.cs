using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;

namespace TribalWars.Tools.JanusExtensions
{
    public static class JanusGridEx
    {
        #region GridEX
        public static void Configure(this GridEX grid, bool forEdit, bool allowFilter)
        {
            grid.AlternatingColors = true;
            grid.BorderStyle = BorderStyle.Flat;
            grid.HideSelection = HideSelection.Highlight;
            grid.AllowRemoveColumns = InheritableBoolean.False;
            grid.HideColumnsWhenGrouped = InheritableBoolean.True;
            grid.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;

            grid.FocusCellDisplayMode = forEdit ? FocusCellDisplayMode.UseFocusCellFormatStyle : FocusCellDisplayMode.UseSelectedFormatStyle;
            grid.AllowEdit = forEdit ? InheritableBoolean.True : InheritableBoolean.False;

            grid.FilterMode = allowFilter ? FilterMode.Automatic : FilterMode.None;
            grid.FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges;
            grid.UpdateMode = UpdateMode.CellUpdate;
            grid.FilterRowButtonStyle = FilterRowButtonStyle.ConditionOperatorDropDown;

            //column: DefaultFilterRowComparison

#if !DEBUG
            grid.SaveSettings = true;
#endif
            grid.TotalRowPosition = TotalRowPosition.BottomFixed;
            grid.ColumnAutoResize = true;
            grid.AutoEdit = true;
        }

        public static void ConfigureAsColor(this GridEXColumn column, Color? defaultColor = null)
        {
            column.ColumnType = ColumnType.Text;
            column.EditType = EditType.Custom;

            {
                // Fancy scope so that the colorControl is not captured in FormattingRow
                var colorControl = new UIColorButton();
                colorControl.Configure();
                if (defaultColor.HasValue)
                {
                    colorControl.ColorPicker.AutomaticColor = defaultColor.Value;
                }

                column.GridEX.InitCustomEdit += (sender, e) =>
                {
                    if (e.Column == column)
                    {
                        if (e.Value != null)
                        {
                            colorControl.SelectedColor = (Color)e.Value;
                        }
                        e.EditControl = colorControl;
                    }
                };

                column.GridEX.EndCustomEdit += (sender, e) =>
                {
                    if (e.Column == column)
                    {
                        e.Value = colorControl.SelectedColor;
                        e.DataChanged = true;
                    }
                };
            }

            column.GridEX.FormattingRow += (sender, e) =>
            {
                if (e.Row.RowType == RowType.Record)
                {
                    GridEXCell cell = e.Row.Cells[column];
                    var color = (Color)cell.Value;
                    cell.FormatStyle = new GridEXFormatStyle();
                    cell.FormatStyle.BackColor = color;
                    cell.Text = color.Description();
                    cell.ToolTipText = cell.Text;
                }
            };
        }

        public static void ConfigureAsNumeric(this GridEXColumn column)
        {
            column.TextAlignment = TextAlignment.Far;
        }

        /// <summary>
        /// Get DataRow when bound to DataSet
        /// </summary>
        public static DataRow GetDataRow(this GridEXRow row)
        {
            return ((DataRowView)row.DataRow).Row;
        }
        #endregion
    }
}
