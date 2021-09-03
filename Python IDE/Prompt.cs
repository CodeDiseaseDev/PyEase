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
    public partial class Prompt : Form
    {
        public Prompt()
        {
            InitializeComponent();
        }

        public string content
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public string input
        {
            get => guna2TextBox1.Text;
            set => guna2TextBox1.Text = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = input != "" ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                button1_Click(null, null);
            }
        }

        private void Prompt_Load(object sender, EventArgs e)
        {

        }
    }
}
