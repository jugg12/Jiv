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
    public partial class DobavKart : Form
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
        public DobavKart(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void DobavKart_Load(object sender, EventArgs e)
        {
           
            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            this.callback = FIO;
            load();
        }
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[Картина] as [Картина], [ФИО Художника] as [ФИО Художника], [Дата создания] as [Дата создания], [Жанр картины] as [Жанр картины] from Картина";
                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);

            }
        }
        private void FIO(string result, int id)
        {
            textBox2.Text = " " + result;
            oId = id;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Vibor form = new Vibor(this.sqlConnect, "select id,[ФИО] as [ФИО] from Художник", "ФИО", callback);
            form.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "INSERT INTO [Картина] ([Картина],[ФИО Художника],[Дата создания],[Жанр картины]) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Value + "','" + textBox4.Text + "')";
            await cmd.ExecuteNonQueryAsync();
            load();
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = Convert.ToDateTime("01,01,1999");
            textBox4.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && dateTimePicker1.Text != "" && textBox4.Text != "")
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
            if (textBox1.Text != "" && textBox2.Text != "" && dateTimePicker1.Text != "" && textBox4.Text != "")
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && dateTimePicker1.Text != "" && textBox4.Text != "")
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && dateTimePicker1.Text != "" && textBox4.Text != "")
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
