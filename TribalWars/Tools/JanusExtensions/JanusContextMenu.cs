using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.UI;
using Janus.Windows.UI.CommandBars;
using TribalWars.Controls.Finders;
using TribalWars.Maps;
using TribalWars.Maps.Markers;
using TribalWars.Villages;
using TribalWars.Villages.ContextMenu;

namespace TribalWars.Tools.JanusExtensions
{
    public static class JanusContextMenu
    {
        public static UIContextMenu Create()
        {
            return new UIContextMenu
            {
                ShowToolTips = InheritableBoolean.True,
                VisualStyle = VisualStyle.OfficeXP,
                ShowShadowUnderMenus = InheritableBoolean.True
            };
        }

        #region UIContextMenu
        #region Regular Commands
        public static void AddSeparator(this UICommandCollection commands)
        {
            var sep = new UICommand("", string.Empty, CommandType.Separator);
            commands.Add(sep);
        }

        public static void AddSeparator(this UIContextMenu menu)
        {
            menu.Commands.AddSeparator();
        }

        public static UICommand AddCommand(this UIContextMenu menu, string text, CommandEventHandler handler, Image image)
        {
            var command = AddCommand(menu, text, handler);
            command.Image = image;
            return command;
        }

        public static void AddCommand(this UIContextMenu menu, string text, CommandEventHandler handler, Icon icon)
        {
            AddCommand(menu, text, handler, null, icon);
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

        public static void AddChangeColorCommand(this UICommandCollection menu, string text, Color defaultSelectedColor, Color automaticColor, Action<object, Color> handler)
        {
            var cmd = new UICommand("", text, CommandType.ColorPickerCommand);

            var colorPicker = new UIColorPicker();
            colorPicker.Configure();

            colorPicker.SelectedColor = defaultSelectedColor;
            colorPicker.AutomaticColor = automaticColor;
            colorPicker.SelectedColorChanged += (sender, e) =>
            {
                Color selectedColor = ((UIColorPicker)sender).SelectedColor;
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
            menu.Add(cmd);
        }

        public static void AddChangeColorCommand(this UIContextMenu menu, string text, Color defaultSelectedColor, Color automaticColor, Action<object, Color> handler)
        {
            menu.Commands.AddChangeColorCommand(text, defaultSelectedColor, automaticColor, handler);
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
            using (Graphics g = Graphics.FromImage(map))
            {
                using (var brush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, 0, 0, canvasSize, canvasSize);
                }
                using (var borderPen = new Pen(Color.Black))
                {
                    g.DrawRectangle(borderPen, 0, 0, canvasSize - 1, canvasSize - 1);
                }

                if (extraColor.HasValue)
                {
                    using (var brush = new SolidBrush(extraColor.Value))
                    {
                        g.FillRectangle(brush, 5, 5, canvasSize - 10, canvasSize - 10);
                    }
                }
            }

            return map;
        }
        #endregion

        #region SetVillageType
        public static void AddSetVillageTypeCommand(this UIContextMenu menu, CommandEventHandler onVillageTypeChange, Village village)
        {
            VillageType currentVillageType = village == null ? VillageType.None : village.Type;
            UICommand villageTypes = menu.AddCommand("Set purpose", null, currentVillageType.GetImage(true));
            AddVillageTypeCommand(villageTypes, VillageType.Attack, currentVillageType, onVillageTypeChange);
            AddVillageTypeCommand(villageTypes, VillageType.Catapult, currentVillageType, onVillageTypeChange);
            AddVillageTypeCommand(villageTypes, VillageType.Defense, currentVillageType, onVillageTypeChange);
            villageTypes.Commands.AddSeparator();
            AddVillageTypeCommand(villageTypes, VillageType.Noble, currentVillageType, onVillageTypeChange);
            AddVillageTypeCommand(villageTypes, VillageType.Scout, currentVillageType, onVillageTypeChange);
            AddVillageTypeCommand(villageTypes, VillageType.Farm, currentVillageType, onVillageTypeChange);
        }

        /// <summary>
        /// Allow change between VillageTypes (Offensive, Defensive, Nobles, ...)
        /// </summary>
        private static void AddVillageTypeCommand(UICommand menu, VillageType typeToSet, VillageType typeCurrent, CommandEventHandler onVillageTypeChange)
        {
            bool isCurrentlySet = typeCurrent.HasFlag(typeToSet);

            var command = new UICommand("", typeToSet.GetDescription());
            command.Tag = typeToSet;
            command.Image = typeToSet.GetImage(true);
            command.Checked = isCurrentlySet ? InheritableBoolean.True : InheritableBoolean.False;
            command.Click += onVillageTypeChange;
            menu.Commands.Add(command);
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

        public static void AddPlayerNobledContextCommands(this UIContextMenu menu, Map map, Player player, bool addTribeCommands)
        {
            var playerCommand = menu.AddCommand("Nobled from " + player.Name, null, Properties.Resources.nobleman);
            AddPlayerContextCommands(map, player, addTribeCommands, playerCommand);
        }

        public static void AddPlayerContextCommands(this UIContextMenu menu, Map map, Player player, bool addTribeCommands)
        {
            string playerDesc = string.Format("{0} ({1})", player.Name, Common.GetPrettyNumber(player.Points));
            if (player.Rank <= 100)
            {
                playerDesc = string.Format("#{0} {1}", player.Rank, playerDesc);
            }
            var playerCommand = menu.AddCommand(playerDesc, null, Properties.Resources.Player);
            AddPlayerContextCommands(map, player, addTribeCommands, playerCommand);
        }

        private static void AddPlayerContextCommands(Map map, Player player, bool addTribeCommands, UICommand playerCommand)
        {
            playerCommand.ToolTipText = player.Tooltip;
            var playerContext = new PlayerContextMenu(map, player, addTribeCommands);
            playerCommand.Commands.AddRange(playerContext.GetCommands().ToArray());
        }

        public static void AddTribeContextCommands(this UIContextMenu menu, Map map, Tribe tribe)
        {
            string tribeDesc = string.Format("{0} ({1})", tribe.Tag, Common.GetPrettyNumber(tribe.AllPoints));
            if (tribe.Rank <= 20)
            {
                tribeDesc = string.Format("#{0} {1}", tribe.Rank, tribeDesc);
            }
            var tribeCommand = menu.AddCommand(tribeDesc, null, Properties.Resources.Tribe);
            tribeCommand.ToolTipText = tribe.Tooltip;
            var tribeContext = new TribeContextMenu(map, tribe);
            tribeCommand.Commands.AddRange(tribeContext.GetCommands().ToArray());
        }
        #endregion
        #endregion
    }
}
