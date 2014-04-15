using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.Design;

namespace TribalWars.Controls
{
    /// <summary>
    /// Combobox that displays images only
    /// </summary>
    /// <remarks>Used as a Unit ComboBox</remarks>
    [ToolboxBitmap(typeof(ComboBox))]
    public partial class ImageCombobox : ComboBox
    {
        #region Fields
        private ImageList _ImageList = new ImageList();
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the ImageList used in the ComboBox DropDown
        /// </summary>
        [Category("Appearance")]
        public ImageList ImageList
        {
            get { return _ImageList; }
            set
            {
                Items.Clear();
                _ImageList = value;
                for (int i = 0; i < _ImageList.Images.Count; i++)
                    Items.Add(i);

                this.DropDownHeight = Items.Count * (_ImageList.ImageSize.Height + 2);
                this.DropDownWidth = ImageList.ImageSize.Width;
            }
        }

        [Browsable(false)]
        public new DrawMode DrawMode
        {
            get { return DrawMode.OwnerDrawFixed; }
            set { }
        }

        [Browsable(false)]
        public new ComboBoxStyle DropDownStyle
        {
            get { return ComboBoxStyle.DropDownList; }
            set { }
        }
        #endregion

        #region Constructors
        public ImageCombobox()
            : base()
        {
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DisplayMember = "ItemData";
            base.BackColor = Color.FromArgb(248, 244, 232);
            base.Size = new System.Drawing.Size(50, 20);
        }
        #endregion

        #region Event Handlers
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0 && Items.Count > 0 && e.Index < Items.Count)
            {
                using (Graphics g = e.Graphics)
                {
                    e.DrawBackground();
                    int index;
                    if (int.TryParse(Items[e.Index].ToString(), out index) && index > -1 && index < ImageList.Images.Count)
                    {
                        //Point pt = new Point(e.Bounds.Location.X, e.Bounds.Location.Y);
                        g.DrawImage(ImageList.Images[index], e.Bounds.Location);
                    }
                    else
                    {
                        g.DrawString(Items[e.Index].ToString(), this.Font, new SolidBrush(this.ForeColor), e.Bounds.Location);
                    }
                    e.DrawFocusRectangle();
                }
            }
            base.OnDrawItem(e);
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            e.ItemHeight = ImageList.ImageSize.Height + 2;
            base.OnMeasureItem(e);
        }
        #endregion
    }
}