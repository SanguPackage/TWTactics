using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Controls.GridExs;
using TribalWars.Forms.Small;
using TribalWars.Maps;
using TribalWars.Villages;
using TribalWars.Worlds;

namespace TribalWars.Forms
{
    /// <summary>
    /// Manage the villages in the Attackers Pool
    /// </summary>
    public partial class AttackersPoolForm : Form
    {
        public AttackersPoolForm()
        {
            InitializeComponent();
        }

        public static void ShowForm()
        {
            var form = new AttackersPoolForm();
            form.villagesGridControl1.Bind(World.Default.Map.Manipulators.AttackManipulator.AttackersPool);

            form.Show();
        }

        private void RemoveSelectedButton_Click(object sender, EventArgs e)
        {
            foreach (Village vil in villagesGridControl1.GetSelectedVillages())
            {
                World.Default.Map.Manipulators.AttackManipulator.AttackersPool.Remove(vil);
            }
            villagesGridControl1.Bind(World.Default.Map.Manipulators.AttackManipulator.AttackersPool);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            World.Default.Map.Manipulators.AttackManipulator.AttackersPool.Clear();
            villagesGridControl1.Bind(Enumerable.Empty<Village>());
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            villagesGridControl1.Bind(World.Default.Map.Manipulators.AttackManipulator.AttackersPool);
        }
    }
}
