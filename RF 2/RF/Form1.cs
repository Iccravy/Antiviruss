using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace RF
{
    public partial class Form1 : Form
    {
        Mutex mutex_label = new Mutex();
        public Form1()
        {
            InitializeComponent();
        }

        
        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           /* string path = "C:\\Users\\user\\Desktop\\BD\\kek\\WpfApp1.exe";
            ScanObjectBuilder.main(path);*/
            
            switch (radioButton1.Checked)
            {
                case true:
                    {
                        
                        using (OpenFileDialog dlg = new OpenFileDialog())
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                mutex_label.WaitOne();
                                listBox1.Items.Add(dlg.FileName);
                                mutex_label.ReleaseMutex();
                            }
                                
                        }
                        
                    }
                    break;
            }
            switch (radioButton2.Checked)
            {
                case true:
                    {

                        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        {
                            mutex_label.WaitOne();
                            listBox1.Items.Add(folderBrowserDialog1.SelectedPath);
                            mutex_label.ReleaseMutex();
                        }
                    }
                    break;

            }
        }



        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                listBox1.Items.Add(file);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            int delet = dataGridView3.SelectedCells[0].RowIndex;
            dataGridView3.Rows.RemoveAt(delet);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int delet = dataGridView2.SelectedCells[0].RowIndex; //вот тут индекс строчки на удаление
            // функция_куда_там_надо(delet);
            dataGridView3.Rows.RemoveAt(delet);
        }

        private void dataGridView2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void dataGridView2_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                dataGridView2.Rows[0].Cells["dataGridViewTextBoxColumn4"].Value = dataGridView2.Rows.Count;
                dataGridView2.Rows[0].Cells["dataGridViewTextBoxColumn5"].Value = file;
                //что-то_там_монитор(file);
                // listBox1.Items.Add(file);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (button10.Text == "Пауза")
            {
                button10.Text = "Продолжить";
                // и тут отправляем куда надо, что реализовывает паузу 
            }
            else
            {
                button10.Text = "Пауза";
                //и тут реализовываем продолжение
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            int i = 0;
            ScanEngine insert = new ScanEngine();
            while (i < listBox1.Items.Count )
            {
                mutex_label.WaitOne();
                insert.make_Queue(Convert.ToString(listBox1.Items[i]), true);
                
                i++;
                mutex_label.ReleaseMutex();
            }
            insert.start_scan();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ScanReportsRepository scanReportsRepository = new ScanReportsRepository();
            string str = scanReportsRepository.read();
            string word = null;
            for (int i = 0; i < str.Length; i++)
            {
                while (str[i] != '\n')
                {
                    if (str[i] != '\r')
                        word = word + str[i];
                    else
                        word = word + ", ";
                    i++;
                }
                listBox2.Items.Add(word);
                word = " ";
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }
    }

}
