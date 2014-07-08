using System;
using System.Diagnostics;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using System.Linq;
using TribalWars.Villages;
using TribalWars.Worlds;

namespace TribalWars.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ActivePlayerForm : Form
    {
        #region Properties
        #endregion

        #region Constructors
        public ActivePlayerForm()
        {
            InitializeComponent();
        }
        #endregion

        private void ActivePlayerForm_Load(object sender, EventArgs e)
        {
            //You.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //You.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //var col = new AutoCompleteStringCollection();
            //col.AddRange(World.Default.Tribes.Select(x => x.Value.Tag).ToArray());
            //col.AddRange(World.Default.Players.Select(x => x.Value.Name).ToArray());
            //You.AutoCompleteCustomSource = col;

            multiColumnCombo1.DropDownList.FormattingRow += DropDownList_FormattingRow;

            multiColumnCombo1.ValueMember = "Value";
            multiColumnCombo1.DisplayMember = "Value";
            //multiColumnCombo1.DropDownList.ValueMember = "Value";
            //multiColumnCombo1.DropDownList.DisplayMember = "Value";

            var col2 =
                World.Default.Tribes.Select(x => new AutocompleteItem(x.Value))
                .Union(World.Default.Players.Select(x => new AutocompleteItem(x.Value)))
                .OrderBy(x => x.Value)
                .ToArray();

            multiColumnCombo1.DataSource = col2;

            

            //multiColumnCombo1.ImageList = imageList1;
            //multiColumnCombo1.DropDownList.ImageList = imageList1;
        }

        private void DropDownList_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.Record)
            {
                e.Row.Cells["Image"].ImageIndex = ((AutocompleteItem)e.Row.DataRow).ImageIndex;
            }
        }

        private class AutocompleteItem
        {
            public bool IsVillage { get; set; }
            public bool IsPlayer { get; set; }
            public bool IsTribe { get; set; }

            public string Value { get; set; }
            public string Text { get; set; }

            public int ImageIndex
            {
                get
                {
                    if (IsVillage) return 0;
                    if (IsPlayer) return 1;
                    if (IsTribe) return 2;
                    return -1;
                }
            }

            public AutocompleteItem(Tribe tribe)
            {
                Value = tribe.Tag;
                Text = string.Format("#{1} ({0} points)", Tools.Common.GetPrettyNumber(tribe.AllPoints), tribe.Rank);
                IsTribe = true;
            }

            public AutocompleteItem(Player player)
            {
                Value = player.Name;
                Text = string.Format("#{1} ({0} points)", Tools.Common.GetPrettyNumber(player.Points), player.Rank);
                IsPlayer = true;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }
    }
}
