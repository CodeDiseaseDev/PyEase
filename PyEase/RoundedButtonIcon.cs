using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace PyEase
{
    class RoundedButtonIcon : IconButton
    {
        public RoundedButtonIcon()
        {

        }

        private Color _backcolor = Color.FromArgb(50,50,50);

        public Color Backcolor
        {
            get => _backcolor;
            set
            {
                foreach (Control c in Controls)
                    c.BackColor = value;
                _backcolor = value;
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //pevent.Graphics.Clear(BackColor);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(BackColor);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = new Rectangle(0, 0, Width - 1, Height - 1);
            GraphicsPath path = Drawing.RoundedRect(r, 6);
            pevent.Graphics.FillPath(new SolidBrush(Backcolor), path);
            pevent.Graphics.DrawPath(new Pen(Backcolor), path);
            Image img = IconChar.ToBitmap(IconFont, IconSize, Enabled ? IconColor : Color.FromArgb(80,80,80));
            pevent.Graphics.DrawImage(img, (r.Width / 2) - (img.Width / 2) + 2, (r.Height / 2) - (img.Height / 2) + 3);
        }
    }
}
