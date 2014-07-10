using System.Drawing;
using System.Windows.Forms;
using Janus.Windows.Common;
using Janus.Windows.EditControls;

namespace TribalWars.Tools.JanusExtensions
{
    /// <summary>
    /// WinForms extensions for Janus controls
    /// </summary>
    public static class JanusControls
    {
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
