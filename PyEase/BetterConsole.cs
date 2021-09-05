using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace PyEase
{
    class BetterConsole : ConsoleControl.ConsoleControl
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public BetterConsole()
        {
            InternalRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            InternalRichTextBox.BackColor = Color.FromArgb(20, 24, 32);
            Resize += BetterConsole_Resize;
        }

        private void BetterConsole_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(1, 1, Width, Height, 6, 6));
        }
    }
}
