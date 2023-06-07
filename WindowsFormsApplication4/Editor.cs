using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public class Editor
    {
        public bool unsaved = false;
        public string filename = "Безымянный";
        public string content = string.Empty;
        RichTextBoxStreamType stream_type;

        public void ToDefaultStyle(System.Windows.Forms.RichTextBox textBox)
        {
            textBox.Font = new Font("Microsoft Sans Serif", 8);
            textBox.BackColor = Color.White;
            textBox.ForeColor = Color.Black;
        }




        public void ToEmptyContent(System.Windows.Forms.RichTextBox textBox, ToolStripLabel status)
        {
            textBox.Clear();
            unsaved = false;
            filename = "Безымянный";
            content = string.Empty;
            status.Text = "Статус: Создан пустой файл";
        }

        public void ChangeForeColor(System.Windows.Forms.RichTextBox textBox)
        {
            ColorDialog myColor = new ColorDialog();
            if (myColor.ShowDialog() == DialogResult.OK)
            {
                textBox.ForeColor = myColor.Color;
            }
        }

        public void ChangeBackColor(System.Windows.Forms.RichTextBox textBox)
        {
            ColorDialog myColor = new ColorDialog();
            if (myColor.ShowDialog() == DialogResult.OK)
            {
                textBox.BackColor = myColor.Color;
            }
        }

        public void Opening(System.Windows.Forms.RichTextBox textBox1, ToolStripLabel status)
        {
            OpenFileDialog file_open = new OpenFileDialog();
            file_open.Filter = "Text File (*.txt)|*.txt|Rich Text File (*.rtf)|*.rtf ";
            file_open.FilterIndex = 1;
            file_open.Title = "Open text or RTF file";

            
            stream_type = RichTextBoxStreamType.RichText;
            if (DialogResult.OK == file_open.ShowDialog())
            {
                
                if (string.IsNullOrEmpty(file_open.FileName))
                    return;
                if (file_open.FilterIndex == 2)
                  
                  stream_type = RichTextBoxStreamType.PlainText;
              
                try
                {
                   
            
                    filename = file_open.FileName;
                    content = File.ReadAllText(filename);
                    textBox1.Text= content ;
                    unsaved = false;
                    status.Text = "Статус: Файл открыт";
                }
                catch
                {
                    status.Text = "Статус: Не удалось открыть файл";
                }
            }
        }

        public void Saving(System.Windows.Forms.RichTextBox textBox1, ToolStripLabel status)
        {
            textBox1.SaveFile(filename, stream_type);
            content = textBox1.Text;
            unsaved = false;
            status.Text = "Статус: Файл сохранен";
        }


        public void SavingAs(System.Windows.Forms.RichTextBox textBox1, ToolStripLabel status)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "Text File (*.txt)|*.txt|Rich Text File (*.rtf)|*.rtf";
            saveDlg.DefaultExt = "*.txt";
            saveDlg.FilterIndex = 1;
            saveDlg.Title = "Save the contents";
            DialogResult retval = saveDlg.ShowDialog();
            if (retval == DialogResult.OK)
                filename = saveDlg.FileName;
            else
                return;
            if (saveDlg.FilterIndex == 2)
                stream_type = RichTextBoxStreamType.PlainText;
            else
                stream_type = RichTextBoxStreamType.RichText;
            textBox1.SaveFile(filename, stream_type);
            content = textBox1.Text;
            unsaved = false;
            status.Text = "Статус: Файл сохранен";
        }
        public void ChangeFont(System.Windows.Forms.RichTextBox textBox1)
        {
            FontDialog myFont = new FontDialog();

            if (myFont.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = myFont.Font;
            }
        }
    }
}

