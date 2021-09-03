using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyEase
{
    static class Program
    {
        public static List<string> startupFiles = new List<string>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            startupFiles.AddRange(args.Where(e => File.Exists(e) && Path.GetExtension(e) == ".py"));
            Python p = new Python();
            if (!p.IsPythonInstalled())
            {
                new NoPython().ShowDialog();
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
