using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Worlds;

namespace TribalWars.Forms
{
    /// <summary>
    /// Import village coords and set their purpose
    /// </summary>
    public partial class VillageCoordinatesImporterForm : Form
    {
        public VillageCoordinatesImporterForm()
        {
            InitializeComponent();
        }

        private void VillageCoordsInputBox_TextChanged(object sender, EventArgs e)
        {
            var villages = World.Default.GetVillages(VillageCoordsInputBox.Text);
            villagesGridExControl1.Bind(villages);
        }

        private void VillageCoordinatesImporterForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
			MessageBox.Show(FormRes.VillageCoordinatesImporterForm_Help_Title, FormRes.VillageCoordinatesImporterForm_Help);
        }

        private void playerTribeDropdown1_PlayerSelected(object sender, Worlds.Events.Impls.PlayerEventArgs e)
        {
            var villages = playerTribeDropdown1.Player;
            villagesGridExControl1.Bind(villages);
        }

        private void ClearTextFieldButton_Click(object sender, EventArgs e)
        {
            VillageCoordsInputBox.Text = "";
        }
    }
}
