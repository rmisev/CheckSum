using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CheckSum
{
    public partial class FormSum : Form
    {
        public FormSum()
        {
            HashAlgorithmsCollection.Init();
            
            InitializeComponent();
            // drag & drop
            this.textBoxFile.AllowDrop = true;
            this.textBoxFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.textBoxFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_DragEnter);
            // hash algorithms
            foreach (string key in HashAlgorithmsCollection.Dic.Keys)
            {
                this.comboBoxAlgoritmas.Items.Add(key);
            }
            this.comboBoxAlgoritmas.SelectedIndex = 0;
        }

        // http://stackoverflow.com/questions/1370538/drag-files-or-folders-in-textbox-c-sharp
        private void textBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileNames.Length == 1)
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                    textBox.Text = fileNames[0];
            }
            else
                MessageBox.Show("Meskite tik vieną failą!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = this.textBoxFile.Text;
            if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
                this.textBoxFile.Text = this.openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string key = this.comboBoxAlgoritmas.SelectedItem.ToString();

                using (FileStream fs = File.OpenRead(this.textBoxFile.Text))
                using (HashAlgorithm hash = HashAlgorithmsCollection.Create(key))
                {
                    byte[] arrRes = hash.ComputeHash(fs);
                    StringBuilder strb = new StringBuilder(arrRes.Length * 2);
                    for (int i = 0; i < arrRes.Length; i++)
                    {
                        strb.Append(String.Format("{0:x2}", arrRes[i]));
                    }
                    this.textBoxSum.Text = strb.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
