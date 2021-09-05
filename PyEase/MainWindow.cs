using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyEase
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
			InitializeComponent();
			pages.Add(tabPage1);
			currentPage = tabPage1;
			customTabMenu1.tabs = tabs;
			customTabMenu1.AddTab(tabPage1);
			guna2Panel1.Enabled = tabPage1.IsEditable;
			SoftBlink(discordplug, Color.FromArgb(60,60,60), Color.FromArgb(200,200,200), 2000, false);
			label1.Text = label1.Text.Replace("v", typeof(Program).Assembly.GetName().Version.ToString());
		}

		List<BTabPage> pages = new List<BTabPage>();
		BTabPage currentPage;
		Python python = new Python();
		public bool isTerminalHidden = false;

		public void LoadFile(string file)
        {
			Scintilla scintilla = new Scintilla();
			SyntaxHighlighting(scintilla);
			scintilla.AutoCIgnoreCase = true;
            scintilla.Insert += (sender, e) => AutoComplete(sender, e, scintilla);
            scintilla.InsertCheck += (sender, e) => Scintilla_InsertCheck(sender, e, scintilla);
            scintilla.CharAdded += (sender, e) => scintilla1_CharAdded(sender, e, scintilla);
			scintilla.Text = File.ReadAllText(file);
			BTabPage page = new BTabPage();
			page.CodeEditor = scintilla;
			page.File = file;
			page.IsEditable = true;
			page.Text = Path.GetFileName(file);
            page.BackColor = Color.FromArgb(44, 44, 44);
			page.Controls.Add(scintilla);
			page.Index = pages.Count;
			scintilla.Dock = DockStyle.Fill;
            tabs.TabPages.Add(page);
			pages.Add(page);
			customTabMenu1.AddTab(page);
			scintilla.KeyDown += (sender, e) => scintilla1_KeyDown(sender, e, page);
			scintilla.TextChanged += (sender, e) => scintilla1_TextChanged(sender, e, page);
			tabs.SelectedIndex = tabs.TabCount - 1;
		}

		private void scintilla1_KeyDown(object sender, KeyEventArgs e, BTabPage page)
		{
			// i will work on a cleaner solution later
			if (e.Control && e.KeyCode == Keys.S)
			{
				// save file
                e.Handled = true;
				e.SuppressKeyPress = true;
				iconButton6_Click(null, null);
			}

			if (e.Control && e.KeyCode == Keys.R)
            {
				// run script
				e.Handled = true;
				e.SuppressKeyPress = true;
				iconButton1_Click(null, null);
			}

			if (e.Control && e.KeyCode == Keys.D)
            {
				// close current tab
				e.Handled = true;
				e.SuppressKeyPress = true;
				iconButton7_Click(null, null);
			}

			if (e.Control && e.KeyCode == Keys.F)
			{
				// stop python process
				e.Handled = true;
				e.SuppressKeyPress = true;
				iconButton2_Click(null, null);
			}
		}

		private void scintilla1_TextChanged(object sender, EventArgs e, BTabPage page)
		{
			if (!page.Unsaved)
				page.Unsaved = true;
		}

		private void scintilla1_CharAdded(object sender, CharAddedEventArgs e, Scintilla scintilla)
		{
		}

		private void Scintilla_InsertCheck(object sender, InsertCheckEventArgs e, Scintilla scintilla)
        {
		}

		private void AutoComplete(object sender, ModificationEventArgs e, Scintilla scintilla)
        {
			var keywords = "__init__ __name__ print False None True and as assert async await break class continue def del elif else except finally for from global if import in is lambda nonlocal not or pass raise return try while with yield";
			var currentPos = scintilla.CurrentPosition;
			var wordStartPos = scintilla.WordStartPosition(currentPos, true);
			var lengthEntered = currentPos - wordStartPos;
			if (lengthEntered > 0)
			{
				scintilla.AutoCShow(lengthEntered, keywords);
			}
		}

        void SyntaxHighlighting(Scintilla scintilla)
        {
			var keywords = "__init__ __name__ print False None True and as assert async await break class continue def del elif else except finally for from global if import in is lambda nonlocal not or pass raise return try while with yield";
			scintilla.StyleResetDefault();
			scintilla.BorderStyle = BorderStyle.None;
			scintilla.HScrollBar = false;
			scintilla.CaretForeColor = Color.White;
			scintilla.CaretWidth = 2;
			scintilla.Styles[Style.Default].Font = "Consolas";
			scintilla.Styles[Style.Default].Size = 10;
			scintilla.Styles[Style.Default].BackColor = IntToColor(0x282C34);
			scintilla.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
			scintilla.Margins[1].Width = 0;
			scintilla.SetSelectionBackColor(true, IntToColor(0x353535));
			scintilla.StyleClearAll();
			
			scintilla.Styles[Style.Python.Identifier].ForeColor = IntToColor(0xD0DAE2);
			scintilla.Styles[Style.Python.CommentLine].ForeColor = IntToColor(0xABB2BF);
			scintilla.Styles[Style.Python.Number].ForeColor = IntToColor(0xE5C07B);
			scintilla.Styles[Style.Python.String].ForeColor = IntToColor(0x98C379);
			scintilla.Styles[Style.Python.Character].ForeColor = IntToColor(0xFFF00);
			scintilla.Styles[Style.Python.Operator].ForeColor = IntToColor(0xE0E0E0);
			scintilla.Styles[Style.Python.Word].ForeColor = IntToColor(0x48A8EE);
			scintilla.Styles[Style.Python.Word2].ForeColor = IntToColor(0xE06C75);
			scintilla.Styles[Style.Python.ClassName].ForeColor = IntToColor(0xE06C75);
			scintilla.Styles[Style.Python.DefName].ForeColor = IntToColor(0x56B6C2);
			scintilla.Styles[Style.Python.StringEol].ForeColor = IntToColor(0xFF3000);
			scintilla.Styles[Style.Python.Decorator].ForeColor = IntToColor(0xf04444);

			scintilla.Lexer = Lexer.Python;
			
			scintilla.SetKeywords(1, keywords);
		}

		public static Color IntToColor(int rgb)
			=> Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);

		private void iconButton1_Click(object sender, EventArgs e)
        {
			iconButton6_Click(null, null);
			consoleControl1.InternalRichTextBox.ResetText();
			consoleControl1.InternalRichTextBox.AppendText($"> python {currentPage.File}\n\n");
			string code = currentPage.CodeEditor.Text;
			if (splitContainer1.SplitterDistance < 5)
				splitContainer1.SplitterDistance = 300;
			iconButton1.Enabled = false;
			python.RunScript(code.Split('\n'), consoleControl1, () =>
			{
				consoleControl1.InternalRichTextBox.AppendText("\n[Process exited]");
				iconButton1.Enabled = true;
			});
			

		}

		private void EndProcessTree(string imageName)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "taskkill",
				Arguments = $"/im {imageName} /f /t",
				CreateNoWindow = true,
				UseShellExecute = false
			});
		}

		private void iconButton2_Click(object sender, EventArgs e)
		{
			try
            {
				Task.Run(() =>
				{
					// python.exe usually the correct name in most cases can go first
					EndProcessTree("python.exe");

					// py.exe probably the second most common goes next
					EndProcessTree("py.exe");

					// python3.exe is a possibility too so i'll leave it here
					EndProcessTree("python3.exe");
				});
			} catch
            {
				MessageBox.Show("Failed to kill python process");
            }
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
        {
			File.WriteAllText(currentPage.File, currentPage.CodeEditor.Text);
			currentPage.Unsaved = false;
		}

		private void iconButton7_Click(object sender, EventArgs e)
        {
			customTabMenu1.RemoveTabAt(tabs.SelectedIndex);
			tabs.TabPages.RemoveAt(tabs.SelectedIndex);
		}

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
				splitContainer1.SplitterDistance = 300;
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
			OpenLink("https://discord.gg/4HTgUrzD");

        private void MainWindow_Load(object sender, EventArgs e)
        {
			foreach (string a in Program.startupFiles)
				LoadFile(a);
        }

		void OpenLink(string link)
        {
			if (MessageBox.Show($"That will open a link in your browser, continue?", link, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				Process.Start(link);
		}

        private void roundedButtonIcon1_Click(object sender, EventArgs e)
        {
			OpenLink("https://github.com/CodeDiseaseDev/PyEase");
		}
    }
}
