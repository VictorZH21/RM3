using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;


namespace РМ3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void Load_Data()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=WIN-JO0SOU9O4VS\\SQLEXPRESS;Initial Catalog=Сотрудники_предприятия;Integrated Security=True");
            sqlConnection.Open();
            string sql = @"SELECT
                        *
                    FROM
                        [dbo].[Сотрудники]
                    INNER JOIN
                        [dbo].[Пол] ON[Сотрудники].[ID_Пол] = [Пол].[ID]
                    INNER JOIN
                        [dbo].[Отделы] ON [Сотрудники].[ID_Отделы] = [Отделы].[ID]
                    INNER JOIN
                        [dbo].[Должность] ON[Сотрудники].[ID_Должность] = [Должность].[ID]";
                  
            SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            sqlConnection.Close();
            
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Название предприятия";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].HeaderText = "ФИО сотрудника";
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].HeaderText = "Зарплата";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Фото";
            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[9].HeaderText = "Пол";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[11].HeaderText = "Отдел";
            dataGridView1.Columns[11].Width = 150;
            dataGridView1.Columns[13].HeaderText = "Должность";
            dataGridView1.Columns[13].Width = 150;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "сотрудники_предприятияDataSet2.Должность". При необходимости она может быть перемещена или удалена.
            this.должностьTableAdapter.Fill(this.сотрудники_предприятияDataSet2.Должность);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "сотрудники_предприятияDataSet1.Сотрудники". При необходимости она может быть перемещена или удалена.
            this.сотрудникиTableAdapter.Fill(this.сотрудники_предприятияDataSet1.Сотрудники);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "сотрудники_предприятияDataSet10.Сотрудники". При необходимости она может быть перемещена или удалена.
