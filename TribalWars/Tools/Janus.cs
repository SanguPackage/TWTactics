using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.Common;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls.Finders;
using TribalWars.Maps;
using TribalWars.Maps.Markers;
using TribalWars.Villages;
using TribalWars.Villages.ContextMenu;
using BorderStyle = Janus.Windows.GridEX.BorderStyle;
using ComboStyle = Janus.Windows.EditControls.ComboStyle;
using CommandType = Janus.Windows.UI.CommandBars.CommandType;

namespace TribalWars.Tools
{
    /// <summary>
    /// WinForms extensions for Janus controls
    /// </summary>
    public static class Janus
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
                                colorControl.SelectedColor = (Color) e.Value;
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
                        var color = (Color) cell.Value;
                        cell.FormatStyle = new GridEXFormatStyle();
                        cell.FormatStyle.BackColor = color;
                        cell.Text = color.Description();
                        cell.ToolTipText = cell.Text;
                    }
                };
        }

        /// <summary>
        /// Get DataRow when bound to DataSet
        /// </summary>
        public static DataRow GetDataRow(this GridEXRow row)
        {
            return ((DataRowView)row.DataRow).Row;
        }
        #endregion

        #region ColorPicker
        public static void Configure(this UIColorPicker colorPicker)
        {
            colorPicker.MoreColorsButtonClick += (sender, args) =>
            {
                Color? color = ShowMoreColorsDialog();
                if (color.HasValue)
                {
                    colorPicker.SelectedColor = color.Value;
                }
            };
        }

        public static void Configure(this UIColorButton colorPicker)
        {
            colorPicker.MoreColorsButtonClick += (sender, args) =>
                {
                    Color? color = ShowMoreColorsDialog();
                    if (color.HasValue)
                    {
                        colorPicker.SelectedColor = color.Value;
                    }
            };
        }

        private static Color? ShowMoreColorsDialog()
        {
            using (var dialog = new ColorDialog
            {
                FullOpen = true,
                AnyColor = true,
                CustomColors = GetUserDefinedColors()
            })
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SaveUserDefinedColors(dialog.CustomColors);
                    return dialog.Color;
                }
            }
            return null;
        }

        private static void SaveUserDefinedColors(int[] value)
        {
            Properties.Settings.Default.CustomColors = value;
            Properties.Settings.Default.Save();
        }

        private static int[] GetUserDefinedColors()
        {
            return Properties.Settings.Default.CustomColors;
        }
        #endregion

        #region UIContextMenu
        #region Regular Commands
        public static void AddSeparator(this UIContextMenu menu)
        {
            var sep = new UICommand("SEP", string.Empty, CommandType.Separator);
            menu.Commands.Add(sep);
        }

        public static UICommand AddCommand(this UIContextMenu menu, string text, CommandEventHandler handler, Image image)
        {
            var command = AddCommand(menu, text, handler);
            command.Image = image;
            return command;
        }

        public static UICommand AddCommand(this UIContextMenu menu, string text, CommandEventHandler handler, Icon icon)
        {
            return AddCommand(menu, text, handler, null, icon);
        }

        public static UICommand AddCommand(this UIContextMenu menu, string text, CommandEventHandler handler = null, Shortcut? shortcut = null, Icon icon = null)
        {
            var cmd = new UICommand("", text, CommandType.Command);
            cmd.Click += handler;
            if (shortcut.HasValue)
            {
                cmd.Shortcut = shortcut.Value;
            }
            cmd.Icon = icon;

            menu.Commands.Add(cmd);

            return cmd;
        }
        #endregion

        #region ChangeColor Command
        public static void AddChangeColorCommand(this UIContextMenu menu, string text, Color defaultSelectedColor, Action<object, Color> handler)
        {
            menu.AddChangeColorCommand(text, defaultSelectedColor, defaultSelectedColor, handler);
        }

        public static void AddChangeColorCommand(this UIContextMenu menu, string text, Color defaultSelectedColor, Color automaticColor, Action<object, Color> handler)
        {
            var cmd = new UICommand("", text, CommandType.ColorPickerCommand);

            var colorPicker = new UIColorPicker();
            colorPicker.Configure();

            colorPicker.SelectedColor = defaultSelectedColor;
            colorPicker.AutomaticColor = automaticColor;
            colorPicker.SelectedColorChanged += (sender, e) =>
                {
                    Color selectedColor = ((UIColorPicker) sender).SelectedColor;
                    cmd.Image = DrawContextIcon(selectedColor);
                    handler(sender, selectedColor);
                };

            colorPicker.AutomaticButtonClick += (sender, e) =>
                {
                    cmd.Image = DrawContextIcon(automaticColor);
                    handler(sender, automaticColor);
                };

            cmd.Control = colorPicker;
            cmd.Image = DrawContextIcon(defaultSelectedColor);
            menu.Commands.Add(cmd);
        }

        /// <summary>
        /// Draws an icon for Commands to represent the currently selected color(s)
        /// </summary>
        public static Image DrawContextIcon(Color color, Color? extraColor = null)
        {
            if (color == Color.Transparent && (extraColor == null || extraColor == Color.Transparent))
            {
                return null;
            }

            const int canvasSize = 16;

            var map = new Bitmap(canvasSize, canvasSize);
            Graphics g = Graphics.FromImage(map);
            using (var brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, 0, 0, canvasSize, canvasSize);
            }
            
            if (extraColor.HasValue)
            {
                using (var brush = new SolidBrush(extraColor.Value))
                {
                    g.FillRectangle(brush, 5, 5, canvasSize - 10, canvasSize - 10);
                }
            }

            return map;
        }
        #endregion

        #region Other Commands
        public static void AddTextBoxCommand(this UIContextMenu menu, string text, string defaultTextBoxValue, EventHandler handler)
        {
            var txtBox = new TextBox();
            txtBox.Text = defaultTextBoxValue;
            txtBox.TextChanged += handler;

            var cmd = new UICommand("", text, CommandType.TextBoxCommand);
            cmd.Control = txtBox;
            menu.Commands.Add(cmd);
        }

        public static void AddToggleCommand(this UIContextMenu menu, string text, bool defaultValue, CommandEventHandler handler)
        {
            var cmd = new UICommand("", text, CommandType.ToggleButton);
            cmd.IsChecked = defaultValue;
            cmd.Click += handler;
            menu.Commands.Add(cmd);
        }

        public static void AddComboBoxCommand(this UIContextMenu menu, string text, IEnumerable<string> list, string defaultValue, EventHandler handler)
        {
            var ctl = new UIComboBox
                {
                    DataSource = list.ToArray(),
                    SelectedValue = defaultValue,
                    ComboStyle = ComboStyle.DropDownList
                };

            ctl.SelectedValueChanged += handler;

            var cmd = new UICommand("", text, CommandType.ComboBoxCommand);
            cmd.Control = ctl;
            menu.Commands.Add(cmd);
        }
        #endregion

        #region MarkerContexts
        public static void AddMarkerContextCommands(this UIContextMenu menu, MarkerContextMenu markerContext)
        {
            var markerHolder = markerContext.GetMainCommand(menu);
            markerHolder.Commands.AddRange(markerContext.GetCommands().ToArray());
        }

        public static void AddPlayerContextCommands(this UIContextMenu menu, Map map, Player player, bool addTribeCommands)
        {
            var playerCommand = menu.AddCommand(player.Name, null, Properties.Resources.Player);
            playerCommand.ToolTipText = player.Tooltip;
            var playerContext = new PlayerContextMenu(map, player, addTribeCommands);
            playerCommand.Commands.AddRange(playerContext.GetCommands().ToArray());
        }

        public static void AddTribeContextCommands(this UIContextMenu menu, Map map, Tribe tribe)
        {
            var tribeCommand = menu.AddCommand(tribe.Tag, null, Properties.Resources.Tribe);
            tribeCommand.ToolTipText = tribe.Tooltip;
            var tribeContext = new TribeContextMenu(map, tribe);
            tribeCommand.Commands.AddRange(tribeContext.GetCommands().ToArray());
        }
        #endregion
        #endregion

        #region JanusSuperTip
        /// <summary>
        /// Create a WinForms tooltip control with default properties set
        /// </summary>
        public static JanusSuperTip CreateTooltip()
        {
            return new JanusSuperTip
                {
                    InitialDelay = 400
                };
        }
        #endregion

    }
}
