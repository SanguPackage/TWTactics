using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using TribalWars.Forms;

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
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("nl-BE");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("nl-BE");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }
    }
}