using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Python_IDE
{
    public partial class NoPython : Form
    {
        public NoPython()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.python.org/ftp/python/3.9.7/python-3.9.7-amd64.exe");
            MessageBox.Show("Install python and restart this application");
        }
    }
}
