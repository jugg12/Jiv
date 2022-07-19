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

namespace Jivopisi.dobavlenie
{
    public partial class DobavMus : Form
    {
        BindingSource source1 = new BindingSource();
        SqlConnection sqlConnect;
        DataTable dataTable = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        DataGridViewRow selectedRow = null;
        Form mainForm = null;
        Action<string, int> callback;
        int oId = 0;
        int k = 0;
        public DobavMus(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void DobavMus_Load(object sender, EventArgs e)
        {
          
            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
          
            load();
           
        }
       
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[Наличие оригиналов картин и копий] as [Наличие оригиналов картин и копий], [ФИО Коллекционера] as [ФИО Коллекционера], [Название музея] as [Название музея] from [Коллекционеры и музеи]";
                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);

            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "INSERT INTO [Коллекционеры и музеи] ([Наличие оригиналов картин и копий],[ФИО Коллекционера],[Название музея]) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
            await cmd.ExecuteNonQueryAsync();
            load();
            textBox1.Text = "";
            textBox2.Text = "-";
            textBox3.Text = "-";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && (textBox2.Text != "" && textBox3.Text != "") && (checkBox1.Checked == true || checkBox2.Checked == true))
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && (textBox2.Text != "" && textBox3.Text != "") && (checkBox1.Checked == true || checkBox2.Checked == true))
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && (textBox2.Text != "" && textBox3.Text != "") && (checkBox1.Checked == true || checkBox2.Checked == true))
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && k==0)
            {
                textBox2.Text = "";
                textBox2.Enabled = true;
                textBox3.Text = "-";
                k++;
            }
            else if (checkBox1.Checked == true && checkBox2.Checked == true && k==1)
            {
                MessageBox.Show("Выбрано может быть только одно значение!");
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                k = 0;
            }
            else
            {
                textBox2.Enabled = false;
                textBox2.Text = "-";
                k = 0;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true && k == 0)
            {
                textBox3.Text = "";
                textBox3.Enabled = true;
                textBox2.Text = "-";
                k++;
            }
            else if (checkBox1.Checked == true && checkBox2.Checked == true && k == 1)
            {
                MessageBox.Show("Выбрано может быть только одно значение!");
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                k = 0;
            }
            else
            {
                textBox3.Enabled = false;
                textBox3.Text = "-";
                k = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
