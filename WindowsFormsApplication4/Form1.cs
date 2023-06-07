using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text File(*.txt)|*.txt";
           
        }
        Editor editor = new Editor();

        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (editor.content == richTextBox1.Text)
            {
                editor.unsaved = false;
                this.Text = $"{editor.filename} - Текстовый Редактор";
                if (editor.filename != "Безымянный") toolStripStatusLabel.Text = "Статус: Файл сохранён";
                else toolStripStatusLabel.Text = "Статус: Создан пустой файл";
            }
            else
            {
                editor.unsaved = true;
                this.Text = $"*{editor.filename} - Текстовый Редактор";
                toolStripStatusLabel.Text = "Статус: Файл изменён";
            }
            toolStripLengthLabel.Text = $"Символов: {richTextBox1.TextLength}";
        }

        public void toolStripStatusLabel_TextChanged(object sender, EventArgs e)
        {
            if (editor.content == richTextBox1.Text)
            {
                this.Text = $"{editor.filename} - Текстовый Редактор";
            }
            else
            {
                this.Text = $"*{editor.filename} - Текстовый Редактор";
            }
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!editor.unsaved)
            {
                editor.ToDefaultStyle(richTextBox1);
                editor.ToEmptyContent(richTextBox1, toolStripStatusLabel);
            }
            else
            {
                DialogResult result = MessageBox.Show($"Вы хотите сохранить изменения в файле: {editor.filename}?", "Блокнот", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.No:
                        editor.ToDefaultStyle(richTextBox1);
                        editor.ToEmptyContent(richTextBox1, toolStripStatusLabel);
                        break;
                    case DialogResult.Yes:
                      
                        saveToolStripMenuItem.PerformClick();
                        editor.ToDefaultStyle(richTextBox1);
                        editor.ToEmptyContent(richTextBox1, toolStripStatusLabel);
                        break;
                }
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editor.unsaved)
            {
                DialogResult result = MessageBox.Show($"Вы хотите сохранить изменения в файле: {editor.filename}?", "Блокнот", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        saveToolStripMenuItem.PerformClick();
                        richTextBox1.Clear();
                        break;
                }
                if (result != DialogResult.Cancel)
                {
                    editor.ToDefaultStyle(richTextBox1);
                    editor.Opening(richTextBox1, toolStripStatusLabel);
                }
            }
            if (!editor.unsaved)
            {
                editor.ToDefaultStyle(richTextBox1);
                editor.Opening(richTextBox1, toolStripStatusLabel);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (editor.filename == "Безымянный")
            {
                saveAsToolStripMenuItem.PerformClick();
            }
            else
            {
                editor.Saving(richTextBox1, toolStripStatusLabel);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SavingAs(richTextBox1, toolStripStatusLabel);
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Focused)
                editor.ChangeFont(richTextBox1);
            else
            {
                editor.ChangeFont(richTextBox1);
            }
        }


        private void цветФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Focused)
                editor.ChangeBackColor(richTextBox1);
            else
            {
                editor.ChangeBackColor(richTextBox1);
            }
        }
        private void цветТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Focused)
                editor.ChangeForeColor(richTextBox1);
            else
            {
                editor.ChangeForeColor(richTextBox1);
            }
        }


        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                richTextBox1.ContextMenuStrip = contextMenuStrip1;
            }
        }


        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.TextLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                richTextBox1.Paste();
            
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Cut();
            }
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.SelectAll();
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!editor.unsaved)
            {
                e.Cancel = false;
            }
            else
            {
                DialogResult result = MessageBox.Show($"Вы хотите сохранить изменения в файле: {editor.filename}?", "Блокнот", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        break;

                    case DialogResult.No:
                        break;

                    case DialogResult.Yes:
                        saveToolStripMenuItem.PerformClick();
                        break;
                }
                e.Cancel = (result == DialogResult.Cancel);
            }
        }
     private void разработчикToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Студент группы ЦПИ-21 Андреев Егор Сергеевич");
        }


    }
}
