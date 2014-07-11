using System;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using System.Linq;
using TribalWars.Controls.Finders;
using TribalWars.Worlds;

namespace TribalWars.Forms.NotUsed
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TestForm : Form
    {
        #region Properties
        #endregion

        #region Constructors
        public TestForm()
        {
            InitializeComponent();
        }
        #endregion

        private void ActivePlayerForm_Load(object sender, EventArgs e)
        {
            multiColumnCombo1.DropDownList.FormattingRow += DropDownList_FormattingRow;

            multiColumnCombo1.ValueMember = "Value";
            multiColumnCombo1.DisplayMember = "Value";

            var col2 =
                World.Default.Tribes.Select(x => new VillagePlayerTribeRow(x))
                .Union(World.Default.Players.Select(x => new VillagePlayerTribeRow(x)))
                .OrderBy(x => x.Value)
                .ToArray();

            multiColumnCombo1.DataSource = col2;


            
            villagePlayerTribeSelector1.Initialize(World.Default.Map);
            villagePlayerTribeSelectorOld1.Initialize(World.Default.Map);

        }

        private void DropDownList_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.Record)
            {
                e.Row.Cells["Image"].ImageIndex = ((VillagePlayerTribeRow)e.Row.DataRow).ImageIndex;
            }
        }

        

        private void CloseButton_Click(object sender, EventArgs e)
        {
           
        }
    }
}
