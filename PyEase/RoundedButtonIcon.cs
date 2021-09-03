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

        private Color _backcolor = Color.FromArgb(50, 50, 50);

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

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = Drawing.RoundedRect(new Rectangle(0, 0, Width - 1, Height - 1), 10);
            pevent.Graphics.FillPath(new SolidBrush(Backcolor), path);
            pevent.Graphics.DrawPath(new Pen(Backcolor), path);

            base.OnPaint(pevent);
        }
    }
}
