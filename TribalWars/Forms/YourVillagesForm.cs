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
    /// Manage the World.You <see cref="Player"/>
    /// </summary>
    public partial class YourVillagesForm : Form
    {
        private Player _player;

        public YourVillagesForm()
        {
            InitializeComponent();
        }

        public static void ShowForm()
        {
            if (World.Default.You.Empty)
            {
                ActivePlayerForm.AskToSetSelf();
            }
            else
            {
                var form = new YourVillagesForm();
                form._player = World.Default.You;

                IEnumerable<VillageGridExRow> villageRows = World.Default.You.Villages.Select(x => new VillageGridExRow(World.Default.Map, x));
                form.villagesGridControl1.Bind(villageRows.ToList());

                form.Show();
            }
        }
    }
}
