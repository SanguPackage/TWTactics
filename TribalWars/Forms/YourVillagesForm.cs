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
                form.villagesGridControl1.Bind(World.Default.You);

                form.Show();
            }
        }
    }
}
