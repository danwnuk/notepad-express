using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
namespace NotepadExpress
{
    public partial class NotepadExpress : Form
    {
        public List<Editor> editors = new List<Editor>();
        public int newFileCount = 1;
        public int bgCurIndex = 0;
        private Boolean closeFile = false;
        private Font verdana10Font;
        private StreamReader reader;
        [DllImport("user32.dll")]
        extern static int SendMessage(IntPtr hwnd, int message, int wparam, int lparam);
        private static int EM_LINEINDEX = 0xbb;

        public NotepadExpress()
        {
            InitializeComponent();
            editors.Add(this.editor1);
            ToolStripMenuItem mu = new ToolStripMenuItem();
            mu.Text = "new file";
            mu.Click += new EventHandler(mu_Click);
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mu });
            editor1.TextEditor.TextChanged += new EventHandler(this.textChanged);
            editor1.TextEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            editor1.TextEditor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp);
            editor1.TextEditor.MouseDown += new MouseEventHandler(this.MouseDown);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == this.tabControl1.TabPages.Count-1 && !closeFile)
            {
                NewTab();
            }
        }

        private void NewTab()
        {
            newFileCount++;
            TabPage tp = new TabPage("new file (" + newFileCount + ")");
            Editor ed = new Editor();
            ed.Dock = System.Windows.Forms.DockStyle.Fill;
            ed.Location = new System.Drawing.Point(3, 3);
            ed.Size = new System.Drawing.Size(818, 787);
            ed.TextEditor.TextChanged += new EventHandler(this.textChanged);
            ed.TextEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            ed.TextEditor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp);
            ed.TextEditor.MouseDown += new MouseEventHandler(this.MouseDown);
            editors.Add(ed);
            tp.Controls.Add(ed);
            this.tabControl1.TabPages.Insert(this.tabControl1.TabPages.Count - 1, tp);
            this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 2;
            ToolStripMenuItem mu = new ToolStripMenuItem();
            mu.Text = "new file (" + newFileCount + ")";
            mu.Click += new EventHandler(mu_Click);
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mu });
        }
        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.DefaultExt = ".txt";
            ofd.InitialDirectory = "C:";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName);
                String line = sr.ReadLine();
                while (line != null)
                {
                    editors[tabControl1.SelectedIndex].TextEditor.AppendText(line + Environment.NewLine);
                    line = sr.ReadLine();
                }
                tabControl1.SelectedTab.Text = ofd.FileName;
            }
        }

        private void SaveFile()
        {
            if (tabControl1.SelectedTab.Text.StartsWith("new file"))
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.DefaultExt = ".txt";
                sfd.InitialDirectory = "C:";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName);
                    sw.Write(editors[tabControl1.SelectedIndex].TextEditor.Text);
                    sw.Close();
                    tabControl1.SelectedTab.Text = sfd.FileName;
                }
            }
            else
            {
                StreamWriter sw = new StreamWriter(tabControl1.SelectedTab.Text);
                sw.Write(editors[tabControl1.SelectedIndex].TextEditor.Text);
                sw.Close();
            }
        }

        private void CloseFile()
        {
            SaveFile();
            this.closeFile = true;
            windowToolStripMenuItem.DropDownItems.Remove(windowToolStripMenuItem.DropDownItems[tabControl1.SelectedIndex]);
            tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
            this.closeFile = false;
        }
        private void CloseAll()
        {
            for (int i = 0; i < tabControl1.TabCount - 1; i++)
            {
                tabControl1.SelectedIndex = i;
                CloseFile();
            }
        }
        private void SaveAll()
        {
            int index = tabControl1.SelectedIndex;
            for (int i = 0; i < tabControl1.TabCount - 1; i++)
            {
                tabControl1.SelectedIndex = i;
                SaveFile();
            }
            tabControl1.SelectedIndex = index;
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void textChanged(object sender, EventArgs e)
        {
            UpdateCaretPosition();  
            if(editors[this.tabControl1.SelectedIndex].TextEditor.TextLength % 75 == 0){
                if(!tabControl1.SelectedTab.Text.StartsWith("new file"))
                {
                    StreamWriter sw = new StreamWriter(tabControl1.SelectedTab.Text);
                    sw.Write(editors[tabControl1.SelectedIndex].TextEditor.Text);
                    sw.Close();
                }
            }
        }

        private void KeyUp(object sender, EventArgs e)
        {
            UpdateCaretPosition();
        }

        private void KeyDown(object sender, EventArgs e)
        {
            UpdateCaretPosition();
        }

        private void MouseDown(object sender, EventArgs e)
        {
            UpdateCaretPosition();
        }

        private void UpdateCaretPosition()
        {
            Editor currentEditor = editors[this.tabControl1.SelectedIndex];
            int line, col, index;
            index = currentEditor.TextEditor.SelectionStart;
            line = currentEditor.TextEditor.GetLineFromCharIndex(index);
            col = index - SendMessage(currentEditor.TextEditor.Handle, EM_LINEINDEX, -1, 0);
            this.toolStripStatusLabelCursorData.Text = (++line).ToString() + ", " + (++col).ToString();
        }

        private void toolStripButtonCut_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void toolStripButtonPaste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            Redo();
        }

        private void Cut()
        {
            editors[this.tabControl1.SelectedIndex].TextEditor.Cut();
        }

        private void Copy()
        {
            editors[this.tabControl1.SelectedIndex].TextEditor.Copy();
        }

        private void Paste()
        {
            editors[this.tabControl1.SelectedIndex].TextEditor.Paste();
        }

        private void Undo()
        {
            editors[this.tabControl1.SelectedIndex].TextEditor.Undo();
        }

        private void Redo()
        {
            editors[this.tabControl1.SelectedIndex].TextEditor.Redo();
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            Print();
        }


        private void Print()
        {
            if (tabControl1.SelectedTab.Text.StartsWith("new file"))
            {
                SaveFile();
            }
            if (!tabControl1.SelectedTab.Text.StartsWith("new file"))
            {
                //Create a StreamReader object
                reader = new StreamReader(tabControl1.SelectedTab.Text);
                //Create a Verdana font with size 10
                verdana10Font = new Font("Verdana", 10);
                //Create a PrintDocument object
                PrintDocument pd = new PrintDocument();
                //Add PrintPage event handler
                pd.PrintPage += new PrintPageEventHandler(this.PrintTextFileHandler);
                PrintDialog print = new PrintDialog();
                print.ShowHelp = true;
                print.AllowSomePages = true;
                print.Document = pd;
                if (print.ShowDialog() == DialogResult.OK)
                {
                    //Call Print Method
                    pd.Print();
                }
                //Close the reader
                if (reader != null)
                    reader.Close();
            }
        }
        private void PrintTextFileHandler(object sender, PrintPageEventArgs ppeArgs)
        {
            //Get the Graphics object
            Graphics g = ppeArgs.Graphics;
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            //Read margins from PrintPageEventArgs
            float leftMargin = ppeArgs.MarginBounds.Left;
            float topMargin = ppeArgs.MarginBounds.Top;
            string line = null;
            //Calculate the lines per page on the basis of the height of the page and the height of the font
            linesPerPage = ppeArgs.MarginBounds.Height /
            verdana10Font.GetHeight(g);
            //Now read lines one by one, using StreamReader
            while (count < linesPerPage &&
            ((line = reader.ReadLine()) != null))
            {
                //Calculate the starting position
                yPos = topMargin + (count *
                verdana10Font.GetHeight(g));
                //Draw text
                g.DrawString(line, verdana10Font, Brushes.Black,
                leftMargin, yPos, new StringFormat());
                //Move to next line
                count++;
            }
            //If PrintPageEventArgs has more pages to print
            if (line != null)
            {
                ppeArgs.HasMorePages = true;
            }
            else
            {
                ppeArgs.HasMorePages = false;
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAll();
        }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void toolStripButtonSaveAll_Click(object sender, EventArgs e)
        {
            SaveAll();
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            NewTab();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTab();
        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            CloseFile();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.Count - 1)
            {
                NewTab();
            }
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAll();
        }

        private void toolStripButtonCloseAll_Click(object sender, EventArgs e)
        {
            CloseAll();
        }


        public void mu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            for (int i = 0; i < tabControl1.TabPages.Count - 1; i++)
            {
                if (tabControl1.TabPages[i].Text.Equals(item.Text))
                {
                    tabControl1.SelectedIndex = i;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAll();
            Application.Exit();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preferences p = new Preferences(this);
            p.Show();
        }
    }
}