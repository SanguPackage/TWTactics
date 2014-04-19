using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TribalWars.Controls
{
    /// <summary>
    /// Combines a Label &amp; TextBox
    /// </summary>
    /// <remarks>Used in the Location control</remarks>
    public partial class LabelTextBox : UserControl
    {
        #region Fields
        private static AutoCompleteStringCollection _autoCompleteList = new AutoCompleteStringCollection();
        #endregion

        #region Constructors
        public LabelTextBox()
        {
            InitializeComponent();
            _TextBox.AutoCompleteCustomSource = LabelTextBox._autoCompleteList;
        }

        public LabelTextBox(string description, int widthTextbox)
        {
            InitializeComponent();
            _Label.Text = description;
            _TextBox.Width = widthTextbox;
            _TextBox.AutoCompleteCustomSource = LabelTextBox._autoCompleteList;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the TextBox text
        /// </summary>
        [Browsable(true)]
        public override string Text
        {
            get { return _TextBox.Text; }
            set { _TextBox.Text = value; }
        }

        /// <summary>
        /// Gets or sets the width of the textbox
        /// </summary>
        public int TextBoxWidth
        {
            get { return _TextBox.Width; }
            set { _TextBox.Width = value; }
        }

        /// <summary>
        /// Gets or sets the text of the label
        /// </summary>
        public string LabelText
        {
            get { return _Label.Text; }
            set { _Label.Text = value; }
        }
        #endregion

        #region Event Handlers
        private void _TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_TextBox.Focused)
                _TextBox.SelectAll();
        }

        private void _TextBox_Leave(object sender, EventArgs e)
        {
            if (!LabelTextBox._autoCompleteList.Contains(_TextBox.Text))
                LabelTextBox._autoCompleteList.Add(_TextBox.Text);
        }
        #endregion
    }
}
