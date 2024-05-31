using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace РМ3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
           
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                int a = 0;
                try
                {
                    if (textBox1.Text.Trim().Length == 0)
                    {
                        throw new Exception("Не заполнено поле 'Название предприятия'");
                    }
                    a++;
                    if (textBox2.Text.Trim().Length == 0)
                    {
                        throw new Exception("Не заполнено поле 'ФИО Сотрудника'");
                    }
                    a++;
                    if (numericUpDown1.Value == 0)
                    {
                        throw new Exception("Заработная плата не может быть нулевой");
                    }
                }
                catch (Exception ex)
                {
                    if (a == 0)
                    {
                        textBox1.Focus();
                    }
                    else if (a == 1)
                    {
                        textBox2.Focus();
                    }
                    else if (a == 2)
                    {
                        numericUpDown1.Focus();
                    }
                    MessageBox.Show(ex.Message);
                    e.Cancel = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

           if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string patch = openFileDialog1.FileName;
                FileInfo fileInfo = new FileInfo(patch);
                string newpatch = @"Фото/" + fileInfo.Name;
                label5.Text = fileInfo.Name;
                // pictureBox1.Load("Фото/" + fileInfo.Name);

                if (fileInfo.Exists)
                {
                    fileInfo.CopyTo(newpatch, true);
                    pictureBox1.Load(@"Фото/" + fileInfo.Name);
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "сотрудники_предприятияDataSet9.Пол". При необходимости она может быть перемещена или удалена.
         //   this.полTableAdapter.Fill(this.сотрудники_предприятияDataSet1.Пол);
            

        }

        private void Form2_Load_1(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "сотрудники_предприятияDataSet3.Пол". При необходимости она может быть перемещена или удалена.
            this.полTableAdapter.Fill(this.сотрудники_предприятияDataSet3.Пол);

        }
    }
}