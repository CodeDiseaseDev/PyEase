using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyEase
{
    public partial class PythonPackage : UserControl
    {
        Python p = new Python();
        public PythonPackage()
        {
            InitializeComponent();
            Backcolor = _backcolor;

            foreach (Control c in Controls)
            {
                if (c.Name == "iconButton1")
                    continue;
                c.MouseEnter += PythonPackage_MouseEnter;
                c.MouseLeave += PythonPackage_MouseLeave;
            }
        }

        private Color _backcolor = Color.FromArgb(44, 44, 44);

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

        public string name
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = Drawing.RoundedRect(new Rectangle(0,0,Width-1,Height-1), 10);
            e.Graphics.FillPath(new SolidBrush(Backcolor), path);
            e.Graphics.DrawPath(new Pen(Backcolor), path);
        }

        private void PythonPackage_MouseEnter(object sender, EventArgs e)
        {
            //if (!iconButton1.Visible)
            //    iconButton1.Visible = true;
        }

        private void PythonPackage_MouseLeave(object sender, EventArgs e)
        {
            //if (iconButton1.Visible)
            //    iconButton1.Visible = false;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
        }

        private void iconButton1_MouseUp(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to delete {name}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                p.DeletePackage(name);
                ((PackageManager)FindForm()).refresh();
            }
        }
    }
}
