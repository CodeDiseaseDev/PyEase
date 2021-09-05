using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace PyEase
{
    class BTabPage : TabPage
    {
        public Scintilla CodeEditor;
        private string file;
        public Rectangle TabRectangle;
        public int Index;
        public string File
        {
            get => file;
            set
            {
                if (value == null)
                    return;
                file = value;
                Text = Path.GetFileName(value);
            }
        }
        public bool IsEditable
        {
            get;
            set;
        }
        public bool Unsaved
        {
            set
            {
                if (value && !Text.EndsWith("*"))
                    Text += "*";
                else if (!value && Text.EndsWith("*"))
                    Text = Text.Substring(0, Text.Length - 1);
            }
            get => Text.EndsWith("*");
        }
    }
}
