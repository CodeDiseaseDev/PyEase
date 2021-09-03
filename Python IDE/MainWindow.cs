using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Python_IDE
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
			InitializeComponent();
			pages.Add(tabPage1);
			currentPage = tabPage1;
			guna2Panel1.Enabled = tabPage1.IsEditable;
			SoftBlink(discordplug, Color.FromArgb(60,60,60), Color.FromArgb(200,200,200), 2000, false);
		}

		List<BTabPage> pages = new List<BTabPage>();
		BTabPage currentPage;
		Python python = new Python();
		public bool isTerminalHidden = false;

		public void LoadFile(string file)
        {
			Scintilla scintilla = new Scintilla();
			SyntaxHighlighting(scintilla);
			scintilla.Text = File.ReadAllText(file);
			BTabPage page = new BTabPage();
			page.CodeEditor = scintilla;
			page.File = file;
			page.IsEditable = true;
			page.Text = Path.GetFileName(file);
            page.BackColor = Color.FromArgb(44, 44, 44);
			page.Controls.Add(scintilla);
			scintilla.Dock = DockStyle.Fill;
            tabs.TabPages.Add(page);
			pages.Add(page);
			tabs.SelectedIndex = tabs.TabCount - 1;
		}

		void SyntaxHighlighting(Scintilla scintilla)
        {
			scintilla.StyleResetDefault();
			scintilla.BorderStyle = BorderStyle.None;
			scintilla.HScrollBar = false;
			scintilla.CaretForeColor = Color.White;
			scintilla.CaretWidth = 2;
			scintilla.Styles[Style.Default].Font = "Consolas";
			scintilla.Styles[Style.Default].Size = 10;
			scintilla.Styles[Style.Default].BackColor = Color.FromArgb(44, 44, 44);
			scintilla.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
			scintilla.Margins[1].Width = 0;
			scintilla.SetSelectionBackColor(true, IntToColor(0x353535));
			scintilla.StyleClearAll();
			
			scintilla.Styles[Style.Python.Identifier].ForeColor = IntToColor(0xD0DAE2);
			scintilla.Styles[Style.Python.CommentLine].ForeColor = IntToColor(0x40BF57);
			scintilla.Styles[Style.Python.Number].ForeColor = IntToColor(0xFFFF00);
			scintilla.Styles[Style.Python.String].ForeColor = IntToColor(0xFFFF00);
			scintilla.Styles[Style.Python.Character].ForeColor = IntToColor(0xFFFF00);
			scintilla.Styles[Style.Python.Operator].ForeColor = IntToColor(0xE0E0E0);
			scintilla.Styles[Style.Python.Word].ForeColor = IntToColor(0x48A8EE);
			scintilla.Styles[Style.Python.Word2].ForeColor = IntToColor(0xF98906);
			scintilla.Styles[Style.Python.ClassName].ForeColor = IntToColor(0x48A8EE);
			scintilla.Styles[Style.Python.DefName].ForeColor = IntToColor(0x48A8EE);
			scintilla.Styles[Style.Python.StringEol].ForeColor = IntToColor(0xFF3000);
			scintilla.Styles[Style.Python.Decorator].ForeColor = IntToColor(0xf04444);

			scintilla.Lexer = Lexer.Python;

			scintilla.SetKeywords(1, "__init__ __name__ print False None True and as assert async await break class continue def del elif else except finally for from global if import in is lambda nonlocal not or pass raise return try while with yield");
		}

		public static Color IntToColor(int rgb)
			=> Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);

		private void iconButton1_Click(object sender, EventArgs e)
        {
			consoleControl1.InternalRichTextBox.ResetText();
			consoleControl1.InternalRichTextBox.AppendText($"> python {currentPage.File}\n\n");
			string code = currentPage.CodeEditor.Text;
			if (splitContainer1.SplitterDistance < 5)
				splitContainer1.SplitterDistance = 300;
			python.RunScript(code.Split('\n'), consoleControl1, () =>
			{
				consoleControl1.InternalRichTextBox.AppendText("\n[Process exited]");
				iconButton1.Enabled = true;
			});
			iconButton1.Enabled = false;
		}

        private void iconButton2_Click(object sender, EventArgs e)
        {
			foreach (Process p in Process.GetProcessesByName("python"))
				p.Kill();
        }

        private void iconButton3_Click(object sender, EventArgs e)
			=> new PackageManager();

		private void iconButton4_Click(object sender, EventArgs e)
        {
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Python Scripts|*.py";
			if (dialog.ShowDialog() == DialogResult.OK)
				LoadFile(dialog.FileName);
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
			currentPage = pages[tabs.SelectedIndex];
			guna2Panel1.Enabled = currentPage.IsEditable;
		}

        private void iconButton5_Click(object sender, EventArgs e)
        {
			Prompt prompt = new Prompt();
			prompt.content = "Choose a file name";
			if (prompt.ShowDialog() == DialogResult.OK)
            {
				if (!prompt.input.EndsWith(".py"))
					prompt.input = prompt.input + ".py";
				if (File.Exists(prompt.input))
					if (MessageBox.Show($"'{prompt.input}' already exists, do you wish to overwrite it?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
						return;
				File.WriteAllBytes(prompt.input, new byte[] { });
				LoadFile(prompt.input);
			}
        }

        private void iconButton6_Click(object sender, EventArgs e)
			=> File.WriteAllText(currentPage.File, currentPage.CodeEditor.Text);

		private void iconButton7_Click(object sender, EventArgs e)
			=> tabs.TabPages.RemoveAt(tabs.SelectedIndex);

		private void iconButton8_Click(object sender, EventArgs e)
        {
			// Even tho not the best implementation good enough
			isTerminalHidden = true;
			splitContainer1.SplitterDistance = splitContainer1.Height;
		}

        private void iconButton9_Click_1(object sender, EventArgs e)
        {
			if (isTerminalHidden)
			{
				splitContainer1.SplitterDistance = 500;
				isTerminalHidden = false;
			}
		}

		private async void SoftBlink(Control ctrl, Color c1, Color c2, short CycleTime_ms, bool BkClr)
		{
			// this is some really fancy code from github
			// its fancy because of the stopwatch :o
			var sw = new Stopwatch();
			sw.Start();
			short halfCycle = (short)Math.Round(CycleTime_ms * 0.5);
			while (true)
			{
				await Task.Delay(1);
				var n = sw.ElapsedMilliseconds % CycleTime_ms;
				var per = (double)Math.Abs(n - halfCycle) / halfCycle;
				var red = (short)Math.Round((c2.R - c1.R) * per) + c1.R;
				var grn = (short)Math.Round((c2.G - c1.G) * per) + c1.G;
				var blw = (short)Math.Round((c2.B - c1.B) * per) + c1.B;
				var clr = Color.FromArgb(red, grn, blw);
				if (BkClr) ctrl.BackColor = clr; else ctrl.ForeColor = clr;
			}
		}

		private void iconButton10_Click(object sender, EventArgs e)
			=> // open the link in the browser
			Process.Start("https://discord.gg/4HTgUrzD");
	}
}
