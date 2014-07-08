using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls.TWContextMenu;
using TribalWars.Data.Maps;
using TribalWars.Data.Players;
using TribalWars.Data.Tribes;
using CommandType = Janus.Windows.UI.CommandBars.CommandType;

namespace TribalWars.Tools
{
    /// <summary>
    /// WinForms extensions for Janus controls
    /// </summary>
    public static class Janus
    {
        #region GridEX
        /// <summary>
        /// Get DataRow when bound to DataSet
        /// </summary>
        public static DataRow GetDataRow(this GridEXRow row)
        {
            return ((DataRowView)row.DataRow).Row;
        }
        #endregion

        #region UIContextMenu
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

        public static void AddChangeColorCommand(this UIContextMenu menu, string text, Color defaultSelectedColor, EventHandler handler)
        {
            var cmd = new UICommand("", text, CommandType.ColorPickerCommand);

            var colorPicker = new UIColorPicker();
            colorPicker.SelectedColor = defaultSelectedColor;
            colorPicker.SelectedColorChanged += (sender, e) =>
                {
                    cmd.Image = DrawContextIcon(((UIColorPicker)sender).SelectedColor);
                    handler(sender, e);
                };

            cmd.Control = colorPicker;
            cmd.Image = DrawContextIcon(defaultSelectedColor);
            menu.Commands.Add(cmd);
        }

        public static Image DrawContextIcon(Color color, Color? extraColor = null)
        {
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

        public static void AddTextBoxCommand(this UIContextMenu menu, string text, string defaultTextBoxValue, EventHandler handler)
        {
            var txtBox = new TextBox();
            txtBox.Text = defaultTextBoxValue;
            txtBox.TextChanged += handler;

            var cmd = new UICommand("", text, CommandType.TextBoxCommand);
            cmd.Control = txtBox;
            menu.Commands.Add(cmd);
        }

        public static void AddMarkerContextCommands(this UIContextMenu menu, MarkerContextMenu markerContext)
        {
            var markerHolder = markerContext.GetMainCommand(menu);
            markerHolder.Commands.AddRange(markerContext.GetCommands().ToArray());
        }

        public static void AddToggleCommand(this UIContextMenu menu, string text, bool defaultValue, CommandEventHandler handler)
        {
            var cmd = new UICommand("", text, CommandType.ToggleButton);
            cmd.IsChecked = defaultValue;
            cmd.Click += handler;
            menu.Commands.Add(cmd);
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
    }
}
