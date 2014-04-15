using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using TribalWars.Data.Villages;
//using JanusExtension.GridEx;

namespace TribalWars
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            /*int i = 0;

            List<Village> vils = new List<Village>();
            foreach (Village vil in World.Default.Villages.Values)
            {
                vils.Add(vil);
                i++;
                if (i > 10) break;
            }

            //VillageCollection col = new VillageCollection(vils);

            ExtendedGridEx gridEX1 = new ExtendedGridEx();
            gridEX1.Visible = true;
            this.Controls.Add(gridEX1);
            gridEX1.Width = 500;
            gridEX1.Height = 500;
            gridEX1.SetDataBinding(vils, "");
            gridEX1.RetrieveStructure();
            gridEX1.Refresh();*/


        }

        private void FormTest_MouseDown(object sender, MouseEventArgs e)
        {
            this.uiContextMenu1.Show(e.Location);
        }

        private void uiContextMenu1_CommandControlValueChanged(object sender, Janus.Windows.UI.CommandBars.CommandEventArgs e)
        {
            //e.Command.
        }
    }
}