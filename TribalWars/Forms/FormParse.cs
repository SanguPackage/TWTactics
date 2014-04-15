using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using System.Xml;

namespace TribalWars
{
    public partial class FormParse : Form
    {
        //MapDisplay Map;

        public FormParse()
        {
            InitializeComponent();
            // for testing purposes
        }

        //public FormParse(/*MapDisplay map*/)
        //{
        //    InitializeComponent();
        //    //Map = map;
        //}

        #region Parsing
        private void cmdParseClip_Click(object sender, EventArgs e)
        {
            string input = Clipboard.GetText();
            string output = Parse(input);
            txtOutput.Text = output;
            if (output.Length > 0) Clipboard.SetText(output);
        }

        private void cmdParse_Click(object sender, EventArgs e)
        {
            txtOutput.Text = Parse(txtInput.Text);
        }
        #endregion

        private string Parse(string input)
        {
            //World.Report report = World.Report.ParseText(input, this.Report);
            //if (report != null)
            //{
            //    report.Save();
            //    return report.BBCode();
            //}

            return "";
        }
    }
}