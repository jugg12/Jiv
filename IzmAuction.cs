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

namespace Jivopisi.izmenenie
{
    public partial class IzmAuction : Form
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
        public IzmAuction(SqlConnection sqlConnect, Form f)
        {
            this.sqlConnect = sqlConnect;
            mainForm = f;
            InitializeComponent();
        }

        private void IzmAuction_Load(object sender, EventArgs e)
        {
            
            cmd.Connection = sqlConnect;
            source1.DataSource = dataTable;
            advancedDataGridView1.DataSource = source1;
            this.callback = FIO;

            load();
           
        }
        private void FIO(string result, int id)
        {
            textBox1.Text = textBox1.Text + " " + result;
            oId = id;

        }
        void load()
        {

            if (this.dataTable != null)
            {
                string C = "SELECT id,[Дата проведения] as [Дата проведения], [Список выставленных шедевров] as [Список выставленных шедевров], [Цена] as [Цена] from [Аукционеры и комиссионки]";
                this.dataTable.Clear();
                adapter = new SqlDataAdapter(C, this.sqlConnect);
                adapter.Fill(dataTable);
                advancedDataGridView1.Columns[0].Visible = false;

            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(selectedRow.Cells[0].Value);

            cmd.CommandText = "Update [Аукционеры и комиссионки] set [Дата проведения]='" + dateTimePicker1.Value + "',[Список выставленных шедевров]=  '" + textBox1.Text + "',[Цена]=  '" + textBox2.Text + "' where id = '" + id + "'";
            await cmd.ExecuteNonQueryAsync();

            load();
            dateTimePicker1.Enabled = false;
            textBox2.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = Convert.ToDateTime("01,01,1999");
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex > -1)
            {
                selectedRow = advancedDataGridView1.Rows[e.RowIndex];
                dateTimePicker1.Value = Convert.ToDateTime(selectedRow.Cells[1].Value.ToString());
                textBox1.Text = selectedRow.Cells[2].Value.ToString();
                textBox2.Text = selectedRow.Cells[3].Value.ToString();
                textBox2.Enabled = true;
                dateTimePicker1.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Vibor form = new Vibor(this.sqlConnect, "select id,[Картина] as [Картина] from Картина", "Картина", callback);
            form.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && dateTimePicker1.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && dateTimePicker1.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && dateTimePicker1.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}
