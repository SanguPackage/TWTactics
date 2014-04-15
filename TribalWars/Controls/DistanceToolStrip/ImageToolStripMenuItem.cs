using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TribalWars.Controls.DistanceToolStrip
{
    public class ImageToolStripMenuItem : ToolStripMenuItem
    {
        private int _Index;

        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }

        public ImageToolStripMenuItem(Image img, int index)
            : base(img)
        {
            _Index = index;
            CheckOnClick = true;
            Size = new Size(img.Size.Width + 4, img.Size.Height + 2);
        }

        public override ToolStripItemDisplayStyle DisplayStyle
        {
            get
            {
                return ToolStripItemDisplayStyle.Image;
            }
            set
            {
                base.DisplayStyle = ToolStripItemDisplayStyle.Image;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rec = e.ClipRectangle;
            rec.Inflate(-20, -2);
            rec.Offset(14, 0);
            e.Graphics.DrawImage(Image, rec);
        }
    }
}
