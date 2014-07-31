#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Browsers.Control;
using Janus.Windows.UI.CommandBars;
using System.Drawing;
using Janus.Windows.UI;
using TribalWars.Controls;
using TribalWars.Maps;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Tools;
using TribalWars.Tools.JanusExtensions;
using TribalWars.Worlds;
using TribalWars.Worlds.Events;

#endregion

namespace TribalWars.Villages.ContextMenu
{
    /// <summary>
    /// ContextMenu for multiple Village
    /// </summary>
    public class VillagesContextMenu : IContextMenu
    {
        #region Fields
        private readonly IEnumerable<Village> _villages;

        private readonly UIContextMenu _menu;
        private readonly Map _map;
        private readonly Action<VillageType> _onVillageTypeChangeDelegate;
        #endregion

        #region Constructors
        public VillagesContextMenu(Map map, IEnumerable<Village> villages, Action<VillageType> onVillageTypeChangeDelegate = null)
        {
            _villages = villages;
            _map = map;
            _onVillageTypeChangeDelegate = onVillageTypeChangeDelegate;

            _menu = JanusContextMenu.Create();
            _menu.AddSetVillageTypeCommand(OnVillageTypeChange, null);
            _menu.AddCommand("BBCode", OnBbCode, Properties.Resources.clipboard);
        }

        private void OnVillageTypeChange(object sender, CommandEventArgs e)
        {
            var changeTo = (VillageType) e.Command.Tag;
            string text = string.Format("Do you want to change the purpose for {0} villages to {1}?\nThis action cannot be undone!", _villages.Count(), changeTo);
            if (_villages.Count() > 1 && MessageBox.Show(text, "Set purpose", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }

            foreach (Village village in _villages)
            {
                if (village.Type.HasFlag(changeTo))
                {
                    village.Type -= changeTo;
                }
                else
                {
                    village.Type |= changeTo;
                }
            }
            _map.Invalidate();

            if (_onVillageTypeChangeDelegate != null)
            {
                _onVillageTypeChangeDelegate(changeTo);
            }
        }

        /// <summary>
        /// Create BB code for the villages and put on clipboard
        /// </summary>
        private void OnBbCode(object sender, EventArgs e)
        {
            var str = new StringBuilder();
            foreach (Village village in _villages)
            {
                str.AppendFormat("{0}{1}", village.BbCode(), Environment.NewLine);
            }

            WinForms.ToClipboard(str.ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Shows the ContextMenuStrip
        /// </summary>
        public void Show(Control control, Point position)
        {
            _menu.Show(control, position);
        }

        public bool IsVisible()
        {
            return _menu.IsVisible;
        }
        #endregion
    }
}