/* this.сотрудникиTableAdapter.Fill(this.сотрудники_предприятияDataSet10.Сотрудники);*/
            Load_Data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            SqlConnection sqlConnection = new SqlConnection("Data Source=WIN-JO0SOU9O4VS\\SQLEXPRESS;Initial Catalog=Сотрудники_предприятия;Integrated Security=True");
            sqlConnection.Open();
            string sql = @"SELECT
                        *
                    FROM
                        [dbo].[Пол]";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            sqlConnection.Close();
            f.comboBox1.DataSource = dataSet.Tables[0];
            f.comboBox1.DisplayMember = "Название";
            f.comboBox1.ValueMember = "ID";

            sqlConnection.Open();
            string sql1 = @"SELECT
                        *
                    FROM
                        [dbo].[Отделы]";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, sqlConnection);
            DataSet dataSet1 = new DataSet();
            adapter1.Fill(dataSet1);
            sqlConnection.Close();
            f.comboBox2.DataSource = dataSet1.Tables[0];
            f.comboBox2.DisplayMember = "Название_отделов";
            f.comboBox2.ValueMember = "ID";

            sqlConnection.Open();
            string sql2 = @"SELECT
                        *
                    FROM
                        [dbo].[Должность]";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, sqlConnection);
            DataSet dataSet2 = new DataSet();
            adapter2.Fill(dataSet2);
            sqlConnection.Close();
            f.comboBox3.DataSource = dataSet2.Tables[0];
            f.comboBox3.DisplayMember = "Название_должности";
            f.comboBox3.ValueMember = "ID";
            if (f.ShowDialog() == DialogResult.OK)
            {
                string photoPath = "0.jpg";
                if (!string.IsNullOrWhiteSpace(f.label5.Text))
                {
                    photoPath = f.label5.Text;
                    Console.WriteLine(photoPath);
                }
                sqlConnection.Open();
                sql = "INSERT INTO [dbo].[Сотрудники] ([Название_предприятия], [ФИО_сотрудников], [Зарплата], [ID_Пол], [ID_Отделы], [ID_Должность], [Фото])" +
                    "VALUES ('" + f.textBox1.Text + "', '" + f.textBox2.Text + "', '" + f.numericUpDown1.Value  + "', '"
                    + f.comboBox1.SelectedValue + "', '" + f.comboBox2.SelectedValue + "'," + f.comboBox3.SelectedValue + ", '" + photoPath+ "')";

                SqlCommand command = new SqlCommand(sql, sqlConnection);
                command.ExecuteNonQuery();
                sqlConnection.Close();
                sqlConnection.Open();

                sql = @"SELECT
                        *
                    FROM
                        [dbo].[Сотрудники]
                    INNER JOIN
                        [dbo].[Пол] ON[Сотрудники].[ID_Пол] = [Пол].[ID]
                    INNER JOIN
                        [dbo].[Отделы] ON [Сотрудники].[ID_Отделы] = [Отделы].[ID]
                    INNER JOIN
                        [dbo].[Должность] ON[Сотрудники].[ID_Должность] = [Должность].[ID]";
                adapter = new SqlDataAdapter(sql, sqlConnection);
                dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
                sqlConnection.Close();

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Название предприятия";
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].HeaderText = "ФИО сотрудника";
                dataGridView1.Columns[2].Width = 180;
                dataGridView1.Columns[3].HeaderText = "Зарплата";
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].HeaderText = "Фото";
                dataGridView1.Columns[4].Width = 50;
                dataGridView1.Columns[9].HeaderText = "Пол";
                dataGridView1.Columns[9].Width = 100;
                dataGridView1.Columns[11].HeaderText = "Отдел";
                dataGridView1.Columns[11].Width = 150;
                dataGridView1.Columns[13].HeaderText = "Должность";
                dataGridView1.Columns[13].Width = 150;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                int n = dataGridView1.CurrentCell.RowIndex;
                SqlConnection sqlConnection = new SqlConnection("Data Source=WIN-JO0SOU9O4VS\\SQLEXPRESS;Initial Catalog=Сотрудники_предприятия;Integrated Security=True");
                sqlConnection.Open();
                string sql = "DELETE [dbo].[Сотрудники] where [ID] =" + Convert.ToInt32(dataGridView1[0, n].Value);
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                sqlConnection.Close();
                sqlConnection.Open();
                SqlCommand command = new SqlCommand(sql, sqlConnection);
                command.ExecuteNonQuery();
                sqlConnection.Close();
                sqlConnection.Open();
                sql = "select * from [dbo].[Сотрудники] INNER JOIN [dbo].[Пол] ON [Сотрудники].[ID_Пол] = [Пол].[ID] INNER JOIN [dbo].[Отделы] ON [Сотрудники].[ID_Отделы] " +
                    "= [Отделы].[ID] INNER JOIN [dbo].[Должность] ON [Сотрудники].[ID_Должность] = [Должность].[ID]";
                adapter = new SqlDataAdapter(sql, sqlConnection);
                dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
                sqlConnection.Close();
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[12].Visible = false;
            }
            else
            {
                MessageBox.Show("Не выбрали данные для удаления");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.CurrentCell.RowIndex;
            Form2 f = new Form2();
            SqlConnection sqlConnection = new SqlConnection("Data Source=WIN-JO0SOU9O4VS\\SQLEXPRESS;Initial Catalog=Сотрудники_предприятия;Integrated Security=True");
            sqlConnection.Open();
            string sql = @"SELECT
                        *
                    FROM
                        [dbo].[Сотрудники]
                    INNER JOIN
                        [dbo].[Пол] ON[Сотрудники].[ID_Пол] = [Пол].[ID]
                    INNER JOIN
                        [dbo].[Отделы] ON [Сотрудники].[ID_Отделы] = [Отделы].[ID]
                    INNER JOIN
                        [dbo].[Должность] ON[Сотрудники].[ID_Должность] = [Должность].[ID]";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            sqlConnection.Close();
            sqlConnection.Open();
            f.comboBox1.DataSource = dataSet.Tables[0];
            f.comboBox1.DisplayMember = "Название";
            f.comboBox1.ValueMember = "ID";
            f.comboBox2.DataSource = dataSet.Tables[0];
            f.comboBox2.DisplayMember = "Название_отделов";
            f.comboBox2.ValueMember = "ID";
            f.comboBox3.DataSource = dataSet.Tables[0];
            f.comboBox3.DisplayMember = "Название_должности";
            f.comboBox3.ValueMember = "ID";
            f.textBox1.Text = dataGridView1[1, n].Value.ToString();
            f.textBox2.Text = dataGridView1[2, n].Value.ToString();
            f.comboBox1.SelectedValue = Convert.ToInt32(dataGridView1[8, n].Value);
            f.comboBox2.SelectedValue = Convert.ToInt32(dataGridView1[10, n].Value);
            f.comboBox3.SelectedValue = Convert.ToInt32(dataGridView1[12, n].Value);
            f.numericUpDown1.Value = Convert.ToInt32(dataGridView1[3, n].Value);
            f.label5.Text = dataGridView1[4, n].Value.ToString();

            if (dataGridView1[4, n].Value.ToString().Trim() != "")
            {
                f.pictureBox1.Load("Фото/" + dataGridView1[4, n].Value.ToString());
            }
            else
            {
                pictureBox1.Load("Фото/0.jpg");
            }
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (dataGridView1.CurrentCell != null)
                {
                    sql = "UPDATE [dbo].[Сотрудники] SET " +
                 "[Название_предприятия] = '" + f.textBox1.Text + "', " +
                 "[ФИО_сотрудников] = '" + f.textBox2.Text + "', " +
                 "[Зарплата] = '" + f.numericUpDown1.Value + "', " +
                 "[Фото] = '" + f.label5.Text + "', " +
                 "[ID_Пол] = '" + f.comboBox1.SelectedValue + "', " +
                 "[ID_Отделы] = '" + f.comboBox2.SelectedValue + "', " +
                 "[ID_Должность] = '" + f.comboBox3.SelectedValue + "' " +
                 "WHERE ID = " + Convert.ToInt32(dataGridView1[0, n].Value);


                    SqlCommand command = new SqlCommand(sql, sqlConnection);
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                    sqlConnection.Open();
                    sql = "select * from [dbo].[Сотрудники] INNER JOIN [dbo].[Пол] ON[Сотрудники].[ID_Пол] = [Пол].[ID] INNER JOIN [dbo].[Отделы] ON [Сотрудники].[ID_Отделы] " +
                    "= [Отделы].[ID] INNER JOIN [dbo].[Должность] ON [Сотрудники].[ID_Должность] = [Должность].[ID]";
                    adapter = new SqlDataAdapter(sql, sqlConnection);
                    dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    dataGridView1.DataSource = dataSet.Tables[0];
                    sqlConnection.Close();
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dataGridView1.Columns[12].Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Не выбрали данные для редактирования");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                numericUpDown1.Visible = true;
                numericUpDown2.Visible = true;
                comboBox1.Visible = false;
            }
            else
            {
                numericUpDown1.Visible = false;
                numericUpDown2.Visible = false;
                comboBox1.Visible= true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                string sql = "SELECT * FROM [dbo].[Сотрудники] WHERE [ID_Должность] LIKE '%" + comboBox1.SelectedValue + "%'";
                SqlConnection sqlConnection = new SqlConnection("Data Source=WIN-JO0SOU9O4VS\\SQLEXPRESS;Initial Catalog=Сотрудники_предприятия;Integrated Security=True");
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];

                sqlConnection.Close();
                sqlConnection.Open(); 
                sql = "SELECT * FROM [dbo].[Сотрудники] " +
                     "INNER JOIN [dbo].[Должность] ON [dbo].[Сотрудники].[ID_Должность] = [dbo].[Должность].[ID] " +
                     "WHERE [dbo].[Сотрудники].[ID_Должность] =" + comboBox1.SelectedValue;
                adapter = new SqlDataAdapter(sql, sqlConnection);
                dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
                sqlConnection.Close();

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
              
            }
            else
            {
                string sql = "SELECT * FROM [dbo].[Сотрудники] " +
                    "WHERE [Зарплата] >= " + numericUpDown1.Value + " AND [Зарплата] <= " + numericUpDown2.Value;

                SqlConnection sqlConnection = new SqlConnection("Data Source=WIN-JO0SOU9O4VS\\SQLEXPRESS;Initial Catalog=Сотрудники_предприятия;Integrated Security=True");
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
                sqlConnection.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Load_Data();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = dataGridView1.CurrentCell.RowIndex;
            string photoPath = dataGridView1[4, n].Value.ToString().Trim();

            if (!string.IsNullOrWhiteSpace(photoPath))
            {
                try
                {
                    pictureBox1.Load("Фото/" + photoPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузки фотографии:" + ex.Message);
                }
            }
            else
            {
                pictureBox1.Image = Image.FromFile("Фото/0.jpg");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=WIN-JO0SOU9O4VS\\SQLEXPRESS;Initial Catalog=Сотрудники_предприятия;Integrated Security=True");
            sqlConnection.Open();
            string sql = @"SELECT
                        *
                    FROM
                        [dbo].[Сотрудники]
                    INNER JOIN
                        [dbo].[Пол] ON[Сотрудники].[ID_Пол] = [Пол].[ID]
                    INNER JOIN
                        [dbo].[Отделы] ON [Сотрудники].[ID_Отделы] = [Отделы].[ID]
                    INNER JOIN
                        [dbo].[Должность] ON[Сотрудники].[ID_Должность] = [Должность].[ID]" +
                "ORDER BY [ФИО_сотрудников]";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            sqlConnection.Close();

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Название предприятия";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].HeaderText = "ФИО сотрудника";
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].HeaderText = "Зарплата";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Фото";
            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[9].HeaderText = "Пол";
            dataGridView1.Columns[9].Width = 100;
            dataGridView1.Columns[11].HeaderText = "Отдел";
            dataGridView1.Columns[11].Width = 150;
            dataGridView1.Columns[13].HeaderText = "Должность";
            dataGridView1.Columns[13].Width = 150;
        }
    }
}