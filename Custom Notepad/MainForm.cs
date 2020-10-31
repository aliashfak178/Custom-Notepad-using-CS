using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Custom_Notepad
{
    public partial class MainForm : Form
    {
        private bool isFileAlredySaved;
        private bool isFileDirty;
        private string currOpenFileName;
        private FontDialog FD1 = new FontDialog();
        public MainForm()
        {
            InitializeComponent();
        }

        private void aboutUINotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All right reserved with the Auther",
                "About UI Notepad",MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
             //MainRichTextBox.Clear();
            NewFileMenu();
        }

        private void NewFileMenu()
        {
            if (isFileDirty)
            {
                DialogResult result = MessageBox.Show("Do You want to Save your changes", "File Save",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFileMenu();
                        ClrScr();
                        break;
                    case DialogResult.No:
                        ClrScr();
                        break;
                }
            }
            else
            {
                ClrScr();
            }
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;

            MessageToolStripStatusLabel.Text = "New Document is created";
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileMenu();
            
        }

        private void OpenFileMenu()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text Files(*.txt)|*.txt|Ritch Text Format(*.rft)|*.rft|Incrypt Text Format(*.inc)|*.inc";
            DialogResult result = openFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(openFile.FileName) == ".txt")
                    MainRichTextBox.LoadFile(openFile.FileName, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(openFile.FileName) == ".rft")
                    MainRichTextBox.LoadFile(openFile.FileName, RichTextBoxStreamType.RichText);

                this.Text = Path.GetFileName(openFile.FileName) + "- UI Notepad";

                isFileAlredySaved = true;
                isFileDirty = false;
                currOpenFileName = openFile.FileName;
                undoToolStripMenuItem.Enabled = false;
                redoToolStripMenuItem.Enabled = false;

                MessageToolStripStatusLabel.Text = "File is Opened";
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveAsFileMenu();
        }

        private void SaveAsFileMenu()
        {
            SaveFileDialog sfd1 = new SaveFileDialog();
            sfd1.Filter = "Text Files(*.txt)|*.txt|Ritch Text Format(*.rft)|*.rft|Incrypt Text Format(*.inc)|*.inc";
            DialogResult result = sfd1.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(sfd1.FileName) == ".txt")
                    MainRichTextBox.SaveFile(sfd1.FileName, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(sfd1.FileName) == ".rft")
                    MainRichTextBox.SaveFile(sfd1.FileName, RichTextBoxStreamType.RichText);

                this.Text = Path.GetFileName(sfd1.FileName) + "- UI NotePad";

                isFileAlredySaved = true;
                isFileDirty = false;
                currOpenFileName = sfd1.FileName;

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileMenu();
        }

        private void SaveFileMenu()
        {
            if (isFileAlredySaved)
            {
                if (Path.GetExtension(currOpenFileName) == ".txt")
                    MainRichTextBox.SaveFile(currOpenFileName, RichTextBoxStreamType.PlainText);
                if (Path.GetExtension(currOpenFileName) == ".rft")
                    MainRichTextBox.SaveFile(currOpenFileName, RichTextBoxStreamType.RichText);
                isFileDirty = false;
            }
            else
            {
                if (isFileDirty)
                {
                    SaveAsFileMenu();
                }
                else
                {
                    ClrScr();
                }
            }
        }

        private void ClrScr()
        {
            MainRichTextBox.Clear();
            this.Text = "Untitle - UI Notepad";
            isFileDirty = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
             isFileAlredySaved=false;
             isFileDirty=false;
             currOpenFileName="";
             if (Control.IsKeyLocked(Keys.CapsLock))
             {
                 CapsToolStripStatusLabel.Text = "Caps On";
             }
             else
             {
                 CapsToolStripStatusLabel.Text = "Caps OFF";
             }
        }

        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            isFileDirty = true;
            undoToolStripMenuItem.Enabled = true;
            toolStripButton7.Enabled = true;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoEditMenu();
        }

        private void UndoEditMenu()
        {
            MainRichTextBox.Undo();
            redoToolStripMenuItem.Enabled = true;
            toolStripButton7.Enabled = false;
            toolStripButton8.Enabled = true;
            undoToolStripMenuItem.Enabled = false;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RedoEditMenu();
        }

        private void RedoEditMenu()
        {
            MainRichTextBox.Redo();
            redoToolStripMenuItem.Enabled = false;
            toolStripButton7.Enabled = true;
            toolStripButton8.Enabled = false;
            undoToolStripMenuItem.Enabled = true;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectAll();
        }

        private void dateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectedText= DateTime.Now.ToString();
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font,FontStyle.Bold);
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Italic);
        }

        private void underLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Underline);
        }

        private void strikeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Strikeout);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Regular);
        }

        private void formatFontToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormatFontEditMenu();
        }

        private void FormatFontEditMenu()
        {
            FD1.ShowColor = true;
            FD1.ShowApply = true;

            FD1.Apply += new System.EventHandler(FD1_Apply);

            DialogResult result = FD1.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (MainRichTextBox.SelectionLength > 0)
                {
                    MainRichTextBox.SelectionFont = FD1.Font;
                    MainRichTextBox.SelectionColor = FD1.Color;
                }
            }
        }

        private void FD1_Apply(object sender, EventArgs e)
        {
            if (MainRichTextBox.SelectionLength > 0)
            {
                MainRichTextBox.SelectionFont = FD1.Font;
                MainRichTextBox.SelectionColor = FD1.Color;
            }
        }

        private void changeTextColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog CD1 = new ColorDialog();
            DialogResult result = CD1.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (MainRichTextBox.SelectionLength > 0)
                {
                    MainRichTextBox.SelectionColor = CD1.Color;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewFileMenu();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileMenu();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileMenu();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SaveAsFileMenu();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            UndoEditMenu();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            RedoEditMenu();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Bold);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Italic);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Underline);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Strikeout);
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            FormatFontEditMenu();
        }

        private void MainRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                CapsToolStripStatusLabel.Text = "Caps On";
            }
            else
            {
                CapsToolStripStatusLabel.Text = "Caps OFF";
            }
        }

        private void normalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Regular);
        }

        private void boldToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Bold);
        }

        private void italicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Italic);
        }

        private void underLineToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Underline);
        }

        private void strikeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Strikeout);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.Cut();
            if (MainRichTextBox.SelectionLength > 0)
            {
                Clipboard.SetText(MainRichTextBox.SelectedText);
                MainRichTextBox.SelectedText = "";
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.Copy();
            if (MainRichTextBox.SelectionLength > 0)
            {
                Clipboard.SetText(MainRichTextBox.SelectedText);
            }
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.Paste();
            if (Clipboard.ContainsText())
            {
                MainRichTextBox.SelectedText = Clipboard.GetText();
            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.Cut();
            if (MainRichTextBox.SelectionLength > 0)
            {
                Clipboard.SetText(MainRichTextBox.SelectedText);
                MainRichTextBox.SelectedText = "";
            }
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.Cut();
            if (MainRichTextBox.SelectionLength > 0)
            {
                Clipboard.SetText(MainRichTextBox.SelectedText);
                MainRichTextBox.SelectedText = "";
            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.Copy();
            if (MainRichTextBox.SelectionLength > 0)
            {
                Clipboard.SetText(MainRichTextBox.SelectedText);
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.Copy();
            if (MainRichTextBox.SelectionLength > 0)
            {
                Clipboard.SetText(MainRichTextBox.SelectedText);
            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.Paste();
            if (Clipboard.ContainsText())
            {
                MainRichTextBox.SelectedText = Clipboard.GetText();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.Paste();
            if (Clipboard.ContainsText())
            {
                MainRichTextBox.SelectedText = Clipboard.GetText();
            }
        }


    }
}
