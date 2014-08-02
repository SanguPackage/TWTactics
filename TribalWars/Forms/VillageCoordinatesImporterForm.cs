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
            MessageBox.Show(
                "Get a list of village coordinates somehow, paste them in the box on the left and they will appear in the grid on the right."
                + Environment.NewLine + Environment.NewLine + "Use Control + A to select all imported villages. Right click to set their purpose or to attack them."
                + Environment.NewLine + Environment.NewLine + "You can find more information at Windows > 'Manage your villages' :)",
                "Import village coordinates?");
        }
    }
}
