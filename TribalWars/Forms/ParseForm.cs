using System;
using System.Windows.Forms;

namespace TribalWars.Forms
{
    public partial class ParseForm : Form
    {
        //MapDisplay Map;

        public ParseForm()
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