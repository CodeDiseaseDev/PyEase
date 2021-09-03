using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyEase
{
    public partial class PackageManager : Form
    {
        Python p = new Python();
        public PackageManager()
        {
            InitializeComponent();
            refresh();
            Show();
        }

        public void refresh()
        {
            List<Control> ctrls = new List<Control>();
            foreach (string a in p.GetModules())
            {
                PythonPackage pack = new PythonPackage();
                pack.name = a;
                ctrls.Add(pack);
            }
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Controls.AddRange(ctrls.ToArray());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Prompt prompt = new Prompt();
            prompt.content = "Enter the name of the package to install";
            prompt.ShowDialog();
            if (prompt.input != "")
            {
                p.InstallPackage(prompt.input, consoleControl1, () => Invoke((MethodInvoker)refresh));
                consoleControl1.Height = 300;
                flowLayoutPanel1.Height = ClientSize.Height - consoleControl1.Height;
            }
        }
    }
}
