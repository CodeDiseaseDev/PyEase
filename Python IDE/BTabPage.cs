using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace Python_IDE
{
    class BTabPage : TabPage
    {
        public Scintilla CodeEditor;
        private string file;
        public string File
        {
            get => file;
            set
            {
                file = value;
                Text = Path.GetFileName(value);
            }
        }
        public bool IsEditable
        {
            get;
            set;
        }
    }
}
