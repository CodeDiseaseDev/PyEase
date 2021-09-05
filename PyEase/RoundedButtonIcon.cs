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
            MouseEnter += RoundedButtonIcon_MouseEnter;
            MouseLeave += RoundedButtonIcon_MouseLeave;

            MouseDown += RoundedButtonIcon_MouseDown;
            MouseUp += RoundedButtonIcon_MouseUp;
        }

        private void RoundedButtonIcon_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            Invalidate();
        }

        private void RoundedButtonIcon_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            Invalidate();
        }

        private void RoundedButtonIcon_MouseLeave(object sender, EventArgs e)
        {
            MouseOver = false;
        }

        private void RoundedButtonIcon_MouseEnter(object sender, EventArgs e)
        {
            MouseOver = true;
        }

        private Color _backcolor = Color.FromArgb(50,50,50);
        private bool MouseOver = false;
        private bool IsMouseDown = false;

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

            Color back = Backcolor;
            if (MouseOver) back = Drawing.Brighten(back, 10);
            else if (IsMouseDown) back = Drawing.Darken(back, 20); // This code needs to be fixed at some point

            pevent.Graphics.FillPath(new SolidBrush(back), path);
            pevent.Graphics.DrawPath(new Pen(back), path);

            Image img = IconChar.ToBitmap(IconFont, IconSize, Enabled ? IconColor : Color.FromArgb(80, 84, 92));
            pevent.Graphics.DrawImage(img, (r.Width / 2) - (img.Width / 2) + 2, (r.Height / 2) - (img.Height / 2) + 3);
        }
    }
}
