using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TribalWars
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //if (DateTime.Now > new DateTime(2010, 10, 01))
            //{
            //    MessageBox.Show("Expired :) Contact Laoujin for the latest version");
            //    return;
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new FormMain());
        }
    }
}